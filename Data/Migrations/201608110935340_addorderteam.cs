namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderteam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "order");
        }
    }
}
