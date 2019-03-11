namespace TestNexMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContextMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeviceDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameService = c.String(),
                        NameSity = c.String(),
                        Temperature = c.Double(nullable: false),
                        DateWeather = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SeviceDatas");
        }
    }
}
