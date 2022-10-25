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
                StreamReader sr = new StreamReader(@"csvFolder\videos.csv");
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

        public override Media Search(string mediaName)
        {
            Read();
            return videos.FirstOrDefault(m => m.Title.Contains(mediaName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}