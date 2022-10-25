using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A9_MovieSearchAssignment.Models
{
    public class Show : Media
    {
        private List<Show> shows = new List<Show>();
        private int _ShowId { get; set; }
        private string _ShowTitle { get; set; }    
        private string _ShowEpisode { get; set; }
        private string _ShowSeason { get; set; }    
        private string _ShowWriters { get; set; }   
        

        public Show() {}

        public Show(int showId, string showTitle, string showEpisode, string showSeason, string showWriters)
        {
            _ShowId = showId;
            _ShowTitle = showTitle;
            _ShowEpisode = showEpisode;
            _ShowSeason = showSeason;
            _ShowWriters = showWriters;
        }

        public override void Read()
        {
            try
            {
                StreamReader sr = new StreamReader(@"csvFolder\shows.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        string[] showDetails = line.Split(',');
                        _ShowId = (int.Parse(showDetails[0]));
                        _ShowTitle = (showDetails[1]);
                        _ShowEpisode = (showDetails[2]);
                        _ShowSeason = (showDetails[3]);
                        _ShowWriters = (showDetails[4].Replace("|", ", "));
                        shows.Add(new Show(_ShowId,_ShowTitle,_ShowEpisode,_ShowSeason,_ShowWriters));
                    }
                }
                sr.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found");
            }
        }
        
        public override void Display()
        {
            foreach (var show in shows)
            {
                Console.WriteLine($"Id: {show._ShowId}");
                Console.WriteLine($"Title: {show._ShowTitle}");
                Console.WriteLine($"Episode: {show._ShowEpisode}");
                Console.WriteLine($"Season: {show._ShowSeason}");
                Console.WriteLine($"Writer/s: {show._ShowWriters}");
                Console.WriteLine();
            }
        }

        public override Media Search(string mediaName)
        {
            Read();
            return shows.FirstOrDefault(m => m.Title.Contains(mediaName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}