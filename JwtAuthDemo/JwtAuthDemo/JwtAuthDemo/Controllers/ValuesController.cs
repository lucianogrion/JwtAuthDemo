using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        public string Get()
        {
            return " token needed here!!!";
        }
    }
}
