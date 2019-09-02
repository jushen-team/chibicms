using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentMeta
    {
        /// <summary>
        /// the file contains the meta data
        /// </summary>
        public const string MetaFileName = @"meta.json";

        /// <summary>
        /// content type is content
        /// </summary>
        public const string TypeContent = "Content";
        
        /// <summary>
        /// content type is directory
        /// </summary>
        public const string TypeDirectory = "Directory";

        /// <summary>
        /// content type is directory
        /// </summary>
        public const string TypeLink = "Link";

        /// <summary>
        /// if this file exsists, than this is a directory, only include this is the folder is both content and a directory
        /// </summary>
        public const string DirectoryMetaFile = @"dmeta.json";
        private string directoryMetaFile;

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

        public string Link { get; set; }

        /// <summary>
        /// hold all kinds of extra infos
        /// </summary>
        public Dictionary<string, object> Extras { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// the content top path on the file system
        /// </summary>
        public string TopPath => topPath;


        public string ContentType { get; set; } = "Content";

        private string realFileName = MetaFileName;
 
        public ContentMeta()
        {

        }


        /// <summary>
        /// load a meta from a given directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="webPath"></param>
        public ContentMeta(string path,string webPath):this(path,webPath,MetaFileName)
        {
            
        }

        public ContentMeta(string path, string webPath, string metaFile)
        {
            try
            {
                string metaJson = File.ReadAllText(Path.Combine(path, metaFile));
                //if the file exist then you want to update the file
                realFileName = metaFile;
                JsonConvert.PopulateObject(metaJson, this);
            }
            catch (Exception e)
            {
                if (Directory.Exists(path))
                {
                    Title = Path.GetFileName(path);
                    ContentType = TypeDirectory;
                }
                //if the file does not exist use the folder as the content title and the type is directory
            }
            //always use the provided value to overide these 2, they are not supposed to be persisted,
            topPath = path;
            WebPath = webPath;
        }

        public void Update()
        {
            File.WriteAllText(Path.Combine(topPath, realFileName), JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }

}
