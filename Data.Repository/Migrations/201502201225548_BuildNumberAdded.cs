namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class BuildNumberAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Builds", "BuildNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Builds", "BuildNumber");
        }
    }
}
