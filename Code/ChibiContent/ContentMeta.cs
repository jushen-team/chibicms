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
        public string WebPath { get; }
        public string Title { get; set; }

        public DateTime ChangeTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int ViewedTimes { get; set; }

        public string Template { get; set; }

        public string Author { get; set; }

        public string Cover { get; set; }


        public string TopPath => topPath;
        public ContentMeta(string path,string webPath)
        {
            topPath = path;
            WebPath = webPath;
            string metaJson = File.ReadAllText(Path.Combine(topPath, MetaFileName));
            JsonConvert.PopulateObject(metaJson, this); 
        }

        internal void Update()
        {
            File.WriteAllText(Path.Combine(topPath, MetaFileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
