using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAll();
        Task<bool> Save(Asset asset);
        Task<bool> Update(Asset asset);
        Task<bool> Delete(Guid id);
        Task<Asset> View(Guid assetId);
    }
}
