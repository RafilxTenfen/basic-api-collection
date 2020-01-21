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
    }
}