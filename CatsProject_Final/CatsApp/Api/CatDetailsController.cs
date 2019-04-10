using CatsWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CatsApp.Models;

namespace CatsApp
{
    public class CatDetailsController : ApiController
    {
        //private ICatDataProvider catsDataProvider = new SqlCatDataProvider();

        private ICatDataProvider catsDataProvider;

        public CatDetailsController()
        {
            //Note: Use a DI container to inject DataProvider into Controller Constructor
            //public CatDetailsController(IDataProviderFactory provFactory)
            //{
            //}
            IDataProviderFactory provFactory = new DataProviderFactory();
            catsDataProvider = provFactory.GetCatsDataProvider();
        }

        // GET api/<controller>
        public IEnumerable<Cat> Get()
        {
            return catsDataProvider.GetCats();
        }


        public IHttpActionResult Get(int id)
        {
            var catsData = catsDataProvider.GetCats();
            var cat = catsData.FirstOrDefault((p) => p.CatId == id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Cat cat)
        {
            IHttpActionResult ret = null;

            if (catsDataProvider.AddNewCat(cat))
            {
                ret = Created<Cat>(Request.RequestUri + cat.CatId.ToString(), cat);
                // ret = Ok(cat);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Cat cat)
        {
            IHttpActionResult ret = null;
            if (catsDataProvider.UpdateCat(cat))
            {
                ret = Ok(cat);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;
            if (catsDataProvider.DeleteCat(id))
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }
    }
}