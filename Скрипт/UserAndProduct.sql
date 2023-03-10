USE [OOO_Myskyl]
GO
ALTER TABLE [dbo].[UserTable] DROP CONSTRAINT [FK_UserTable_RoleTable]
GO
ALTER TABLE [dbo].[UserTable] DROP CONSTRAINT [FK_UserTable_GenderTable]
GO
ALTER TABLE [dbo].[ProductTable] DROP CONSTRAINT [FK_ProductTable_UserTable]
GO
ALTER TABLE [dbo].[ProductTable] DROP CONSTRAINT [FK_ProductTable_CategoryTable]
GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 26.02.2023 21:31:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTable]') AND type in (N'U'))
DROP TABLE [dbo].[UserTable]
GO
/****** Object:  Table [dbo].[RoleTable]    Script Date: 26.02.2023 21:31:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleTable]') AND type in (N'U'))
DROP TABLE [dbo].[RoleTable]
GO
/****** Object:  Table [dbo].[ProductTable]    Script Date: 26.02.2023 21:31:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductTable]') AND type in (N'U'))
DROP TABLE [dbo].[ProductTable]
GO
/****** Object:  Table [dbo].[GenderTable]    Script Date: 26.02.2023 21:31:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenderTable]') AND type in (N'U'))
DROP TABLE [dbo].[GenderTable]
GO
/****** Object:  Table [dbo].[CategoryTable]    Script Date: 26.02.2023 21:31:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryTable]') AND type in (N'U'))
DROP TABLE [dbo].[CategoryTable]
GO
USE [master]
GO
/****** Object:  Database [OOO_Myskyl]    Script Date: 26.02.2023 21:31:52 ******/
DROP DATABASE [OOO_Myskyl]
GO
/****** Object:  Database [OOO_Myskyl]    Script Date: 26.02.2023 21:31:52 ******/
CREATE DATABASE [OOO_Myskyl]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OOO_Myskyl', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OOO_Myskyl.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OOO_Myskyl_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OOO_Myskyl_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OOO_Myskyl] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OOO_Myskyl].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OOO_Myskyl] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET ARITHABORT OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OOO_Myskyl] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OOO_Myskyl] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OOO_Myskyl] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OOO_Myskyl] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OOO_Myskyl] SET  MULTI_USER 
GO
ALTER DATABASE [OOO_Myskyl] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OOO_Myskyl] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OOO_Myskyl] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OOO_Myskyl] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OOO_Myskyl] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OOO_Myskyl] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OOO_Myskyl] SET QUERY_STORE = OFF
GO
USE [OOO_Myskyl]
GO
/****** Object:  Table [dbo].[CategoryTable]    Script Date: 26.02.2023 21:31:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryTable](
	[IDCategory] [int] NOT NULL,
	[NameCategory] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryTable] PRIMARY KEY CLUSTERED 
(
	[IDCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenderTable]    Script Date: 26.02.2023 21:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderTable](
	[IDGender] [int] NOT NULL,
	[GenderName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GenderTable] PRIMARY KEY CLUSTERED 
(
	[IDGender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTable]    Script Date: 26.02.2023 21:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTable](
	[IDProduct] [int] NOT NULL,
	[ArtickleProduct] [varchar](5) NULL,
	[NameProduct] [varchar](100) NOT NULL,
	[DescriptionProduct] [varchar](500) NOT NULL,
	[CategoryIDProduct] [int] NOT NULL,
	[PriceProduct] [int] NOT NULL,
	[DiscountPriduct] [int] NOT NULL,
	[ImageProduct] [varchar](500) NULL,
	[CountProduct] [int] NOT NULL,
	[IDUser] [int] NOT NULL,
 CONSTRAINT [PK_ProductTable] PRIMARY KEY CLUSTERED 
(
	[IDProduct] ASC,
	[IDUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleTable]    Script Date: 26.02.2023 21:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleTable](
	[IDRole] [int] NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RoleTable] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 26.02.2023 21:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTable](
	[IDUser] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirsName] [varchar](50) NOT NULL,
	[Login] [varchar](150) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Image] [varchar](500) NOT NULL,
	[GenderID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserTable] PRIMARY KEY CLUSTERED 
(
	[IDUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CategoryTable] ([IDCategory], [NameCategory]) VALUES (1, N'Гантели')
INSERT [dbo].[CategoryTable] ([IDCategory], [NameCategory]) VALUES (2, N'Штанги')
INSERT [dbo].[CategoryTable] ([IDCategory], [NameCategory]) VALUES (3, N'Гири')
GO
INSERT [dbo].[GenderTable] ([IDGender], [GenderName]) VALUES (1, N'мужской')
INSERT [dbo].[GenderTable] ([IDGender], [GenderName]) VALUES (2, N'женский')
GO
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (1, N'A234D', N'Разборная гантель', N' Brutal Sport 4 кг с цветными дисками', 1, 500, 10, N'dsa.jpg', 6, 1)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (2, N'REW32', N'Разборная гантель', N'Brutal Sport 9 кг с цветными дисками', 1, 750, 5, N'dacx.jpg', 5, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (3, N'RFDC3', N'Разборная гантель', N'Brutal Sport 39 кг с цветными дисками', 1, 1000, 3, N'bfda.jpg', 2, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (4, N'WQESA', N'Штанга прямая', N' 30кг', 2, 1200, 3, N'asdeq.jpg', 4, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (5, N'23EWQ', N'Штанга прямая', N'35кг', 2, 1400, 2, N'wqqe.jpg', 5, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (6, N'CXZ78', N'Штанга прямая', N' 40кг', 2, 1600, 4, N'aczx.jpg', 8, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (7, N'JFE23', N'Гиря', N'Гиря чугунная SportElite 6 кг', 3, 540, 3, N'', 2, 2)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (8, N'ADC43', N'Гиря', N'Гиря пластиковая Euro Classic 5 кг Черная', 3, 350, 10, N'', 19, 1)
INSERT [dbo].[ProductTable] ([IDProduct], [ArtickleProduct], [NameProduct], [DescriptionProduct], [CategoryIDProduct], [PriceProduct], [DiscountPriduct], [ImageProduct], [CountProduct], [IDUser]) VALUES (9, NULL, N'Гиря', N'Гиря чугунная обрезиненная SportElite 24 кг', 3, 850, 14, N'', 4, 3)
GO
INSERT [dbo].[RoleTable] ([IDRole], [RoleName]) VALUES (1, N'Администратор')
INSERT [dbo].[RoleTable] ([IDRole], [RoleName]) VALUES (2, N'Менеджер')
INSERT [dbo].[RoleTable] ([IDRole], [RoleName]) VALUES (3, N'Клиент')
GO
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (1, N'Иванов', N'Иван', N'Иванович', N'Ivanov@namecomp.ru', N'2L6KZG', N'man.jpg', 1, 1)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (2, N'Петров', N'Петр', N'Петрович', N'petrov@namecomp.ru', N'uzWC67', N'man.jpg', 1, 1)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (3, N'Федоров', N'Федор', N'Федорович', N'fedorov@namecomp.ru', N'8ntwUp', N'man.jpg', 1, 1)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (4, N'Миронов', N'Вениамин', N'Куприянович', N'mironov@namecomp.ru', N'YOyhfR', N'man.jpg', 1, 2)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (5, N'Ширяев', N'Ермолай', N'Вениаминович', N'shiryev@namecomp.ru', N'RSbvHv', N'man.jpg', 1, 2)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (6, N'Игнатов', N'Кассиан', N'Васильевич', N'ignatov@namecomp.ru', N'rwVDh9', N'man.jpg', 1, 2)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (7, N'Хохлов', N'Владимир', N'Мэлсович', N'hohlov@namecomp.ru', N'LdNyos', N'man.jpg', 1, 3)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (8, N'Стрелков', N'Мстислав', N'Георгьевич', N'strelkov@namecomp.ru', N'gynQMT', N'man.jpg', 1, 3)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (9, N'Беляева', N'Лилия', N'Наумовна', N'belyeva@@namecomp.ru', N'AtnDjr', N'woman.jpg', 2, 3)
INSERT [dbo].[UserTable] ([IDUser], [Name], [LastName], [FirsName], [Login], [Password], [Image], [GenderID], [RoleID]) VALUES (10, N'Смирнова', N'Ульяна', N'Гордеевна', N'smirnova@@namecomp.ru', N'JlFRCZ', N'woman.jpg', 2, 3)
GO
ALTER TABLE [dbo].[ProductTable]  WITH CHECK ADD  CONSTRAINT [FK_ProductTable_CategoryTable] FOREIGN KEY([CategoryIDProduct])
REFERENCES [dbo].[CategoryTable] ([IDCategory])
GO
ALTER TABLE [dbo].[ProductTable] CHECK CONSTRAINT [FK_ProductTable_CategoryTable]
GO
ALTER TABLE [dbo].[ProductTable]  WITH CHECK ADD  CONSTRAINT [FK_ProductTable_UserTable] FOREIGN KEY([IDUser])
REFERENCES [dbo].[UserTable] ([IDUser])
GO
ALTER TABLE [dbo].[ProductTable] CHECK CONSTRAINT [FK_ProductTable_UserTable]
GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [FK_UserTable_GenderTable] FOREIGN KEY([GenderID])
REFERENCES [dbo].[GenderTable] ([IDGender])
GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [FK_UserTable_GenderTable]
GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [FK_UserTable_RoleTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RoleTable] ([IDRole])
GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [FK_UserTable_RoleTable]
GO
USE [master]
GO
ALTER DATABASE [OOO_Myskyl] SET  READ_WRITE 
GO
