
using System;
using System.Data.SqlClient;
using Classes;

namespace API
{
    public abstract class DatabaseHandler
    {
        public static string GetConnectionString()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "adamdev.database.windows.net";
                builder.UserID = "adam";
                builder.Password = "Move123!";
                builder.InitialCatalog = "AdamTest"; //databsase name
                return builder.ConnectionString;
            }
            catch (Exception e)
            {
                throw new Exception("Error in GetConnectionString(): " + e.Message);
            }
        }
    }
}