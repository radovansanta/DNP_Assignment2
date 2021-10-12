using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNP_Assignment2.Models;
using DNP_Assignment2.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DNP_Assignment2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AdultController : Controller
    {
        FileContext _fileContext = new ();
        
        private readonly ILogger<AdultController> _logger;

        public AdultController(ILogger<AdultController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> Get()
        {
            try
            {
                IList<Adult> adults = _fileContext.Adults.ToList();
                return Ok(adults);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adult>> Get(int id)
        {
            try
            {
                Adult adult = _fileContext.Adults[id];
                return Ok(adult);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}