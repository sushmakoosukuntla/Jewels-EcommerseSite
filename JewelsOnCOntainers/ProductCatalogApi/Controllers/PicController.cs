using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Controllers
{
    //Below line says Path(Route- nothing but url) to get to this api is, api/The name of the controller(api/[controller])
    //[controller] is just a plcaeholder. We need to replace the placeholder with pic.
    //so now Route(path) becomes [Route("api/pic")]
    //Route = http://domainName/api/pic, this is the route to the picController we are writing
    //Get and Post request are the two commonly used requests in the industry
    //Square brackets are used to bind to the class names
    [Route("api/[controller]")] //This is route to how to get to this class
    [ApiController] //Api class
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env; //is a global variable
        //Injecting(IIs Express will Inject) the initial path from virtual machine
        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }
        /*How does this route Route = http://domainName/api/pic know to come to this below method? We have Individual
         route on each method, that can tell you how to get to that method.*/
        [HttpGet("{id}")]
        //Curly brackets are used to bind to the parameters. {id} is binding to  GetImage(int id)
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath;//gives the absolute path to the directory.
            var path = Path.Combine($"{webRoot}/Pics/", $"Ring{id}.jpg");//gives the path to that file.

            //this is actual picture, but in binary form.
            //File is a overloaded class.That is why we are explicitly using system.IO
            var buffer = System.IO.File.ReadAllBytes(path);

            //You cannot send data in binary form. We should send the file to the client
            //We are telling in the below line that, make a copy of the picture, from the content i gave(buffer)
            //When you send a file to the client, Its a string, always Json send the response in string type
            //Unfortunately we are sending a pic not a string, thats why we should send the type("image/jpeg") of the picture
            return File(buffer, "image/jpeg"); //This "File" is called file content result
        }

        //IACTIONRESULT, In every Api class always all methods are accessed by the client should return IActionResult.
    }
}
