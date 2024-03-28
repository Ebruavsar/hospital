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

                    string query = "SELECT Randevu.HastaKimlik, Randevu.DoktorKimlik, Randevu.RandevuTarihi, Randevu.RandevuSaati, Hasta.HastaAd, Hasta.HastaSoyad, Hasta.HastaDogumTarihi, Hasta.HastaMail FROM Randevu INNER JOIN Hasta ON Randevu.HastaKimlik = Hasta.HastaKimlik WHERE (Randevu.RandevuTarihi=@RandevuTarihi AND Randevu.DoktorKimlik=@DoktorKimlik)";
                    OleDbCommand komut = new OleDbCommand(query, connection);
                    komut.Parameters.AddWithValue("@RandevuTarihi", tarih);
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
    }
}
