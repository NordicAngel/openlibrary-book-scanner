﻿using Microsoft.Data.SqlClient;
using OpenLibraryWebServiceLibrary.Model;

namespace OpenLibraryWebService.Managers
{
    public class ListManager : IListManager<List_Names>
    {
        // Connectionstring to database
        private const string connectionstring = "Server=tcp:datamatiker-daniel.database.windows.net,1433;Initial Catalog=OpenLibrary;Persist Security Info=False;User ID=DanielAdmin;Password=AdminDaniel1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        // Method to create a new list in database. Used through front-end.
        public List_Names Create(List_Names item)
        {
            string sql = "INSERT INTO List_Names values(@List_Name)";
            using(SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@List_Name", item.List_Name);

                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    throw new ArgumentException("Can't Create List");
                }

                return item;

            }
        }
        // Method to show all Lists in database.
        public List<List_Names> GetAll()
        {
            List<List_Names> liste = new List<List_Names>();
            string sql = "SELECT * FROM List_Names";


            using (SqlConnection connection = new SqlConnection(connectionstring))
            {

                connection.Open();


                SqlCommand cmd = new SqlCommand(sql, connection);


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    List_Names list = ReadList(reader);
                    liste.Add(list);
                }
                
            }
            return liste;
        }
        // Method to get a specific list from database.
        public List_Names GetByID(int id)
        {
            string sql = "SELECT * FROM List_Names where ID=@ID";
            using(SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    List_Names listnames = ReadList(reader);
                    return listnames;
                }
            }

            throw new ArgumentException("Can't find the list with given ID");
        }
        // Used to map the model from database. Used for GetByID / GetAll
        private List_Names ReadList(SqlDataReader reader)
        {
            List_Names listnames = new List_Names();

            listnames.ID = reader.GetInt32(0);
            listnames.List_Name = reader.GetString(1);

            return listnames;
        }

    }
}
