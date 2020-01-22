using System; 
using basic_api_collection.Persistence;
using System.Collections.Generic;

// Bussines Object for Collection
namespace basic_api_collection.Models
{
    public class CollectionBO {
        private Collection coll;

        public CollectionBO(Collection coll) {
            this.coll = coll;
        } 

        public CollectionBO() {}

        public bool Valid() {
            if (string.IsNullOrEmpty(this.coll.getKey())) {
                return false;
            }
            if (this.coll.getSubIndex() < 0) {
                return false;
            } 
            if (string.IsNullOrEmpty(this.coll.getValue())) {
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
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(this.coll.getKey(), out subDictionary)) {

                    int subIndex = getSubIndexIndice(subDictionary, this.coll.getValue());
                    if (subIndex != 0) {
                        if (!removeAtSubIndex(subIndex, ref subDictionary)) {
                            return false;
                        }
                    } 

                    addOrderedSubDictionary(ref subDictionary);
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
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(key, out subDictionary)) {
                   
                    int count = 0;
                    if (start < 0) {
                        start = 0;
                    }

                    var valuesSub = countValues(subDictionary);
                    if (end < 0) {
                        end = end + valuesSub;
                    }

                    foreach (var keyValuePair in subDictionary) {    
                        foreach (var value in keyValuePair.Value) {
                            if (count >= start && count <= end) {
                                    list.Add(value);
                                }
                            count++;
                        }
                    }
                }
            }

            return list;
        }

        public bool Remove(string key) {
            IDictionary<string, SortedDictionary<int, List<string>>> collections = getCollections();
            return collections.Remove(key);
        }

        public bool RemoveValuesFromSubIndex(string key, int subIndex) {
            IDictionary<string, SortedDictionary<int, List<string>>> collections = getCollections();
            if (collections.ContainsKey(key)) {
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(key, out subDictionary)) {
                    return subDictionary.Remove(subIndex);
                }
            }
            return false;
        }

        public long IndexOf(string key, string value) {
            int count = 0;
            IDictionary<string, SortedDictionary<int, List<string>>> collections = getCollections();
            if (collections.ContainsKey(key)) {
                SortedDictionary<int, List<string>> subDictionary; 
                if (collections.TryGetValue(key, out subDictionary)) {
                    foreach (var keyValuePair in subDictionary) {    
                        foreach (var set in keyValuePair.Value) {
                            if (set.Equals(value)) {
                                return count;
                            }
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private IDictionary<string, SortedDictionary<int, List<string>>> getCollections() {
             CollectionPersistence persistence = CollectionPersistence.getInstance(); 
             return persistence.getCollections();
        }

        private int countValues(SortedDictionary<int, List<string>> subDictionary) {
            int count = 0;
            foreach (var keyValuePair in subDictionary) {    
                count += keyValuePair.Value.Count;       
            }
            return count;
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