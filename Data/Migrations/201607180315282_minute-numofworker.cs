namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minutenumofworker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "NumOfWorker", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetailTemplates", "Minute", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetailTemplates", "Minute");
            DropColumn("dbo.Teams", "NumOfWorker");
        }
    }
}
