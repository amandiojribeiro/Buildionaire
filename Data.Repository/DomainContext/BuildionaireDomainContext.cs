using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farfetch.Buildionaire.Domain.Model;

namespace Farfetch.Buildionaire.Data.Repository.DomainContext
{
    public class BuildionaireDomainContext : DbContext
    {
        public BuildionaireDomainContext()
            : base("Name=BuildionaireDomainContextConnectionString")
        {

        }

        public virtual DbSet<CodeReview> CodeReviews { get; set; }

        public virtual DbSet<CodeCoverage> CodeCoverages { get; set; }

        public virtual DbSet<Error> Errors { get; set; }

        public virtual DbSet<Build> Builds { get; set; }

        public virtual DbSet<BuildScoreDetail> BuildScoreDetails { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserBadge> UserBadges { get; set; }

        public virtual DbSet<BadgeType> BadgeTypes { get; set; }

        public virtual DbSet<Badge> Badges { get; set; }

        public virtual DbSet<Warning> Warnings { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Metric> Metrics { get; set; }

        public virtual DbSet<ChangeSet> ChangeSets { get; set; }

        public virtual DbSet<Dashboard> Dashboards { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Build>()
                .HasOptional(b => b.CodeCoverage)
                .WithRequired(c => c.Build);

            modelBuilder.Entity<Build>()
                .HasOptional(b => b.Error)
                .WithRequired(e => e.Build);
        }
    }
}
