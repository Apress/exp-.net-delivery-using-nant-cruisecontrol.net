<?xml version="1.0" encoding="utf-8"?>
<FxCopProject Version="1.3" Name="Transformer">
 <ProjectOptions>
  <SharedProject>False</SharedProject>
  <Stylesheet Apply="False">D:\Tools\FxCop\1.30\Xml\FxCopReport.xsl</Stylesheet>
  <SaveMessages>
   <Project Status="Active, Excluded" NewOnly="False" />
   <Report Status="Active" NewOnly="False" />
  </SaveMessages>
  <ProjectFile Compress="True" DefaultTargetCheck="True" DefaultRuleCheck="True" SaveByRuleGroup="" Deterministic="False" />
  <PermitAnalysis>True</PermitAnalysis>
  <SourceLookup>True</SourceLookup>
  <AnalysisExceptionsThreshold>100</AnalysisExceptionsThreshold>
  <RuleExceptionsThreshold>10</RuleExceptionsThreshold>
  <Spelling Locale="en-us" />
 </ProjectOptions>
 <Targets>
  <AssemblyReferenceDirectories>
   <Directory>D:\BookCode\Projects\TransformerEngine\bin\Debug\</Directory>
   <Directory>D:\BookCode\Projects\TransformerGui\bin\Debug\</Directory>
   <Directory>D:\BookCode\Projects\TransformerWeb\bin\</Directory>
  </AssemblyReferenceDirectories>
  <Target Name="D:\BookCode\Projects\Transformer\TransformerGui\bin\Debug\TransformerGui.exe" Analyze="True" AnalyzeAllChildren="True" />
  <Target Name="D:\BookCode\Projects\Transformer\TransformerGui\bin\Debug\TransformerEngine.dll" Analyze="True" AnalyzeAllChildren="True" />
 </Targets>
 <Rules>
  <RuleFiles>
   <RuleFile Name="$(FxCopDir)\Rules\DesignRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\UsageRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\PerformanceRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\GlobalizationRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\ComRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\NamingRules.dll" Enabled="True" AllRulesEnabled="True" />
   <RuleFile Name="$(FxCopDir)\Rules\SecurityRules.dll" Enabled="True" AllRulesEnabled="True" />
  </RuleFiles>
  <Groups />
  <Settings />
 </Rules>
 <FxCopReport Version="1.3" LastAnalysis="2004-11-08 21:16:47Z">
  <Namespaces>
   <Namespace Name="TransformerEngine">
    <Messages>
     <Message Status="Active" Created="2004-10-28 21:33:26Z">
      <Rule TypeName="AvoidNamespacesWithFewMembers" />
      <Issues>
       <Issue Certainty="50" Level="Warning">
        <Resolution>
         <Data>
          <Items>
           <Item>TransformerEngine</Item>
          </Items>
         </Data>
        </Resolution>
       </Issue>
      </Issues>
     </Message>
    </Messages>
   </Namespace>
  </Namespaces>
  <Targets>
   <Target Name="D:\BookCode\VSS\Solutions\Transformer\TransformerGui\bin\Debug\TransformerGui.exe">
    <Messages>
     <Message Status="Active" Created="2004-10-28 21:34:18Z">
      <Rule TypeName="AssembliesHaveStrongNames" />
      <Issues>
       <Issue Certainty="95" Level="Error">
        <Resolution>
         <Data>
          <Items>
           <Item>TransformerGui</Item>
          </Items>
         </Data>
        </Resolution>
       </Issue>
      </Issues>
     </Message>
     <Message Status="Active" Created="2004-10-28 21:34:18Z">
      <Rule TypeName="AssembliesHavePermissionRequests" />
      <Issues>
       <Issue Certainty="99" Level="CriticalError">
        <Resolution>
         <Data>
          <Items>
           <Item>TransformerGui</Item>
          </Items>
         </Data>
        </Resolution>
       </Issue>
      </Issues>
     </Message>
    </Messages>
    <Modules>
     <Module Name="transformergui.exe">
      <Namespaces>
       <Namespace Name="TransformerGui">
        <Classes>
         <Class Name="MainForm">
          <Methods>
           <Method Name="btnTransform_Click(System.Object,System.EventArgs):System.Void">
            <Messages>
             <Message Status="Active" Created="2004-11-08 21:16:47Z">
              <Rule TypeName="ExceptionAndSystemExceptionTypesAreNotCaught" />
              <Issues>
               <Issue Certainty="95" Level="CriticalError">
                <SourceCode Path="D:\BookCode\VSS\Solutions\Transformer\TransformerGui" File="MainForm.cs" Line="118" />
                <Resolution>
                 <Data>
                  <Items>
                   <Item>btnTransform_Click</Item>
                   <Item>Exception</Item>
                  </Items>
                 </Data>
                </Resolution>
               </Issue>
              </Issues>
             </Message>
            </Messages>
           </Method>
          </Methods>
         </Class>
        </Classes>
       </Namespace>
      </Namespaces>
     </Module>
    </Modules>
   </Target>
   <Target Name="D:\BookCode\VSS\Solutions\Transformer\TransformerGui\bin\Debug\TransformerEngine.dll">
    <Messages>
     <Message Status="Active" Created="2004-10-28 21:33:26Z">
      <Rule TypeName="AssembliesHaveStrongNames" />
      <Issues>
       <Issue Certainty="95" Level="Error">
        <Resolution>
         <Data>
          <Items>
           <Item>TransformerEngine</Item>
          </Items>
         </Data>
        </Resolution>
       </Issue>
      </Issues>
     </Message>
     <Message Status="Active" Created="2004-10-28 21:33:26Z">
      <Rule TypeName="AssembliesHavePermissionRequests" />
      <Issues>
       <Issue Certainty="99" Level="CriticalError">
        <Resolution>
         <Data>
          <Items>
           <Item>TransformerEngine</Item>
          </Items>
         </Data>
        </Resolution>
       </Issue>
      </Issues>
     </Message>
    </Messages>
   </Target>
  </Targets>
  <Rules>
   <Rule TypeName="AssembliesHavePermissionRequests">
    <Name>Assemblies specify permission requests</Name>
    <Description>Permission requests prevent security exceptions from being thrown after code in an assembly has already begun executing. With permission requests, the assembly does not load if it has insufficient permissions. This rule will fire if you have specified a permission request incorrectly, or incompletely. If you have specified requests but FxCop reports a violation for this rule, use the PermView.exe tool to view the security permissions in the assembly. An unenforceable permission appears as an empty permission set.</Description>
    <LongDescription>You should add attributes specifying what permissions your assembly will demand, might demand, and what permissions it does not want granted.  For example, the following attribute indicates that an assembly will, at minimum, require read access to the USERNAME environment variable: [assembly:EnvironmentPermissionAttribute(SecurityAction.RequestMinimum,
Read="USERNAME")]. To specify permissions that the assembly might demand, use SecurityAction.RequestOptional. To specify permissions that the assembly must not be granted, use SecurityAction.RequestRefuse.</LongDescription>
    <GroupOwner>MS FxCopDev</GroupOwner>
    <DevOwner />
    <Url>http://www.gotdotnet.com/team/fxcop/docs/rules/UsageRules/AssembliesPermissionRequests.html</Url>
    <Email>askfxcop@microsoft.com</Email>
    <MessageLevel Certainty="99">CriticalError</MessageLevel>
    <File Name="UsageRules.dll" Version="1.30.0.0" />
   </Rule>
   <Rule TypeName="AssembliesHaveStrongNames">
    <Name>Assemblies have strong names</Name>
    <Description>Assemblies with strong names can be placed in the global assembly cache and can be checked for tampering at run time.</Description>
    <LongDescription>The common language runtime compares the key in the assembly's manifest to the key used to generate the strong name, to ensure that the assembly's bits have not been tampered with.</LongDescription>
    <GroupOwner>MS FxCopDev</GroupOwner>
    <DevOwner />
    <Url>http://www.gotdotnet.com/team/fxcop/docs/rules/DesignRules/AssembliesHaveStrongNames.html</Url>
    <Email>askfxcop@microsoft.com</Email>
    <MessageLevel Certainty="95">Error</MessageLevel>
    <File Name="DesignRules.dll" Version="1.30.0.0" />
   </Rule>
   <Rule TypeName="AvoidNamespacesWithFewMembers">
    <Name>Avoid having a namespace with a small number of types</Name>
    <Description>A namespace should generally have more than five types.</Description>
    <LongDescription />
    <GroupOwner>MS FxCopDev</GroupOwner>
    <DevOwner />
    <Url>http://www.gotdotnet.com/team/fxcop/docs/rules/DesignRules/AvoidSmallNamespaces.html</Url>
    <Email>askfxcop@microsoft.com</Email>
    <MessageLevel Certainty="50">Warning</MessageLevel>
    <File Name="DesignRules.dll" Version="1.30.0.0" />
   </Rule>
   <Rule TypeName="ExceptionAndSystemExceptionTypesAreNotCaught">
    <Name>System.Exception and System.SystemException are not caught</Name>
    <Description>You should not catch Exception or SystemException.</Description>
    <LongDescription>Catching generic exception types can hide run-time problems from the library user, and can complicate debugging. You should catch only those exceptions that you can handle gracefully.</LongDescription>
    <GroupOwner>MS FxCopDev</GroupOwner>
    <DevOwner />
    <Url>http://www.gotdotnet.com/team/fxcop/docs/rules/DesignRules/ExceptionAndSystemException.html</Url>
    <Email>askfxcop@microsoft.com</Email>
    <MessageLevel Certainty="95">CriticalError</MessageLevel>
    <File Name="DesignRules.dll" Version="1.30.0.0" />
   </Rule>
  </Rules>
 </FxCopReport>
</FxCopProject>
