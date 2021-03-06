﻿// ******************************
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
    public class Books : ControllerBase
    {
        readonly ICosmosService<Book> _service;

        public Books(ICosmosService<Book> cosmosDbService)
        {
            _service = cosmosDbService;
        }

        // GET: api/<Books>
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _service.GetItemsAsync("SELECT * FROM c");
        }

        // GET api/<Books>/ISBN/Partition
        [HttpGet("{id}/{partition}")]
        public async Task<Book> Get(string id, string partition = Utils.DEFAULT_PARTITION)
        {
            return await _service.GetItemAsync(id, partition);
        }

        // POST api/<Books>
        [HttpPost]
        public async Task<bool> Post([FromBody] Book item)
        {
            return await _service.AddItemAsync(item);
        }

        // PUT api/<Books>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] Book item)
        {
            return await _service.UpdateItemAsync(id, item);
        }

        // DELETE api/<Books>/ISBN/partition
        [HttpDelete("{id}")]
        [HttpDelete("{id}/{partition}")]
        public async Task<bool> Delete(string id, string partition = Utils.DEFAULT_PARTITION)
        {
            return await _service.DeleteItemAsync(id, partition);
        }
    }
}
