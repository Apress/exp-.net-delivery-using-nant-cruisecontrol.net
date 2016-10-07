SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
-- Pointer used for text / image updates. This might not be needed, but is declared here just in case
DECLARE @ptrval binary(16)

BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[Transformations] ON
INSERT INTO [dbo].[Transformations] ([TransformId], [XmlContent], [XslContent], [Output], [Title]) VALUES (69, N'<testing><test>1</test></testing>', N'<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="testing">
   <xsl:value-of select="test" />
</xsl:template>
</xsl:stylesheet>', N'<?xml version="1.0" encoding="utf-8"?>1', N'Standing Transform 1')
INSERT INTO [dbo].[Transformations] ([TransformId], [XmlContent], [XslContent], [Output], [Title]) VALUES (70, N'<testing><test>1</test></testing>', N'<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="testing">
   <xsl:value-of select="test" />
</xsl:template>
</xsl:stylesheet>', N'<?xml version="1.0" encoding="utf-8"?>1', N'Standing Transformation 2')
SET IDENTITY_INSERT [dbo].[Transformations] OFF
COMMIT TRANSACTION