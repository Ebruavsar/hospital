using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.OleDb;
using System.Data;

namespace BusinessLayer
{
    public class HastaIslemleri
    {
        public EntityLayer.Hasta BL_Hasta = new EntityLayer.Hasta();

        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();

        public void ekle(string kimlik, string ad, string soyad, string tel, DateTime tarih,string cinsiyet, string mail)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO Hasta (HastaKimlik, HastaAd, HastaSoyad, HastaTelefon, HastaDogumTarihi, HastaCinsiyet, HastaMail) VALUES (@HastaKimlik, @HastaAd, @HastaSoyad, @HastaTelefon, @HastaDogumTarihi, @HastaCinsiyet, @HastaMail)", connection);

                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                komut.Parameters.AddWithValue("@HastaAd", ad);
                komut.Parameters.AddWithValue("@HastaSoyad", soyad);
                komut.Parameters.AddWithValue("@HastaTelefon", tel);
                komut.Parameters.AddWithValue("@HastaDogumTarihi", tarih);
                komut.Parameters.AddWithValue("@HastaCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@HastaMail", mail);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }

            connection.Close();
        }
        public DataTable goruntule(string filter, string text)
        {

            DataTable dt = new DataTable();
            OleDbConnection connection = baglanti.ConnectionOpen();
            string query;

            try
            {
                if (filter == "Hasta Ad")
                    query = "SELECT * FROM Hasta where HastaAd like '" + text + "%'";
                else if (filter == "Hasta Soyad")
                    query = "SELECT * FROM Hasta where HastaSoyad like '" + text + "%'";
                else if(filter == "Hasta Kimlik")
                    query = "SELECT * FROM Hasta where HastaKimlik like '" + text + "%'";
                else
                    query = "SELECT * FROM Hasta";

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
        public void sil(string kimlik)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("DELETE FROM Hasta WHERE HastaKimlik = @HastaKimlik", connection);
                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }

            connection.Close();
        }
        public void guncelle(string kimlik, string ad, string soyad, string tel, DateTime tarih, string cinsiyet, string mail)
        {
            
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE Hasta SET HastaAd=@HastaAd, HastaSoyad=@HastaSoyad, HastaTelefon=@HastaTelefon, HastaDogumTarihi=@HastaDogumTarihi, HastaCinsiyet=@HastaCinsiyet, HastaMail=@HastaMail  WHERE HastaKimlik = @HastaKimlik", connection);

                komut.Parameters.AddWithValue("@HastaAd", ad);
                komut.Parameters.AddWithValue("@HastaSoyad", soyad);
                komut.Parameters.AddWithValue("@HastaTelefon", tel);
                komut.Parameters.AddWithValue("@HastaDogumTarihi", tarih);
                komut.Parameters.AddWithValue("@HastaCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@HastaMail", mail);
                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                komut.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }

            connection.Close();
        }
    }
}