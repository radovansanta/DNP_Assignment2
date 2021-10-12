using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> Get()
        {
            try
            {
                IList<Adult> adults = _fileContext.Adults.ToList();
                Console.Out.Write(_fileContext.Adults.ToList());
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

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            try
            {
                _fileContext.AddAdult(adult);
                return Created($"/{adult}", adult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete]
        [Route("/Delete/{id}")]
        public void Delete(int id)	
        {
            _fileContext.DeleteAdult(_fileContext.SearchAdult("id",id.ToString())[0]);
        }
        
        [HttpPut]
        [Route("/Update")]
        public void Edit([FromQuery]int id, [FromBody]Adult updatedAdult)	
        {
            _fileContext.UpdateAdult(id,updatedAdult);
        }
        
        
    }
}