namespace MusicFactory.Data
{
    using System;
    using System.Linq;

    using XmlDataLoader;
    using MongoDB.Driver;
    using MusicFactory.Models;

    public class XmlDataImporter
    {
        private MusicFactoryDbContext musicFactoryContext;
        private MongoDatabase mongoDatabase;

        public XmlDataImporter(MusicFactoryDbContext musicFactoryContext, MongoDatabase mongoDbContext)
        {
            this.musicFactoryContext = musicFactoryContext;
            this.mongoDatabase = mongoDbContext;
        }

        public void ImportDataFromXML()
        {
            string storesPath = "..\\..\\..\\..\\Inputs\\stores.xml";

            try
            {
                var xmlDataLoader = new XmlLoader(storesPath);
                var stores = xmlDataLoader.LoadStoresData();

                using (musicFactoryContext)
                {
                    this.mongoDatabase.DropCollection("stores");
                    this.mongoDatabase.CreateCollection("stores");
                    var mongoStores = this.mongoDatabase.GetCollection("stores");

                    foreach (var store in stores)
                    {
                        var mergedStore = this.MergeExistingStores(store);

                        this.musicFactoryContext.Stores.Add(mergedStore);

                        mongoStores.Insert(mergedStore);
                    } 

                    Console.WriteLine("Data from the stores.xml has been loaded successfully into mongoDb database.");
                    this.musicFactoryContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Store MergeExistingStores(Store store)
        {
            var sto222re = this.musicFactoryContext.Stores.FirstOrDefault();
            var existingStore = this.musicFactoryContext.Stores.Where(st => st.Name == store.Name).FirstOrDefault();
            
            if (existingStore != null)
            {
                return existingStore;
            }

            var mergingAddress = FindIfAddressExists(store.Address);
            if (mergingAddress != null)
            {
                store.Address = mergingAddress;
            }

            var mergingCountry = FindIfCountryExists(store.Address.Country);
            if (mergingCountry != null)
            {
                store.Address.Country = mergingCountry;
            }

            return store;
        }

        private Address FindIfAddressExists(Address inputAddress)
        {
            var existingAddress = this.musicFactoryContext.Addresses.Where(ad => ad.AddressText == inputAddress.AddressText).FirstOrDefault();

            if (existingAddress != null)
            {
                return existingAddress;
            }

            return inputAddress;
        }
        private Country FindIfCountryExists(Country inputCountry)
        {
            var existingCountry = this.musicFactoryContext.Countries.Where(c => c.Name == inputCountry.Name).FirstOrDefault();

            if (existingCountry != null)
            {
                return existingCountry;
            }

            return inputCountry;
        }
    }
}

