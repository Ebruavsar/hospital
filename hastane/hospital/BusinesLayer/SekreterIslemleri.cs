using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace BusinessLayer
{
    public class SekreterIslemleri
    {
        public EntityLayer.Sekreter BL_Sekreter = new EntityLayer.Sekreter();

        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();

        public void ekle(string kimlik, string ad, string soyad, string tel, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO Sekreter (SekreterKimlik, SekreterAd,SekreterSoyad,SekreterTelefon,SekreterSifre) " +
                    "VALUES (@SekreterKimlik, @SekreterrAd, @SekreterSoyad,@SekreterTelefon,@SekreterSifre)", connection);
                komut.Parameters.AddWithValue("@SekreterKimlik", kimlik);
                komut.Parameters.AddWithValue("@SekreterAd", ad);
                komut.Parameters.AddWithValue("@SekreterSoyad", soyad);
                komut.Parameters.AddWithValue("@SekreterTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterSifre", sifre);
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
                if (filter == "Sekreter Ad")
                    query = "SELECT SekreterKimlik, SekreterAd, SekreterSoyad, SekreterTelefon, SekreterSifre FROM Sekreter where SekreterAd like '" + text + "%'";
                else if (filter == "Sekreter Soyad")
                    query = "SELECT SekreterKimlik, SekreterAd, SekreterSoyad, SekreterTelefon, SekreterSifre FROM Sekreter where SekreterSoyad like '" + text + "%'";
                else
                    query = "SELECT SekreterKimlik, SekreterAd, SekreterSoyad, SekreterTelefon, SekreterSifre FROM Sekreter";

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
                OleDbCommand komut = new OleDbCommand("DELETE FROM Sekreter WHERE SekreterKimlik = @Sekreterkimlik", connection);
                komut.Parameters.AddWithValue("@Sekreterkimlik", kimlik);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }

            connection.Close();
        }
        public void guncelle(string kimlik, string ad, string soyad, string tel, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE Sekreter SET SekreterAd=@SekreterAd, SekreterSoyad=@SekreterSoyad, SekreterTelefon=@SekreterTelefon, SekreterSifre=@SekreterSifre " +
                    " WHERE SekreterKimlik = @SekreterKimlik", connection);

                komut.Parameters.AddWithValue("@SekreterAd", ad);
                komut.Parameters.AddWithValue("@SekreterSoyad", soyad);
                komut.Parameters.AddWithValue("@SekreterTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterSifre", sifre);
                komut.Parameters.AddWithValue("@SekreterKimlik", kimlik);
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
