using Library.Shared;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Books : ControllerBase
    {
        readonly ICosmosDbService<Book> _service;

        public Books(ICosmosDbService<Book> cosmosDbService)
        {
            _service = cosmosDbService;
        }

        // GET: api/<Books>
        [HttpGet]
        public async Task <IEnumerable<Book>> Get()
        {
            return await _service.GetItemsAsync("SELECT * FROM c");
        }

        // GET api/<Books>/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _service.GetItemAsync(id.ToString());
        }

        // POST api/<Books>
        [HttpPost]
        public async Task<bool> Post([FromBody] Book item)
        {
            return await _service.AddItemAsync(item);
        }

        // PUT api/<Books>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Book item)
        {
            return await _service.UpdateItemAsync(id.ToString(), item);
        }

        // DELETE api/<Books>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteItemAsync(id.ToString());
        }
    }
}
