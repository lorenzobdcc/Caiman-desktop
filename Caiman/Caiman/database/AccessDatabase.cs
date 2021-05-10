using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace Caiman.database
{
    public class AccessDatabase
    {
        SqlConnection myConnection;

        public AccessDatabase()
        {
            myConnection = new SqlConnection();
            myConnection.ConnectionString = Properties.Settings.Default.connexionString;

        }

        public void Execute(string sql)
        {

            myConnection.Open();
            SqlCommand command = new SqlCommand(sql, myConnection);

            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                  int lol =  reader.FieldCount;
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            myConnection.Close();
        }

        public void Select(string sql)
        {

            
            SqlCommand command = new SqlCommand(sql, myConnection);
            myConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int lol = reader.FieldCount;
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            myConnection.Close();
        }

    }
}
