<VisualStudioProject>
    <CSHARP
        ProjectType = "Local"
        ProductVersion = "7.10.3077"
        SchemaVersion = "2.0"
        ProjectGuid = "{F5F6F51E-EAB4-4B07-B2D7-5620E9CA6FDE}"
        SccProjectName = '"$/Solutions/Etomic.VSSManager", UNAAAAAA'
        SccLocalPath = ".."
        SccProvider = "MSSCCI:Microsoft Visual SourceSafe"
    >
        <Build>
            <Settings
                ApplicationIcon = ""
                AssemblyKeyContainerName = ""
                AssemblyName = "Etomic.VSSManager.ServerConsole"
                AssemblyOriginatorKeyFile = ""
                DefaultClientScript = "JScript"
                DefaultHTMLPageLayout = "Grid"
                DefaultTargetSchema = "IE50"
                DelaySign = "false"
                OutputType = "Exe"
                PreBuildEvent = ""
                PostBuildEvent = ""
                RootNamespace = "Etomic.VSSManager.ServerConsole"
                RunPostBuildEvent = "OnBuildSuccess"
                StartupObject = ""
            >
                <Config
                    Name = "Debug"
                    AllowUnsafeBlocks = "false"
                    BaseAddress = "285212672"
                    CheckForOverflowUnderflow = "false"
                    ConfigurationOverrideFile = ""
                    DefineConstants = "DEBUG;TRACE"
                    DocumentationFile = ""
                    DebugSymbols = "true"
                    FileAlignment = "4096"
                    IncrementalBuild = "false"
                    NoStdLib = "false"
                    NoWarn = ""
                    Optimize = "false"
                    OutputPath = "bin\Debug\"
                    RegisterForComInterop = "false"
                    RemoveIntegerChecks = "false"
                    TreatWarningsAsErrors = "false"
                    WarningLevel = "4"
                />
                <Config
                    Name = "Release"
                    AllowUnsafeBlocks = "false"
                    BaseAddress = "285212672"
                    CheckForOverflowUnderflow = "false"
                    ConfigurationOverrideFile = ""
                    DefineConstants = "TRACE"
                    DocumentationFile = ""
                    DebugSymbols = "false"
                    FileAlignment = "4096"
                    IncrementalBuild = "false"
                    NoStdLib = "false"
                    NoWarn = ""
                    Optimize = "true"
                    OutputPath = "bin\Release\"
                    RegisterForComInterop = "false"
                    RemoveIntegerChecks = "false"
                    TreatWarningsAsErrors = "false"
                    WarningLevel = "4"
                />
            </Settings>
            <References>
                <Reference
                    Name = "System"
                    AssemblyName = "System"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.dll"
                />
                <Reference
                    Name = "System.Data"
                    AssemblyName = "System.Data"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Data.dll"
                />
                <Reference
                    Name = "System.XML"
                    AssemblyName = "System.Xml"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.XML.dll"
                />
                <Reference
                    Name = "Server"
                    Project = "{038170F5-7AE6-467E-B940-1F5ED8B61945}"
                    Package = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"
                />
                <Reference
                    Name = "System.Runtime.Remoting"
                    AssemblyName = "System.Runtime.Remoting"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Runtime.Remoting.dll"
                />
                <Reference
                    Name = "Remote"
                    Project = "{8775DAB9-42FE-4FA2-BCE7-38EA355EC4C3}"
                    Package = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"
                />
            </References>
        </Build>
        <Files>
            <Include>
                <File
                    RelPath = "App.config"
                    BuildAction = "None"
                />
                <File
                    RelPath = "AssemblyInfo.cs"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "CommonAssemblyInfo.cs"
                    Link = "..\CommonAssemblyInfo.cs"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "ServerStartup.cs"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>

