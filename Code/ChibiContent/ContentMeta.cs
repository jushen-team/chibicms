using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentMeta
    {
        public DateTime ChangeTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int ViewedTimes { get; set; }

        public string Template { get; set; }

        public ContentMeta(string path)
        {
            string metaJson = File.ReadAllText(path + @"\meta.json");
            JsonConvert.PopulateObject(metaJson, this); 
        }
    }
}
