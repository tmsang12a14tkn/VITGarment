namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class factory2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "FactoryId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Teams", "FactoryId");
            AddForeignKey("dbo.Teams", "FactoryId", "dbo.Factories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "FactoryId", "dbo.Factories");
            DropIndex("dbo.Teams", new[] { "FactoryId" });
            DropColumn("dbo.Teams", "FactoryId");
        }
    }
}
