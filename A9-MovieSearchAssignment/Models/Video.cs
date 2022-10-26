using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A9_MovieSearchAssignment.Models
{
    public class Video : Media
    {
        private List<Video> videos = new List<Video>();
        private int _VideoId { get; set; } 
        private string _VideoTitle { get; set; }   
        private string _VideoFormat { get; set; }   
        private int _VideoLength { get; set; }  
        private string _VideoRegions { get; set; }

        public Video() {}

        private Video(int videoId, string videoTitle, string videoFormat, int videoLength, string videoRegions)
        {
            _VideoId = videoId;
            _VideoTitle = videoTitle;
            _VideoFormat = videoFormat;
            _VideoLength = videoLength;
            _VideoRegions = videoRegions;
        }

        public override void Read()
        {
            try
            {
                StreamReader sr = new StreamReader(@"C:\Users\Carls\Documents\Jake School\Fall 2022 (2)\.Net Database Programming\Module9\A9-MovieSearchAssignment\A9-MovieSearchAssignment\csvFolder\videos.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        string[] videoDetails = line.Split(',');
                            _VideoId = (int.Parse(videoDetails[0]));
                            _VideoTitle = (videoDetails[1]);
                            _VideoFormat = (videoDetails[2]);
                            _VideoLength = (int.Parse(videoDetails[3]));
                            _VideoRegions = line.Substring(idx);
                            videos.Add(new Video(_VideoId,_VideoTitle,_VideoFormat,_VideoLength,_VideoRegions));
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
            foreach (var video in videos)
            {
                Console.WriteLine($"Id: {video._VideoId}");
                Console.WriteLine($"Title: {video._VideoTitle}");
                Console.WriteLine($"Video Format: {video._VideoFormat}");
                Console.WriteLine($"Video Length: {video._VideoLength}");
                Console.WriteLine($"Video Region: {video._VideoRegions}");
                Console.WriteLine();
            }
        }

        public Video Search(string mediaName)
        {
            return videos.FirstOrDefault(m => m._VideoTitle.Contains(mediaName, StringComparison.CurrentCultureIgnoreCase));
        }
        public void Results(string mediaName)
        {
            IEnumerable<Video> videoResults = new List<Video>();
            videoResults = videos.FindAll(m => m._VideoTitle.ToLower().Contains(mediaName.ToLower())).ToList();
            int count = 0;
            foreach (var m in videoResults)
            {
                Console.WriteLine($"\nId: {m._VideoId}");
                Console.WriteLine($"Title: {m._VideoTitle}");
                Console.WriteLine($"Video Format: {m._VideoFormat}");
                Console.WriteLine($"Video Length: {m._VideoLength}");
                Console.WriteLine($"Video Region: {m._VideoRegions}\n");
                count++;
            }

            Console.Write(count > 1 ? $"You found {count} matches in Videos\n" : $"You found {count} match in Videos\n");
        }
    }
}