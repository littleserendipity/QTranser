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
rem @cd "%~dp0"/QTranser/bin/Debug

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
rem start explorer.exe

rem Pause


if defined %PROGRAMFILES(x86)% (
    rem use /unregister to uninstall
    @echo off

	rem "%~dp0tools\gacutil.exe" /nologo /i "%~dp0QTranser.dll"
	"%~dp0RegAsm.exe" /nologo /codebase "%~dp0..\QTranser.dll"

) else (
    rem %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\regasm.exe /nologo /codebase "QTranser.dll"
)
