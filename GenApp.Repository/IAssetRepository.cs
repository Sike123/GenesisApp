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
        IEnumerable<Asset> GetAll();
        void Save(Asset asset);
        bool Update(Asset asset);
        void Delete(Guid id);
        Asset View(Guid assetId);
    }
}
