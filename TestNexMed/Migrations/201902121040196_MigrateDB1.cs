namespace TestNexMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Sity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Sity");
        }
    }
}
