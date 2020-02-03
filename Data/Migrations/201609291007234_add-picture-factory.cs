namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpicturefactory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factories", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Factories", "Picture");
        }
    }
}
