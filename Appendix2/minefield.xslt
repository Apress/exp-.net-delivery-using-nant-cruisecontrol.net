<?xml version="1.0" encoding="UTF-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html"/>

<xsl:template match="/">
	<html>
		<head>
			<link rel="stylesheet" type="text/css" href="minefield.css"></link>
		</head>
		<body>
			<H1>NAnt Sweeper</H1>
			<H2>Written by <a href="mailto:alexhildyard@hotmail.com">Alex Hildyard</a>, Edinburgh 2004</H2>
			<br/>
			<hr/>
			<table cellpadding="2" bgcolor="#c0c0c0">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="1" bgcolor="#e0e0e0">
							<xsl:apply-templates></xsl:apply-templates>
						</table>
					</td>
				</tr>
			</table>
			<br/>
			<table cols="3" cellspacing="0" cellpadding="0" width="170">
				<tr>
					<td width="30%">
						<xsl:choose>
							<xsl:when test="//@status='0'">
								<img src="smile.gif"/>
							</xsl:when>
							<xsl:when test="//@status='1'">
								<img src="shades.gif"/>
							</xsl:when>
							<xsl:when test="//@status='2'">
								<img src="frown.gif"/>
							</xsl:when>
						</xsl:choose>
					</td>
					<td width="30%">
						Turns:
					</td>
					<td align="right">
						<xsl:value-of select="//@turns"/>
					</td>
				</tr>
			</table>
			<hr/>
		</body>
	</html>
</xsl:template>

<xsl:template match="row">
	<tr>
		<xsl:call-template name="processRow">
			<xsl:with-param name="this_row" select="."/>
			<xsl:with-param name="columns" select="//@columns"/>
		</xsl:call-template>
	</tr>
</xsl:template>

<xsl:template name="processRow">
	<xsl:param name="counter" select="0"/>
	<xsl:param name="columns" select="0"/>
	<xsl:param name="this_row" select="."/>
	<xsl:choose>
		<xsl:when test="substring($this_row, $counter, 1)='?'">
			<td><img src="square.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='X'">
			<td><img src="square.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)=' '">
			<td><img src="0.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='1'">
			<td><img src="1.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='2'">
			<td><img src="2.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='3'">
			<td><img src="3.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='4'">
			<td><img src="4.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='Z'">
			<td><img src="bang.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='M'">
			<td><img src="mine.gif"/></td>
		</xsl:when>
		<xsl:when test="substring($this_row, $counter, 1)='F'">
			<td><img src="flag.gif"/></td>
		</xsl:when>
	</xsl:choose>
	<xsl:if test="$counter &lt; $columns">
		<xsl:call-template name="processRow">
			<xsl:with-param name="counter" select="$counter+1"/>
			<xsl:with-param name="columns" select="$columns"/>
			<xsl:with-param name="this_row" select="$this_row"/>
		</xsl:call-template>
	</xsl:if>
</xsl:template>
</xsl:transform>
 


