namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class ExternalChangesetIdAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeSets", "ExternalChangesetId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeSets", "ExternalChangesetId");
        }
    }
}
