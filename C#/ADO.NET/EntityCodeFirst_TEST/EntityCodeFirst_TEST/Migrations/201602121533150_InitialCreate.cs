namespace EntityCodeFirst_TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Cars","YearOfCreation",c => c.Int());
            
        }
        
        public override void Down()
        {
            DropColumn("Cars", "YearOfCreation");
        }
    }
}
