namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class AddDashboards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DashboardProjects",
                c => new
                    {
                        Dashboard_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dashboard_Id, t.Project_Id })
                .ForeignKey("dbo.Dashboards", t => t.Dashboard_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.Dashboard_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DashboardProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.DashboardProjects", "Dashboard_Id", "dbo.Dashboards");
            DropIndex("dbo.DashboardProjects", new[] { "Project_Id" });
            DropIndex("dbo.DashboardProjects", new[] { "Dashboard_Id" });
            DropTable("dbo.DashboardProjects");
            DropTable("dbo.Dashboards");
        }
    }
}
