using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatServerApi.Controllers
{
    [Route("")]
    public class indexController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {            
            return new string[] { "for get data for chat use  GET chatserverapi.azurewebsites.net/api/messages",
                                  "for send data for chat use  POST chatserverapi.azurewebsites.net/api/messages with body {'message':'messageHere','name':'nameHere'}"
            };
        }

    }
}