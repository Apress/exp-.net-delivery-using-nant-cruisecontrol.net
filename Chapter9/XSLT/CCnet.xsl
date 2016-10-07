<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" encoding="utf-8" />
	<xsl:template match="ProjectSet">
		<cruisecontrol>
			<xsl:for-each select="Projects/Project">
				<xsl:variable name="ProjectName"><xsl:value-of select="../../Settings/@CompanyName" />.<xsl:value-of select="@Name" /></xsl:variable>
				<project name="{$ProjectName}">
					<webURL><xsl:value-of select="../../Settings/@CcnetUrl" />/Controller.aspx?_action_ViewProjectReport=true&amp;server=local&amp;project=<xsl:value-of select="$ProjectName" /></webURL>
					<artifactDirectory><xsl:value-of select="../../Settings/@EnvironmentMain" />\Publish\<xsl:value-of select="$ProjectName" />\</artifactDirectory>
					<modificationDelaySeconds>10</modificationDelaySeconds>
					<triggers>
						<intervalTrigger />
					</triggers>
					<sourcecontrol type="vss" autoGetSource="true">
						<ssdir>"<xsl:value-of select="../../Settings/@VssFolder" />"</ssdir>
						<project>$/Solutions/<xsl:value-of select="$ProjectName" />/</project>
						<username>
							<xsl:value-of select="S../../Settings/@VssUsername" />
						</username>
						<password>
							<xsl:value-of select="../../Settings/@VssPassword" />
						</password>
						<workingDirectory><xsl:value-of select="../../Settings/@EnvironmentMain" />\Source\<xsl:value-of select="$ProjectName" /></workingDirectory>
					</sourcecontrol>
					<build type="nant">
						<baseDirectory>D:\dotNetDelivery\Chapter9\</baseDirectory>
						<buildArgs>-D:debug=false</buildArgs>
						<buildFile><xsl:value-of select="$ProjectName" />.Build.xml</buildFile>
						<targetList>
							<target>ci</target>
						</targetList>
						<buildTimeoutSeconds>300</buildTimeoutSeconds>
					</build>
					<labeller type="defaultlabeller">
						<prefix>1.0.</prefix>
					</labeller>
					<tasks>
						<merge>
							<files>
								<file><xsl:value-of select="../../Settings/@EnvironmentMain" />\Reports\<xsl:value-of select="$ProjectName" />\*-results.xml</file>
								<file><xsl:value-of select="../../Settings/@EnvironmentMain" />\Reports\<xsl:value-of select="$ProjectName" />\fxcop.xml</file>
							</files>
						</merge>
					</tasks>
					<publishers>
						<xmllogger />
					</publishers>
				</project>
			</xsl:for-each>
		</cruisecontrol>
	</xsl:template>
</xsl:stylesheet>