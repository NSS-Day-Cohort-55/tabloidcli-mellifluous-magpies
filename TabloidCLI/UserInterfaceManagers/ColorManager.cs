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
            Random random = new Random();

            Console.WriteLine("Choose a Background Color");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Green");
            Console.WriteLine(" 3) Pretty Mode");
            Console.WriteLine(" 4) Mega Lame-o mode"); 
            Console.WriteLine(" 5) Bee Mode");
            Console.WriteLine(" 6) WACKY MODE! (warning)");
            


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
                case "6":
                    Console.BackgroundColor = (ConsoleColor)random.Next(0,16);
                    Console.ForegroundColor = (ConsoleColor)random.Next(0,16);
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }
        }
    }
}