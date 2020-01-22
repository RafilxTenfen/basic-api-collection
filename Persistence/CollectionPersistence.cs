using basic_api_collection.Models;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections; 

namespace basic_api_collection.Persistence 
{
    // simulates database

    public class CollectionPersistence
    {   
        
        public IDictionary<string, SortedDictionary<int, List<string>>> collections;

        // Instance for singleton
        private static volatile CollectionPersistence Instance;

        
        private CollectionPersistence()
        {
            this.collections = new Dictionary<string, SortedDictionary<int, List<string>>>();
        }

        public static CollectionPersistence getInstance() 
        {
            if (Instance == null) 
            {
                Instance = new CollectionPersistence();
            } 
            return Instance;
        }

        public IDictionary<string, SortedDictionary<int, List<string>>> getCollections() {
            return this.collections;
        }
    }


}