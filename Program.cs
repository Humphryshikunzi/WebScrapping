using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Linq;
using System.Text.RegularExpressions;

namespace eBayScrap
{
    class Program
    {
        static void Main(string[] args)
        {
            ScrapNet();
        }
        public static  void ScrapNet()
        {
            var eBayurl = "https://www.ebay.com/sch/i.html?_nkw=xbox+one&_in_kw=1&_ex_kw=&_sacat=0&_udlo=&_udhi=&_ftrt=901&_ftrv=1&_sabdlo=&_sabdhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=1&_sop=12&_dmd=1&_ipg=200&_fosrp=1";
            var amazonurl = "https://www.amazon.com/YETI-Rambler-Stainless-Vacuum-Insulated/dp/B074W99X7X/ref=zg_bs_sporting-goods_2?_encoding=UTF8&psc=1&refRID=QV81EGGXKJ5KM3F56GHC";
            var alibaba = "https://sale.alibaba.com/pages/globalmanufacturinghub/index.html?spm=a2700.8293689.categoryInfoIndustry-1.2.42ec67afSs7WLp&deliveryId=400001_90400010_STOCK_9_66941111&topOfferIds=60822590026&tracelog=from_home_category";
            var safcom = "https://www.safaricom.co.ke/";
            var microsoft = "https://www.microsoft.com/en-us/";
            var httpclient = new HttpClient();
            var kenyapower = "https://www.kplc.co.ke/content/item/3098/industrial-attachment-opportunities-september-%E2%80%93-november-2019-intake";
            var shiks = "https://shiks.azurewebsites.net/";
            var html =  httpclient.GetStringAsync(eBayurl);

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
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r' , '\n' , 't'));
            }
            // writeline all prices
            Console.WriteLine("\n\n");
            foreach (var item in productlistitem)
            {
                var prices= item.Descendants("li")
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
