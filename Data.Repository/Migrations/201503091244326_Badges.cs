namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class Badges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Badges", "MinThreshold", c => c.Int(nullable: false));
            DropColumn("dbo.Badges", "Min");
            DropColumn("dbo.Badges", "Max");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Badges", "Max", c => c.Int(nullable: false));
            AddColumn("dbo.Badges", "Min", c => c.Int(nullable: false));
            DropColumn("dbo.Badges", "MinThreshold");
        }
    }
}
