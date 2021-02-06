[![NuGet version (RssParserHelper.NetCore)](https://img.shields.io/badge/nuget-RssParserHelper.NetCore-blue)](https://www.nuget.org/packages/RssParserHelper.NetCore)

# RssParserHelper.NetCore
A simple library that will parse either a url or list of urls and return you an object of type Feed.

## To Use:

You can just pass in a single url

```cs 
string url = "https://www.feedtoparse.com/feed";

var res = await RssParserHelper.GetParsedFeed(url);

```

Or pass in an ``` IEnumerable<string> ``` of the url's

```cs 

var urlList = new List<string> 
  {
    "https://www.feedtoparse1.com/feed",
    "https://www.feedtoparse2.com/feed",
    "https://www.feedtoparse3.com/feed",
  };
  
var res = await RssParserHelper.GetParsedFeed(urlList);

```

These will return Type ``` List<Feed> ```

Then process however you want, simple! 😊
