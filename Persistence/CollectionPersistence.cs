using basic_api_collection.Models;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections; 

namespace basic_api_collection.Persistence 
{
    // simulates database

    public class CollectionPersistence
    {   
        // private IList<Collection> collections; 
        
        public IDictionary<string, Dictionary<int, List<string>>> collections;
        //sortList<string> copy = new List<string>(array); copy.Sort();

        // Instance for singleton
        private static volatile CollectionPersistence Instance;

        
        private CollectionPersistence()
        {
            // this.collections = new List<Collection>();
            // Dictionary subDictionary = new Dictionary<int, string[]>();
            this.collections = new Dictionary<string, Dictionary<int, List<string>>>();
        }

        public static CollectionPersistence getInstance() 
        {
            if (Instance == null) 
            {
                Instance = new CollectionPersistence();
            } 
            return Instance;
        }

        public IDictionary<string, Dictionary<int, List<string>>> getCollections() {
            return this.collections;
        }
    }


}