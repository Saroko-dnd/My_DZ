using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using NewsInfrastructure;
using Microsoft.Data.OData;
using NewsWebsite.App_Start;
using Ninject;
using NewsWebsite.ClassesForNewsWebsite;

namespace NewsWebsite.Areas.News.Controllers
{
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NewsInfrastructure;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<News>("GettingNewsUsingHeaderValue");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GettingNewsUsingHeaderValueController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: NewsOdata/GettingNewsUsingHeaderValue
        [EnableQuery]
        public IQueryable<NewsInfrastructure.News> GetGettingNewsUsingHeaderValue(ODataQueryOptions<NewsInfrastructure.News> queryOptions, string NewsHeaderForSearch)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return null;
            }

            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            if (NewsHeaderForSearch != null)
            {
                return CurrentNewsWebsiteDataManager.GetDistinctNewsWithSimilarHeader(NewsHeaderForSearch).AsQueryable();
            }
            else
            {
                return null;
            }
        }

        // GET: NewsOdata/GettingNewsUsingHeaderValue(5)
        public IHttpActionResult GetNews([FromODataUri] long key, ODataQueryOptions<NewsInfrastructure.News> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<News>(news);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: NewsOdata/GettingNewsUsingHeaderValue(5)
        public IHttpActionResult Put([FromODataUri] long key, Delta<NewsInfrastructure.News> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(news);

            // TODO: Save the patched entity.

            // return Updated(news);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: NewsOdata/GettingNewsUsingHeaderValue
        public IHttpActionResult Post(NewsInfrastructure.News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(news);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: NewsOdata/GettingNewsUsingHeaderValue(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<NewsInfrastructure.News> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(news);

            // TODO: Save the patched entity.

            // return Updated(news);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: NewsOdata/GettingNewsUsingHeaderValue(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
