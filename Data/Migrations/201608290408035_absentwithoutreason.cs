namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class absentwithoutreason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "AbsentWithoutReason", c => c.Int(nullable: false));
            DropColumn("dbo.Goals", "AbsentWithReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "AbsentWithReason", c => c.Int(nullable: false));
            DropColumn("dbo.Goals", "AbsentWithoutReason");
        }
    }
}
