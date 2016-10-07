CREATE TABLE [dbo].[Users] (
	[ID] [uniqueidentifier] NOT NULL ,
	[Name] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Email] [char] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
)
GO

ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED  ([ID])
GO