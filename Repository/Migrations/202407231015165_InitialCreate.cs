namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.URLDTOes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        url = c.String(),
                        generatedUrl = c.String(),
                        createdby = c.String(),
                        createdDate = c.DateTime(nullable: false),
                        modifieddby = c.String(),
                        modifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.URLDTOes");
        }
    }
}
