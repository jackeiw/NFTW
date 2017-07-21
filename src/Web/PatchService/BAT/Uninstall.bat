@color 2
@echo 开始卸载服务
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u %~dp0PatchService.exe

@echo 服务卸载成功

@echo 正在关闭运行的进程...
@echo off&taskkill /f /im "PatchService.exe"&exit

@Pause