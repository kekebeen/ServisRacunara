namespace ServisRacunara.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUposlenikTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uposleniks", "Administrator", c => c.Boolean());
            AlterColumn("dbo.Uposleniks", "Serviser", c => c.Boolean());
            AlterColumn("dbo.Uposleniks", "Prodavac", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uposleniks", "Prodavac", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Uposleniks", "Serviser", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Uposleniks", "Administrator", c => c.Boolean(nullable: false));
        }
    }
}
