using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogApi.Data;
using ProductCatalogApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        //There are multiple ways to accept the Input from the user
        //1. from uri- this we have did for piccontroller
        //2. from query- we are doing this now
        //3. from body
        //Async Runs in a separate thread, and once it is done, the thread returns the task.
        //We have the dependency here with the CatalogContext class(EntityFramework class).So we are injecting 
        public readonly CatalogContext _context;//global variable
        public readonly IConfiguration _config;//global variable
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("[action]")]//action is nothing but another name for method.
        public async Task<IActionResult> Items([FromQuery]int pageIndex=0, [FromQuery]int pageSize = 6)
        {
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var items = await _context.Catalog.OrderBy(c => c.Name).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(item =>
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]));
            return items;
        }

        public async Task<IActionResult> CatalogTypes()
        {
            var types = await _context.CatalogTypes.ToListAsync();
            return Ok(types);
        }

        public async Task<IActionResult> CatalogBrands()
        {
            var types = await _context.CatalogBrands.ToListAsync();
            return Ok(types);
        }


        //By keeping Question mark beside valuetype(int), we are making valuetype as nullable.
        [HttpGet("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]//action is nothing but another name for method.
        public async Task<IActionResult> Items(
            int? catalogTypeId, 
            int? catalogBrandId, 
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<CatalogItem>)_context.Catalog;
            //By default, the ablove line means selct * from catalog.
            //Anytime we need to query the database table, we need to do it through Entity framework.
            if (catalogTypeId.HasValue)
            {
                query = query.Where(c => c.CatalogTypeId == catalogTypeId);
            }
            if (catalogBrandId.HasValue)
            {
                query = query.Where(c => c.CatalogBrandId == catalogBrandId);
            }
            var items = await query
                .OrderBy(c => c.Name).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
        }
    }
}
