using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDownLoad();
        }


        public static void TestDownLoad()
        {
            string clientId = "69302";
            string clientSecret = "sH8bMrCuMRkvkwV-Gwxj2FE1z6Ak202X2cEEBVvESM8";
            string content = "&grant_type=client_credentials";
            string fileName = "AGRI199303000";
            string discNo = "CCDA06S1";
            string resourceType = "期刊";
            string fileType = "caj";

            HttpClient client = new HttpClient();
            //文件大小
            string fileLength = "";
            string callbackUri = "";
            long fl = long.Parse(fileLength);
            long per = 1024 * 80;
            int i = 0;

            byte[] data = new byte[fl];

            //下载
            while (true)
            {
                using (HttpRequestMessage requestRegist = new HttpRequestMessage(HttpMethod.Get, callbackUri))
                {
                    long from = i * per;
                    if (from > fl)
                    {
                        break;
                    }
                    long to = (i + 1) * per;
                    if (to >= fl)
                    {
                        to = fl;
                    }
                    requestRegist.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "token");
                    requestRegist.Headers.Add("User-Agent", "ReaderEx2.1");
                    requestRegist.Headers.Range = new RangeHeaderValue(from, to - 1);
                    HttpResponseMessage respone = client.SendAsync(requestRegist).Result;
                    var t = respone.Content.ReadAsStreamAsync();                    
                    t.Wait();
                    //t.Result.Read(data, from, to - 1);

                    i++;
                }
            }
        }

    }
}
