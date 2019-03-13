# Chibi CMS White Paper
## Why I want to build this?
Simple, i need a CMS that is simple and lightweight. I want to focus on the content not the presentations. But I also want it to be good looking and easy and joyful to read.

There are some options out, that almost meet my exttact expectaion. Thay are call flat file CMS, and use markdown as its content.

But I just need some ting even  simpler. So I decide to code one. This is going to be a very simple system, but it will be incremental, as I will put more functions in as time goes by.

## Requiements
1. Flat file storage, no database, at list DB is not mandetory
2. Markdown as content
3. very easy timeplate system.
4. Docker
5. Caching for rendered Markdowns.

## Basci Designs

### Content
* Content is a folder in `\top\content` folder
* can be organized in directories in file system
* contents a content.md file which is rendered to readers
* contents a res folder to store the images
* at the begining of the markdown file there is a section to store a key value pair as metadata
* an objcet in chibiCMS
### Content Manager
* a class to get content
* GetContent(Name) name can be a path
* GetContentList(filter=[filed:value]) get a list of content metadata by the filter
### Template
* template for index page
* template for content page
* sections specifies content or contentList
* the route decied which template to use

