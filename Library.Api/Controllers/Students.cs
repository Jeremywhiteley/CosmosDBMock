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

        // GET: api/<Students>
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _service.GetItemsAsync("SELECT * FROM c");
        }

        // GET api/<Students>/identifier/partition
        [HttpGet("{id}/{partition}")]
        public async Task<Student> Get(string id, string partition = Utils.DEFAULT_PARTITION)
        {
            return await _service.GetItemAsync(id, partition);
        }

        // POST api/<Books>
        [HttpPost]
        public async Task<bool> Post([FromBody] Student item)
        {
            return await _service.AddItemAsync(item);
        }

        // PUT api/<Students>/Identifier
        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] Student item)
        {
            return await _service.UpdateItemAsync(id, item);
        }

        // DELETE api/<Students>/identifier/partition
        [HttpDelete("{id}/{partition}")]
        public async Task<bool> Delete(string id, string partition = Utils.DEFAULT_PARTITION)
        {
            return await _service.DeleteItemAsync(id, partition);
        }
    }
}
