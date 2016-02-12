namespace EntityMigrationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("CarsSecondTable", "YearOfCreation", c => c.Int());
            /*CreateTable(
                "dbo.CarsSecondTable",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarName = c.String(),
                        YearOfCreation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);*/
            
        }
        
        public override void Down()
        {
            DropColumn("CarsSecondTable", "YearOfCreation");
            //DropTable("dbo.CarsSecondTable");
        }
    }
}
