using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Helper
{
    public static class FormHelper
    {
        public static JObject ColletionToJSON(IFormCollection formCollection)
        {
            var jobject = new JObject();
            foreach (var k in formCollection.ToList())
            {
                jobject.Add(k.Key, k.Value.ToString());
            }

            return jobject;
        }

        public static Dictionary<string, string> ColletionToDictionary(IFormCollection formCollection)
        {
            var dicts = new Dictionary<string, string>();
            foreach (var k in formCollection.ToList())
            {
                dicts.Add(k.Key, k.Value.ToString());
            }

            return dicts;
        }
    }
}
