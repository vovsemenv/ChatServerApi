using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace ChatServerApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
            private ConnectionMultiplexer redis;
            IDatabase db;//redissource

            public MessagesController()
            {
            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { "redis-16883.c56.east-us.azure.cloud.redislabs.com", 16883 },
                    
                },
                CommandMap = CommandMap.Create(new HashSet<string>
                    { // EXCLUDE a few commands
                        "INFO", "CONFIG", "CLUSTER",
                        "PING", "ECHO", "CLIENT"
                    }, available: false),
                KeepAlive = 180,
                DefaultVersion = new Version(5, 0, 4),
                Password = "vpTq6IUvudoLR4PwnXw7IDSRnckG12ZH"
            };

            redis = ConnectionMultiplexer.Connect(config);
                db = redis.GetDatabase();
            
            }
            
            //GET: api/Messages
            [HttpGet]
            public IEnumerable<Message> Get()
            {

                var res = db.Execute("get", "messages");
                
                if (res.IsNull)
                {
                    return new List<Message>();
                }
                else
                {
                    var deser = JsonConvert.DeserializeObject<List<Message>>(res.ToString());

                    return deser;
                }

            }


            // POST: api/Messages
            [HttpPost]
            public void Post([FromBody] Message value)
            {



                var res = db.Execute("get", "messages");
                if (res.IsNull)
                {
                    var arr = new List<Message>();

                    arr.Add(new Message(value.message, value.name));
                    var jsst = JsonConvert.SerializeObject(arr);
                    db.Execute("set", new object[] { "messages", jsst });
                }
                else
                {

                    var deser = JsonConvert.DeserializeObject<List<Message>>(res.ToString());
                    deser.Add(new Message(value.message, value.name));

                    var jsst = JsonConvert.SerializeObject(deser);
                    db.Execute("set", new object[] { "messages", jsst });

                }
            }

        }
}
