using System.Collections.Generic;
using basic_api_collection.Models;

namespace basic_api_collection.Controllers
{
    public class CollectionsController : ICollectionsController
    {   
        public CollectionsController()
        {
            
        }
        private CollectionBO Bo;

        public bool Add(string key, int subIndex, string value) {
            this.Bo = new CollectionBO(new Collection(key, subIndex, value));
            return this.Bo.Add();
        }

        public IList<string> Get(string key, int start, int end) {
            this.Bo = new CollectionBO();
            return this.Bo.Get(key, start, end);
        }

        public  bool Remove(string key) {
            this.Bo = new CollectionBO();
            return this.Bo.Remove(key);
        }

        public bool RemoveValuesFromSubIndex(string key, int subIndex) {
            return true;
        }

        public long IndexOf(string key, string value) {
            return 1;
        }
    }
}