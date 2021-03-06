SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentDetail](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ContentDetail_Id]  DEFAULT (newid()),
	[ContentTypeId] [uniqueidentifier] NULL,
	[Title] [nvarchar](256) NULL,
	[ContentText] [ntext] NULL,
	[Sort] [int] NULL CONSTRAINT [DF_ContentDetail_Sort]  DEFAULT ((0)),
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ContentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ContentDetail', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sys_Enum]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sys_Enum](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Sys_Enum_Id]  DEFAULT (newid()),
	[EnumCode] [nvarchar](50) NULL,
	[EnumName] [nvarchar](50) NULL,
	[EnumValue] [nvarchar](256) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Sort] [int] NULL CONSTRAINT [DF_Sys_Enum_Sort]  DEFAULT ((0)),
	[Remark] [nvarchar](256) NULL,
 CONSTRAINT [PK_Sys_Enum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResidenceCommunity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResidenceCommunity](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ResidenceCommunity_Id]  DEFAULT (newid()),
	[CommunityName] [nvarchar](30) NULL,
	[PropertyCompanyId] [uniqueidentifier] NULL,
	[ProvinceCityId] [uniqueidentifier] NULL,
	[Province] [nvarchar](10) NULL,
	[City] [nvarchar](10) NULL,
	[District] [nvarchar](20) NULL,
	[Address] [nvarchar](30) NULL,
	[AboutDescri] [nvarchar](300) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ResidenceCommunity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ResidenceCommunity', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResidentialBuilding]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResidentialBuilding](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ResidentialBuilding_Id]  DEFAULT (newid()),
	[BuildingCode] [varchar](20) NULL,
	[CoveredArea] [float] NULL CONSTRAINT [DF_ResidentialBuilding_CoveredArea]  DEFAULT ((0)),
	[PropertyCompanyId] [uniqueidentifier] NULL,
	[ResidenceCommunityId] [uniqueidentifier] NULL,
	[Remark] [nvarchar](50) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ResidentialBuilding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ResidentialBuilding', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResidentialUnit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResidentialUnit](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ResidentialUnit_Id]  DEFAULT (newid()),
	[UnitCode] [varchar](30) NULL,
	[PropertyCompanyId] [uniqueidentifier] NULL,
	[ResidenceCommunityId] [uniqueidentifier] NULL,
	[ResidentialBuildingId] [uniqueidentifier] NULL,
	[Remark] [nvarchar](50) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ResidentialUnit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ResidentialUnit', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[House]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[House](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_House_Id]  DEFAULT (newid()),
	[HouseCode] [varchar](20) NULL,
	[PropertyCompanyId] [uniqueidentifier] NULL,
	[ResidenceCommunityId] [uniqueidentifier] NULL,
	[ResidentialBuildingId] [uniqueidentifier] NULL,
	[ResidentialUnitId] [uniqueidentifier] NULL,
	[HouseAcreage] [float] NULL CONSTRAINT [DF_House_HouseAcreage]  DEFAULT ((0)),
	[Remark] [nvarchar](50) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_House] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'House', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserHouseOwner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserHouseOwner](
	[UserId] [uniqueidentifier] NOT NULL,
	[HouseOwnerId] [uniqueidentifier] NOT NULL,
	[Password] [varchar](30) NULL,
 CONSTRAINT [PK_UserHouseOwner] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[HouseOwnerId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'UserHouseOwner', @level2type=N'COLUMN', @level2name=N'UserId'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseOwner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HouseOwner](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_HouseOwner_Id]  DEFAULT (newid()),
	[HouseOwnerName] [nvarchar](30) NULL,
	[MobilePhone] [char](15) NULL,
	[TelPhone] [char](15) NULL,
	[PropertyCompanyId] [uniqueidentifier] NULL,
	[ResidenceCommunityId] [uniqueidentifier] NULL,
	[ResidentialBuildingId] [uniqueidentifier] NULL,
	[ResidentialUnitId] [uniqueidentifier] NULL,
	[HouseId] [uniqueidentifier] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_HouseOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HouseOwner', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseOwnerNotice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HouseOwnerNotice](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_HouseOwnerNotice_Id]  DEFAULT (newid()),
	[HouseOwnerId] [uniqueidentifier] NULL,
	[NoticeId] [uniqueidentifier] NULL,
	[Status] [tinyint] NULL CONSTRAINT [DF_HouseOwnerNotice_Status]  DEFAULT ((1)),
	[IsRead] [bit] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_HouseOwnerNotice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HouseOwnerNotice', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplainRepair]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ComplainRepair](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ComplainRepair_Id]  DEFAULT (newid()),
	[UserId] [uniqueidentifier] NULL,
	[SysEnumId] [uniqueidentifier] NULL,
	[Phone] [varchar](20) NULL,
	[Address] [nvarchar](50) NULL,
	[Descri] [nvarchar](1000) NULL,
	[Status] [tinyint] NULL CONSTRAINT [DF_ComplainRepair_Status]  DEFAULT ((0)),
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ComplainRepair] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ComplainRepair', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Announcement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Announcement](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Announcement_Id]  DEFAULT (newid()),
	[ContentTypeId] [uniqueidentifier] NULL,
	[Title] [nvarchar](100) NULL,
	[Descr] [nvarchar](300) NULL,
	[ContentText] [ntext] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Announcement', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Notice](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Notice_Id]  DEFAULT (newid()),
	[ContentTypeId] [uniqueidentifier] NULL,
	[Title] [nvarchar](100) NULL,
	[Descr] [nvarchar](300) NULL,
	[ContentText] [ntext] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Notice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Notice', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentPicture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentPicture](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ContentPicture_Id]  DEFAULT (newid()),
	[OriginalPicture] [varchar](100) NULL,
	[BPicture] [varchar](100) NULL,
	[MPicture] [varchar](100) NULL,
	[SPicture] [varchar](100) NULL,
	[OtherPicture] [varchar](100) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ContentPicture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ContentPicture', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Advertisement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Advertisement](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Advertisement_Id]  DEFAULT (newid()),
	[Title] [nvarchar](100) NULL,
	[SiteFunId] [uniqueidentifier] NULL,
	[LayoutPositionId] [uniqueidentifier] NULL,
	[Timeout] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Advertisement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Advertisement', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdvertisementItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdvertisementItem](
	[AdvertisementId] [uniqueidentifier] NOT NULL,
	[Descr] [nvarchar](300) NULL,
	[ContentText] [nvarchar](3000) NULL,
 CONSTRAINT [PK_AdvertisementItem] PRIMARY KEY CLUSTERED 
(
	[AdvertisementId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AdvertisementItem', @level2type=N'COLUMN', @level2name=N'AdvertisementId'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdvertisementLink]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdvertisementLink](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_AdvertisementLink_Id]  DEFAULT (newid()),
	[AdvertisementId] [uniqueidentifier] NULL,
	[ActionTypeId] [uniqueidentifier] NULL,
	[ContentPictureId] [uniqueidentifier] NULL,
	[Url] [varchar](100) NULL,
	[Sort] [int] NULL,
	[IsDisable] [bit] NULL,
 CONSTRAINT [PK_AdvertisementLink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AdvertisementLink', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picture_Advertisement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Picture_Advertisement](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Picture_Advertisement_Id]  DEFAULT (newid()),
	[FileName] [nvarchar](100) NULL,
	[FileSize] [int] NULL,
	[FileExtension] [varchar](10) NULL,
	[FileDirectory] [nvarchar](100) NULL,
	[RandomFolder] [varchar](20) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Picture_Advertisement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Picture_Advertisement', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProvinceCity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProvinceCity](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ProvinceCity_Id]  DEFAULT (newid()),
	[Named] [nvarchar](30) NULL,
	[Pinyin] [varchar](30) NULL,
	[FirstChar] [char](1) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Sort] [int] NULL CONSTRAINT [DF_ProvinceCity_Sort]  DEFAULT ((0)),
	[Remark] [nvarchar](300) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ProvinceCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProvinceCity', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyCompany]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PropertyCompany](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_PropertyCompany_Id]  DEFAULT (newid()),
	[CompanyName] [nvarchar](30) NULL,
	[ShortName] [varchar](30) NULL,
	[Province] [nvarchar](10) NULL,
	[ProvinceCityId] [uniqueidentifier] NULL,
	[City] [nvarchar](10) NULL,
	[District] [nvarchar](20) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PropertyCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PropertyCompany', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentType](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ContentType_Id]  DEFAULT (newid()),
	[TypeName] [nvarchar](50) NULL,
	[TypeCode] [varchar](50) NULL,
	[TypeValue] [nvarchar](256) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Sort] [int] NULL CONSTRAINT [DF_ContentType_Sort]  DEFAULT ((0)),
	[IsSys] [bit] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ContentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ContentType', @level2type=N'COLUMN', @level2name=N'Id'

