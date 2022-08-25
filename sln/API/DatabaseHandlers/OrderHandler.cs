using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections;
using Classes;

namespace API
{
    public class OrderHandler : DatabaseHandler
    {
        public IEnumerable<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using(SqlConnection conn = new SqlConnection(GetConnectionString())) 
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM [Order];", conn))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ProdID = reader.GetString(4);
                            Product prod = new Product();
                            prod.ProdID = ProdID;
                            using(SqlConnection conn2 = new SqlConnection(GetConnectionString()))
                            {
                                conn2.Open();
                                using(SqlCommand command2 = new SqlCommand($"SELECT * FROM Product WHERE ProdID = '{ProdID}';", conn2))
                                {
                                    using(SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            prod.Description = reader2.GetString(1);
                                        prod.UnitPrice = reader2.GetInt32(2);
                                        prod.CatID = reader2.GetInt32(3);
                                        }                                        
                                    }
                                }
                            }


                            orders.Add(new Order(){
                                OrderDate = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                ShipDate = reader.GetString(2),
                                ShipMode = reader.GetString(3),
                                CustID = reader.GetString(5),
                                Prod = prod
                            });
                        }
                    }
                }
                conn.Close();
            }
            return orders;
        }

        public float Total(Order order){
            return order.total(order.Quantity, (float)order.Prod.UnitPrice);
        }

        public float GST(Order order){
            return order.GST(order.Quantity, (float)order.Prod.UnitPrice);
        }

        public int Delete(Order order)
        {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())) 
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand("DELETE FROM [ORDER] WHERE OrderDate = @Order AND ProdID = @ProdID AND CustID = @CustID", conn))
                {
                    command.Parameters.AddWithValue("@Order", order.OrderDate);
                    command.Parameters.AddWithValue("@ProdID", order.Prod.ProdID);
                    command.Parameters.AddWithValue("@CustID", order.CustID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                conn.Close();
            }
            
        }

        public int AddOrder(Order order)
        {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())) 
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand("INSERT INTO [ORDER] VALUES (@Order,@Qty,@ShipDate,@ShipMode, @ProdID,@CustID)", conn))
                {
                    command.Parameters.AddWithValue("@Order", order.OrderDate);
                    command.Parameters.AddWithValue("@ProdID", order.Prod.ProdID);
                    command.Parameters.AddWithValue("@CustID", order.CustID);
                    command.Parameters.AddWithValue("@Qty", order.Quantity);
                    command.Parameters.AddWithValue("@ShipDate", order.ShipDate);
                    command.Parameters.AddWithValue("@ShipMode", order.ShipMode);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                conn.Close();
            }
            
        }
    }
}