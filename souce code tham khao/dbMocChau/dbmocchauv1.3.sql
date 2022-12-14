USE [master]
GO
/****** Object:  Database [QLSuaMocChau]    Script Date: 12/2/2020 7:24:02 AM ******/
CREATE DATABASE [QLSuaMocChau]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLSuaMocChau', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLSuaMocChau.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLSuaMocChau_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLSuaMocChau_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLSuaMocChau] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLSuaMocChau].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLSuaMocChau] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLSuaMocChau] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLSuaMocChau] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLSuaMocChau] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLSuaMocChau] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLSuaMocChau] SET  MULTI_USER 
GO
ALTER DATABASE [QLSuaMocChau] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLSuaMocChau] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLSuaMocChau] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLSuaMocChau] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLSuaMocChau] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLSuaMocChau] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLSuaMocChau] SET QUERY_STORE = OFF
GO
USE [QLSuaMocChau]
GO
/****** Object:  Table [dbo].[LoHang]    Script Date: 12/2/2020 7:24:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoHang](
	[MaLoHang] [int] IDENTITY(1,1) NOT NULL,
	[TenLoHang] [nvarchar](50) NULL,
	[NgaySanXuat] [datetime] NULL,
	[GhiChu] [nvarchar](500) NULL,
 CONSTRAINT [PK_LoHang] PRIMARY KEY CLUSTERED 
(
	[MaLoHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 12/2/2020 7:24:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[MaNguoiDung] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [nchar](30) NULL,
	[MatKhau] [nchar](20) NULL,
	[MoTa] [nvarchar](max) NULL,
	[MaVaiTro] [int] NULL,
	[SoE] [nvarchar](500) NULL,
	[SoN] [nvarchar](500) NULL,
 CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuyTrinh]    Script Date: 12/2/2020 7:24:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuyTrinh](
	[MaQuyTrinh] [int] IDENTITY(1,1) NOT NULL,
	[TenQuyTrinh] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
	[MaSanPham] [int] NULL,
	[ChuKi] [nvarchar](max) NULL,
	[MaNguoiDung] [int] NULL,
	[NgayKy] [datetime] NULL,
	[TrangThai] [int] NULL,
	[MaLoHang] [int] NULL,
	[TepTinChungThuc] [nvarchar](max) NULL,
	[TrangThaiXacThuc] [int] NULL,
 CONSTRAINT [PK_QuyTrinh] PRIMARY KEY CLUSTERED 
(
	[MaQuyTrinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 12/2/2020 7:24:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
	[HinhAnh] [nchar](50) NULL,
	[MaLoHang] [int] NULL,
	[TrangThai] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaiTro]    Script Date: 12/2/2020 7:24:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaiTro](
	[MaVaitro] [int] IDENTITY(1,1) NOT NULL,
	[VaiTro] [nvarchar](100) NULL,
	[ThongTinKhoa] [nvarchar](500) NULL,
 CONSTRAINT [PK_VaiTro] PRIMARY KEY CLUSTERED 
(
	[MaVaitro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_NguoiDung_VaiTro] FOREIGN KEY([MaVaiTro])
REFERENCES [dbo].[VaiTro] ([MaVaitro])
GO
ALTER TABLE [dbo].[NguoiDung] CHECK CONSTRAINT [FK_NguoiDung_VaiTro]
GO
ALTER TABLE [dbo].[QuyTrinh]  WITH CHECK ADD  CONSTRAINT [FK_QuyTrinh_LoHang] FOREIGN KEY([MaLoHang])
REFERENCES [dbo].[LoHang] ([MaLoHang])
GO
ALTER TABLE [dbo].[QuyTrinh] CHECK CONSTRAINT [FK_QuyTrinh_LoHang]
GO
ALTER TABLE [dbo].[QuyTrinh]  WITH CHECK ADD  CONSTRAINT [FK_QuyTrinh_NguoiDung] FOREIGN KEY([MaNguoiDung])
REFERENCES [dbo].[NguoiDung] ([MaNguoiDung])
GO
ALTER TABLE [dbo].[QuyTrinh] CHECK CONSTRAINT [FK_QuyTrinh_NguoiDung]
GO
ALTER TABLE [dbo].[QuyTrinh]  WITH CHECK ADD  CONSTRAINT [FK_QuyTrinh_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[QuyTrinh] CHECK CONSTRAINT [FK_QuyTrinh_SanPham]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoHang] FOREIGN KEY([MaLoHang])
REFERENCES [dbo].[LoHang] ([MaLoHang])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoHang]
GO
USE [master]
GO
ALTER DATABASE [QLSuaMocChau] SET  READ_WRITE 
GO
