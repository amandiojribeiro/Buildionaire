namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    public class ImportChangesetServices : IImportChangesetServices
    {
        private const string Startdate = "01/01/2014";

        private readonly IChangesetRepository changesetRepository;
        private readonly IUserRepository userRepository;
        private readonly IChangesetGateway changesetGateway;
        private readonly IProjectRepository projectRepository;

        public ImportChangesetServices(IChangesetRepository changesetRepository, IUserRepository userRepository, IChangesetGateway changesetGateway, IProjectRepository projectRepository)
        {
            this.changesetRepository = changesetRepository;
            this.userRepository = userRepository;
            this.changesetGateway = changesetGateway;
            this.projectRepository = projectRepository;
        }

        public void ImportChangesets()
        {
            var lastCodeReview = this.changesetRepository.GetLast();

            var beginingDate = lastCodeReview != null ? lastCodeReview.CreatedAt : DateTime.Parse(Startdate);

            var projects = this.projectRepository.GetAll();

            foreach (var project in projects)
            {
                this.ImportChangesets(beginingDate, project);
            }
        }

        public void ImportChangesets(DateTime beginingDate, Project project)
        {
            var changesets = this.changesetGateway.GetChangesets(beginingDate, string.Format("$/{0}",project.Name));

            foreach (ChangeSet changeSet in changesets)
            {
                var externalChangesetId = changeSet.ExternalChangesetId;
                if (this.changesetRepository.Exists(e => e.ExternalChangesetId == externalChangesetId))
                {
                    continue;
                }

                var changeSetUserName = changeSet.User.Name;
                var user = this.userRepository.Find(x => x.Name.Equals(changeSetUserName));

                if (user != null)
                {
                    changeSet.User = user;
                }

                changeSet.Project = project;

                this.changesetRepository.Add(changeSet);
            }
        }
    }
}