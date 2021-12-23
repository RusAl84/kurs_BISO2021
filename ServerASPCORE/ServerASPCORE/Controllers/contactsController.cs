using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerASPCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class contactsController : ControllerBase
    {

        // GET api/<contactsController>
        [HttpGet()]
        public string Get(int id)
        {
            
            List<string> contacts = Program.storedLogins.GetContacts();
            string tmpStr = JsonConvert.SerializeObject(contacts);
            return tmpStr;
        }

    }
}
