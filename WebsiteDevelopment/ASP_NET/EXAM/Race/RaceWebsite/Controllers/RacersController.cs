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
    builder.EntitySet<Racer>("Racers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RacersController : ODataController
    {
        private IRaceRepository CurrentRaceRepository;
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: RaceApi/Racers
        public IQueryable<Racer> GetRacers(ODataQueryOptions<Racer> queryOptions)
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

            // return Ok<IEnumerable<Racer>>(racers);
            return CurrentRaceRepository.AllRacers;
        }

        // GET: odata/Racers(5)
        public IHttpActionResult GetRacer([FromODataUri] long key, ODataQueryOptions<Racer> queryOptions)
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

            // return Ok<Racer>(racer);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Racers(5)
        public IHttpActionResult Put([FromODataUri] long key, Delta<Racer> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(racer);

            // TODO: Save the patched entity.

            // return Updated(racer);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: RaceApi/Racers
        public IHttpActionResult Post(Racer ChangedRacer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CurrentRaceRepository.UpdateRacerWithSameId(ChangedRacer);

            return StatusCode(HttpStatusCode.OK);
        }

        // PATCH: odata/Racers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<Racer> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(racer);

            // TODO: Save the patched entity.

            // return Updated(racer);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Racers(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        public RacersController(IRaceRepository NewRaceRepository)
        {
            CurrentRaceRepository = NewRaceRepository;
        }
    }
}
