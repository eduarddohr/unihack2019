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
    [RoutePrefix("api/Manager")]
    public class ManagerController : ApiController
    {
        
        [Route("GetManagers")]
        public async Task<IHttpActionResult> GetManagers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetManagers());

            List<ManagerModel> managers = Utils.DataTableToList<ManagerModel>(dt);

            foreach(ManagerModel m in managers)
            {
                DataTable dtc = Utils.ExecuteTable(SQLCommands.GetCollectorsByManager(m.Id));
                m.Collectors = Utils.DataTableToList<CollectorModel>(dtc);
            }

            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(managers);
        }
    }
}
