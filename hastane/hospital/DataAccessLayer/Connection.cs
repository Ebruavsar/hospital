using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Connection
    {
       
            public OleDbConnection ConnectionOpen()
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ebrua\\Desktop\\hastane\\hospital.accdb;");
                connection.Open();
                return connection;
            }
        
    }
}
