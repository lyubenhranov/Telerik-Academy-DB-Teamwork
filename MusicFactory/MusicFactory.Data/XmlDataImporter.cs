namespace MusicFactory.Data
{
    using System;
    using System.Linq;

    using XmlDataLoader;
    using MusicFactory.Models;

    public class XmlDataImporter
    {
        private MusicFactoryDbContext musicFactoryContext;
        public XmlDataImporter(MusicFactoryDbContext musicFactoryContext)
        {
            this.musicFactoryContext = musicFactoryContext;
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
                    foreach (var store in stores)
                    {
                        var mergedStore = this.MergeExistingStores(store);

                        this.musicFactoryContext.Stores.Add(mergedStore);
                    }

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

