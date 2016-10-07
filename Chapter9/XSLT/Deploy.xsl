<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output indent="yes" encoding="utf-8" />
	<xsl:param name="ProjectData" />
	<xsl:template match="ProjectSet">
		<xsl:variable name="ProjectName"><xsl:value-of select="Settings/@CompanyName" />.<xsl:value-of select="$ProjectData/@Name" /></xsl:variable>
		
		<project name="{$ProjectName}" default="help">
			<description>Deploy file for the <xsl:value-of select="$ProjectName" /> application</description>
			<property name="nant.onfailure" value="fail" />
			<property name="company.name" value="{Settings/@CompanyName}" />
			<property name="solution.name" value="{$ProjectName}" />
			<property name="core.publish" value="http://localhost/ccnet/files/{$ProjectName}" />
			<property name="core.deploy" value="{Settings/@EnvironmentTempDeploy}" />
			<property name="core.environment" value="D:\dotNetDelivery\Assemblies" />
			<target name="go" description="The main target for full deploy process execution." depends="selectversion, get, createenvironments, position, notify" />
			<target name="selectversion" description="Selects the correct version of the system.">
				<if>
					<xsl:attribute name="test">${debug}</xsl:attribute>
					<property name="sys.version" value="0.0.0.0" />
				</if>
			</target>
			<target name="get" description="Grab the correct assets.">
				<delete failonerror="false"><xsl:attribute name="dir">${core.deploy}\</xsl:attribute></delete>
				<mkdir><xsl:attribute name="dir">${core.deploy}\${sys.version}\</xsl:attribute></mkdir>
				<get>
					<xsl:attribute name="src">${core.publish}/${solution.name}-Build-${sys.version}.zip</xsl:attribute>
					<xsl:attribute name="dest">${core.deploy}\${solution.name}-Build-${sys.version}.zip</xsl:attribute>
				</get>
				<unzip>
					<xsl:attribute name="zipfile">${core.deploy}\${solution.name}-Build-${sys.version}.zip</xsl:attribute>
					<xsl:attribute name="todir">${core.deploy}\${sys.version}\</xsl:attribute>
				</unzip>
			</target>
			<target name="createenvironments" description="Create the environments required">
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.environment}\${solution.name}\Latest\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.environment}\${solution.name}\Specific\</xsl:attribute></mkdir>
				<mkdir failonerror="false"><xsl:attribute name="dir">${core.environment}\${solution.name}\Deprecated\</xsl:attribute></mkdir>
			</target>
			<target name="position" description="Place required assets">
				<xsl:for-each select="$ProjectData/OutputAssemblies/OutputAssembly[@ShouldTest='false']">
					<copy overwrite="true">
						<xsl:attribute name="file">${core.deploy}\${sys.version}\<xsl:value-of select="$ProjectName" />.<xsl:value-of select="@Name" />.<xsl:value-of select="@AssemblyType" /></xsl:attribute>
						<xsl:attribute name="todir">${core.environment}\${solution.name}\Latest\</xsl:attribute>
					</copy>
					<copy overwrite="true">
						<xsl:attribute name="file">${core.deploy}\${sys.version}\<xsl:value-of select="$ProjectName" />.<xsl:value-of select="@Name" />.<xsl:value-of select="@AssemblyType" /></xsl:attribute>
						<xsl:attribute name="tofile">${core.environment}\${solution.name}\Specific\<xsl:value-of select="$ProjectName" />.<xsl:value-of select="@Name" />_${sys.version}.<xsl:value-of select="@AssemblyType" /></xsl:attribute>
					</copy>
				</xsl:for-each>
			</target>
			<target name="notify" description="Tell everyone of the success or failure.">
				<echo message="Notifying you of the deploy process success." />
			</target>
			<target name="fail">
				<echo message="Notifying you of a failure in the deploy process." />
			</target>
			<target name="help">
				<echo message="The deploy file is designed to execute the following targets in turn:" />
				<echo message="-- selectversion" />
				<echo message="-- get" />
				<echo message="-- createenvironments" />
				<echo message="-- position" />
				<echo message="-- notify" />
			</target>
		</project>
	</xsl:template>
</xsl:stylesheet>