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

        public string Title { get; set; }

        public DateTime ChangeTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int ViewedTimes { get; set; }

        public string Template { get; set; }

        public ContentMeta(string path)
        {
            topPath = path;
            string metaJson = File.ReadAllText(Path.Combine(topPath, MetaFileName));
            JsonConvert.PopulateObject(metaJson, this); 
        }

        internal void Update()
        {
            File.WriteAllText(Path.Combine(topPath, MetaFileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
