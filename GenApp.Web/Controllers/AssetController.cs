using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using GenApp.Repository;
using GenApp.Web.Models;

namespace GenApp.Web.Controllers
{
    public class AssetController : ApiController
    {
        private readonly IAssetRepository _assetAssetRepository;

        public AssetController(IAssetRepository assetRepository)
        {
            _assetAssetRepository = assetRepository;
        }


        // GET: api/Asset
        [Authorize]
        public IEnumerable<AssetViewModel> Get()
        {
            var assetList = _assetAssetRepository.GetAll();


            var assetListViewModel = assetList.Select(Mapper.Map<BookViewModel>).ToList();

            return assetListViewModel;
        }

        //    public AssetViewModel Create(AssetViewModel model) => null;


        // GET: api/Asset/5
        [Authorize]
        public Asset Get(Guid id)
        {
            var asset = _assetAssetRepository.View(id);
            return asset;
        }

        // POST: api/Asset
        [Authorize]
        public async Task<IHttpActionResult> Post([FromBody] BookViewModel model)
        {
            if (!model.IsEmpty)
            {
                _assetAssetRepository.Save(Mapper.Map<Book>(model));
                return Ok();
            }
            return Ok();
        }

        // PUT: api/Asset/5
        [Authorize]
        public bool Put([FromBody] BookViewModel model)
        {
            bool updated = _assetAssetRepository.Update(Mapper.Map<Book>(model));
            return updated;
        }

        // DELETE: api/Asset/5
        [Authorize]
        public void Delete(Guid id)
        {
            _assetAssetRepository.Delete(id);
        }
    }
}