using LibraryApi.Services;
using Library.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly ICosmosDbService<Book> _cosmosDbService;

        public BooksController(ICosmosDbService<Book> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

    }
}
