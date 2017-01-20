using System;
using System.Text.RegularExpressions;
using System.Net.Http;
using BashParserCore.Models;
using BashParserCore.Data;
using System.Text;

namespace task_2
{
    class Program
    {

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://bash.im";
            HttpClient client = new HttpClient();
            string data = client.GetStringAsync(url).Result;

            Console.Write(data);

            string datePattern = @"\w?=.?date.{2}(?<date>.+)</span>";
            string titlePattern = @"title:\s'(?<title>.+)'";
            string bodyPattern = @"class=.text.{2}(?<text>.+)</div>";
            string ratingPattern = @"class=""rating"".(?<rating>.{0,6})</span>";
            string pattern = @"<div class=""quote"">\n.+\n.+\n?.+\n?</div>\n?.+</div>";

            Regex posts = new Regex(pattern);
            Regex date = new Regex(datePattern);
            Regex title = new Regex(titlePattern);
            Regex body = new Regex(bodyPattern);
            Regex rating = new Regex(ratingPattern);

            MatchCollection matches = posts.Matches(data);

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (Match match in matches)
                {
                    Post post = new Post()
                    {
                        PostName = title.Match(match.ToString()).Groups["title"].Value,
                        Date = date.Match(match.ToString()).Groups["date"].Value,
                        Rating = rating.Match(match.ToString()).Groups["rating"].Value,
                        Text = (body.Match(match.ToString()).Groups["text"].Value).Replace(@"<br>", "\n")
                    };
                    db.Add(post);
                    db.SaveChanges();
                }
            }
        }
    }
}