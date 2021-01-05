//using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Models;
using BusinessLayer.Factories;
using System;

namespace KlantBestellingen.WPF
{
    public static class Context
    {
        public static IDFactory IdFactory { get; } = new IDFactory(0, 100, 5000);
        // DbKlantManager!
        public static KlantManager KlantManager{ get; } = new KlantManager(); // Experimenteer: kan ook nog altijd DbKlantManager zijn!
        public static ProductManager ProductManager { get; } = new ProductManager();
        public static BestellingManager BestellingManager { get; } = new BestellingManager();

        // Method which populates Managers with dummy data
        public static void Populate()
        {

            // Test code: moet weg indien db opgevuld
            KlantManager.VoegKlantToe(KlantFactory.MaakKlant("klant 1", "adres 1", IdFactory));
            KlantManager.VoegKlantToe(KlantFactory.MaakKlant("klant 2", "adres 2", IdFactory));
            //DbProductManager dbProductMgr = new DbProductManager();
            //dbProductMgr.VoegToe(ProductFactory.MaakProduct("Product 1", 5.6, IdFactory));
            //dbProductMgr.VoegToe(ProductFactory.MaakProduct("Product 2", 6.7, IdFactory));
            //var klanten = KlantManager.HaalOp();
            //var producten = dbProductMgr.HaalOp();
            //DbBestellingManager testDbOrderMgr = new DbBestellingManager();
            //{
            //    var counter = 1;
            //    Bestelling bestelling = new Bestelling(0, DateTime.Now) { Klant = klanten[0] };
            //    foreach (var p in producten)
            //    {
            //        bestelling.VoegProductToe(p, counter++);
            //    }
            //    testDbOrderMgr.VoegToe(bestelling);
            //}



            //Test code: we initialiseren een timer die elke 10 seconden het adres aanpast -alsof dit op de business layer gebeurt
            // ----------
            //_timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(10) }; // timer loopt af om de 10 seconden
            //_timer.Tick += _timer_Tick; // voer method uit wanner timer afloopt
            //_timer.Start();
        }
    }

    
}
