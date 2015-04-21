namespace Farfetch.Buildionaire.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class CodeReviewRefactoring : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.CodeReviewInteractions", "CodeReview_Id", "dbo.CodeReviews");
            this.DropForeignKey("dbo.CodeReviewInteractions", "User_Id", "dbo.Users");
            this.DropIndex("dbo.CodeReviewInteractions", new[] { "CodeReview_Id" });
            this.DropIndex("dbo.CodeReviewInteractions", new[] { "User_Id" });
            this.AddColumn("dbo.CodeReviews", "ExternalId", c => c.Int(nullable: false));
            this.AddColumn("dbo.CodeReviews", "Type", c => c.Int(nullable: false));
            this.AddColumn("dbo.ChangeSets", "CreatedAt", c => c.DateTime(nullable: false));
            this.DropTable("dbo.CodeReviewInteractions");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            this.DropColumn("dbo.ChangeSets", "CreatedAt");
            this.DropColumn("dbo.CodeReviews", "Type");
            this.DropColumn("dbo.CodeReviews", "ExternalId");
            this.CreateIndex("dbo.CodeReviewInteractions", "User_Id");
            this.CreateIndex("dbo.CodeReviewInteractions", "CodeReview_Id");
            this.AddForeignKey("dbo.CodeReviewInteractions", "User_Id", "dbo.Users", "Id");
            this.AddForeignKey("dbo.CodeReviewInteractions", "CodeReview_Id", "dbo.CodeReviews", "Id");
        }
    }
}
