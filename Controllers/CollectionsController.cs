using System.Collections.Generic;
using basic_api_collection.Models;

namespace basic_api_collection.Controllers
{
    public class CollectionsController : ICollectionsController
    {
        private CollectionBO Bo;

        public bool Add(string key, int subIndex, string value) {
            // string[] values = new string[]{value};
            this.Bo = new CollectionBO(new Collection(key, subIndex, value));
            return this.Bo.Add();
        }

        public IList<string> Get(string key, int start, int end) {
            IList<string> allFaqs = new List<string>();
            return allFaqs;
        }

        public  bool Remove(string key) {
            return true;
        }

        public bool RemoveValuesFromSubIndex(string key, int subIndex) {
            return true;
        }

        public long IndexOf(string key, string value) {
            return 1;
        }
    }
}