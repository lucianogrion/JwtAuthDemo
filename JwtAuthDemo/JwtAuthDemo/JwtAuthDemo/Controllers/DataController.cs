using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    
    public class DataController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "No token needed here!!! --> go to CreationToken";
        }
    }
}
