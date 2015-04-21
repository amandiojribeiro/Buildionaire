namespace Farfetch.Buildionaire.Data.Gateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Build.Common;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;

    using BuildStatus = Microsoft.TeamFoundation.Build.Client.BuildStatus;
    using Project = Farfetch.Buildionaire.Domain.Model.Project;
    using WarningType = Farfetch.Buildionaire.Domain.Model.WarningType;

    public class TfsBuildGateway : IBuildGateway
    {
        private const string MainBuildDefinitionFilter = "Main";

        private readonly TfsTeamProjectCollection tfsTeamProjectCollection;

        public TfsBuildGateway(TfsConnection tfsConnection)
        {
            this.tfsTeamProjectCollection = tfsConnection.Connect();
        }

        private IBuildServer BuildServer
        {
            get
            {
                return this.tfsTeamProjectCollection.GetService<IBuildServer>();
            }
        }

        private VersionControlServer VersionControlServer
        {
            get
            {
                return this.tfsTeamProjectCollection.GetService<VersionControlServer>();
            }
        }

        public IEnumerable<Build> GetBuilds(DateTime sinceDate)
        {
            var projects = this.GetProjects();

            return projects.SelectMany(x => this.GetBuilds(sinceDate, x.Name));
        }

        public IEnumerable<Build> GetBuilds(DateTime sinceDate, string project)
        {
            var builds = new List<Build>();

            string buildMainGroup = this.GetBuildMainGroup(project);

            if (string.IsNullOrEmpty(buildMainGroup))
            {
                return builds;
            }

            var completedBuildsSpec = this.GetBuildDetailSpec(this.BuildServer, project, buildMainGroup, sinceDate);

            var tfsBuilds = this.BuildServer.QueryBuilds(completedBuildsSpec).Builds;

            var testManagementTeamProject = this.GetTestManagementProject(project);

            foreach (var tfsBuild in tfsBuilds)
            {
                var build = this.ConvertFromTfsBuild(tfsBuild);
                build.Project = new Project { Name = build.Project.Name };

                this.AddCodeCoverage(testManagementTeamProject, tfsBuild, build);
                this.AddBuildsErrors(tfsBuild, build);
                this.AddBuildWarnings(tfsBuild, build);

                builds.Add(build);
            }

            return builds;
        }

        private IEnumerable<Project> GetProjects()
        {
            var teamProjects = this.VersionControlServer.GetAllTeamProjects(true);

            return teamProjects != null ? teamProjects.Select(x => new Project { Name = x.Name }) : new List<Project>();
        }

        private void AddBuildWarnings(IBuildDetail tfsBuild, Build build)
        {
            List<IBuildWarning> associatedBuildWarnings = InformationNodeConverters.GetBuildWarnings(tfsBuild);

            var groupedWarnings = associatedBuildWarnings.GroupBy(this.ParseWarningType);

            build.Warnings =
                groupedWarnings.Select(
                    x => new Domain.Model.Warning { Type = x.Key, Total = x.Count() }).ToList();
        }

        private WarningType ParseWarningType(IBuildWarning buildWarning)
        {
            switch (buildWarning.WarningType)
            {
                case "Compilation":
                    return WarningType.Compilation;
                case "StaticAnalysis":
                    return WarningType.StaticAnalysis;
                case "":
                    return this.ParseWarningTypeByCode(buildWarning.Code);
                default:
                    throw new ArgumentException(string.Format("Unknown WarningType {0}", buildWarning.WarningType));
            }
        }

        private string GetBuildMainGroup(string project)
        {
            var buildDefinitions = this.BuildServer.QueryBuildDefinitions(project);

            var selectedBuildDefinition =
                buildDefinitions.FirstOrDefault(x => x.Name.Contains(MainBuildDefinitionFilter));

            return selectedBuildDefinition == null ? null : selectedBuildDefinition.Name;
        }

        private WarningType ParseWarningTypeByCode(string warningCode)
        {
            if (warningCode.StartsWith("CA"))
            {
                return WarningType.CodeAnalisys;
            }

            if (warningCode.StartsWith("SA"))
            {
                return WarningType.StyleCop;
            }

            if (warningCode.Equals(string.Empty))
            {
                return WarningType.Compilation;
            }

            throw new ArgumentException(string.Format("Unknown WarningCode {0}", warningCode));
        }

        private Build ConvertFromTfsBuild(IBuildDetail tfsBuild)
        {
            return new Build
                       {
                           Name = string.Format("{0} {1}", tfsBuild.TeamProject, tfsBuild.BuildNumber),
                           BuildNumber = tfsBuild.BuildNumber,
                           Project = new Project { Name = tfsBuild.TeamProject },
                           Status = this.ParseBuildStatus(tfsBuild.Status),
                           CreatedAt = tfsBuild.StartTime,
                           CompletedAt = tfsBuild.FinishTime
                       };
        }

        private Domain.Model.BuildStatus ParseBuildStatus(BuildStatus buildStatus)
        {
            switch (buildStatus)
            {
                case BuildStatus.Failed:
                    return Domain.Model.BuildStatus.Failed;
                case BuildStatus.PartiallySucceeded:
                    return Domain.Model.BuildStatus.PartiallySucceeded;
                case BuildStatus.Succeeded:
                    return Domain.Model.BuildStatus.Succeeded;
            }

            throw new ArgumentException("BuildStatus Not Mapped");
        }

        private void AddBuildsErrors(IBuildDetail tfsBuild, Build build)
        {
            var associatedBuildErrors = InformationNodeConverters.GetBuildErrors(tfsBuild);

            build.Error = new Error { Total = associatedBuildErrors.Count() };
        }

        private void AddCodeCoverage(
            ITestManagementTeamProject testManagementTeamProject,
            IBuildDetail tfsBuild,
            Build build)
        {
            var blocks = 0;
            var blocksCovered = 0;

            var coverageAnalysisManager = testManagementTeamProject.CoverageAnalysisManager;

            var testAnalysisManager = testManagementTeamProject.TestRuns;

            var buildCoverage = coverageAnalysisManager.QueryBuildCoverage(
                tfsBuild.Uri.ToString(),
                    CoverageQueryFlags.Modules | CoverageQueryFlags.Functions | CoverageQueryFlags.BlockData);

            if (buildCoverage != null)
            {
                foreach (IBuildCoverage coverage in buildCoverage)
                {
                    foreach (IModuleCoverage moduleCoverage in coverage.Modules)
                    {
                        blocksCovered += moduleCoverage.Statistics.BlocksCovered;
                        blocks += moduleCoverage.Statistics.BlocksCovered + moduleCoverage.Statistics.BlocksNotCovered;
                    }
                }
            }

            if (blocks > 0)
            {
                build.CodeCoverage = new CodeCoverage { CoveredBlocks = blocksCovered, TotalBlocks = blocks };
                var testStats = testAnalysisManager.ByBuild(tfsBuild.Uri);

                foreach (var testStat in testStats)
                {
                    build.CodeCoverage.TotalTests += testStat.TotalTests;
                    build.CodeCoverage.PassedTests += testStat.PassedTests;
                    build.CodeCoverage.Incomplete += testStat.IncompleteTests;
                }
            }
            else
            {
                build.CodeCoverage = new CodeCoverage();
            }
        }

        private ITestManagementTeamProject GetTestManagementProject(string projectName)
        {
            var testManagementService =
                (ITestManagementService)this.tfsTeamProjectCollection.GetService(typeof(ITestManagementService));
            return testManagementService.GetTeamProject(projectName);
        }

        private IBuildDetailSpec GetBuildDetailSpec(
            IBuildServer buildServer,
            string project,
            string buildGroup,
            DateTime since)
        {
            IBuildDetailSpec spec = buildServer.CreateBuildDetailSpec(project, buildGroup);

            spec.MinFinishTime = since;

            spec.QueryOrder = BuildQueryOrder.StartTimeAscending;

            spec.InformationTypes = null;

            spec.InformationTypes = new[]
                                        {
                                            InformationTypes.ConfigurationSummary, InformationTypes.AssociatedCommit,
                                            InformationTypes.AssociatedChangeset, InformationTypes.CompilationSummary,
                                            InformationTypes.BuildError, InformationTypes.BuildWarning
                                        };

            spec.Status = BuildStatus.Failed
                          | BuildStatus.PartiallySucceeded
                          | BuildStatus.Succeeded;

            spec.QueryDeletedOption = QueryDeletedOption.ExcludeDeleted;
            return spec;
        }
    }
}
