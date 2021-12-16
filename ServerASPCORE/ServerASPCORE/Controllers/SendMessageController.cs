using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerASPCORE.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SendMessageController : ControllerBase
  {

    // POST api/<SendMessageController>
    [HttpPost]
    public void Post([FromBody] ClassLib.MessageClass mes)
    {
        if (mes.token.Length > 3) { 
                mes.timeStamp = DateTime.Now.ToString();
                Console.WriteLine(mes);
                mes.userName=Program.storedLogins.getLoginByToken(mes.token);
                Program.listOfMessages.Add(mes);
        }
    }

  }
}
