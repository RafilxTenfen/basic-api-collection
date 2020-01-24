
namespace basic_api_collection.Models {
    public class Collection {
        private string key;
        private int subIndex;
        private string value;

        public Collection(string key, int subIndex, string value) {
            this.key = key;
            this.subIndex = subIndex;
            this.value = value;
        }

        public string getKey() {
            return this.key;
        }

        public void setKey(string key) {
            this.key = key;
        }
        public int getSubIndex() {
            return this.subIndex;
        }

        public void setSubIndex(int subIndex) {
            this.subIndex = subIndex;
        }

        public string getValue() {
            return this.value;
        }

        public void setValue(string value) {
            this.value = value;
        }
    }
}
