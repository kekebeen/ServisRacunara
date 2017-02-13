namespace ServisRacunara.Data.MODELS
{
    public class Proizvod
    {
        public int ProizvodId { get; set; }

        public string Naziv { get; set; }

        public float Cijena { get; set; }

        public string Opis { get; set; }

        public bool Usluga { get; set; }
    }
}