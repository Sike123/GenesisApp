using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

using AutoMapper;
using GenApp.Repository;
using GenApp.Web.Models;
using log4net;

/*
    Use of Content
    http://stackoverflow.com/questions/28588652/web-api-2-return-content-with-ihttpactionresult-for-non-ok-response  
*/
namespace GenApp.Web.Controllers
{
    public class AssetController : ApiController
    {
        private readonly IAssetRepository _assetAssetRepository;
        private readonly ILog _logger;
        public AssetController(IAssetRepository assetRepository, ILog logger)
        {
            _assetAssetRepository = assetRepository;
            _logger = logger;
        }


        // GET: api/Asset
        //  [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                _logger.Info("Enterning Get()");
                var assetList = await _assetAssetRepository.GetAll();
                var assetListViewModel = assetList.Select(Mapper.Map<BookViewModel>).ToList();
                _logger.Info("No Of AssetList Retrieved " + assetListViewModel.Count());
                return Content(HttpStatusCode.OK, assetListViewModel);

            }
            catch (Exception ex)
            {
                _logger.Error("Exception From Get() " + ex);
                return Content(HttpStatusCode.InternalServerError, "Exception Message" + ex.Message + " " + ex.StackTrace);
            }

        }

        // GET: api/Asset/5
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                _logger.Info("Method: Get(Guid id)");
                var result = await _assetAssetRepository.View(id);

                if (result == null)
                {
                    return NotFound();
                }

                var properties = typeof(Book).GetProperties();

                foreach (var property in properties)
                {
                    _logger.Info("Property Name : " + property.Name +
                                 "Property Value: " + property.GetValue(result, null));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception from Get(GuidId) Method " + ex.Message + " " + ex.StackTrace);
                return Content(HttpStatusCode.InternalServerError,
                    "Exception Occurred" + ex.Message + " " + ex.StackTrace);
            }

        }

        // POST: api/Asset
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Post([FromBody] BookViewModel model)
        {
            try
            {
                if (model.IsEmpty) return BadRequest(ModelState);
                bool result = await _assetAssetRepository.Save(Mapper.Map<Book>(model));
                return !result ? Content(HttpStatusCode.InternalServerError, "Registration could not be completed") :
                    Content(HttpStatusCode.Created, "Asset Has Been Registered Successfully.");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message + "," + ex.StackTrace);
            }
        }

        // PUT: api/Asset/5
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Put([FromBody] BookViewModel model)
        {
            try
            {
                if (model.IsEmpty) return BadRequest(ModelState);

                bool result = await _assetAssetRepository.Update(Mapper.Map<Book>(model));
                return result
                    ? Content(HttpStatusCode.Created, "Asset has been Updated Successfully")
                    : Content(HttpStatusCode.InternalServerError, "Asset could not be updated.");

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message + " " + ex.StackTrace);
            }
        }

        // PUT: api/Asset/5
        /*
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Put([FromBody] BookViewModel model)
        {
            string result = null;
            try
            {
                if (model.IsEmpty) return BadRequest(ModelState);
                result = _assetAssetRepository.Update(Mapper.Map<Book>(model));
                return Content(HttpStatusCode.Created, result);
                //  ? Content(HttpStatusCode.Created, "Asset has been Updated Successfully")
                //  : Content(HttpStatusCode.InternalServerError, "Asset could not be updated. ");
            }
            catch (Exception ex)
            {
            //    return Content(HttpStatusCode.InternalServerError, ex.Message + " " + ex.StackTrace);
                return Content(HttpStatusCode.InternalServerError, result);
            }
           
        }
        */

        // DELETE: api/Asset/5
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _assetAssetRepository.Delete(id);
                return result
                    ? Content(HttpStatusCode.OK, "Asset has been removed from the System")
                    : Content(HttpStatusCode.InternalServerError, "Error in removing the Asset from the system");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "Exception occurred. " + ex.Message + " " + ex.StackTrace);
            }
        }
    }
}

