USE [master]
GO
/****** Object:  Database [WebApiTest]    Script Date: 7/9/2023 1:21:16 PM ******/
CREATE DATABASE [WebApiTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebApiTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebApiTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WebApiTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebApiTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WebApiTest] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebApiTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebApiTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebApiTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebApiTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebApiTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebApiTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebApiTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebApiTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebApiTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebApiTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebApiTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebApiTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebApiTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebApiTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebApiTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebApiTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebApiTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebApiTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebApiTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebApiTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebApiTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebApiTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebApiTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebApiTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebApiTest] SET  MULTI_USER 
GO
ALTER DATABASE [WebApiTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebApiTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebApiTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebApiTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebApiTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WebApiTest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WebApiTest] SET QUERY_STORE = OFF
GO
USE [WebApiTest]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (1, N'Books')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (2, N'Courses')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [CategoryId], [CreatedDate]) VALUES (1, N'ASP.NET Core Book', N'ASP.NET Core Bookdsfsdfdfgfd', CAST(1000.00 AS Decimal(18, 2)), 1, CAST(N'2022-10-12T23:24:36.7366667' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
/****** Object:  StoredProcedure [dbo].[AddNewProductDetails]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AddNewProductDetails]   
(    
@Name varchar (50),   
@Description varchar (50),    
@UnitPrice decimal,    
@CategoryId int    
)   
as  
begin     
Insert into Products(Name,Description,UnitPrice, CategoryId, CreatedDate) values(@Name,@Description,@UnitPrice,@CategoryId, GETDATE()) 
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteProductById]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteProductById]  
(      
@ProductId int
) 
as 
begin    
Delete from Products where ProductId=@ProductId;  
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductDetails]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateProductDetails]   
(    
@ProductId int,    
@Name varchar (50),     
@Description varchar (50),      
@UnitPrice decimal,   
@CategoryId int     
)   
as   
begin      
Update Products Set Name=@Name, Description=@Description,UnitPrice=@UnitPrice, CategoryId=@CategoryId
Where ProductId=@ProductId; 
End 
GO
/****** Object:  StoredProcedure [dbo].[usp_getproduct]    Script Date: 7/9/2023 1:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_getproduct]
@ProductId int 
As 
Begin  
Select * from Products Where ProductId = @ProductId; 
End;
GO
USE [master]
GO
ALTER DATABASE [WebApiTest] SET  READ_WRITE 
GO
