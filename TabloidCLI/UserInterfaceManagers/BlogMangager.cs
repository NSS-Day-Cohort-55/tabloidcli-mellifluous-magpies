using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using System.Linq;


namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blog");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    return this;
                case "3":
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }
        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private void Edit()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} - {blog.Title}");
            }
            Console.Write("Which blog would you like to edit?");
            int selectedBlogId = int.Parse(Console.ReadLine());
            Blog selectedBlog = blogs.FirstOrDefault(b => b.Id == selectedBlogId);

            Console.Write("New Title: ");
            string response = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(response))
            {
                selectedBlog.Title = response;
            }
            

            Console.Write("New URL: ");
            string URLresponse = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(URLresponse))
            {
                selectedBlog.Url = URLresponse;
            }

            _blogRepository.Update(selectedBlog);

            Console.WriteLine("Blog has been successfully updated");
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }
    }

}