<?xml version="1.0"?>
<xsl:stylesheet
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <xsl:output method="html"/>

    <xsl:template match="/">
   		<xsl:variable name="messages" select="/cruisecontrol//buildresults//message" />
   		<xsl:if test="count($messages) > 0">   	
			<xsl:variable name="error.messages" select="$messages[(contains(text(), 'error ')) or @level='Error'] | /cruisecontrol//builderror/message" />
	        <xsl:variable name="error.messages.count" select="count($error.messages)" />
	        <xsl:variable name="warning.messages" select="$messages[(contains(text(), 'warning ')) or @level='Warning']" />
	        <xsl:variable name="warning.messages.count" select="count($warning.messages)" />
	        <xsl:variable name="total" select="count($error.messages) + count($warning.messages)"/>

	        <xsl:if test="$error.messages.count > 0">
	            <table class="section-table" cellpadding="2" cellspacing="0" border="0" width="98%">
	                <tr>
	                    <td class="compile-sectionheader">
	                        Errors: (<xsl:value-of select="$error.messages.count"/>)
	                    </td>
	                </tr>
	                <tr>
						<td>
							<xsl:apply-templates select="$error.messages"/>
						</td>
					</tr>
	            </table>
	        </xsl:if>
	        <xsl:if test="$warning.messages.count > 0">
	            <table class="section-table" cellpadding="2" cellspacing="0" border="0" width="98%">
	                <tr>
	                    <td class="compile-sectionheader">
	                        Warnings: (<xsl:value-of select="$warning.messages.count"/>)
	                    </td>
	                </tr>
	                <tr><td><xsl:apply-templates select="$warning.messages"/></td></tr>
	            </table>
	        </xsl:if>
        </xsl:if>
    </xsl:template>

    <xsl:template match="message">
        <pre class="compile-error-data"><xsl:value-of select="text()"/></pre>
    </xsl:template>

</xsl:stylesheet>
