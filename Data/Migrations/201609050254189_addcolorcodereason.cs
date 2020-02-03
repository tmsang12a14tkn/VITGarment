namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolorcodereason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reasons", "ColorCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reasons", "ColorCode");
        }
    }
}
