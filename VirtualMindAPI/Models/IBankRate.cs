using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMindAPI.Models
{
  public interface IBankRate
  {
    ExchangeRate GetRatebyCode(string isoCode);
  }
}
