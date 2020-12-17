@echo off
powershell -ExecutionPolicy ByPass -NoProfile -command "& """%~dp0\build_tool.ps1""" -restore -build -warnAsError 0 %*"
exit /b %ErrorLevel%