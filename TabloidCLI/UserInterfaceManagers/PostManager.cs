using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }


        private void Add()
        {
            Post post = new Post();
            Console.WriteLine("Post Title");
            Console.Write("> ");
            post.Title = Console.ReadLine();

            Console.WriteLine("Enter URL");
            Console.Write("> ");
            post.Url = Console.ReadLine();

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {authors[i]}");
            }

            Console.WriteLine("Select author");
            Console.Write("> ");

            int authorIndex = 0;
            bool testAuthor = int.TryParse(Console.ReadLine(), out authorIndex);

            while (!testAuthor)
            {
                Console.WriteLine("Select number of author");
                Console.Write("> ");
                testAuthor = int.TryParse(Console.ReadLine(), out authorIndex);
            }
            post.Author = authors[authorIndex -1];

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Console.WriteLine($"{i+1}) {blogs[i]}");
            }

            Console.WriteLine("Select blog");
            Console.Write("> ");

            int blogIndex = 0;
            bool testBlog = int.TryParse(Console.ReadLine(), out blogIndex);

            while (!testBlog)
            {
                Console.WriteLine("Select number of author");
                Console.Write("> ");
                testAuthor = int.TryParse(Console.ReadLine(), out blogIndex);
            }
            post.Blog = blogs[blogIndex - 1];



            Console.WriteLine("Enter publish date");
            Console.Write("> ");

            DateTime date = DateTime.Now;

            bool testDate = DateTime.TryParse(Console.ReadLine(), out date);

            while (!testDate)
            {
                Console.WriteLine("Enter publish date in valid format");
                Console.Write("> ");

                testDate = DateTime.TryParse(Console.ReadLine(), out date);
            }
            post.PublishDateTime = date;

            _postRepository.Insert(post);

        }

        private void Edit()
        {
            List();
            Post chosenPost = Choose();
            

            Console.WriteLine("Enter a new Title");
            Console.Write("> ");
            string newTitle = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(newTitle))
            {
                chosenPost.Title = newTitle;
            }


            Console.WriteLine("Enter a new URL");
            Console.Write("> ");

            string newUrl = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(newUrl))
            {
                chosenPost.Url = newUrl;
            }


            Console.WriteLine("Update publication date");
            Console.Write("> ");

            string dateString = Console.ReadLine();
            DateTime newDate = chosenPost.PublishDateTime;

            if (!String.IsNullOrWhiteSpace(dateString))
            {

                bool testDate = DateTime.TryParse(dateString, out newDate);

                while (!testDate)
                {
                    Console.WriteLine("Enter an updated publication date in a valid DateTime format");
                    Console.Write("> ");
                    testDate = DateTime.TryParse(Console.ReadLine(), out newDate);

                }
                chosenPost.PublishDateTime = newDate;

            }


            

            List<Author> authors = _authorRepository.GetAll();

            foreach (Author author in authors)
            {
                Console.WriteLine(author);
            }

            
            Console.WriteLine("Choose an author");
            Console.Write("> ");

            int authorIndex=0;

            string authorChoice = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(authorChoice))
            {
                bool testNum = int.TryParse(authorChoice, out authorIndex);

                while (!testNum)
                {
                    Console.WriteLine("Choose the number of an author");
                    Console.Write("> ");

                    authorChoice = Console.ReadLine();
                }
                chosenPost.Author = authors[authorIndex - 1];
            }
            

            List<Blog> blogs = _blogRepository.GetAll();

            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }

            Console.WriteLine("Choose a blog");
            Console.Write("> ");

            int blogIndex = 0;

            string blogChoice = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(blogChoice))
            {
                bool testNum = int.TryParse(blogChoice, out blogIndex);

                while (!testNum)
                {
                    Console.WriteLine("Choose the number of a blog");
                    Console.Write("> ");

                    blogChoice = Console.ReadLine();
                }
                chosenPost.Blog = blogs[blogIndex - 1];
            }
            

            _postRepository.Update(chosenPost);

        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which Post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}
