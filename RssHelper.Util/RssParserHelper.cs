using Microsoft.Toolkit.Parsers.Rss;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssHelper.Util
{
    /// <summary>
    /// Get a parsed RSS feed
    /// </summary>
    public static class RssParserHelper
    {
        /// <summary>
        /// Parses the RSS Feed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Feed>> GetParsedFeed(string url)
        {
            return await ParseRss(url);

        }

        /// <summary>
        /// Parses the RSS Feed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Feed>> GetParsedFeed(IEnumerable<string> urls)
        {
            var feedList = new List<Feed>();

            foreach (var site in urls)
            {
                feedList.AddRange(await ParseRss(site));
            }


            return feedList;

        }

        /// <summary>
        /// Gets the feed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<IEnumerable<Feed>> ParseRss(string url)
        {
            string feed = null;
            var feedList = new List<Feed>();

            try
            {
                using (var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                }))
                {

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    // Get the XML
                    var res = await client.GetAsync(url);

                    if (res.IsSuccessStatusCode)
                    {
                        feed = await res.Content.ReadAsStringAsync();

                        feed = feed.Replace("\r\n", string.Empty).Trim();
                        // Check XML is Valid
                        if (IsValidXml(feed))
                        {

                            if (feed != null)
                            {

                                var parser = new RssParser();
                                var rss = parser.Parse(feed);

                                foreach (var element in rss)
                                {

                                    feedList.Add(new Feed()
                                    {
                                        Author = element.Author,
                                        Categories = element.Categories,
                                        Content = element.Content,
                                        ExtraImageUrl = element.ExtraImageUrl,
                                        FeedUrl = element.FeedUrl,
                                        ImageUrl = element.ImageUrl,
                                        InternalID = element.InternalID,
                                        MediaUrl = element.MediaUrl,
                                        PublishDate = element.PublishDate,
                                        Summary = element.Summary,
                                        Title = element.Title

                                    });

                                }

                            }

                        }

                    }

                }

                return feedList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Checks the XML is Valid
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static bool IsValidXml(string xml)
        {
            try
            {
                XDocument.Parse(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
