using System.Web.Http;

namespace GenApp.Web.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        //[AllowAnonymous]
        //public IEnumerable<AssetViewModel> Get()
        //{

        //  return  GetAllAssets();

        //}

     

        // GET api/values/5
     
        public string Get(int id)
        {
        
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
