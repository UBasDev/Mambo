using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    internal class UsersController : BaseController
    {
        [HttpPost("create-single-user")]
        public async Task CreateSingleUser()
        {
        }
    }
}