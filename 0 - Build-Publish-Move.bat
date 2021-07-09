@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Atomic\bin\Release\Panosen.Atomic.*.nupkg D:\LocalSavoryNuget\

pause