namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specdetailelement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QCSpecDetails", "Element", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QCSpecDetails", "Element");
        }
    }
}
