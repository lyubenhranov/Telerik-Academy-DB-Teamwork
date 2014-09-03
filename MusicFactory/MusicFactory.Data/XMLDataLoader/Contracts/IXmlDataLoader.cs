namespace MusicFactory.Data.XmlDataLoader.Contracts
{
    using System.Collections.Generic;

    using MusicFactory.Models;

    public interface IXmlDataLoader
    {
        ICollection<Store> LoadStoresData();
    }
}
