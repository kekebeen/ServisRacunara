namespace ServisRacunara.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedKorisnikTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uposleniks", "Korisnik_Id", c => c.Int());
            AddColumn("dbo.Korisniks", "Uposlenjaa_UposlenikId", c => c.Int());
            CreateIndex("dbo.Uposleniks", "Korisnik_Id");
            CreateIndex("dbo.Korisniks", "Uposlenjaa_UposlenikId");
            AddForeignKey("dbo.Uposleniks", "Korisnik_Id", "dbo.Korisniks", "Id");
            AddForeignKey("dbo.Korisniks", "Uposlenjaa_UposlenikId", "dbo.Uposleniks", "UposlenikId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisniks", "Uposlenjaa_UposlenikId", "dbo.Uposleniks");
            DropForeignKey("dbo.Uposleniks", "Korisnik_Id", "dbo.Korisniks");
            DropIndex("dbo.Korisniks", new[] { "Uposlenjaa_UposlenikId" });
            DropIndex("dbo.Uposleniks", new[] { "Korisnik_Id" });
            DropColumn("dbo.Korisniks", "Uposlenjaa_UposlenikId");
            DropColumn("dbo.Uposleniks", "Korisnik_Id");
        }
    }
}
