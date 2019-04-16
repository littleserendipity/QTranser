@ echo off
%1 %2
ver|find "5.">nul&&goto :Admin
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :Admin","","runas",1)(window.close)&goto :eof
:Admin

rem 以上管理员权限

@echo OFF
title Install DeskBand
@echo ON
@setlocal enableextensions

rem Check permissions
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Administrative permissions confirmed.
) else (
    echo Please run this script with administrator permissions.
	pause
    goto EXIT
)

rem "%~dp0tools\gacutil.exe" /nologo /u "%~dp0QTranser.dll"
"%~dp0RegAsm.exe" /nologo /unregister "%~dp0..\QTranser.dll"

rem taskkill /f /im "explorer.exe"
rem Pause
rem start explorer.exe

rem Pause



