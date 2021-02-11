using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utils
{
    public class HttpUtil
    {
        public async Task<string> PostMessage(string url, string jsonMsg)
        {
            HttpClient ApiClient = new HttpClient();
            StringContent stringContent = new StringContent(jsonMsg, Encoding.UTF8, "application/json");
            var response = await ApiClient.PostAsync(url, stringContent);
            return await response.Content.ReadAsStringAsync();
        }

        public string PostMessageCurl(string url,  string postData)
        {
          

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.  
            request.Method = "POST";

            //CredentialCache mycache = new CredentialCache();
            //request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));



            // Create POST data and convert it to a byte array.  
            //"This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.Headers["accept"] = "application/json";
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            string responseFromServer = "";

            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                responseFromServer = reader.ReadToEnd();
                // Display the content.  
                //Console.WriteLine(responseFromServer);
            }

            // Close the response.  
            response.Close();

            return responseFromServer;
        }
    }
}
