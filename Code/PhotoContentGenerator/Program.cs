using Jushen.ChibiCms.ChibiContent;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoContentGenerator
{
    class Program
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG", ".SVG" };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">0= the top path, 1= the title</param>
        static void Main(string[] args)
        {
            var path = args[0];
            var contentMeta = new ContentMeta(path, "not applicapable");
            if (args.Length > 1)
            {
                contentMeta.Title = args[1];
            }
            else
            {
                contentMeta.Title = Path.GetFileName(path);
            }
            //get all photo file name
            var photosFiles = Directory.GetFiles(path);
            contentMeta.Extras["photos"] = new List<string>();
            foreach (var photo in photosFiles)
            {
                if (ImageExtensions.Contains(Path.GetExtension(photo).ToUpperInvariant()))
                {
                    (contentMeta.Extras["photos"] as List<string>).Add(Path.GetFileName(photo));
                }
            }
            contentMeta.Template = "photoView";
            contentMeta.Update();
            File.Create(Path.Combine(path,Content.ContentFileName));
        }
    }
}
