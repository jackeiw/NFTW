@color 2
@echo ��ʼж�ط���
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u %~dp0PatchService.exe

@echo ����ж�سɹ�

@echo ���ڹر����еĽ���...
@echo off&taskkill /f /im "PatchService.exe"&exit

@Pause