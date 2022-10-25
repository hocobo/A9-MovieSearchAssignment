using System;
using System.Collections.Generic;
using A9_MovieSearchAssignment.Models;

namespace A9_MovieSearchAssignment
{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu();
            Switch();
        }

        public static void Menu()
        {
            Console.WriteLine("Select media type:\n1. Movie\n2. Show\n3. Video\n4. Search");
        }

        public static void Switch()
        {
            Media movie = new Movie();
            Media show = new Show();
            Media video = new Video();
            string input = Console.ReadLine();

            while (input == "1" || input == "2" || input == "3"|| input == "4")
            {
                switch (input)
                {
                    case "1":
                        movie.Read();
                        movie.Display();
                        break;
                    case "2":
                        show.Read();
                        show.Display();
                        break;
                    case "3":
                        video.Read();
                        video.Display();
                        break;
                    case "4":
                        Console.WriteLine("Enter the name of the Movie, show, or video.");
                        var mediaName = Console.ReadLine();
                        Console.Write(movie.Search(mediaName));
                        break;
                        
                }
                Menu();
                input = Console.ReadLine();
            }
                
        }
    }
}