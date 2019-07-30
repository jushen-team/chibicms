#!/bin/bash
touch jushenLiterature.update
cd /home/zhenghaku/JushenChibicms/wwwroot/contents/literature
git clean -d -f
git reset --hard
git pull
