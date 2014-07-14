

md ..\NugetPackage\lib
md ..\NugetPackage\lib\wp8
md ..\NugetPackage\lib\portable-win8+wpa81
md ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable
md ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable\Images
md ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable\Themes


xcopy  /Y ..\AnimationManager.WP8\Bin\Release\*.* ..\NugetPackage\lib\wp8

xcopy  /Y ..\AnimationManager.Shared\Images ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable\Images
xcopy  /Y ..\AnimationManager.Portable\bin\Release\Themes ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable\Themes
xcopy  /Y ..\AnimationManager.Portable\bin\Release\*.xbf ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable
xcopy  /Y ..\AnimationManager.Portable\bin\Release\*.xr.xml ..\NugetPackage\lib\portable-win8+wpa81\AnimationManager.Portable
xcopy  /Y ..\AnimationManager.Portable\bin\Release\AnimationManager.Portable.* ..\NugetPackage\lib\portable-win8+wpa81\

mkdir c:\NuGet
"..\.nuget\nuget" pack "Package.nuspec" 

pause
