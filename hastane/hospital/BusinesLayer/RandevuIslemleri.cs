using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class RandevuIslemleri
    {
        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();

        //------------------hasta görüntüleme-------------
        public string HastaGoruntuleAd(string kimlik)
        {
            string ad = "";
            string soyad = "";
            DateTime dogumTarihi = DateTime.MinValue;
            string cinsiyet = "";

            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("SELECT HastaAd, HastaSoyad, HastaDogumTarihi, HastaCinsiyet FROM Hasta WHERE HastaKimlik LIKE @kimlik", connection);
                komut.Parameters.AddWithValue("@kimlik", kimlik + "%");

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                OleDbDataReader reader = komut.ExecuteReader();

                if (reader.Read())
                {
                    Hasta hasta = new Hasta()
                    {
                        HastaAd = reader["HastaAd"].ToString(),
                        HastaSoyad = reader["HastaSoyad"].ToString(),
                        HastaDogumTarihi =(DateTime)reader["HastaDogumTarihi"],
                        HastaCinsiyet = reader["HastaCinsiyet"].ToString(),
                    };
                    ad = hasta.HastaAd;
                    soyad = hasta.HastaSoyad;
                    dogumTarihi = hasta.HastaDogumTarihi;
                    cinsiyet =hasta.HastaCinsiyet;
                    
                   
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            // Combine all retrieved information into a single string
            string result = $"Hasta Adı: {ad}       Soyadı: {soyad}       Doğum Tarihi: {dogumTarihi.ToShortDateString()}       Cinsiyeti: {cinsiyet}";
            return result;
        }

        //-----------seçilen polikinliğe göre doktorları listeleme-------------
        public DataTable poliklinikDoktorGoruntule(string brans)
        {

            DataTable dt = new DataTable();
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {

                OleDbCommand komut = new OleDbCommand("SELECT DoktorKimlik, DoktorAd, DoktorSoyad, DoktorBransAd FROM Doktor where DoktorBransAd like '" + brans + "%'", connection);
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

        /*------------------------------randevunun aktifliğinin kontrolü----------------------------------------------------------------*/
        public bool RandevuPlanla(string doktorKimlik, string hastaKimlik, DateTime randevuTarihi, string randevuSaati, string BransAd)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                // Tarih ve saat bilgisiyle tarih zamanı oluşturun
                //DateTime randevuZamani = randevuTarihi.Date + randevuSaati;

                // Randevu zamanının uygun olup olmadığını kontrol edin
                OleDbCommand kontrolKomutu = new OleDbCommand("SELECT COUNT(*) FROM Randevu WHERE DoktorKimlik = @DoktorKimlik AND RandevuTarihi = @RandevuTarihi AND RandevuSaati = @RandevuSaati", connection);
                kontrolKomutu.Parameters.AddWithValue("@DoktorKimlik", doktorKimlik);
                kontrolKomutu.Parameters.AddWithValue("@RandevuTarihi", randevuTarihi.Date);
                kontrolKomutu.Parameters.AddWithValue("@RandevuSaati", randevuSaati);
               
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                int mevcutRandevuSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (mevcutRandevuSayisi == 0)
                {
                    // Eğer uygunsa, randevuyu planlamaya devam edin
                    OleDbCommand ekleKomutu = new OleDbCommand("INSERT INTO Randevu (DoktorKimlik, HastaKimlik, RandevuTarihi, RandevuSaati,BransAd) VALUES (@DoktorKimlik, @HastaKimlik, @RandevuTarihi, @RandevuSaati,@BransAd)", connection);
                    ekleKomutu.Parameters.AddWithValue("@DoktorKimlik", doktorKimlik);
                    ekleKomutu.Parameters.AddWithValue("@HastaKimlik", hastaKimlik);
                    ekleKomutu.Parameters.AddWithValue("@RandevuTarihi", randevuTarihi.Date);
                    ekleKomutu.Parameters.AddWithValue("@RandevuSaati", randevuSaati);
                    ekleKomutu.Parameters.AddWithValue("@BransAd", BransAd);

                    ekleKomutu.ExecuteNonQuery();

                    Console.WriteLine("Randevu başarıyla oluşturuldu.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Seçilen saatte doktorun randevusu bulunmaktadır. Lütfen başka bir saat seçiniz.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        //----------------seçilen doktorun randevularını listeleme---------------
        public DataTable DoktorRandevuGoruntule(string Dkimlik, DateTime randevuTarihi, string randevuSaati)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                try
                {
                    // Randevu tarihi ve saatini kullanarak başlangıç ve bitiş zamanlarını belirleyin
                    DateTime baslangicZamani = randevuTarihi.Date + TimeSpan.Parse(randevuSaati); // Seçilen randevu saati
                    DateTime bitisZamani = baslangicZamani.AddHours(1); // Seçilen randevu saati ile bir sonraki saat

                    string baslangicDate = baslangicZamani.ToString("dd.MM.yyyy");
                    string baslangicSaati = baslangicZamani.ToString("HH:mm:ss");
                    string bitisDate = bitisZamani.ToString("dd.MM.yyyy");
                    string bitisSaati = bitisZamani.ToString("HH:mm:ss");

                    string query = "SELECT DoktorKimlik, HastaKimlik, RandevuTarihi, FORMAT(RandevuSaati, 'Short Time') AS RandevuSaati FROM Randevu WHERE DoktorKimlik = @DoktorKimlik AND RandevuTarihi = @RandevuTarihi AND RandevuSaati >= @BaslangicSaati AND RandevuSaati < @BitisSaati ORDER BY RandevuSaati ASC";
                    OleDbCommand komut = new OleDbCommand(query, connection);
                    komut.Parameters.AddWithValue("@DoktorKimlik", Dkimlik);
                    komut.Parameters.AddWithValue("@RandevuTarihi", baslangicDate);
                    komut.Parameters.AddWithValue("@BaslangicSaati", baslangicSaati);
                    komut.Parameters.AddWithValue("@BitisSaati", bitisSaati);

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

    }
}
