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
            Console.WriteLine("\nSelect Option:\n1. Display all Movies\n2. Display all Shows\n3. Display all Videos\n4. Search all databases\n");
        }

        public static void Switch()
        {
            Movie movie = new Movie();
            Show show = new Show();
            Video video = new Video();
            movie.Read();
            show.Read();
            video.Read();
            string input = Console.ReadLine();

            while (input == "1" || input == "2" || input == "3"|| input == "4")
            {
                switch (input)
                {
                    case "1":
                        movie.Display();
                        break;
                    case "2":
                        show.Display();
                        break;
                    case "3":
                        video.Display();
                        break;
                    case "4":
                        Console.WriteLine("\nEnter the name of the Movie, show, or video.\n");
                        var mediaName = Console.ReadLine();
                        
                        if (movie.Search(mediaName) == null)
                        {
                            Console.WriteLine($"There was no result for {mediaName} in Movies");
                        }
                        else
                        {
                            movie.Results(mediaName);
                        }
                        if (show.Search(mediaName) == null)
                        {
                            Console.WriteLine($"There was no result for {mediaName} in Shows");
                        }
                        else
                        {
                            show.Results(mediaName);
                        }
                        
                        if (video.Search(mediaName) == null)
                        {
                            Console.WriteLine($"There was no result for {mediaName} in Videos");
                        }
                        else
                        {
                            video.Results(mediaName);
                        }
                        break;
                        
                }
                Menu();
                input = Console.ReadLine();
            }
                
        }
    }
}