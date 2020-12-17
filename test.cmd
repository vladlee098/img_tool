@echo off
powershell -ExecutionPolicy ByPass -NoProfile -command "& """%~dp0\run_test.ps1""" -warnAsError 0 %*"
exit /b %ErrorLevel%