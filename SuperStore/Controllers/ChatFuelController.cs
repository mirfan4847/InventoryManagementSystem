using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperStore.ViewModel;
using SuperStore.Models;

namespace SuperStore.Controllers
{
    public class ChatFuelController : ApiController
    {
        SuperStoreEntities _db = new SuperStoreEntities();

        [Route("api/ChatFuel/ClientRecords/{id}")]
        [HttpGet]
        public tblUser ClientRecord(int id)
        {
            return
                  _db.tblUsers.SingleOrDefault(x => x.UserId == id);
        }
    }
}
