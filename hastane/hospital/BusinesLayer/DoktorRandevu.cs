using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccessLayer;
using System.Data.OleDb;
using System.Data;

namespace BusinessLayer
{
    public class DoktorRandevu
    {
        public EntityLayer.Sekreter BL_Sekreter = new EntityLayer.Sekreter();

        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();
        public DataTable goruntule(DateTime tarih, string dkimlik)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                try
                {

                    string query = "SELECT Randevu.HastaKimlik, Randevu.DoktorKimlik, Format(Randevu.RandevuTarihi, 'dd/mm/yyyy') AS RandevuTarihi, Format(Randevu.RandevuSaati, 'hh:nn:ss') AS RandevuSaati,  Hasta.HastaAd, Hasta.HastaSoyad, Format(Hasta.HastaDogumTarihi, 'dd/mm/yyyy') AS HastaDogumTarihi, Hasta.HastaMail FROM Randevu INNER JOIN Hasta ON Randevu.HastaKimlik = Hasta.HastaKimlik WHERE (Randevu.RandevuTarihi=@RandevuTarihi AND Randevu.DoktorKimlik=@DoktorKimlik)";
                    OleDbCommand komut = new OleDbCommand(query, connection);
                    komut.Parameters.AddWithValue("@RandevuTarihi", tarih.ToString("dd/MM/yyyy")); // Tarih formatını uygun şekilde belirtin
                    komut.Parameters.AddWithValue("@DoktorKimlik", dkimlik);

                    using (OleDbDataAdapter da = new OleDbDataAdapter(komut))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda yapılacak işlemler
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
            return dt;
        }
        public void ekle(string dkimlik, string hkimlik, string gorus, string tahlil)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO Gorusme (DoktorKimlik, HastaKimlik, Gorusler, Tahlil) VALUES (@DoktorKimlik, @HastaKimlik, @Gorusler,@Tahlil)", connection);
                komut.Parameters.AddWithValue("@DoktorKimlik", dkimlik);
                komut.Parameters.AddWithValue("@HastaKimlik", hkimlik);
                komut.Parameters.AddWithValue("@Gorusler", gorus);
                komut.Parameters.AddWithValue("@Tahlil", tahlil);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }

            connection.Close();
        }
        public DataTable eski_gorusmeler(string filter, string text)
        {

            DataTable dt = new DataTable();
            OleDbConnection connection = baglanti.ConnectionOpen();
            string query;

            try
            {
                if (filter == "Hasta Kimlik")
                    query = "SELECT HastaKimlik, Gorusler, Tahlil FROM Gorusme WHERE HastaKimlik LIKE '" + text + "%'";
                else if (filter == "Randevu Tarih")
                {
                    query = "SELECT Gorusme.HastaKimlik, Gorusme.Gorusler, Gorusme.Tahlil, Randevu.RandevuTarihi FROM Gorusme INNER JOIN Randevu ON Gorusme.HastaKimlik = Randevu.HastaKimlik WHERE Randevu.RandevuTarihi LIKE '" + text + "%'";
                }
                else
                    query = "SELECT HastaKimlik, Gorusler, Tahlil FROM Gorusme";

                OleDbCommand komut = new OleDbCommand(query, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(komut);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                Console.WriteLine("Hata: " + ex.Message);
            }
            return dt;
        }
    }
}
