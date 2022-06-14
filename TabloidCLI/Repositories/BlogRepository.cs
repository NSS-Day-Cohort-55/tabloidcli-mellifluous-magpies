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

        }

        public List<Blog> GetAll()
        {
            List<Blog> blogs = new List<Blog>();
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, Url FROM Blog;";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Blog blog = new Blog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url"))
                            };
                            blogs.Add(blog);
                        }
                    }
                }
            }
            return blogs;
        }

        public void Update(Blog blog)
        {
             using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog
                                        SET Title = @title,
                                        URL = @url
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Blog Get(int i)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Title, URL FROM Blog WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", i);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Blog blog = null;
                        if (reader.Read())
                        {
                            blog = new Blog
                            {
                                Id = i,
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("URL"))
                            };
                        }
                        return blog;
                    }
                }
            }
        }
    }
}
