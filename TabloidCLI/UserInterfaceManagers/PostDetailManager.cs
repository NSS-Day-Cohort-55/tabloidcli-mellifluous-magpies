using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUi;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUi, string connectionString, int postId)
        {
            _parentUi = parentUi;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title}");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    Note();
                    return this;
                case "0":
                    return _parentUi;
                default:
                    Console.WriteLine("Invalid choice");
                    return this;
            }
        }

        private void View ()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"URL: {post.Url}");
            Console.WriteLine($"Published On: {post.PublishDateTime}");
            Console.WriteLine($"Blog: {post.Blog.Title}");
            Console.WriteLine($"Author: {post.Author.FullName}");
        }

        private void AddTag ()
        {
            List<Tag> tags = _tagRepository.GetAll();
            List<Post> posts = _postRepository.GetAll();
  
            for (int i = 0; i < tags.Count; i++)
            {
                Console.WriteLine($"{i+1}) {tags[i]}");
            
            }

            Console.WriteLine("Choose a tag");
            Console.Write("> ");

            int tagIndex = 0;
            bool testTagIndex = int.TryParse(Console.ReadLine(), out tagIndex);

            while (!testTagIndex)
            {
                Console.WriteLine("Choose a post number");
                Console.Write("> ");
                testTagIndex = int.TryParse(Console.ReadLine(), out tagIndex);
            }

            _postRepository.InsertTag(posts[_postId], tags[tagIndex-1] );


        }
        private void RemoveTag()
        {
            throw new NotImplementedException();
        }
        private void Note()
        {
            throw new NotImplementedException();
        }
}
}
