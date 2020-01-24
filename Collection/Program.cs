using System;
using basic_api_collection.Controllers;

namespace basic_api_collection {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var collection = NewCollection();
            collection.Add("ano.nascimento", 1980, "pedro");
            collection.Add("ano.nascimento", 1980, "maria");
            collection.Add("ano.nascimento", 1980, "joao");
            collection.Add("ano.nascimento", 1975, "rodrigo");

            var nascimentos = collection.Get("ano.nascimento", 0, -1);
            Console.WriteLine("Deveria ter 4 elementos: " + nascimentos.Count);
            Console.WriteLine("Deveria ser o elemento 'rodrigo':" + nascimentos[0]);
            Console.WriteLine("Deveria ser o elemento 'joao':" + nascimentos[1]);
            Console.WriteLine("Deveria ser o elemento 'maria':" + nascimentos[2]);
            Console.WriteLine("Deveria ser o elemento 'pedro':" + nascimentos[3]);


            collection.Add("chave", 1, "c");
            collection.Add("chave", 1, "b");
            collection.Add("chave", 1, "a");

            var list = collection.Get("chave", 0, 0);
            Console.WriteLine("Deveria ter 1 elementos: " + list.Count);
            Console.WriteLine("Deveria ser o elemento 'a': " + list[0]);
            
            list = collection.Get("chave", 0, -2);
            Console.WriteLine("Deveria ter 2 elementos: " + list.Count);
            Console.WriteLine("Deveria ser o elemento 'a': " + list[0]);
            Console.WriteLine("Deveria ser o elemento 'b': " + list[1]);

            collection.Add("chave", 0, "x");
            list = collection.Get("chave", 0, 0);
            Console.WriteLine("Deveria ter 1 elementos: " + list.Count);
            Console.WriteLine("Deveria ser o elemento 'x': " + list[0]);
        }
        
        public static ICollectionsController NewCollection() {
            return new CollectionsController();
        }
    }

}
