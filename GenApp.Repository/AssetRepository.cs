using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    public class AssetRepository:IAssetRepository
    {

        private readonly GenAppContext _context = new GenAppContext();

        public IEnumerable<Asset> GetAll()
        {
            var assets = _context.Books;
            return assets;
        }




        public void Delete(Guid id)
        {
            var asset = _context.Assets.SingleOrDefault(x => x.Id == id);
            _context.Assets.Remove(asset);
            _context.SaveChanges();
        }

        public Asset View(Guid id)
        {
            var asset = _context.Assets.SingleOrDefault(x => x.Id == id);
            return asset;
        }

        public bool Update(Asset asset)
        {
            if (asset is Book)
            {
                var _asset = _context.Assets.SingleOrDefault(x => x.Id == asset.Id);
                if (_asset == null) throw new ArgumentNullException(nameof(_asset));
                 _asset.Name = asset.Name;
                ((Book) _asset).Edition = ((Book) asset).Edition;
                ((Book) _asset).Publisher = ((Book) asset).Publisher;
                ((Book) _asset).Isbn = ((Book) asset).Isbn;
                _context.Assets.AddOrUpdate(_asset);
                return _context.SaveChanges() == 0;

            }
            return false;

        }

        public void Save(Asset asset)
        {
            asset.Id = Guid.NewGuid();

            _context.Assets.Add(asset);
            _context.SaveChanges();
        }
    }
}
