using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Mail
    {
        public string senderEmail { get; private set; } = "duhastanesi@gmail.com";
        public string senderpassword { get; private set; } = "orgjcoexzfhchdri";
        public string smtphost { get; private set; } = "smtp.gmail.com";
        public string konu { get; set; }
        public string baslik { get; set; }
        public int smtpport { get; private set; } = 587;
    }
}
