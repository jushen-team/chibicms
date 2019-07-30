using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChibiCmsWeb.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ChibiCmsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private UpdateScripts updateScripts;

        public WebHookController(UpdateScripts scripts)
        {
            updateScripts = scripts;
        }

        /// <summary>
        /// call this when you want the web to update you content using github
        /// </summary>
        /// <returns></returns>
        [HttpPost("GitUpdate")]
        public IActionResult GitUpdate([FromBody]dynamic repository)
        {
            var githubPush = JObject.Parse(repository.ToString());
            var repoName = githubPush["repository"]["full_name"].ToString();
            var result = updateScripts.Run(repoName);
            return new OkObjectResult(result);
        }


        /// <summary>
        /// call this when you want the web to update you content using github
        /// </summary>
        /// <returns></returns>
        [HttpPost("GitUpdateTest")]
        public IActionResult GitUpdateTest(string repository)
        {
            updateScripts.Run(repository);
            return Ok();
        }

    }
}