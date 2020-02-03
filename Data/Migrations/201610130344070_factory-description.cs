namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class factorydescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factories", "ShortDescription", c => c.String());
            AddColumn("dbo.Factories", "FullDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Factories", "FullDescription");
            DropColumn("dbo.Factories", "ShortDescription");
        }
    }
}
