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
    public class DbKlantManager : IDbManager<Klant>
    {
        // readonly possible if instance constructor of the class contains the instance field declaration
        private readonly string connectionString;

        public DbKlantManager(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new DbKlantManagerException("Fout bij het aanmaken van DbKlantManager: connectionstring moet ingevuld zijn");
            }
            this.connectionString = connection;
        }

        public Klant MaakNieuweKlant(string naam, string adres)
        {
            if (string.IsNullOrEmpty(naam)) throw new DbKlantManagerException("DbKlantManager: Naam van een klant mag niet leeg zijn");
            if (string.IsNullOrEmpty(adres)) throw new DbKlantManagerException("DbKlantManager: Adres van een klant mag niet leeg zijn");
            
            return new Klant(naam, adres);
        }

        public Klant MaakNieuweKlant(string naam, string adres, long id)
        {
            if (id < 0) throw new DbKlantManagerException("DbKlantManager: Id van klant is invalide");
            Klant k = MaakNieuweKlant(naam, adres);
            k.KlantId = id;

            return k;
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
                throw new DbKlantManagerException("Fout bij aanmaken van connectie met databank: check connectiestring");
            }
            return connection;
        }

        public IReadOnlyList<Klant> HaalOp()
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Klant";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Klant> klanten = new List<Klant>();
                    while (reader.Read())
                    {
                        klanten.Add(MaakNieuweKlant((string)reader["Naam"], (string)reader["Adres"], (long)reader["Id"]));
                    }
                    reader.Close();
                    return klanten;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbKlantManagerException("DbKlantManager: Fout bij ophalen van klanten uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Klant HaalOp(long id)
        {
            if (id <= 0)
            {
                throw new DbKlantManagerException("DbKlantManager: Id van klant moet groter zijn dan 0");
            }

            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Klant WHERE Id=@id";

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
                    Klant klant = MaakNieuweKlant((string)reader["Naam"], (string)reader["Adres"], (long)reader["Id"]);
                    reader.Close();
                    return klant;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbKlantManagerException("DbKlantManager: Fout bij ophalen van klant uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Klant> HaalOp(Func<Klant, bool> predicate)
        {
            // Note: Performanter om te implementeren via LINQ to Entities of LINQ to SQL
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM Klant";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Klant> klanten = new List<Klant>();
                    while (reader.Read())
                    {
                        klanten.Add(MaakNieuweKlant((string)reader["Naam"], (string)reader["Adres"], (long)reader["Id"]));
                    }
                    reader.Close();
                    var selection = klanten.Where<Klant>(predicate).ToList();
                    return selection;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbKlantManagerException("DbKlantManager: Fout bij ophalen van klant(en) uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void Verwijder(Klant klant)
        {
            if (klant == null) throw new DbKlantManagerException("DbKlantManager: Klant mag niet null zijn");
            if (klant.KlantId <= 0) throw new DbKlantManagerException("DbKlantManager: Te verwijderen klant heeft een invalide Id");

            SqlConnection connection = GetConnection();
            string query = "DELETE FROM Klant WHERE Id=@id";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt));
                    command.Parameters["@id"].Value = klant.KlantId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbKlantManagerException("DbKlantManager: Fout bij verwijderen van klant uit database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VoegToe(Klant klant)
        {
            if (klant == null) throw new DbKlantManagerException("DbKlantManager: Klant mag niet null zijn");
            if (string.IsNullOrEmpty(klant.Naam) || string.IsNullOrEmpty(klant.Adres))
            {
                throw new DbKlantManagerException("DbKlantManager: Klant moet een naam en adres hebben");
            }

            SqlConnection connection = GetConnection();
            string query = "INSERT INTO Klant (Naam, Adres) VALUES (@naam, @adres)";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();

                try
                {
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    command.Parameters["@naam"].Value = klant.Naam;
                    command.Parameters["@adres"].Value = klant.Adres;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                    throw new DbKlantManagerException("DbKlantManager: Fout bij toevoegen van klant aan database");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
