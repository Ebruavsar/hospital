using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class DoktorIslemleri
    {
        public EntityLayer.Doktor BL_Doktor = new EntityLayer.Doktor(); //entitylayerdan doktor clasının içine ulaşabilmek için bl_doktor olarak oluşturulur.
        //bl_doktor bussinestaki doktor classını temsil eder

        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection(); //veritabanına bağlanmak için

        public void ekle(string kimlik,string ad, string soyad, string brans, string tel, string sekreter, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO Doktor (DoktorKimlik, DoktorAd,DoktorSoyad,DoktorBransAd,DoktorTelefon,SekreterKimlik,DoktorSifre) VALUES (@DoktorKimlik, @DoktorAd, @DoktorSoyad, @DoktorBransAd,@DoktorTelefon, @SekreterKimlik,@DoktorSifre)", connection);
                komut.Parameters.AddWithValue("@DoktorKimlik", kimlik);
                komut.Parameters.AddWithValue("@DoktorAd", ad);
                komut.Parameters.AddWithValue("@DoktorSoyad", soyad);
                komut.Parameters.AddWithValue("@DoktorBransAd", brans);
                komut.Parameters.AddWithValue("@DoktorTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterKimlik", sekreter);
                komut.Parameters.AddWithValue("@DoktorSifre", sifre);
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
                if (filter == "Doktor Ad")
                    query = "SELECT DoktorKimlik, DoktorAd, DoktorSoyad, DoktorBransAd, DoktorTelefon, DoktorSifre FROM Doktor where DoktorAd like '" + text + "%'";
                else if (filter == "Doktor Soyad")
                    query = "SELECT DoktorKimlik,DoktorAd, DoktorSoyad, DoktorBransAd, DoktorTelefon, DoktorSifre FROM Doktor where DoktorSoyad like '" + text + "%'";
                else if (filter == "Doktor Brans")
                    query = "SELECT DoktorKimlik,DoktorAd, DoktorSoyad, DoktorBransAd, DoktorTelefon, DoktorSifre FROM Doktor where DoktorBransAd like '" + text + "%'";
                else if (filter == "Doktor Kimlik")
                    query = "SELECT DoktorKimlik,DoktorAd, DoktorSoyad, DoktorBransAd, DoktorTelefon, DoktorSifre FROM Doktor where DoktorKimlik like '" + text + "%'";
                else
                    query = "SELECT DoktorKimlik, DoktorAd, DoktorSoyad, DoktorBransAd, DoktorTelefon, DoktorSifre FROM Doktor";

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
                OleDbCommand komut = new OleDbCommand("DELETE FROM Doktor WHERE DoktorKimlik = @doktorkimlik", connection);
                komut.Parameters.AddWithValue("@doktorkimlik", kimlik);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Eklenemedi");
            }
            connection.Close();
        }
        public void guncelle(string kimlik, string ad, string soyad, string brans, string tel, string sekreter, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE Doktor SET DoktorAd=@DoktorAd, DoktorSoyad=@DoktorSoyad, DoktorBransAd=@DoktorBransAd, DoktorTelefon=@DoktorTelefon, SekreterKimlik=@SekreterKimlik, DoktorSifre=@DoktorSifre  WHERE DoktorKimlik = @DoktorKimlik", connection);

                komut.Parameters.AddWithValue("@DoktorAd", ad);
                komut.Parameters.AddWithValue("@DoktorSoyad", soyad);
                komut.Parameters.AddWithValue("@DoktorBransAd", brans);
                komut.Parameters.AddWithValue("@DoktorTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterKimlik", sekreter);
                komut.Parameters.AddWithValue("@DoktorSifre", sifre);
                komut.Parameters.AddWithValue("@DoktorKimlik", kimlik);
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            connection.Close();
        }
    }
}
