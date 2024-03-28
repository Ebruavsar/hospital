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


        // Veritabanından doktorlara ait hasta sayılarını alacak metot
        public Dictionary<string, int> GetDoctorPatientCounts()
        {
            Dictionary<string, int> doctorPatientCounts = new Dictionary<string, int>();

            // Veritabanına bağlanma ve sorgu çalıştırma işlemleri
            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                string query = "SELECT DoktorKimlik, COUNT(*) AS HastaSayısı FROM Randevu GROUP BY DoktorKimlik";
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string doctorName = reader["DoktorKimlik"].ToString();
                    int patientCount = Convert.ToInt32(reader["HastaSayısı"]);
                    doctorPatientCounts.Add(doctorName, patientCount);
                }
            }

            return doctorPatientCounts;

        }

    }
}
