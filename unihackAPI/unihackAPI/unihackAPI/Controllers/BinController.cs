using Microsoft.AspNet.Identity;
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
        [Route("GetBinsByZone")]
        public async Task<IHttpActionResult> GetBinsByZone(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetBinsByZone(Id));

            List<BinModel> bin = Utils.DataTableToList<BinModel>(dt);
            foreach (BinModel b in bin)
            {
                DataTable dtc = Utils.ExecuteTable(SQLCommands.GetCollectorsByBin(b.Id));
                b.Collectors = Utils.DataTableToList<CollectorModel>(dtc);
            }

            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(bin);
        }
        [Route("GetBinsByManager")]
        public async Task<IHttpActionResult> GetBinsByManager(string Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataTable dt = Utils.ExecuteTable(SQLCommands.GetBinsByManager(Id));

            List<BinModel> bin = Utils.DataTableToList<BinModel>(dt);
            foreach (BinModel b in bin)
            {
                DataTable dtc = Utils.ExecuteTable(SQLCommands.GetCollectorsByBin(b.Id));
                b.Collectors = Utils.DataTableToList<CollectorModel>(dtc);
            }

            if (dt.Rows.Count == 0)
                return NotFound();
            else
                return Ok(bin);
        }
        [Route("UpdateBin")]
        public async Task<IHttpActionResult> UpdateBin(Guid Id, float Capacity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            Utils.ExecuteNonQuery(SQLCommands.UpdateBin(Id, Capacity));

            return Ok();
        }
        [Route("AddBin")]
        public async Task<IHttpActionResult> AddBin(BinModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try{
                model.Zone = Int32.Parse(Utils.ExecuteScalar(SQLCommands.GetZoneByCollector(User.Identity.GetUserId())));
                model.Id = Guid.NewGuid();
                Utils.ExecuteNonQuery(SQLCommands.AddBin(model));
            }
            catch(Exception e)
            {
                return InternalServerError();
            }
            return Ok();
        }


    }
}
