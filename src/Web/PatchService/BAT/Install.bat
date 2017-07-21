@color 2
@echo 开始安装服务
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe %~dp0PatchService.exe

@echo 服务安装成功

@Pause