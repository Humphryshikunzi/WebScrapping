using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace WebScrapeBayAmazon
{
    class eBay
    {
        public static void ScrapNet()
        {
            var eBayurl = "https://www.ebay.com/sch/i.html?_nkw=xbox+one&_in_kw=1&_ex_kw=&_sacat=0&_udlo=&_udhi=&_ftrt=901&_ftrv=1&_sabdlo=&_sabdhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=1&_sop=12&_dmd=1&_ipg=200&_fosrp=1";
            var httpclient = new HttpClient();
            var html = httpclient.GetStringAsync(eBayurl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html.Result);
            var products = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("ListViewInner")).ToList();
            var productlistitem = products[0].Descendants("li")
               .Where(node => node.GetAttributeValue("id", "")
               .Contains("item")).ToList();
            Console.WriteLine(productlistitem.Count());
            Console.WriteLine("Item Ids");
            foreach (var item in productlistitem)
            {
                Console.WriteLine(item.GetAttributeValue("listingid", ""));
            }
            Console.WriteLine("Item Textx");

            //witeline all names for the products
            Console.WriteLine("\n\n");
            foreach (var item in productlistitem)
            {
                Console.WriteLine(item.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', 't'));
            }
            // writeline all prices
            Console.WriteLine("\n\n");
            foreach (var item in productlistitem)
            {
                var prices = item.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', 't');
                var cleanedPrices = Regex.Match(prices, @"\d+.\d");
                Console.WriteLine(cleanedPrices);
            }
            //witeline all links for the products
            Console.WriteLine("\n\n");
            foreach (var item in productlistitem)
            {
                Console.WriteLine(item.Descendants("a").FirstOrDefault().GetAttributeValue("href", ""));

            }
            //lvformat Buy it Now
            Console.WriteLine("\n\n");
            foreach (var item in productlistitem)
            {
                Console.WriteLine(item.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvformat")).FirstOrDefault().InnerText.Trim());
            }

        }
    }
}
