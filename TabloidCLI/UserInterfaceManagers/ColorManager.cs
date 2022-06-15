using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ColorManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;

        public ColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Choose a Background Color");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Green");
            Console.WriteLine(" 3) Magenta");
            Console.WriteLine(" 4) White"); 
            Console.WriteLine(" 5) Yellow");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    return _parentUI;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Clear();
                    return _parentUI;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return _parentUI;
                case "4":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return _parentUI;
                case "5":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }
        }
    }
}