using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMindAPI.Models
{
  public class ExchangeRate
  {
    public string IsoCode { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SellRate { get; set; }
  }
}
