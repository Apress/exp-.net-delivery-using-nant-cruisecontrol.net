<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:param name="ProjectData" />
	<xsl:output method="xml" indent="yes" encoding="utf-8" />
	
	<xsl:template match="ProjectSet">
		<xsl:variable name="ProjectName"><xsl:value-of select="Settings/@CompanyName" />.<xsl:value-of select="$ProjectData/@Name" /></xsl:variable>
		<project name="{$ProjectName}" default="help">
			<description>Build file for the <xsl:value-of select="$ProjectName" /> system.</description>
			
			<xsl:variable name="company.name" select="Settings/@CompanyName" />
			<property name="nant.onfailure" value="fail" />
			<property name="company.name" value="{Settings/@CompanyName}" />
			<property name="solution.name" value="{$ProjectName}" />
			<property name="core.directory" value="{Settings/@EnvironmentMain}" />
			<property name="core.source"><xsl:attribute name="value">${core.directory}\Source\${solution.name}</xsl:attribute></property>
			<property name="core.output"><xsl:attribute name="value">${core.directory}\Output\${solution.name}</xsl:attribute></property>
			<property name="core.docs"><xsl:attribute name="value">${core.directory}\Docs\${solution.name}</xsl:attribute></property>
			<property name="core.reports"><xsl:attribute name="value">${core.directory}\Reports\${solution.name}</xsl:attribute></property>
			<property name="core.distribution"><xsl:attribute name="value">${core.directory}\Distribution\${solution.name}</xsl:attribute></property>
			<property name="core.publish"><xsl:attribute name="value">${core.directory}\Publish\${solution.name}</xsl:attribute></property>

			<property name="vss.dbpath" value="{Settings/@VssFolder}\srcsafe.ini" />
			<property name="vss.path" value="$/Solutions/{$ProjectName}/" />
			
			<sysinfo />
			
			<loadtasks assembly="D:\dotNetDelivery\Tools\NAntContrib\0.85rc2\bin\NAnt.Contrib.Tasks.dll"/>
			<loadtasks assembly="D:\dotNetDelivery\Tools\NUnit2Report\1.2.2\bin\NAnt.NUnit2ReportTasks.dll"/>
			<loadtasks assembly="D:\dotNetDelivery\Tools\Etomic.NAntExtensions\Etomic.NAntExtensions.GeneralTasks.dll"/>
			
			<target name="go" description="The main target for full build process execution." depends="clean, get, version1, version2, build, test, document, publish, notify" />
			<target name="ci" description="The main target for continuous integration." depends="clean, version1, version2, build, test, document, publish, notify" />
			<target name="clean" description="Clean up the build environment.">
				<delete failonerror="false"><xsl:attribute name="dir">${core.output}\</xsl:attribute></delete>
				<delete failonerror="false"><xsl:attribute name="dir">${core.docs}\</xsl:attribute></delete>
				<delete failonerror="false"><xsl:attribute name="dir">${core.reports}\</xsl:attribute></delete>
				<delete failonerror="false"><xsl:attribute name="dir">${core.distribution}\</xsl:attribute></delete>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.source}\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.output}\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.docs}\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.reports}\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.distribution}\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.publish}\</xsl:attribute></mkdir>
			</target>
			<target name="get" description="Grab the source code.">
				<vssget user="{Settings/@VssUsername}" password="{Settings/@VssPassword}" recursive="true" replace="true">
					<xsl:attribute name="localpath">${core.source}\</xsl:attribute>
					<xsl:attribute name="dbpath">${vss.dbpath}\</xsl:attribute>
					<xsl:attribute name="path">${vss.path}\</xsl:attribute>
				</vssget>
			</target>
			<target name="version1" description="Apply versioning to the source code files.">
				<property name="sys.version" value="0.0.0.0" />
				<ifnot>
					<xsl:attribute name="test">${debug}</xsl:attribute>
					<property name="sys.version"><xsl:attribute name="value">${ccnet.label}.0</xsl:attribute></property>
				</ifnot>
				<attrib readonly="false">
					<xsl:attribute name="file">${core.source}\CommonAssemblyInfo.cs</xsl:attribute>
				</attrib>
				<asminfo language="CSharp">
					<xsl:attribute name="output">${core.source}\CommonAssemblyInfo.cs</xsl:attribute>
					<imports>
						<import name="System" />
						<import name="System.Reflection" />
					</imports>
					<attributes>
						<attribute type="AssemblyVersionAttribute">
							<xsl:attribute name="value">${sys.version}</xsl:attribute>
						</attribute>
						<attribute type="AssemblyProductAttribute" value="{$ProjectName}" />
						<attribute type="AssemblyCopyrightAttribute">
							<xsl:attribute name="value">Copyright (c) 2005, ${company.name} Ltd.</xsl:attribute>
						</attribute>
					</attributes>
				</asminfo>
				<attrib readonly="true">
					<xsl:attribute name="file">${core.source}\CommonAssemblyInfo.cs</xsl:attribute>
				</attrib>
			</target>
			<target name="version2">
				<ifnot>
					<xsl:attribute name="test">${debug}</xsl:attribute>
					<vsslabel user="{Settings/@VssUsername}" password="{Settings/@VssPassword}" comment="Automated Label">
						<xsl:attribute name="dbpath">${vss.dbpath}</xsl:attribute>
						<xsl:attribute name="path">${vss.path}</xsl:attribute>
						<xsl:attribute name="label">NAnt - ${sys.version}</xsl:attribute>
					</vsslabel>
				</ifnot>
			</target>
			<target name="build" description="Compile the application.">
				<solution configuration="Debug">
					<xsl:attribute name="solutionfile">${core.source}\${solution.name}.sln</xsl:attribute>
					<xsl:attribute name="outputdir">${core.output}\</xsl:attribute>
				</solution>
			</target>
			<target name="test" description="Apply the unit tests.">
				<property name="nant.onfailure" value="fail.test" />
				<nunit2>
					<formatter type="Xml" usefile="true" extension=".xml">
						<xsl:attribute name="outputdir">${core.reports}\</xsl:attribute>
					</formatter>
					<test>
						<assemblies>
							<xsl:attribute name="basedir">${core.output}\</xsl:attribute>
							<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldTest='true']">
								<include name="{$ProjectName}.{@Name}.{@AssemblyType}" />
							</xsl:for-each>
						</assemblies>
					</test>
				</nunit2>
				<nunit2report>
					<xsl:attribute name="out">${core.reports}\NUnit.html</xsl:attribute>
					<fileset>
							<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldTest='true']">
								<include name="${core.reports}\{$ProjectName}.{@Name}.{@AssemblyType}-results.xml" />
							</xsl:for-each>
					</fileset>
				</nunit2report>
				<fxcop executable="D:\dotNetDelivery\Tools\FxCop\1.30\FxCopCmd.exe" failonerror="false">
					<xsl:attribute name="report">${core.reports}\fxcop.xml</xsl:attribute>
					<targets>
						<xsl:attribute name="basedir">${core.output}</xsl:attribute>
						<include name="{$ProjectName}*.dll" />
						<include name="{$ProjectName}*.exe" />
						<exclude name="*Tests*" />
					</targets>
					<ruleset basedir=" D:\dotNetDelivery\Tools\FxCop\1.30\Rules">
						<include name="*.dll" />
					</ruleset>
				</fxcop>
				<style style="D:\dotNetDelivery\Tools\FxCop\1.30\Xml\FxCopReport.xsl">
					<xsl:attribute name="in">${core.reports}\fxcop.xml</xsl:attribute>
					<xsl:attribute name="out">${core.reports}\fxcop.html</xsl:attribute>
				</style>
				<property name="nant.onfailure" value="fail" />
			</target>
			<target name="document" description="Generate documentation and reports.">
				<ndoc>
					<assemblies>
						<xsl:attribute name="basedir">${core.output}\</xsl:attribute>
						<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldDocument='true']">
							<include name="{$ProjectName}.{@Name}.{@AssemblyType}" />
						</xsl:for-each>
					</assemblies>
					<summaries>
						<xsl:attribute name="basedir">${core.output}\</xsl:attribute>
						<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldDocument='true']">
							<include name="{$ProjectName}.{@Name}.xml" />
						</xsl:for-each>
					</summaries>
					<documenters>
						<documenter name="MSDN">
							<property name="OutputDirectory"><xsl:attribute name="value">${core.docs}\</xsl:attribute></property>
							<property name="HtmlHelpName" value="{$ProjectName}" />
							<property name="HtmlHelpCompilerFilename" value="hhc.exe" />
							<property name="IncludeFavorites" value="False" />
							<property name="Title" value="{$ProjectName} (NDoc)" />
							<property name="SplitTOCs" value="False" />
							<property name="DefaulTOC" value="" />
							<property name="ShowVisualBasic" value="False" />
							<property name="ShowMissingSummaries" value="True" />
							<property name="ShowMissingRemarks" value="False" />
							<property name="ShowMissingParams" value="True" />
							<property name="ShowMissingReturns" value="True" />
							<property name="ShowMissingValues" value="True" />
							<property name="DocumentInternals" value="True" />
							<property name="DocumentProtected" value="True" />
							<property name="DocumentPrivates" value="False" />
							<property name="DocumentEmptyNamespaces" value="False" />
							<property name="IncludeAssemblyVersion" value="True" />
							<property name="CopyrightText" value="{Settings/@CompanyName} Ltd., 2005" />
							<property name="CopyrightHref" value="" />
						</documenter>
					</documenters>
				</ndoc>
			</target>
			<target name="publish" description="Place the compiled assets, reports etc. in agreed location.">
				<copy>
					<xsl:attribute name="todir">${core.distribution}\</xsl:attribute>
					<fileset>
						<xsl:attribute name="basedir">${core.output}\</xsl:attribute>
						<include name="*.dll" />
						<include name="*.exe" />
						<exclude name="*Tests*" />
					</fileset>
				</copy>
				<zip>
					<xsl:attribute name="zipfile">${core.publish}\<xsl:value-of select="$ProjectName"/>-Build-${sys.version}.zip</xsl:attribute>
					<fileset>
						<xsl:attribute name="basedir">${core.distribution}\</xsl:attribute>
						<include name="**" />
					</fileset>
				</zip>
			</target>
			<target name="notify" description="Tell everyone of the success or failure.">
				<echo message="Notifying you of the build process success." />
			</target>
			<target name="fail">
				<echo message="Notifying you of a failure in the build process." />
			</target>
			<target name="fail.test">
				<nunit2report>
					<xsl:attribute name="out">${core.reports}\NUnit.html</xsl:attribute>
					<fileset>
						<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldTest='true']">
							<include name="${core.reports}\{$ProjectName}.{@Name}.{@AssemblyType}-results.xml" />
						</xsl:for-each>
					</fileset>
				</nunit2report>
			</target>
			<target name="help">
				<echo message="The skeleton file for the build process is designed to execute the following targets in turn:" />
				<echo message="-- clean" />
				<echo message="-- get" />
				<echo message="-- version" />
				<echo message="-- build" />
				<echo message="-- test" />
				<echo message="-- document" />
				<echo message="-- publish" />
				<echo message="-- notify" />
				<echo message="This file should be run with a Boolean value for 'debug'." />
				<echo message="-- True indicates that no versioning be set (0.0.0.0)." />
				<echo message="-- False indicates that a regular version be set(1.0.x.0)." />
				<echo message="Example: -D:debug=true" />
			</target>
		</project>
	</xsl:template>
</xsl:stylesheet>