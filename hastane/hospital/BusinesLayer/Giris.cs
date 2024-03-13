using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BusinessLayer
{
    public class Giris
    {
        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();

        public Boolean SekreterGiris(string kimlik, string sifre)
        {

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                OleDbCommand komut = new OleDbCommand("SELECT COUNT(*) FROM Sekreter WHERE SekreterKimlik=@kimlik AND SekreterSifre=@sifre", connection);
                komut.Parameters.AddWithValue("@kimlik", kimlik);
                komut.Parameters.AddWithValue("@sifre", sifre);
                int count = (int)komut.ExecuteScalar();
                if (count > 0)
                    return true;
                else
                    return false;
            }

        }
        //doktor giriş
        public Boolean DoktorGiris(string kimlik, string sifre)
        {

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM Doktor WHERE DoktorKimlik=@kimlik AND DoktorSifre=@sifre", connection);
                komut2.Parameters.AddWithValue("@kimlik", kimlik);
                komut2.Parameters.AddWithValue("@sifre", sifre);
                int count = (int)komut2.ExecuteScalar();
                if (count > 0)
                    return true;
                else
                    return false;
            }

        }

    }
}
