namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metrics", "Code", c => c.String());
            AddColumn("dbo.CodeCoverages", "Incomplete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodeCoverages", "Incomplete");
            DropColumn("dbo.Metrics", "Code");
        }
    }
}
