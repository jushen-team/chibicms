using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentManager
    {
        /// <summary>
        /// this file containst the title text for this directory
        /// </summary>
        public const string TitleFileName = @"title.txt";
        /// <summary>
        /// when this file is present, this content of this directory is not listed by automatic indexing
        /// </summary>
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

        public (List<ContentMeta> metas, ContentMeta rootMeta) GetContentMeta(int page = 1, int pageSize = 10)
        {
            return GetContentMeta("",page,pageSize);
        }

        /// <summary>
        /// list lasted content with pagination, this will retrieve the content recursively, and ignore the directory contrent
        /// </summary>
        /// <param name="path">the start path of this content search</param>
        /// <param name="page">the page start from 1</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public (List<ContentMeta> metas, ContentMeta rootMeta) GetContentMeta(string path, int page = 1, int pageSize = 10)
        {
            return GetContentMeta(path, true, true, page, pageSize);
        }

        /// <summary>
        ///  list lasted content with pagination
        /// </summary>
        /// <param name="path">the start path of this content search</param>
        /// <param name="isRecurent">is the search recursive, if not only list the content in the path, if ture it will list all the content in deeper directories</param>
        /// <param name="isIgnoreDirectory">is ture. it will ignroe the directoty content, not the directoty is not counted in pagesize</param>
        /// <param name="page">the page start from 1</param>
        /// <param name="pageSize">page size</param>
        /// <returns>The returned list of content meta, you can get content and further info with it; titel is the title of the top directory</returns>
        public (List<ContentMeta> metas, ContentMeta rootMeta) GetContentMeta(string path,bool isRecurent,bool isIgnoreDirectory, int page = 1, int pageSize = 0)
        {
            //get all content top directories
            var searchRecursive = isRecurent ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var contentTops = Directory.GetDirectories(Path.Combine(TopPath, path), "*", searchRecursive);

            //get the top directory meata
            ContentMeta rootMeta;
            rootMeta = new ContentMeta(Path.Combine(TopPath, path), path);

            var metas = new List<ContentMeta>();
            var directoryMeta = new List<ContentMeta>();
            //load meta
            foreach (var contentTop in contentTops)
            {
                var webpath = contentTop.Substring(TopPath.Length);
                
                //validata the top path before after meta
                var tMeta = new ContentMeta(contentTop, webpath);
                if (string.IsNullOrEmpty(tMeta.Title) || tMeta.Title.StartsWith("."))
                {
                    continue;
                }
                //if hide do not list
                if (File.Exists(Path.Combine(contentTop, HideFileName)))
                {
                    continue;
                }

                //if the meta says it is a directoty add it to directory metas
                if (tMeta.ContentType == ContentMeta.TypeDirectory)
                {
                    if (isIgnoreDirectory == false)
                    {
                        directoryMeta.Add(tMeta);
                    }
                    continue;
                }

                //only check this if the folder is a real content
                if (File.Exists(Path.Combine(contentTop, ContentMeta.DirectoryMetaFile)))
                {
                    if (isIgnoreDirectory == false)
                    {
                        var tDMeta = new ContentMeta(contentTop, webpath, ContentMeta.DirectoryMetaFile);
                        tDMeta.ContentType = ContentMeta.TypeDirectory;
                        directoryMeta.Add(tDMeta);
                    }
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
            if (pageSize > 0)
            {
                directoryMeta.AddRange(metas.OrderByDescending(mt => mt.ChangeTime).Skip((page - 1) * pageSize).Take(pageSize).ToList());
            }
            else
            {
                directoryMeta.AddRange(metas.OrderByDescending(mt => mt.ChangeTime).ToList());
            }
            
            return (directoryMeta, rootMeta);

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
