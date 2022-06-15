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
            throw new NotImplementedException();
        }

        private void Edit()
        {
            List();
            Post chosenPost = Choose();
            Post updatedPost = new Post();

            Console.WriteLine("Enter a new Title");
            Console.Write("> ");
            string newTitle = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(newTitle))
            {
                updatedPost.Title = newTitle;
            }

            Console.WriteLine("Enter a new URL");
            Console.Write("> ");

            string newUrl = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(newUrl))
            {
                updatedPost.Url = newUrl;
            }

            Console.WriteLine("Update publication date");
            Console.Write("> ");

            string dateString = Console.ReadLine();
            DateTime newDate;

            if (!String.IsNullOrWhiteSpace(dateString))
            {
                
                bool testDate = DateTime.TryParse(dateString, out newDate);

                while (!testDate)
                {
                    Console.WriteLine("Enter an updated publication date in a valid DateTime format");
                    Console.Write("> ");
                    testDate = DateTime.TryParse(Console.ReadLine(), out newDate);
                }

            }

            _authorRepository.GetAll();

            
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
                
            }
            updatedPost.Author = _authorRepository.Get(authorIndex - 1);

            _blogRepository.GetAll();

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
            
            }
            updatedPost.Blog = _blogRepository.Get(blogIndex - 1);

            _postRepository.Update(updatedPost);

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
