<VisualStudioProject>
    <CSHARP
        ProjectType = "Local"
        ProductVersion = "7.10.3077"
        SchemaVersion = "2.0"
        ProjectGuid = "{66BF30CD-0579-44BB-8DD9-E0DA3873883C}"
        SccProjectName = '"$/Solutions/Etomic.Transformer.Web", OJAAAAAA'
        SccLocalPath = ".."
        SccProvider = "MSSCCI:Microsoft Visual SourceSafe"
    >
        <Build>
            <Settings
                ApplicationIcon = ""
                AssemblyKeyContainerName = ""
                AssemblyName = "Etomic.Transformer.Web.UI"
                AssemblyOriginatorKeyFile = ""
                DefaultClientScript = "JScript"
                DefaultHTMLPageLayout = "Grid"
                DefaultTargetSchema = "IE50"
                DelaySign = "false"
                OutputType = "Library"
                PreBuildEvent = ""
                PostBuildEvent = ""
                RootNamespace = "Etomic.Transformer.Web.UI"
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
                    DocumentationFile = "docs\Etomic.Transformer.Web.UI.xml"
                    DebugSymbols = "true"
                    FileAlignment = "4096"
                    IncrementalBuild = "false"
                    NoStdLib = "false"
                    NoWarn = ""
                    Optimize = "false"
                    OutputPath = "bin\"
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
                    OutputPath = "bin\"
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
                    Name = "System.Web"
                    AssemblyName = "System.Web"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Web.dll"
                />
                <Reference
                    Name = "System.Drawing"
                    AssemblyName = "System.Drawing"
                    HintPath = "C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\System.Drawing.dll"
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
                    RelPath = "Default.aspx"
                    SubType = "Form"
                    BuildAction = "Content"
                />
                <File
                    RelPath = "Default.aspx.cs"
                    DependentUpon = "Default.aspx"
                    SubType = "ASPXCodeBehind"
                    BuildAction = "Compile"
                />
                <File
                    RelPath = "Default.aspx.resx"
                    DependentUpon = "Default.aspx.cs"
                    BuildAction = "EmbeddedResource"
                />
                <File
                    RelPath = "Web.config"
                    BuildAction = "None"
                />
                <File
                    RelPath = "CSS\style.css"
                    BuildAction = "Content"
                />
            </Include>
        </Files>
    </CSHARP>
</VisualStudioProject>

