using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"INSERT INTO Blog (Title, URL)
                                        OUTPUT INSERTED.Id
                                        VALUES (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);

                    int id = (int)cmd.ExecuteScalar();

                    blog.Id = id;
                   
                    
                }
                conn.Close();
            }
        }

        public List<Blog> GetAll()
        {
            return null;
        
        }

        public void Update(Blog blog)
        { 
        
        }

        public void Delete(int i)
        { 
        
        }

        public Blog Get(int i)
        {
            return null;
        }




    }
}

