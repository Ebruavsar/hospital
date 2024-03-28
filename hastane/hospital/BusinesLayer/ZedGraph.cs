using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ZedGraph
    {
        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();

        public Dictionary<string, int> GetHastalikBolumleriHastaSayilari()
        {
            Dictionary<string, int> hastalikBolumleriHastaSayilari = new Dictionary<string, int>();
            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                string query = "SELECT BransAd, COUNT(*) AS HastaSayisi FROM Randevu GROUP BY BransAd";
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string hastalikBolumu = reader["BransAd"].ToString();
                    int hastaSayisi = Convert.ToInt32(reader["HastaSayisi"]);
                    hastalikBolumleriHastaSayilari.Add(hastalikBolumu, hastaSayisi);
                }
            }
            return hastalikBolumleriHastaSayilari;
        }
    }
}
