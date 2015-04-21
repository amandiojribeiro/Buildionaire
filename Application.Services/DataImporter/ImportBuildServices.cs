namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services;

    public class ImportBuildServices : IImportBuildServices
    {
        private readonly IBuildGateway buildGateway;
        private readonly IBuildRepository buildRepository;
        private readonly IScoreService scoreService;
        private readonly IMetricRepository metricRepository;
        private readonly IProjectRepository projectRepository;
        private readonly DateTime loadHistorySince;

        public ImportBuildServices(
            IBuildGateway buildGateway,
            IBuildRepository buildRepository,
            IScoreService scoreService,
            IMetricRepository metricRepository,
            IProjectRepository projectRepository,
            DateTime loadHistorySince)
        {
            this.buildGateway = buildGateway;
            this.buildRepository = buildRepository;
            this.scoreService = scoreService;
            this.metricRepository = metricRepository;
            this.projectRepository = projectRepository;
            this.loadHistorySince = loadHistorySince;
        }

        public void ImportBuilds()
        {
            var lastBuild = this.buildRepository.GetLastBuild();

            var beginingDate = lastBuild != null ? lastBuild.CreatedAt : this.loadHistorySince;

            var importedBuilds = this.buildGateway.GetBuilds(beginingDate);
            var metrics = this.metricRepository.GetAll().ToList();

            foreach (var build in importedBuilds)
            {
                var projectName = build.Project.Name;
                var project = this.projectRepository.Find(x => x.Name.Equals(projectName));

                if (project == null)
                {
                    project = new Project { Name = projectName };
                    this.projectRepository.Add(project);
                }

                build.Project = project;
                build.BuildScoreDetails = this.CalulateScoreDetails(build, metrics);

                build.TotalScore = build.BuildScoreDetails.Sum(x => x.Score);
                this.buildRepository.Add(build);
            }
        }

        public void ImportBuilds(DateTime since)
        {
            var builds = this.buildGateway.GetBuilds(since);
            var metrics = this.metricRepository.GetAll().ToList();
            foreach (Build build in builds)
            {
                build.BuildScoreDetails = this.CalulateScoreDetails(build, metrics);
                this.buildRepository.Add(build);
            }
        }

        private List<BuildScoreDetail> CalulateScoreDetails(Build build, List<Metric> metrics)
        {
            var buildScoreDetails = this.scoreService.CalculateScoreDetail(build, metrics).ToList();
            return buildScoreDetails;
        }
    }
}
