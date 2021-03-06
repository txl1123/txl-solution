USE [master]
GO
/****** Object:  Database [Prices]    Script Date: 2015/8/29 14:39:10 ******/
CREATE DATABASE [Prices]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Prices', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Prices.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Prices_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Prices_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Prices] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prices].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Prices] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Prices] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Prices] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Prices] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Prices] SET ARITHABORT OFF 
GO
ALTER DATABASE [Prices] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Prices] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Prices] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Prices] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Prices] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Prices] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Prices] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Prices] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Prices] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Prices] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Prices] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Prices] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Prices] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Prices] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Prices] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Prices] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Prices] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Prices] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Prices] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Prices] SET  MULTI_USER 
GO
ALTER DATABASE [Prices] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Prices] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Prices] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Prices] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Prices]
GO
/****** Object:  Table [dbo].[Details]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Details](
	[Id] [int] NULL,
	[Content] [varchar](1000) NULL,
	[Price] [money] NULL,
	[Mid] [int] NULL,
	[Fid] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FunModule]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FunModule](
	[Fid] [int] IDENTITY(1,1) NOT NULL,
	[FunName] [varchar](500) NULL,
 CONSTRAINT [PK_FUNMODULE] PRIMARY KEY CLUSTERED 
(
	[Fid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberShip]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberShip](
	[userid] [int] NULL,
	[NickName] [varchar](200) NULL,
	[CreateDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[Mid] [int] IDENTITY(1,1) NOT NULL,
	[MName] [varchar](200) NULL,
	[Pid] [int] NULL,
	[Atype] [bit] NULL,
 CONSTRAINT [PK_MODULE] PRIMARY KEY CLUSTERED 
(
	[Mid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PriceType]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PriceType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](200) NULL,
 CONSTRAINT [PK_PRICETYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](200) NULL,
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempShip]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempShip](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Mid] [int] NULL,
	[Fid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 2015/8/29 14:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProfile](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](260) NULL,
 CONSTRAINT [PK_USERPROFILE] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[FunModule] ON 

INSERT [dbo].[FunModule] ([Fid], [FunName]) VALUES (1, N'内存')
INSERT [dbo].[FunModule] ([Fid], [FunName]) VALUES (2, N'硬盘')
INSERT [dbo].[FunModule] ([Fid], [FunName]) VALUES (3, N'CPU')
SET IDENTITY_INSERT [dbo].[FunModule] OFF
SET IDENTITY_INSERT [dbo].[Module] ON 

INSERT [dbo].[Module] ([Mid], [MName], [Pid], [Atype]) VALUES (1, N'ST001', 1, 0)
INSERT [dbo].[Module] ([Mid], [MName], [Pid], [Atype]) VALUES (2, N'ST002', 1, 0)
INSERT [dbo].[Module] ([Mid], [MName], [Pid], [Atype]) VALUES (3, N'CT001', 2, 0)
SET IDENTITY_INSERT [dbo].[Module] OFF
SET IDENTITY_INSERT [dbo].[PriceType] ON 

INSERT [dbo].[PriceType] ([Id], [TypeName]) VALUES (1, N'服务器')
INSERT [dbo].[PriceType] ([Id], [TypeName]) VALUES (2, N'存储器')
SET IDENTITY_INSERT [dbo].[PriceType] OFF
SET IDENTITY_INSERT [dbo].[TempShip] ON 

INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (1, 1, 1)
INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (2, 1, 2)
INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (3, 1, 3)
INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (4, 2, 1)
INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (5, 2, 2)
INSERT [dbo].[TempShip] ([id], [Mid], [Fid]) VALUES (6, 3, 3)
SET IDENTITY_INSERT [dbo].[TempShip] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细描述表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Details'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能模块表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunModule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单个和套餐区分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Module', @level2type=N'COLUMN',@level2name=N'Atype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Module'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号,总类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PriceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关系临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempShip'
GO
USE [master]
GO
ALTER DATABASE [Prices] SET  READ_WRITE 
GO
