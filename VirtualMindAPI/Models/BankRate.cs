using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace VirtualMindAPI.Models
{
  public class BankRate : IBankRate
  {
    public ExchangeRate GetRatebyCode(string isoCode)
    {
      if(!(isoCode == "BRL" || isoCode =="USD"))
      {
        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent(string.Format("Invalid IsoCode {0}", isoCode), Encoding.UTF8, "application/json"),
          ReasonPhrase = "Valid IsoCodes: [BRL] [USD]."
        };
        throw new HttpResponseException(resp);

      }
      var task = GetExchangeRateFromAPIAsync();
      var result = task.Result;
      result.IsoCode = isoCode;

      if(isoCode == "BRL")
      {

        result.SellRate = Math.Round(result.SellRate * 0.25m, 3); 
        result.BuyRate = Math.Round(result.BuyRate * 0.25m, 3);
      }
      //ExchangeRate m = new ExchangeRate()
      //{ BuyRate = 0.1m, IsoCode = isoCode, SellRate = 0.2m };
      //return m;
      return result;
    }
    private async Task<ExchangeRate> GetExchangeRateFromAPIAsync()
    {
      var provider = new CultureInfo("en-US");
      using (var httpClient = new HttpClient())
      {
        using (var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar"))
        {
          string apiResponse = await response.Content.ReadAsStringAsync();
          //var model = JsonConvert.DeserializeObject<ProvinciaDolar>(apiResponse);
          var model = JsonConvert.DeserializeObject<string[]>(apiResponse);

          ExchangeRate returnModel = new ExchangeRate() { BuyRate = Decimal.Parse(model[0], provider), SellRate = Decimal.Parse(model[1], provider) };
          return returnModel;
        }
      }
    }
  }
}
