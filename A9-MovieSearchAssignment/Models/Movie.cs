using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A9_MovieSearchAssignment.Models
{
    public class Movie : Media
    {
        public List<Movie> movies { get; set; }= new List<Movie>();

        private int _MovieId { get; set; }
        private string Title { get; set; } 
        private string _MovieGenre { get; set;}

        public Movie()
        {Read();}
        private Movie(int movieId, string title, string movieGenre)
        {
            _MovieId = movieId;
            Title = title;
            _MovieGenre = movieGenre;
        }
        public override void Read()
        {
            try
            {
                StreamReader sr = new StreamReader(@"csvFolder/movies.csv");
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
                            Title = (movieDetails[1]);
                            _MovieGenre = (movieDetails[2].Replace("|", ", "));
                            movies.Add(new Movie(_MovieId,Title,_MovieGenre));
                        }
                        else
                        {
                            _MovieId = (int.Parse(line.Substring(0, idx - 1)));
                            line = line.Substring(idx + 1);
                            idx = line.IndexOf('"');
                            Title = (line.Substring(0, idx));
                            line = line.Substring(idx + 2);
                            _MovieGenre = (line.Replace("|", ", "));
                            movies.Add(new Movie(_MovieId,Title,_MovieGenre));
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
                Console.WriteLine($"Title: {movie.Title}");
                Console.WriteLine($"Genre(s): {movie._MovieGenre}");
                Console.WriteLine(); 
            }
        }

        public override Media Search(string mediaName)
        {
            return movies.FirstOrDefault(m => m.Title.Contains(mediaName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}