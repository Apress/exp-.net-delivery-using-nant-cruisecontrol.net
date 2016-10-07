<VisualStudioProject>
    <CSHARP
        ProjectType = "Local"
        ProductVersion = "7.10.3077"
        SchemaVersion = "2.0"
        ProjectGuid = "{8BF8E570-F51A-48DB-A7B9-90D3CAF2AE1D}"
        SccProjectName = '"$/Solutions/Etomic.Transformer.Win", DJAAAAAA'
        SccLocalPath = ".."
        SccProvider = "MSSCCI:Microsoft Visual SourceSafe"
    >
        <Build>
            <Settings
                ApplicationIcon = "App.ico"
                AssemblyKeyContainerName = ""
                AssemblyName = "Etomic.Transformer.Win.UI"
                AssemblyOriginatorKeyFile = ""
                DefaultClientScript = "JScript"
                DefaultHTMLPageLayout = "Grid"
                DefaultTargetSchema = "IE50"
                DelaySign = "false"
                OutputType = "WinExe"
                PreBuildEvent = ""
                PostBuildEvent = ""
                RootNamespace = "Etomic.Transformer.Win.UI"
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
                    DocumentationFile = "docs\Etomic.Transformer.Win.UIi.xml"
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
                    Name = "System.Drawing"
                    AssemblyName = "System.Drawing"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Drawing.dll"
                />
                <Reference
                    Name = "System.Windows.Forms"
                    AssemblyName = "System.Windows.Forms"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Windows.Forms.dll"
                />
                <Reference
                    Name = "System.XML"
                    AssemblyName = "System.Xml"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.XML.dll"
                />
                <Reference
                    Name = "Etomic.Library.Transformer.Engine"
                    AssemblyName = "Etomic.Library.Transformer.Engine"
                    HintPath = "..\..\..\..\Assemblies\Etomic.Library.Transformer\Latest\Etomic.Library.Transformer.Engine.dll"
                />
            </References>
        </Build>
        <Files>
            <Include>
                <File
                    RelPath = "App.ico"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "AssemblyInfo.cs"
                    SubType = "Code"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "CommonAssemblyInfo.cs"
                    Link = "..\CommonAssemblyInfo.cs"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "MainForm.cs"
                    SubType = "Form"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "MainForm.resx"
                    DependentUpon = "MainForm.cs"
                    BuildAction = "EmbeddedResource"
                />
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>

