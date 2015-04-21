namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Model;

    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<DomainContext.BuildionaireDomainContext>
    {
        internal Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DomainContext.BuildionaireDomainContext context)
        {
            context.Metrics.AddOrUpdate(p => p.Code, new Metric { Code = "CODECOVERAGE", Description = "Code Coverage", Weight = 30 });
            context.Metrics.AddOrUpdate(p => p.Code, new Metric { Code = "ERROR", Description = "Errors in build.", Weight = -100 });
            context.Metrics.AddOrUpdate(p => p.Code, new Metric { Code = "CODEREVIEW", Description = "Code Reviews.", Weight = 2 });
            context.Metrics.AddOrUpdate(p => p.Code, new Metric { Code = "WARNING", Description = "Warnings in build.", Weight = -3 });
            context.Metrics.AddOrUpdate(p => p.Code, new Metric { Code = "BUILDSTATUS", Description = "Status of the build.", Weight = 1500 });

            var badgeType = new BadgeType
            {
                Id = 1,
                Code = "CHECKIN",
                Name = "CheckinsCount",
            };

            context.BadgeTypes.AddOrUpdate(p => p.Code, badgeType);
            context.Badges.AddOrUpdate(
                p => p.Code,
                new Badge { Id = 1, BadgeType = badgeType, Code = "CHECKIN1", Name = "Checkin timber", Min = 1 });
            context.Badges.AddOrUpdate(
                p => p.Code,
                new Badge { Id = 2, BadgeType = badgeType, Code = "CHECKIN2", Name = "Checkin bronze", Min = 1000 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CHECKIN3", Name = "Checkin silver", Min = 5000 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CHECKIN4", Name = "Checkin gold", Min = 10000 });

            badgeType = new BadgeType
            {
                Id = 1,
                Code = "CODEREVIEWREQUEST",
                Name = "Code review request",
            };

            context.BadgeTypes.AddOrUpdate(p => p.Code, badgeType);
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 1, BadgeType = badgeType, Code = "CODEREVIEWREQUEST1", Name = "Code review requester lammer", Min = 1 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 2, BadgeType = badgeType, Code = "CODEREVIEWREQUEST2", Name = "Code review requester bronze", Min = 100 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CODEREVIEWREQUEST3", Name = "Code review requester silver", Min = 1000 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CODEREVIEWREQUEST4", Name = "Code review requester gold", Min = 5000 });

            badgeType = new BadgeType
            {
                Id = 1,
                Code = "CODEREVIEWRESPONSE",
                Name = "Code reviewer",
            };

            context.BadgeTypes.AddOrUpdate(p => p.Code, badgeType);
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 1, BadgeType = badgeType, Code = "CODEREVIEWRESPONSE1", Name = "Code reviewer lammer", Min = 1 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 2, BadgeType = badgeType, Code = "CODEREVIEWRESPONSE2", Name = "Code reviewer bronze", Min = 100 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CODEREVIEWRESPONSE3", Name = "Code reviewer silver", Min = 1000 });
            context.Badges.AddOrUpdate(p => p.Code, new Badge { Id = 3, BadgeType = badgeType, Code = "CODEREVIEWRESPONSE4", Name = "Code reviewer gold", Min = 5000 });

            var dashboard = new Dashboard
                                      {
                                          Code = "warp",
                                          Description = "Payment Team Dashboard"
                                      };

            context.Dashboards.AddOrUpdate(p => p.Code, dashboard);
            context.Projects.First(x => x.Name.Equals("Portal")).Dashboards.Add(dashboard);
            context.Projects.First(x => x.Name.Equals("PaymentGatewayService")).Dashboards.Add(dashboard);
            context.Projects.First(x => x.Name.Equals("RiskManagementService")).Dashboards.Add(dashboard);
            context.SaveChanges();
        }
    }
}