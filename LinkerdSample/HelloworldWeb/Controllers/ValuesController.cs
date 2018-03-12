using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloworldWeb.Controllers
{
    using Grpc.Core;
    using Helloworld.Proto;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // TODO: use DI
        private Hello.HelloClient helloClient = new Hello.HelloClient(new Channel("helloserver:30001", ChannelCredentials.Insecure));
        private World.WorldClient worldClient = new World.WorldClient(new Channel("worldserver:30002", ChannelCredentials.Insecure));

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var hello = await helloClient.GreetingAsync(new GreetRequest());
            var world = await worldClient.GreetingAsync(new GreetRequest());

            return hello.Message + " " + world.Message;
        }
    }
}
