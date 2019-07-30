using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChibiCmsWeb.Helpers
{
    public class UpdateScripts
    {
        /// <summary>
        /// use the configuration to build a script dictionary
        /// </summary>
        /// <param name="configuration"></param>
        public UpdateScripts(IConfiguration configuration,string path)
        {
            Scripts = configuration.GetSection("UpdateScripts").Get<Dictionary<string, string>>();
            ScriptPath = path;
        }

        public string ScriptPath { get; set; }

        /// <summary>
        /// the key is the repo full name, the value is the script to run
        /// </summary>
        public Dictionary<string,string> Scripts { get; set; }

        /// <summary>
        /// run the update script coreponding to the key. it will check if it is on windows or linus. 
        /// the script is located in wwwroot/updatescript, this canb e change using the path property
        /// </summary>
        /// <param name="key"></param>
        public string Run(string key)
        {
            var cmd = Path.Combine(ScriptPath, Scripts[key]);
            return cmd.RunCommand();
        }
    }
}
