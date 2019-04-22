@echo off

%1 %2
ver|find "5.">nul&&goto :Admin
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :Admin","","runas",1)(window.close)&goto :eof
:Admin
rem 以上管理员权限

"%~dp0RegAsm.exe" /nologo /unregister "%~dp0..\QTranser.dll"

taskkill /f /im "explorer.exe"

