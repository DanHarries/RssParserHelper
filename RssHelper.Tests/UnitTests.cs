using NUnit.Framework;
using RssHelper.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssHelper.Tests
{
    public class Tests
    {
        private static readonly object[] _sourceLists =
        {
            //case 1
            new object[] 
            { new List<string> 
                { 
                    "http://feeds.skynews.com/feeds/rss/world.xml",
                    "https://www.theguardian.com/uk/rss",
                    "https://www.dailymail.co.uk/home/index.rss"
                } 
            }, 
            
             //case 2
            new object[] 
            {
                new List<string> 
                {
                    "https://www.telegraph.co.uk/rss.xml",
                    "http://www.independent.co.uk/rss",
                    "https://www.thesun.co.uk/feed/"
                }
            }
        };
      

        [Test]
        [TestCase("http://feeds.skynews.com/feeds/rss/world.xml")]
        [TestCase("https://www.theguardian.com/uk/rss")]
        [TestCase("https://www.dailymail.co.uk/home/index.rss")]
        public async Task TestSingleUrlToParse(string url)
        {
            var res = await RssParserHelper.GetParsedFeed(url);

            Assert.IsTrue(res.ToList().Count != 0);
        }

        [Test]
        [TestCaseSource("_sourceLists")]
        public async Task TestListOfUrlToParse(IEnumerable<string> url)
        {
            var res = await RssParserHelper.GetParsedFeed(url);

            Assert.IsTrue(res.ToList().Count != 0);
        }
    }
}