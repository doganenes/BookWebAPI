using BookWebApi.ReturnModels;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult ResponseForStatusCode<T>(ReturnModel<T> response)
        {
            IActionResult result = response.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Ok(response),
                System.Net.HttpStatusCode.NotFound => NotFound(response),
                System.Net.HttpStatusCode.BadRequest => BadRequest(response),
                System.Net.HttpStatusCode.Created => Created("/", response),
                _ => NoContent()
            }; 
            return result;

        }
    }
}
