using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLayer.Managers
{
    public class DbProductManager : IDbManager<Product>
    {
        // readonly possible if instance constructor of the class contains the instance field declaration
        private readonly string connectionString;

        public DbProductManager(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new DbProductManagerException("Fout bij het aanmaken van DbProductManager: connectionstring moet ingevuld zijn");
            }
            this.connectionString = connection;
        }

        public Product MaakNieuwProduct(string naam, decimal prijs)
        {
            if (string.IsNullOrEmpty(naam)) throw new DbProductManagerException("DbProductManager: Naam van een product mag niet leeg zijn");
            if (prijs < 0) throw new DbProductManagerException("DbProductManager: Prijs van een product mag niet kleiner dan 0 zijn");
            
            return new Product(naam, prijs);
        }

        public Product MaakNieuwProduct(string naam, decimal prijs, long id)
        {
            if (id < 0) throw new DbProductManagerException("DbProductManager: Id van product is invalide");
            Product p = MaakNieuwProduct(naam, prijs);
            p.ProductId = id;

            return p;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbProductManagerException("Fout bij aanmaken van connectie met databank: check connectiestring");
            }
            return connection;
        }

        public IReadOnlyList<Product> HaalOp()
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Product";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Product> producten = new List<Product>();
                    while (reader.Read())
                    {
                        producten.Add(MaakNieuwProduct((string)reader["Naam"], (decimal)reader["Prijs"], (long)reader["Id"]));
                    }
                    reader.Close();
                    return producten;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProductManagerException("DbProductManager: Fout bij ophalen van producten uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Product HaalOp(long id)
        {
            if (id <= 0)
            {
                throw new DbProductManagerException("DbProductManager: Id van product moet groter zijn dan 0");
            }

            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Product WHERE Id=@id";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt));
                command.Parameters["@id"].Value = id;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Product product = MaakNieuwProduct((string)reader["Naam"], (decimal)reader["Prijs"], (long)reader["Id"]);
                    reader.Close();
                    return product;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProductManagerException("DbProductManager: Fout bij ophalen van product uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Product> HaalOp(Func<Product, bool> predicate)
        {
            // Note: Performanter om te implementeren via LINQ to Entities of LINQ to SQL
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Product";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Product> producten = new List<Product>();
                    while (reader.Read())
                    {
                        producten.Add(MaakNieuwProduct((string)reader["Naam"], (decimal)reader["Prijs"], (long)reader["Id"]));
                    }
                    reader.Close();
                    var selection = producten.Where<Product>(predicate).ToList();
                    return selection;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProductManagerException("DbProductManager: Fout bij ophalen van product(en) uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void Verwijder(Product product)
        {
            if (product == null) throw new DbProductManagerException("DbProductManager: Product mag niet null zijn");
            if (product.ProductId <= 0) throw new DbProductManagerException("DbProductManager: Te verwijderen product heeft een invalide Id");

            SqlConnection connection = GetConnection();
            string query = "DELETE FROM Product WHERE Id=@id";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt));
                    command.Parameters["@id"].Value = product.ProductId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProductManagerException("DbProductManager: Fout bij verwijderen van product uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VoegToe(Product product)
        {
            if (product == null) throw new DbKlantManagerException("DbKlantManager: Klant mag niet null zijn");
            if (string.IsNullOrEmpty(product.Naam)) throw new DbProductManagerException("DbProductManager: Naam van product mag niet leeg zijn");
            if (product.Prijs < 0) throw new DbProductManagerException("DbProductManager: Prijs van product mag niet kleiner dan 0 zijn");

            SqlConnection connection = GetConnection();
            string query = "INSERT INTO Product (Naam, Prijs) VALUES (@naam, @prijs)";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@prijs", SqlDbType.Decimal));
                    command.Parameters["@naam"].Value = product.Naam;
                    command.Parameters["@prijs"].Value = product.Prijs;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProductManagerException("DbProductManager: Fout bij toevoegen van product aan database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
