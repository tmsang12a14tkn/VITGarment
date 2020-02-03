namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoalDetailTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartCountTime = c.Time(nullable: false, precision: 7),
                        EndCountTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.GoalDetails", "StartCountTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.GoalDetails", "EndCountTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Goals", "StartHour", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Goals", "EndHour", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Goals", "Quantity");
            DropColumn("dbo.Goals", "TotalHour");
            DropColumn("dbo.Goals", "QuantityPerHour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "QuantityPerHour", c => c.Int(nullable: false));
            AddColumn("dbo.Goals", "TotalHour", c => c.Int(nullable: false));
            AddColumn("dbo.Goals", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Goals", "EndHour", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Goals", "StartHour", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GoalDetails", "EndCountTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GoalDetails", "StartCountTime", c => c.DateTime(nullable: false));
            DropTable("dbo.GoalDetailTemplates");
        }
    }
}
