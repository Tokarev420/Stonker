using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace Stonker
{
    public class WebReq
    {

        public static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            return reader.ReadToEnd();
        }

        public static void GetPrices()
        {
            
            
        }
    }

    [Serializable]
    public class SearchData
    {
        public string query { get; set; }
        public bool search_descriptions { get; set; }
        public int total_count { get; set; }
        public int pagesize { get; set; }
        public string prefix { get; set; }
        public string class_prefix { get; set; }

        public SearchData() { }
    }

    [Serializable]
    public class Request
    {
        public bool succes { get; set; }
        public int start { get; set; }
        public int pagesize { get; set; }
        public int total_count { get; set; }
        public SearchData searchdata { get; set; }
        public List<Item> results{ get; set; }

    }

    [Serializable]
    public class Item
    {
        public string name { get; set; }
        public string hash_name { get; set; }
        public int sell_listings { get; set; }
        public int sell_price { get; set; }
        public string sell_price_text { get; set; }
        public string app_icon { get; set; }
        public string app_name { get; set; }
        //public Description asset_description { get; set; }
        public string sale_price_text { get; set; }

        public Item() { }
    }

    [Serializable]
    public class Description
    {
        public int appid { get; set; }
        public int classid { get; set; }
        public int instanceid { get; set; }
        public int currency { get; set; }
        public string icon_url { get; set; }
        public string icon_url_large { get; set; }
        public object descriptions { get; set; }
        public int tradable { get; set; }

        public Description() { }
    }
} 
