using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers
{
    [Route("/")]
    [ApiController]
    public class Base : ControllerBase
    {
        //public void Start()
        //{
        //    var response = HttpContext;
        //    response.Response.ContentType = "text/html";
        //    string path = "C:\\Новая папка\\OnlineShopping\\OnlineShopping\\wwwroot\\index.html";
        //    response.Response.SendFileAsync(path).Wait();
        //}
    }
}
