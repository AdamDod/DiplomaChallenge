using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections;
using Classes;

namespace API
{
    public class CustomerHandler : DatabaseHandler
    {
        public IEnumerable<Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            using(SqlConnection conn = new SqlConnection(GetConnectionString())) 
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Customer", conn))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer(){
                                CustID = reader.GetString(0),
                                FullName = reader.GetString(1),
                                Country = reader.GetString(2),
                                City = reader.GetString(3),
                                State = reader.GetString(4),
                                PostCode = reader.GetInt32(5),
                                SegID = reader.GetInt32(6),
                                Region = reader.GetString(7),
                            });
                        }
                    }
                }
                conn.Close();
            }
            return customers;
        }

        
    }
}