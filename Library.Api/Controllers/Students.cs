// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Library.Shared;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students : ControllerBase
    {
        readonly ICosmosService<Student> _service;

        public Students(ICosmosService<Student> cosmosDbService)
        {
            _service = cosmosDbService;
        }

        // GET: api/<Books>
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _service.GetItemsAsync("SELECT * FROM c");
        }

        // GET api/<Books>/5
        [HttpGet("{id}")]
        public async Task<Student> Get(string id)
        {
            return await _service.GetItemAsync(id);
        }

        // POST api/<Books>
        [HttpPost]
        public async Task<bool> Post([FromBody] Student item)
        {
            return await _service.AddItemAsync(item);
        }

        // PUT api/<Books>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] Student item)
        {
            return await _service.UpdateItemAsync(id, item);
        }

        // DELETE api/<Books>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _service.DeleteItemAsync(id);
        }
    }
}
