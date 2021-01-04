using System;
using BusinessLayer.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Klant klant = new Klant("Tom", "Test");
            //Console.WriteLine(klant.Adres);
            Product product = new Product("Item");
            Console.WriteLine(product.Naam);
            Klant k = new Klant("Piet", "Test");
            Console.WriteLine(k.GeefBestellingen().Count);
            foreach (var i in k.GeefBestellingen())
            {
                Console.WriteLine("Item:");
                Console.WriteLine(i.ToString());
            }
        }
    }
}
