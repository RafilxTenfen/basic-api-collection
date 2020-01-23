using basic_api_collection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace basic_api_collection.Controllers
{
    public class CollectionsControllerApi : ControllerBase
    {   
        private readonly ILogger<CollectionsControllerApi> _logger;
        private static List<Collection> collections = new List<Collection>();
        public CollectionsControllerApi(ILogger<CollectionsControllerApi> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Collection> Get()
        {
            return collections;
        }

        public void Post([FromBody]string key, int subIndex, string[]set)
        {       
            Collection coll = new Collection(key, subIndex, set);

            collections.Add(coll);
        }
    }
}