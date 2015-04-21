namespace Farfetch.Buildionaire.Presentation.Web
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Web.Http;
    using System.Web.Mvc;

    using Farfetch.Buildionaire.Application.Services.Dashboard;
    using Farfetch.Buildionaire.Application.Services.DataImporter;
    using Farfetch.Buildionaire.Application.Services.Management;
    using Farfetch.Buildionaire.Data.Gateway;
    using Farfetch.Buildionaire.Data.Repository;
    using Farfetch.Buildionaire.Data.Repository.DomainContext;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Services;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters.Automapper;

    using Microsoft.Practices.Unity;

    using Unity.Mvc5;

    public static class UnityConfig
    {
        public static IUnityContainer Container;

        public static void RegisterComponents()
        {
            IUnityContainer container = new UnityContainer();

            RegisterRepositories(container);

            RegisterGateways(container);

            RegisterServices(container);

            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());

            Container = container;

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IScoreService, ScoreService>();
            container.RegisterType<IBadgeService, BadgeService>();

            container.RegisterType<IImportBuildServices, ImportBuildServices>(
                new InjectionConstructor(
                    new ResolvedParameter<IBuildGateway>(),
                    new ResolvedParameter<IBuildRepository>(),
                    new ResolvedParameter<IScoreService>(),
                    new ResolvedParameter<IMetricRepository>(),
                    new ResolvedParameter<IProjectRepository>(),
                    new DateTime(2014, 1, 1)));

            container.RegisterType<IBadgesService, BadgesServices>();

            container.RegisterType<IImportChangesetServices, ImportChangesetServices>();
            container.RegisterType<ICodeReviewServices, CodeReviewServices>();
            container.RegisterType<IBuildServices, BuildServices>();

            container.RegisterType<IBadgeDashboardServices, BadgeDashboardServices>();
            container.RegisterType<IBuildDashboardServices, BuildDashboardServices>();
            container.RegisterType<IChangesetDashboardServices, ChangesetDashboardServices>();
            container.RegisterType<ICodeReviewDashboardServices, CodeReviewDashboardServices>();
            container.RegisterType<IScoreDashboardServices, ScoreDashboardServices>();
            container.RegisterType<IUserDashboardServices, UserDashboardServices>();
            container.RegisterType<IScoreDetailsService, ScoreDetailsService>();
        }

        private static void RegisterGateways(IUnityContainer container)
        {
            var sourceControlEndpoint = new Uri(ConfigurationManager.AppSettings["SourceControlEndpoint"]);
            var tfsUsername = ConfigurationManager.AppSettings["SourceControlUserName"];
            var tfsPassword = ConfigurationManager.AppSettings["SourceControlUserPassword"];
            var tfsDomain = ConfigurationManager.AppSettings["SourceControlDomain"];

            var tfsConnection = new TfsConnection(sourceControlEndpoint, tfsUsername, tfsPassword, tfsDomain);

            container.RegisterType<IBuildGateway, TfsBuildGateway>(new InjectionConstructor(tfsConnection));
            container.RegisterType<IChangesetGateway, TfsChangesetGateway>(new InjectionConstructor(tfsConnection));
            container.RegisterType<ICodeReviewGateway, TfsCodeReviewGateway>(new InjectionConstructor(tfsConnection));
            container.RegisterType<IProjectGateway, TfsProjectGateway>(new InjectionConstructor(tfsConnection));
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<DbContext, BuildionaireDomainContext>(
                "BuildionaireDomainContext",
                new PerResolveLifetimeManager(),
                new InjectionConstructor());

            container.RegisterType<IProjectRepository, ProjectRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IMetricRepository, MetricRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IBuildRepository, BuildRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IBadgeRepository, BadgeRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IBadgeTypeRepository, BadgeTypeRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IUserRepository, UserRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IUserRepository, UserRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IUserBadgeRepository, UserBadgeRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IChangesetRepository, ChangesetRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IDashboardRepository, DashboardRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<ICodeReviewRepository, CodeReviewRepository>(
                new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));

            container.RegisterType<IBuildScoreDetailRepository, BuildScoreDetailRepository>(
             new InjectionConstructor(new ResolvedParameter<DbContext>("BuildionaireDomainContext")));
        }
    }
}