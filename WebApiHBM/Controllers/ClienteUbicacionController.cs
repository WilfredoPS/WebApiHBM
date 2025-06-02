using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static WebApiHBM.Models.Ubicacion;

namespace WebApiHBM.Controllers
{
    public class ClienteUbicacionController : ApiController
    {
        [HttpPost]
        public async Task<object> ClienteUbicacion(ubicacion Ubicacion)
        {
            return "";
        }
   }
}
