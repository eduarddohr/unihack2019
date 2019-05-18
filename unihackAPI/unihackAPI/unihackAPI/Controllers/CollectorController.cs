using ResoApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using unihackAPI.Models;

namespace unihackAPI.Controllers
{
    [RoutePrefix("api/Collector")]
    public class CollectorController : ApiController
    {
        [Route("GetCollectorsByManager")]
        public async Task<IHttpActionResult> GetCollectorsByManager(string Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetCollectorsByManager(Id));

            List<CollectorModel> collectors = Utils.DataTableToList<CollectorModel>(dt);
           

            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(collectors);
        }
    }
}
