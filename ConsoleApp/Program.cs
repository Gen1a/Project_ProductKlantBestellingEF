using BusinessLayer.Managers;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using BusinessLayer.Factories;
using System;
using System.Collections.Generic;

namespace ConsoleAppKlantBestellingen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProductKlantBestelling ConsoleApp:");
            
            string connectionString = "Data Source=HP-ENVYX360;Initial Catalog=Project_ProductKlantBestelling;Integrated Security=True";
            IDbManager<Klant> kM = new DbKlantManager(connectionString);
            IDbManager<Product> pM = new DbProductManager(connectionString);
            IDbManager<Bestelling> bM = new DbBestellingManager(connectionString);
            Klant k = kM.HaalOp(1);
            kM.VoegToe(new Klant("Elena", "Geraardsbergsesteenweg"));
            

            // MEMORY VERSION
            //KlantManager kM = new KlantManager();
            //ProductManager pM = new ProductManager();
            //BestellingManager bM = new BestellingManager();
            //IDFactory idF = new IDFactory(0, 100, 5000); //klant,bestelling,product
            //pM.VoegProductToe(ProductFactory.MaakProduct("product 1", 10.0m, idF));
            //pM.VoegProductToe(ProductFactory.MaakProduct("product 2", 12.0m, idF));
            //pM.VoegProductToe(ProductFactory.MaakProduct("product 3", 13.0m, idF));
            //foreach (var x in pM.GeefProducten()) Console.WriteLine(x);

            //kM.VoegKlantToe(KlantFactory.MaakKlant("klant 1", "adres 1", idF));
            //kM.VoegKlantToe(KlantFactory.MaakKlant("klant 2", "adres 2", idF));
            //foreach (var x in kM.GeefKlanten()) //Console.WriteLine(x);
            //    x.Show();

            //bM.VoegBestellingToe(BestellingFactory.MaakBestelling(idF));
            //bM.VoegBestellingToe(BestellingFactory.MaakBestelling(kM.GeefKlant(1), idF));

            //Bestelling b = bM.GeefBestelling(101);
            //b.VoegProductToe(pM.GeefProduct("product 2"), 8);
            //b.VoegProductToe(pM.GeefProduct("product 1"), 7);
            //Console.WriteLine($"Prijs:{b.BerekenKostprijs()}, {b.PrijsBetaald}");
            //b.Betaald = true;
            //Console.WriteLine($"Prijs:{b.BerekenKostprijs()}, {b.PrijsBetaald}");

            //foreach (var x in bM.GeefBestellingen()) //Console.WriteLine(x);
            //    x.Show();
            //foreach (var x in kM.GeefKlanten()) //Console.WriteLine(x);
            //    x.Show();
            //Console.WriteLine("-----------------");
            //Klant k1 = kM.GeefKlant(1);
            //k1.Show();
            //k1.VoegBestellingToe(b);
            //k1.Show();
        }
    }
}
