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
        /// this add all the photos in a folder to the photo extra fields of the meta file
        /// </summary>
        /// <param name="args">0= the top path, 1= the title</param>
        static void Main(string[] args)
        {
            var path = "";
            if (args.Length > 0)
            {
                path = args[0];
            }
            else
            {
                path = Environment.CurrentDirectory;
            }
            
            var contentMeta = new ContentMeta(path, "not applicapable");
            if (args.Length > 1)
            {
                if (args[1] == "*")
                {
                    contentMeta.Title = Path.GetFileName(path);
                }
                else
                {
                    contentMeta.Title = args[1];
                }
                
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
            
            contentMeta.Update();
        }
    }
}
