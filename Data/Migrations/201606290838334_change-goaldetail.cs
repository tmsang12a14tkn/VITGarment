namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changegoaldetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Goals", "ProductId", "dbo.Products");
            DropIndex("dbo.Goals", new[] { "ProductId" });
            CreateTable(
                "dbo.Reasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GoalDetails", "NoEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "ReasonId", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "Comment", c => c.String());
            AddColumn("dbo.GoalDetails", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.GoalDetails", "ReasonId");
            CreateIndex("dbo.GoalDetails", "ProductId");
            AddForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons", "Id", cascadeDelete: true);
            DropColumn("dbo.Goals", "StartHour");
            DropColumn("dbo.Goals", "EndHour");
            DropColumn("dbo.Goals", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Goals", "EndHour", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Goals", "StartHour", c => c.Time(nullable: false, precision: 7));
            DropForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons");
            DropForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.GoalDetails", new[] { "ProductId" });
            DropIndex("dbo.GoalDetails", new[] { "ReasonId" });
            DropColumn("dbo.GoalDetails", "ProductId");
            DropColumn("dbo.GoalDetails", "Comment");
            DropColumn("dbo.GoalDetails", "ReasonId");
            DropColumn("dbo.GoalDetails", "NoEmployee");
            DropTable("dbo.Reasons");
            CreateIndex("dbo.Goals", "ProductId");
            AddForeignKey("dbo.Goals", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
