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
        private Lazy<Hello.HelloClient> helloClient = new Lazy<Hello.HelloClient>(() => new Hello.HelloClient(new Channel("nginx:80", ChannelCredentials.Insecure)));
        private Lazy<World.WorldClient> worldClient = new Lazy<World.WorldClient>(() => new World.WorldClient(new Channel("nginx:80", ChannelCredentials.Insecure)));


        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var hello = await helloClient.Value.GreetingAsync(new GreetRequest());
            var world = await worldClient.Value.GreetingAsync(new GreetRequest());

            return hello.Message + " " + world.Message;
        }
    }
}
