USE [master]
GO
/****** Object:  Database [TuXuanTrung_CayCanh]    Script Date: 03/01/2023 12:04:45 SA ******/
CREATE DATABASE [TuXuanTrung_CayCanh]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TuXuanTrung_CayCanh', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TuXuanTrung_CayCanh.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TuXuanTrung_CayCanh_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TuXuanTrung_CayCanh_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TuXuanTrung_CayCanh].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ARITHABORT OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET  MULTI_USER 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TuXuanTrung_CayCanh]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configs]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Site_Name] [nvarchar](max) NOT NULL,
	[Hot_line] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Img] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[Number] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Configs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UseId] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[CreateBy] [int] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Links]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Slug] [nvarchar](max) NULL,
	[TypeLink] [nvarchar](max) NULL,
	[TableId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Links] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
	[Link] [nvarchar](max) NULL,
	[TypeMenu] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[TableId] [int] NULL,
 CONSTRAINT [PK_dbo.Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[DeliveryName] [nvarchar](max) NULL,
	[DeliveryEmail] [nvarchar](max) NULL,
	[DeliveryPhone] [nvarchar](max) NULL,
	[DeliveryAddress] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[DateOrder] [datetime] NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[Number] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[CreateBy] [int] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Img] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
	[PostType] [nvarchar](max) NULL,
	[TopicId] [int] NOT NULL,
	[Detail] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[Img] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Number] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[BrandId] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[CreateBy] [int] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sliders]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sliders](
	[Img] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.Sliders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[MetaKey] [nvarchar](max) NOT NULL,
	[MetaDesc] [nvarchar](max) NOT NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/01/2023 12:04:45 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Img] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Roles] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[CreateBy] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [Code]
GO
ALTER TABLE [dbo].[Posts] ADD  DEFAULT ((0)) FOR [TopicId]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT ((0)) FOR [TopicId]
GO
USE [master]
GO
ALTER DATABASE [TuXuanTrung_CayCanh] SET  READ_WRITE 
GO
