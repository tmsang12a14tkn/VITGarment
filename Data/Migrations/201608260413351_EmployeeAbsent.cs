namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAbsent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "TotalEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.Goals", "AbsentWithReason", c => c.Int(nullable: false));
            AddColumn("dbo.Goals", "AbsentComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "AbsentComment");
            DropColumn("dbo.Goals", "AbsentWithReason");
            DropColumn("dbo.Goals", "TotalEmployee");
        }
    }
}
