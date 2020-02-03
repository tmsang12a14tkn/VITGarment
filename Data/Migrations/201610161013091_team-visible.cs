namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamvisible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "VisibleStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "VisibleStatus");
        }
    }
}
