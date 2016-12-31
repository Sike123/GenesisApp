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
            //can use the commented code as well 
            //  var assets = Task.Factory.StartNew(() =>(IEnumerable<Asset>) _context.Books);
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
            /*
             var updateTask = Task<bool>.Factory.StartNew(() =>
                {
                   
                        if (!(asset is Book)) return false;
                        var _asset = _context.Assets.SingleOrDefault(x => x.Id == asset.Id);
                        if (_asset == null) throw new ArgumentNullException(nameof(_asset));
                        _asset.Name = asset.Name;
                        ((Book) asset).Edition = ((Book) asset).Edition;
                        ((Book) _asset).Publisher = ((Book) asset).Publisher;
                        ((Book) _asset).Isbn = ((Book) asset).Isbn;
                       _context.Entry(_asset).State = EntityState.Modified;
                           _context.Assets.AddOrUpdate(_asset);
                        return _context.SaveChanges() > 0;
                 


                });
                return updateTask;
            */

 
        
            
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


        /*
        public Task<string> Update(Asset asset)
        {
            
            try
            {
                var updateTask = Task<string>.Factory.StartNew(() =>
                {

                    if (!(asset is Book)) return "Please return correct type of asset";
                    var _asset = _context.Assets.SingleOrDefault(x => x.Id == asset.Id);
                    if (_asset == null) throw new ArgumentNullException(nameof(_asset));
                    _asset.Name = asset.Name;
                    ((Book)asset).Edition = ((Book) asset).Edition;
                    ((Book) _asset).Publisher = ((Book) asset).Publisher;
                    ((Book) _asset).Isbn = ((Book) asset).Isbn;
                    _context.Entry(_asset).State=EntityState.Modified;
                    _context.Assets.AddOrUpdate(_asset);
                    return _context.SaveChanges() > 0
                        ? "Asset has been updated successfully"
                        : "Asset could not be updated" + _asset.Id +"//// "+ asset.Name +"/// "+ ((Book) asset).Edition +"/// "+
                          ((Book) asset).Publisher +"/// "+ ((Book) asset).Isbn;
                       ;
                });
                return updateTask;
            }
            catch (Exception ex)
            {
                return Task<string>.Factory.StartNew(() => ex.Message + " " + ex.StackTrace);
            }
        }
        */
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
