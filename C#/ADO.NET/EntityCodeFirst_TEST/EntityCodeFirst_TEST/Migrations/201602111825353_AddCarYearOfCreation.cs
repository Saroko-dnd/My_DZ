namespace EntityCodeFirst_TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarYearOfCreation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cars","YearOfCreation",c => c.Int());
            /*CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Speed = c.Double(nullable: false),
                        YearOfCreation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);*/
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Cars");
            DropColumn("Cars", "YearOfCreation");
        }
    }
}
