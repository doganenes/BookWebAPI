using BookWebApi.ReturnModels;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult ResponseForStatusCode<T>(ReturnModel<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode,
            };
        }
    }
}
