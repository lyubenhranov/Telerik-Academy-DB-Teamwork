namespace MusicFactory.Data.XmlDataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using MusicFactory.Data.XmlDataLoader.Contracts;
    using MusicFactory.Models;

    public class XmlLoader : IXmlDataLoader
    {
        private string storesPath;

        private XmlNode root;
        private ICollection<Store> stores;

        public XmlLoader(string filePath)
        {
            this.storesPath = filePath;
            this.stores = new List<Store>();
        }

        public ICollection<Store> LoadStoresData()
        {
            var doc = new XmlDocument();
            doc.Load(storesPath);

            root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                var currentStore = BuildStoreFromXMLInformation(node);
                stores.Add(currentStore);
            }

            return stores;
        }

        private Store BuildStoreFromXMLInformation(XmlNode node)
        {
            var store = new Store()
            {
                Name = node["Name"].InnerText,
                Owner = node["Owner"].InnerText,
                Address = new Address()
                {
                    AddressText = node["AddressText"].InnerText,
                    Country = new Country()
                    {
                        Name = node["CountryName"].InnerText
                    }
                }
            };

            return store;
        }
    }
}
