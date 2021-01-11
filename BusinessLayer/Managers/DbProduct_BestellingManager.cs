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
    public class DbProduct_BestellingManager
    {
        private readonly string connectionString;

        public DbProduct_BestellingManager(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new DbProduct_BestellingManagerException("Fout bij het aanmaken van DbProduct_BestellingManager: connectionstring moet ingevuld zijn");
            }
            this.connectionString = connection;
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
                throw new DbProduct_BestellingManagerException("Fout bij aanmaken van connectie met databank: check connectiestring");
            }
            return connection;
        }

        public IReadOnlyList<Product_Bestelling> HaalOp()
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Product_Bestelling";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Product_Bestelling> product_bestellingen = new List<Product_Bestelling>();
                    while (reader.Read())
                    {
                        product_bestellingen.Add(new Product_Bestelling((long)reader["productId"], (long)reader["bestellingId"], (int)reader["aantal"]));
                    }
                    reader.Close();
                    return product_bestellingen;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Fout bij ophalen van Product_Bestelling uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Product_Bestelling> HaalOp(Func<Product_Bestelling, bool> predicate)
        {
            List<Product_Bestelling> product_bestellingen = (List<Product_Bestelling>)HaalOp();
            var selection = product_bestellingen.Where<Product_Bestelling>(predicate).ToList();

            return selection;
        }

        public void Verwijder(Product_Bestelling item)
        {
            if (item == null) throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Product_Bestelling mag niet null zijn");
            if (item.ProductId <= 0 || item.BestellingId <= 0) throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Te verwijderen Product_Bestelling heeft een invalide Id");

            SqlConnection connection = GetConnection();
            string query = "DELETE FROM Product_Bestelling WHERE productId=@productId AND bestellingId=@bestellingId";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@productId", SqlDbType.BigInt));
                    command.Parameters.Add(new SqlParameter("@bestellingId", SqlDbType.BigInt));
                    command.Parameters["@productId"].Value = item.ProductId;
                    command.Parameters["@bestellingId"].Value = item.BestellingId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Fout bij verwijderen van Product_Bestelling uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VoegToe(Product_Bestelling item)
        {
            if (item == null) throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Product_Bestelling mag niet null zijn");
            if (item.ProductId <= 0 || item.BestellingId <= 0) throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Product_Bestelling heeft een invalide Id");
            if (item.Aantal <= 0) throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Aantal moet groter dan 0 zijn");

            SqlConnection connection = GetConnection();
            string query = "INSERT INTO Product_Bestelling (productId, bestellingId, aantal) VALUES (@productId, @bestellingId, @aantal)";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@productId", SqlDbType.BigInt));
                    command.Parameters.Add(new SqlParameter("@bestellingId", SqlDbType.BigInt));
                    command.Parameters.Add(new SqlParameter("@aantal", SqlDbType.Int));
                    command.Parameters["@productId"].Value = item.ProductId;
                    command.Parameters["@bestellingId"].Value = item.BestellingId;
                    command.Parameters["@aantal"].Value = item.Aantal;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Fout bij toevoegen van Product_Bestelling aan database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
