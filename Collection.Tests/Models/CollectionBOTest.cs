using System;
using Xunit;
using basic_api_collection.Models;
using System.Collections.Generic;

namespace Collection.Tests.Models {
    public class CollectionBOTest {
        [Fact]
        public void TestValid() {
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection("hu3", 10, "tao");
            CollectionBO bo = new CollectionBO(coll);
            Assert.True(bo.Valid());
        }

        [Fact]
        public void TestInvalidValue() {
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection("hu3", 10, "");
            CollectionBO bo = new CollectionBO(coll);
            Assert.False(bo.Valid());
        }

        [Fact]
        public void TestInvalidSubIndice() {
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection("hu3", -1, "tao");
            CollectionBO bo = new CollectionBO(coll);
            Assert.False(bo.Valid());
        }

        [Fact]
        public void TestInvalidKey() {
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection("", 5, "tao");
            CollectionBO bo = new CollectionBO(coll);
            Assert.False(bo.Valid());
        }

        [Fact]
        public void TestAdd() {   
            string key = "hu33";
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection(key, 0, "tao");
            CollectionBO bo = new CollectionBO(coll);
            
            Assert.True(bo.Add());
            
            var list = bo.Get(key, 0, 0);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void TestAddFalse() {
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection("", 5, "tao");
            CollectionBO bo = new CollectionBO(coll);
            Assert.False(bo.Add());
        }

        [Fact]
        public void TestGet() {
            string key = "hu3";
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection(key, 5, "tao");
            CollectionBO bo = new CollectionBO(coll);
            bo.Add();

            var list = bo.Get(key, 0, 0);
            Assert.Equal(1, list.Count);

            list = bo.Get(key, 0, -1);
            Assert.Equal(1, list.Count);

            list = bo.Get(key, -1, -1);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void TestRemove() {
            string key = "hu3";
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection(key, 5, "tao");
            CollectionBO bo = new CollectionBO(coll);
            bo.Add();

            Assert.True(bo.Remove(key));
            Assert.False(bo.Remove(key + "dasijdasi"));
        }

        [Fact]
        public void TestRemoveValuesFromSubIndex() {
            string key = "hu3";
            int subIndice = 10;
            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection(key, subIndice, "tao");
            CollectionBO bo = new CollectionBO(coll);
            bo.Add();

            Assert.True(bo.RemoveValuesFromSubIndex(key, subIndice));
            Assert.False(bo.RemoveValuesFromSubIndex(key, subIndice + 1));
            Assert.False(bo.RemoveValuesFromSubIndex(key + "hu3", subIndice));
        }

        [Fact]
        public void TestindexOf() {
            string key = "hu31";
            string value = "anyValue";
            string value1 = "anyValue2";

            basic_api_collection.Models.Collection coll = new basic_api_collection.Models.Collection(key, 10, value);
            CollectionBO bo = new CollectionBO(coll);
            bo.Add();
    
            coll = new basic_api_collection.Models.Collection(key, 12, value1);
            bo = new CollectionBO(coll);
            bo.Add();

            Assert.Equal(0, bo.IndexOf(key, value));
            Assert.Equal(1, bo.IndexOf(key, value1));
            Assert.Equal(0, bo.IndexOf(key, "didn't exists"));
        }


    }
}
