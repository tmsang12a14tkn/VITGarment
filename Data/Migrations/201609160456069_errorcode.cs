namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reasons", "ErrorCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reasons", "ErrorCode");
        }
    }
}
