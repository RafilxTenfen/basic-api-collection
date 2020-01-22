using System; 
using System.Linq; 
using basic_api_collection.Persistence;
using System.Collections.Generic;
using System.Collections; 

// Bussines Object for Collection
namespace basic_api_collection.Models
{
    public class CollectionBO 
    {
        private Collection coll;

        // Dependency Injection
        public CollectionBO(Collection coll)
        {
            this.coll = coll;
        } 

        public CollectionBO()
        {
            
        }

        public bool Valid() {
            if (string.IsNullOrEmpty(this.coll.getKey())) {
                return false;
            }
            if (this.coll.getSubIndex() <= 0) {
                return false;
            } 
            if (this.coll.getValue().Length <= 0) {
                return false;
            }
            return true;
        }

        public bool Add() {
            if (!this.Valid()) {
                return false;
            }

            IDictionary<string, SortedDictionary<int, List<string>>> collections = getCollections();

            if (collections.ContainsKey(this.coll.getKey())) {
                // contains key 
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(this.coll.getKey(), out subDictionary)) {

                    int subIndex = getSubIndexIndice(subDictionary, this.coll.getValue());
                    if (subIndex != 0) {
                        if (!removeAtSubIndex(subIndex, ref subDictionary)) {
                            return false;
                        }
                    } 

                    addOrderedSubDictionary(ref subDictionary);
                    // subDictionary = subDictionary.OrderBy(key => key.Key);
            
                    return true;
                }
            }

            collections.Add(this.coll.getKey(), new SortedDictionary<int, List<string>>(){
                {this.coll.getSubIndex(), new List<string>(){this.coll.getValue()}}
            });
            return true;
        }

        public IList<string> Get(string key, int start, int end) {
            IDictionary<string, SortedDictionary<int, List<string>>> collections = getCollections();
            IList<string> list = new List<string>();

            if (collections.ContainsKey(key)) {
                // contains key 
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(key, out subDictionary)) {
                   int count = 0;
                   if (start < 0) {
                       start = 0;
                   }
                   if (end < 0) {
                       end =+ subDictionary.Count;
                   }
                   foreach (var keyValuePair in subDictionary)
                   {
                       if (count >= start && count <= end) {
                           list = list.Concat(keyValuePair.Value).ToList();
                       }
                       count++;
                   }
                }
            }

            return list;
        }

        private IDictionary<string, SortedDictionary<int, List<string>>> getCollections() {
             CollectionPersistence persistence = CollectionPersistence.getInstance(); 
             return persistence.getCollections();
        }

        // getSubIndexIndice returns the subindex of some set, if didn't exists return 0
        private int getSubIndexIndice(SortedDictionary<int, List<string>> subDictionary, string set) {
            foreach (var keyValuePair in subDictionary)
            {
                if (keyValuePair.Value.BinarySearch(set) >= 0) {
                     return keyValuePair.Key;
                }
            }
            return 0;
        }

        private void addOrderedSubDictionary(ref SortedDictionary<int, List<string>> subDictionary) {
            if (subDictionary.ContainsKey(this.coll.getSubIndex())) {
                List<string> values;
                if (subDictionary.TryGetValue(this.coll.getSubIndex(), out values)) {
                    values.Add(this.coll.getValue());
                    values.Sort((x,y) => String.Compare(x, y));
                    return;
                }
            }
            subDictionary.Add(this.coll.getSubIndex(), new List<string>{this.coll.getValue()});
        }

        private bool removeAtSubIndex(int subIndex, ref SortedDictionary<int, List<string>> subDictionary) {
            if (subDictionary.ContainsKey(subIndex)) {
                List<string> values;
                if (subDictionary.TryGetValue(subIndex, out values)) {
                    return values.Remove(this.coll.getValue());
                }
            } 
            return false;
        }

        private int indexOf(SortedDictionary<int, string[]> subDictionary, string[] value) {
            foreach (var keyValuePair in subDictionary) {
                if (keyValuePair.Value.Equals(value)) {
                    return keyValuePair.Key;
                }
            }
            return 0;
        }

    }
}