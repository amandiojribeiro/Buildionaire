namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class BadgesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        BadgeType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BadgeTypes", t => t.BadgeType_Id)
                .Index(t => t.BadgeType_Id);
            
            CreateTable(
                "dbo.BadgeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceivedDate = c.DateTime(nullable: false),
                        Badge_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badges", t => t.Badge_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Badge_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ChangeSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeSets", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "Badge_Id", "dbo.Badges");
            DropForeignKey("dbo.Badges", "BadgeType_Id", "dbo.BadgeTypes");
            DropIndex("dbo.ChangeSets", new[] { "User_Id" });
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropIndex("dbo.UserBadges", new[] { "Badge_Id" });
            DropIndex("dbo.Badges", new[] { "BadgeType_Id" });
            DropTable("dbo.ChangeSets");
            DropTable("dbo.UserBadges");
            DropTable("dbo.BadgeTypes");
            DropTable("dbo.Badges");
        }
    }
}
