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

    [Authorize]
    [RoutePrefix("api/Bins")]
    public class BinController : ApiController
    {
        [Route("GetBin")]
        public async Task<IHttpActionResult> GetBin(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetBin(Id));
            
            BinModel bin = Utils.DataTableToList<BinModel>(dt)[0];
            
            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(bin);
        }
        [Route("GetBins")]
        public async Task<IHttpActionResult> GetBins()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetBins());

            List<BinModel> bin = Utils.DataTableToList<BinModel>(dt);
            foreach(BinModel b in bin)
            {
                DataTable dtc = Utils.ExecuteTable(SQLCommands.GetCollectorsByBin(b.Id));
                b.Collectors = Utils.DataTableToList<CollectorModel>(dtc);
            }

            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(bin);
        }
        [Route("AddBin")]
        public async Task<IHttpActionResult> AddBin(BinModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = Guid.NewGuid();
            Utils.ExecuteNonQuery(SQLCommands.AddBin(model));

            return Ok();
        }


    }
}
