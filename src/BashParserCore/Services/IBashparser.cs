using BashParserCore.Data;
using BashParserCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BashParserCore.Services
{
    interface IBashparser
    {
        void parseBash();
    }

    public class BashParser : IBashparser
    {
        public void parseBash()
        {
            string datePattern = @"\w?=.?date.{2}(?<date>.+)</span>";
            string titlePattern = @"title:\s'(?<title>.+)'";
            string bodyPattern = @"class=.text.{2}(?<text>.+)</div>";
            string ratingPattern = @"class=""rating"".(?<rating>.{0,6})</span>";
            string pattern = @"<div class=""quote"">\n.+\n.+\n?.+\n?</div>\n?.+</div>";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string url = "http://bash.im";
            HttpClient client = new HttpClient();
            string data = client.GetStringAsync(url).Result;

            Console.Write("refd");

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
                        Text = (body.Match(match.ToString()).Groups["text"].Value).Replace(@"<br>", "\n"),
                        Author = db.Users.Find("6bc287b7-cc63-4ef3-b651-483d009ae27f")
                    };
                    db.Add(post);
                    db.SaveChanges();
                }
            }
        }
    }
}
