using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;


namespace A9_MovieSearchAssignment.Models
{
    public class Movie : Media
    {
        public List<Movie> movies { get; set; }= new List<Movie>();

        private int _MovieId { get; set; }
        private string _MovieTitle { get; set; } 
        private string _MovieGenre { get; set;}

        public Movie()
        {}
        private Movie(int movieId, string movieTitle, string movieGenre)
        {
            _MovieId = movieId;
            _MovieTitle = movieTitle;
            _MovieGenre = movieGenre;
        }

        public override void Read()
        {
            try
            {
                //I have no idea why my streamreader won't work when I just use csvFolder\movies.csv like I did on A6.
                //So this is what I had to do for it to work on my home computer.
                StreamReader sr = new StreamReader(@$"C:\Users\Carls\Documents\Jake School\Fall 2022 (2)\.Net Database Programming\Module9\A9-MovieSearchAssignment\A9-MovieSearchAssignment\csvFolder\movies.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            string[] movieDetails = line.Split(',');
                            _MovieId = (int.Parse(movieDetails[0]));
                            _MovieTitle = (movieDetails[1]);
                            _MovieGenre = (movieDetails[2].Replace("|", ", "));
                            movies.Add(new Movie(_MovieId, _MovieTitle, _MovieGenre));
                        }
                        else
                        {
                            _MovieId = (int.Parse(line.Substring(0, idx - 1)));
                            line = line.Substring(idx + 1);
                            idx = line.IndexOf('"');
                            _MovieTitle = (line.Substring(0, idx));
                            line = line.Substring(idx + 2);
                            _MovieGenre = (line.Replace("|", ", "));
                            movies.Add(new Movie(_MovieId, _MovieTitle, _MovieGenre));
                        }
                    }
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("File not found");
            }
        }

        public override void Display()
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"Id: {movie._MovieId}");
                Console.WriteLine($"Title: {movie._MovieTitle}");
                Console.WriteLine($"Genre(s): {movie._MovieGenre}");
                Console.WriteLine(); 
            }
        }
        public Movie Search(string mediaName)
        {
            return movies.FirstOrDefault(m => m._MovieTitle.Contains(mediaName, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Results(string mediaName)
        {
            IEnumerable<Movie> movieResults = new List<Movie>();
            movieResults = movies.FindAll(m => m._MovieTitle.ToLower().Contains(mediaName.ToLower())).ToList();
            int count = 0;
            foreach (var m in movieResults)
            {
                Console.WriteLine($"\nId: {m._MovieId}");
                Console.WriteLine($"Title: {m._MovieTitle}");
                Console.WriteLine($"Genre(s): {m._MovieGenre}\n");
                count++;
            }

            Console.Write(count > 1 ? $"You found {count} matches in Movies\n" : $"You found {count} match in Movies\n");
        }
    }
}