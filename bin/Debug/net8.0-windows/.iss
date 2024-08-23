[Setup]
AppName=My Application
AppVersion=1.0
DefaultDirName={pf}\My Application
DefaultGroupName=My Application
OutputBaseFilename=MyAppInstaller
Compression=lzma
SolidCompression=yes

[Files]
Source: "path\to\your\executable.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "path\to\your\additionalfiles\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\My Application"; Filename: "{app}\executable.exe"
Name: "{group}\Uninstall My Application"; Filename: "{uninstallexe}"
