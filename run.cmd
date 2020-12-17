@echo off
powershell -ExecutionPolicy ByPass -NoProfile -command "& """%~dp0\run_tool.ps1""" -build -run -warnAsError 0 %*"
exit /b %ErrorLevel%