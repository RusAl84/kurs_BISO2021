using ClassLib;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerASPCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getLoginByTokenController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] string reciveData)
        {
            string _login = Program.storedLogins.getLoginByToken(reciveData);
            Console.WriteLine(_login);
            return _login;
        }
    }
}
