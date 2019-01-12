using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    //public interface IAssetRepository<T> where T:new()
    //{
    //    Task<IEnumerable<T>> GetAll();
    //    Task<bool> Save(T asset);
    //    Task<bool> Update(T asset);
    //    Task<bool> Delete(Guid id);
    //    Task<T> View(Guid assetId);
    //}

    public interface IAssetRepository
    {
        
        Task<IEnumerable<Asset>> GetAll();
        Task<bool> Save(Asset asset);
        Task<bool> Update(Asset asset);
        Task<bool> Delete(Guid id);
        Task<Asset> View(Guid assetId);
    }




}
