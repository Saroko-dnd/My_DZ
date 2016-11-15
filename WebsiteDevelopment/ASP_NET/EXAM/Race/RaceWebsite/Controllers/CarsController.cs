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
using RaceInfrastructure;
using Microsoft.Data.OData;

namespace RaceWebsite.Controllers
{
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RaceInfrastructure;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Car>("Cars");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CarsController : ODataController
    {
        private IRaceRepository CurrentRaceRepository;
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: RaceApi/Cars
        [EnableQuery]
        public IQueryable<Car> GetCars(ODataQueryOptions<Car> queryOptions)
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
            // return Ok<IEnumerable<Car>>(cars);
            return CurrentRaceRepository.AllCars;
        }

        // GET: odata/Cars(5)
        public IHttpActionResult GetCar([FromODataUri] long key, ODataQueryOptions<Car> queryOptions)
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

            // return Ok<Car>(car);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Cars(5)
        public IHttpActionResult Put([FromODataUri] long key, Delta<Car> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(car);

            // TODO: Save the patched entity.

            // return Updated(car);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Cars
        public IHttpActionResult Post(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(car);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Cars(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<Car> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(car);

            // TODO: Save the patched entity.

            // return Updated(car);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Cars(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        public CarsController(IRaceRepository NewRaceRepository)
        {
            CurrentRaceRepository = NewRaceRepository;
        }
    }
}
