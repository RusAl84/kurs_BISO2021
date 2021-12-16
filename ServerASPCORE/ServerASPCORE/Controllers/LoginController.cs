using ClassLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerASPCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public string Post(LoginClass reciveData)
        {
            string token = Program.storedLogins.CheckLoginPassword(reciveData);
            Console.WriteLine(Program.storedLogins);
            return token;
        }
    }
}
