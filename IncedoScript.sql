USE [master]
GO
/****** Object:  Database [SongDatabase]    Script Date: 03-02-2024 22:59:19 ******/
CREATE DATABASE [SongDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SongDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SongDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SongDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SongDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SongDatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SongDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SongDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SongDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SongDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SongDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SongDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [SongDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SongDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SongDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SongDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SongDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SongDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SongDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SongDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SongDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SongDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SongDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SongDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SongDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SongDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SongDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SongDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SongDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SongDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [SongDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [SongDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SongDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SongDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SongDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SongDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SongDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SongDatabase', N'ON'
GO
ALTER DATABASE [SongDatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [SongDatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SongDatabase]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 03-02-2024 22:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[SongId] [int] NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SongFinalDB]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongFinalDB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Artist] [varchar](50) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_SongFinalDB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SongUser]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SongUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[userPassword] [varchar](255) NOT NULL,
	[Email] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([SongId])
REFERENCES [dbo].[SongFinalDB] ([Id])
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[SongUser] ([UserId])
GO
/****** Object:  StoredProcedure [dbo].[AddToCart]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddToCart]
    @UserId INT,
    @SongId INT
AS
BEGIN
    -- Add your logic here to insert the specified song into the Cart table
    INSERT INTO Cart(UserId, SongId, Quantity)
    VALUES (@UserId, @SongId, 1); -- You might adjust the Quantity as needed
END
GO
/****** Object:  StoredProcedure [dbo].[CreateMusic]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateMusic]
    @Title VARCHAR(255),
    @Artist VARCHAR(255),
    @Price INT
AS
BEGIN
    INSERT INTO SongFinalDB(Title, Artist, Price)
    VALUES (@Title, @Artist, @Price);
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteMusic]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteMusic]
    @Id INT
AS
BEGIN
    DELETE FROM SongFinalDB
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetCartSongs]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Stored procedure to retrieve songs in the user's cart
CREATE PROCEDURE [dbo].[GetCartSongs]
    @UserId INT
AS
BEGIN
    SELECT SongFinalDB.*
    FROM Cart
    JOIN SongFinalDB ON Cart.SongId = SongFinalDB.Id
    WHERE Cart.UserId = @UserId;
END

GO
/****** Object:  StoredProcedure [dbo].[GetMusicById]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMusicById]
    @Id INT
AS
BEGIN
    SELECT Id, Title, Artist, Price
    FROM SongFinalDB
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetMusicsAll]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMusicsAll]
AS
BEGIN
    SELECT Id, Title, Artist, Price
    FROM SongFinalDB;
END

GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RegisterUser]
    @Username VARCHAR(255),
    @Password VARCHAR(255),
    @Email VARCHAR(255)
AS
BEGIN
    INSERT INTO SongUser(Username, userPassword, Email) 
	VALUES (@Username, @Password, @Email)
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateMusic]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateMusic]
    @Id INT,
    @Title VARCHAR(255),
    @Artist VARCHAR(255),
    @Price INT
AS
BEGIN
    UPDATE SongFinalDB
    SET Title = @Title, Artist = @Artist, Price = @Price
    WHERE Id = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 03-02-2024 22:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValidateUser]
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SELECT UserId, Username
    FROM [SongUser]
    WHERE Username = @Username AND userPassword = @Password;
END

GO
USE [master]
GO
ALTER DATABASE [SongDatabase] SET  READ_WRITE 
GO
