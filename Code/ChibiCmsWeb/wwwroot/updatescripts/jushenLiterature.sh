#!/bin/bash
#just for debug use
touch jushenLiterature.update
# cd into the git folder
cd /home/zhenghaku/JushenChibicms/wwwroot/contents/literature
# clean and reset before pull, why? when you access the content the modified time in meta.json can be changed for the first time, you don't whant this change be conflict to the remote file
git clean -d -f
git reset --hard
git pull
