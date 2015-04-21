namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Builds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        TotalScore = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CompletedAt = c.DateTime(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.BuildScoreDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Build_Id = c.Int(),
                        Metric_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Builds", t => t.Build_Id)
                .ForeignKey("dbo.Metrics", t => t.Metric_Id)
                .Index(t => t.Build_Id)
                .Index(t => t.Metric_Id);
            
            CreateTable(
                "dbo.Metrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CodeCoverages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TotalBlocks = c.Int(nullable: false),
                        CoveredBlocks = c.Int(nullable: false),
                        TotalTests = c.Int(nullable: false),
                        PassedTests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Builds", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CodeReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        RequestedBy_Id = c.Int(),
                        Build_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.RequestedBy_Id)
                .ForeignKey("dbo.Builds", t => t.Build_Id)
                .Index(t => t.RequestedBy_Id)
                .Index(t => t.Build_Id);
            
            CreateTable(
                "dbo.CodeReviewInteractions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        InteractionType = c.Int(nullable: false),
                        CodeReview_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CodeReviews", t => t.CodeReview_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CodeReview_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Builds", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Warnings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Build_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Builds", t => t.Build_Id)
                .Index(t => t.Build_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warnings", "Build_Id", "dbo.Builds");
            DropForeignKey("dbo.Builds", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Errors", "Id", "dbo.Builds");
            DropForeignKey("dbo.CodeReviews", "Build_Id", "dbo.Builds");
            DropForeignKey("dbo.CodeReviewInteractions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.CodeReviews", "RequestedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CodeReviewInteractions", "CodeReview_Id", "dbo.CodeReviews");
            DropForeignKey("dbo.CodeCoverages", "Id", "dbo.Builds");
            DropForeignKey("dbo.BuildScoreDetails", "Metric_Id", "dbo.Metrics");
            DropForeignKey("dbo.BuildScoreDetails", "Build_Id", "dbo.Builds");
            DropIndex("dbo.Warnings", new[] { "Build_Id" });
            DropIndex("dbo.Errors", new[] { "Id" });
            DropIndex("dbo.CodeReviewInteractions", new[] { "User_Id" });
            DropIndex("dbo.CodeReviewInteractions", new[] { "CodeReview_Id" });
            DropIndex("dbo.CodeReviews", new[] { "Build_Id" });
            DropIndex("dbo.CodeReviews", new[] { "RequestedBy_Id" });
            DropIndex("dbo.CodeCoverages", new[] { "Id" });
            DropIndex("dbo.BuildScoreDetails", new[] { "Metric_Id" });
            DropIndex("dbo.BuildScoreDetails", new[] { "Build_Id" });
            DropIndex("dbo.Builds", new[] { "Project_Id" });
            DropTable("dbo.Warnings");
            DropTable("dbo.Projects");
            DropTable("dbo.Errors");
            DropTable("dbo.Users");
            DropTable("dbo.CodeReviewInteractions");
            DropTable("dbo.CodeReviews");
            DropTable("dbo.CodeCoverages");
            DropTable("dbo.Metrics");
            DropTable("dbo.BuildScoreDetails");
            DropTable("dbo.Builds");
        }
    }
}
