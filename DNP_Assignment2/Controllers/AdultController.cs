using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment2.Models;
using DNP_Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DNP_Assignment2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AdultController : Controller
    {
        AdultServices _adultServices = new ();
        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> Get()
        {
            try
            {
                IList<Adult> adults = _adultServices.Adults.ToList();
                Console.Out.Write(_adultServices.Adults.ToList());
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
                Adult adult = _adultServices.Adults.First(x => x.Id==id);
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
                _adultServices.AddAdult(adult);
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
        public async Task<ActionResult> Delete(int id)	
        {
            try
            {
                await _adultServices.DeleteAdult(_adultServices.SearchAdult("id", id.ToString())[0]);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        [Route("/Update/{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody]Adult updatedAdult)	
        {
            try
            {
                await _adultServices.UpdateAdult(id, updatedAdult);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
    }
}