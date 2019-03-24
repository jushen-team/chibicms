using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentManager
    {
        public const string TitleFileName = @"title.txt";
        public const string HideFileName = @"hide.txt";

        public string TopPath { get; }

        public ContentManager(string topPath)
        {
            TopPath = topPath;
        }

        /// <summary>
        /// get a list of content by using a filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Content> GetContents(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        public (List<ContentMeta> metas, string title) GetContentMeta(int page = 1, int pageSize = 10)
        {
            return GetContentMeta("",page,pageSize);
        }

        /// <summary>
        /// list lasted content with pagination
        /// </summary>
        /// <param name="page">the page start from 1</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public (List<ContentMeta> metas,string title) GetContentMeta(string path, int page = 1, int pageSize = 10)
        {
            //get all content top directories
            var contentTops = Directory.GetDirectories(Path.Combine(TopPath, path), "*", SearchOption.AllDirectories);

            var title = "index";
            //get the tile from a file in the top directory name title.txt
            try
            {
                title = File.ReadAllText(Path.Combine(Path.Combine(TopPath, path), TitleFileName));
            }
            catch (Exception)
            {
                //does nothing so the title will no change                
            }
           

            var metas = new List<ContentMeta>();
            //load meta
            foreach (var contentTop in contentTops)
            {
                var webpath = contentTop.Substring(TopPath.Length);
                
                //validata the top path before after meta
                var tMeta = new ContentMeta(contentTop, webpath);
                if (string.IsNullOrEmpty(tMeta.Title))
                {
                    continue;
                }
                //if hide do not list
                if (File.Exists(Path.Combine(contentTop, HideFileName)))
                {
                    continue;
                }

                metas.Add(tMeta);
                //update the update time
                //this is very ineffecient must revise
                var updateTime = File.GetLastWriteTime(Path.Combine(contentTop, Content.ContentFileName));
                if (tMeta.ChangeTime != updateTime)
                {
                    tMeta.ChangeTime = updateTime;
                    tMeta.Update();
                }
            }
            //sort and return
            return (metas.OrderByDescending(mt => mt.ChangeTime).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                title);

        }


        /// <summary>
        /// get a lis tof metadata of content filtered by fileter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<ContentMeta> GetContentMeta(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// get a sigle content using a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Content GetConent(string path)
        {
            return new Content(Path.Combine(TopPath, path), path);
        }


    }
}
