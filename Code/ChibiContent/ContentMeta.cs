using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentMeta
    {
        public const string MetaFileName = @"meta.json";

        /// <summary>
        /// the folder hold the meta file
        /// </summary>
        private string topPath { get; set; }

        /// <summary>
        /// the relavent path provided by the web server or something similar. not he path on the file system
        /// </summary>
        public string WebPath { get; }
        /// <summary>
        /// this is the madatory field, if null this meta is not valid
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the change time of the content.md file, the index will order on this field
        /// </summary>
        public DateTime ChangeTime { get; set; } = DateTime.Now;

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public int ViewedTimes { get; set; }

        public string Template { get; set; }

        public string Author { get; set; }

        public string Cover { get; set; }

        /// <summary>
        /// hold all kinds of extra infos
        /// </summary>
        public Dictionary<string, object> Extras { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// the content top path on the file system
        /// </summary>
        public string TopPath => topPath;


        public ContentMeta(string path,string webPath)
        {
            try
            {
                string metaJson = File.ReadAllText(Path.Combine(path, MetaFileName));
                JsonConvert.PopulateObject(metaJson, this);
            }
            catch (Exception)
            {

                //if the file does not esit, does nothing
            }
            //always use the provided value to overide these 2, they are not supposed to be persisted,
            topPath = path;
            WebPath = webPath;
        }

        public void Update()
        {
            File.WriteAllText(Path.Combine(topPath, MetaFileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
