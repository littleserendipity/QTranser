@ echo off
%1 %2
ver|find "5.">nul&&goto :Admin
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :Admin","","runas",1)(window.close)&goto :eof
:Admin

rem 以上请求管理员权限

title 安装 QTranser
@setlocal enableextensions

rem Check permissions
net session >nul 2>&1
if %errorLevel% == 0 (
    echo 管理权限已确认 >nul 2>&1
) else (
    echo 安装无法继续，请以管理员权限运行此脚本
	pause
    goto EXIT
)

"%~dp0RegAsm.exe" /nologo /unregister "%~dp0..\QTranser.dll" >nul 2>&1

taskkill /f /im "explorer.exe" >nul 2>&1
start explorer.exe >nul 2>&1
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo.
@ echo         \\        按任意键        //     
@ echo         //    继续安装QTranser    \\
@ echo.
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo **********************************************
@ echo 当您想关闭QTranser时 
@ echo 再次双击快捷方式
@ echo 不续安装即是关闭QTrnaser 
Pause  

if defined %PROGRAMFILES(x86)% (
	"%~dp0RegAsm.exe" /nologo /codebase "%~dp0..\QTranser.dll"
) 
