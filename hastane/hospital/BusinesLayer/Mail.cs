using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class Mail
    {
        private DataAccessLayer.Connection baglanti = new DataAccessLayer.Connection();
        private EntityLayer.Mail mail = new EntityLayer.Mail();

        public void SendMail(List<string> list, string kimlik, string baslik, string konu)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();

            try
            {

                string query = "SELECT HastaMail FROM Hasta where HastaKimlik = @HastaKimlik ";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HastaKimlik", kimlik);
                    command.ExecuteNonQuery();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hasta posta = new Hasta()
                            {
                                HastaMail = reader["HastaMail"].ToString(),
                            };

                            list.Add(posta.HastaMail.ToString());
                        }
                    }
                }


                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(mail.senderEmail, mail.senderpassword);
                smtp.Host = mail.smtphost;
                smtp.Port = mail.smtpport;
                smtp.EnableSsl = true;
                mail.baslik = baslik;
                mail.konu = konu;
                foreach (var mil in list)
                {
                    MailMessage message = new MailMessage(mail.senderEmail, mil, baslik, konu);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

    }
}
