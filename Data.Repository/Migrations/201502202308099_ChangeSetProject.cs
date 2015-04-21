namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class ChangeSetProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeSets", "Project_Id", c => c.Int());
            CreateIndex("dbo.ChangeSets", "Project_Id");
            AddForeignKey("dbo.ChangeSets", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeSets", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ChangeSets", new[] { "Project_Id" });
            DropColumn("dbo.ChangeSets", "Project_Id");
        }
    }
}
