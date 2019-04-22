@ echo off

%1 %2
ver|find "5.">nul&&goto :Admin
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :Admin","","runas",1)(window.close)&goto :eof
:Admin

rem 以上管理员权限

if defined %PROGRAMFILES(x86)% (
	"%~dp0RegAsm.exe" /nologo /codebase "%~dp0..\QTranser.dll"
)
