@echo off
echo.

if "%~1"=="" goto blank
echo %1
git add *.*
git commit -m %1
git push
goto end

:blank
echo Quick script to commit GIT changes for project to linked repo, GIT needs to be installed.
echo CommitRepo ["Message"]

:end
echo Script end.
