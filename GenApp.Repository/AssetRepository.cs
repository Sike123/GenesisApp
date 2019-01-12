using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    public class AssetRepository : IAssetRepository
    {
        
        private readonly GenAppContext _context = new GenAppContext();
        
        
        public async Task<IEnumerable<Asset>> GetAll()
        {  
            var assets = _context.Books.ToListAsync();
            return await assets;
        }

        public Task<bool> Delete(Guid id)
        {
            var asset = _context.Assets.SingleOrDefault(x => x.Id == id);
            if (asset == null) return Task.Factory.StartNew(() => false);
            var deleted = Task.Factory.StartNew(() =>
            {
                _context.Assets.Remove(asset);
                return _context.SaveChanges() > 0;
            });
            return deleted;
        }

        public Task<Asset> View(Guid id)
        {
            var asset = Task.Factory.StartNew(() =>
            {
                return _context.Assets.SingleOrDefault(x => x.Id == id);

            });
            return asset;
        }
        public Task<bool> Update(Asset asset)
        {
                   
            var updateTask = Task<bool>.Factory.StartNew(() =>
            {
                if (!(asset is Book)) return false;
                var book = (Book)asset;
                _context.Books.Attach(book);
                _context.Entry(book).State = EntityState.Modified;
                var bob = _context.SaveChanges();
                return bob > 0;
            });
            return updateTask;            
        }

        public Task<bool> Save(Asset asset)
        {

            var saveTask = Task<bool>.Factory.StartNew(() =>
            {
                asset.Id = Guid.NewGuid();
                _context.Assets.Add(asset);
                return _context.SaveChanges() > 0;

            });
            return saveTask;


        }
    }
}
