namespace DbMockingExtensions.ExampleDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRevisions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Revisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangeDescription = c.String(),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Revisions", "Book_Id", "dbo.Books");
            DropIndex("dbo.Revisions", new[] { "Book_Id" });
            DropTable("dbo.Revisions");
        }
    }
}
