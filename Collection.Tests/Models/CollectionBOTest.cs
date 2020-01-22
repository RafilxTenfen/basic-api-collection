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


    }
}
