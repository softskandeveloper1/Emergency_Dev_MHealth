using DAL.Models;
using DAL.Utils;
using MHealth.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Helper
{
    public class PayStackHelper
    {
        private readonly PayStackSettings _payStackSettings;
        public PayStackHelper(PayStackSettings payStackSettings)
        {
            _payStackSettings = payStackSettings;
        }

        public bool Refund(string transactioRefId)
        {
            try
            {
                if (!string.IsNullOrEmpty(transactioRefId))
                {
                    var client = new RestClient($"{_payStackSettings.APIBaseUrl}refund")
                    {
                        Timeout = -1
                    };
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", $"Bearer {_payStackSettings.SecretKey}");
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("transaction", transactioRefId);
                    IRestResponse response = client.Execute(request);
                    var result = JObject.Parse(response.Content);
                    return Convert.ToBoolean(result["status"]);
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
