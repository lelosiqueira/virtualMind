using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMindAPI.Models;

namespace VirtualMindAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  //[ExceptionHandlingAttribute]
  public class BankRateController : ControllerBase
  {
    private IBankRate repository;
    public BankRateController(IBankRate repo) => repository = repo;

    [HttpGet("{isoCode}")]
    
    public ExchangeRate Get(string isoCode) => repository.GetRatebyCode(isoCode);
  }
}
