USE [master]
GO
/****** Object:  Database [KronsoftFarma]    Script Date: 9/8/2022 3:01:02 PM ******/
CREATE DATABASE [KronsoftFarma]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KronsoftFarma', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\KronsoftFarma.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KronsoftFarma_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\KronsoftFarma_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [KronsoftFarma] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KronsoftFarma].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KronsoftFarma] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KronsoftFarma] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KronsoftFarma] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KronsoftFarma] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KronsoftFarma] SET ARITHABORT OFF 
GO
ALTER DATABASE [KronsoftFarma] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KronsoftFarma] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KronsoftFarma] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KronsoftFarma] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KronsoftFarma] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KronsoftFarma] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KronsoftFarma] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KronsoftFarma] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KronsoftFarma] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KronsoftFarma] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KronsoftFarma] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KronsoftFarma] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KronsoftFarma] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KronsoftFarma] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KronsoftFarma] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KronsoftFarma] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KronsoftFarma] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KronsoftFarma] SET RECOVERY FULL 
GO
ALTER DATABASE [KronsoftFarma] SET  MULTI_USER 
GO
ALTER DATABASE [KronsoftFarma] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KronsoftFarma] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KronsoftFarma] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KronsoftFarma] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KronsoftFarma] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'KronsoftFarma', N'ON'
GO
ALTER DATABASE [KronsoftFarma] SET QUERY_STORE = OFF
GO
USE [KronsoftFarma]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/8/2022 3:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Producer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 9/8/2022 3:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[StockId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [float] NOT NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/8/2022 3:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'e686dff2-bf78-49e2-b7c5-24951f466c64', N'Anispläzchen', N'Anispläzchen are traditional German Christmas cookies. As the name suggests, the cookies are flavored with aniseed, and may optionally incorporate cinnamon. Although other ingredients include only eggs, sugar, and flour, the unique technique in which the batter is left to partially dry before baking gives these cookies a light and crispy top layer while the center remains chewy and moist.

The cookies are usually baked and enjoyed for Christmas.', CAST(10.00 AS Decimal(18, 2)), N'Home')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'9fb84549-cc23-4d9f-84f2-275d79fe7584', N'Nürnberger Lebkuchen', N'The history of Nuremberg''s gingerbread is closely tied to the city''s role as a crossroad of several European trade routes, a position that bestowed the city with spices from distant countries. Nürnberger Lebkuchen are typically large, round gingerbread wafers with icing that can either be chocolate-coated (schokoliert) or plain (natural).

These gingerbread cookies are often decorated with almonds and candied lemon peel. Nürnberger Lebkuchen are produced by virtually all bakers in Nuremberg. They should contain at least 25% almonds, hazelnuts, or walnuts, and no more than 10% flour or starch, while the manufacturers must produce these cookies exclusively within the city limits of Nuremberg.', CAST(80.00 AS Decimal(18, 2)), N'Restaurant')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'6b10bc44-bb6e-49e0-a9de-2998da152fa0', N'Hausfreunde', N'Hausfreunde are classic German Christmas cookies. In order to make them, a few layers of crispy shortcrust pastry are covered with apricot jam and almond paste, then dipped in dark chocolate and garnished with caramelized walnuts on top. The ingredients usually include flour, sugar, butter, eggs, salt, vanilla, almond paste, dark chocolate, apricot jam, and walnut halves.

Before serving, the chocolate should be left to fully set. The name of these cookies means friend of the house, probably because they are easy to prepare and keep for unexpected guests during the festive Christmas season.', CAST(20.00 AS Decimal(18, 2)), N'Home')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa6', N'Zimtsterne', N'Zimtsterne are crispy star-shaped German cookies. They are made with a combination of whipped egg whites that are mixed with sugar, cinnamon, ground nuts—traditionally almonds with the occasional addition of hazelnuts—and vanilla sugar.

The dough is rolled and cut into star-shaped cookies. After baking, each cookie is coated with a layer of white frosting that is made from a combination of egg whites and sugar. The name of these cookies translates as cinnamon stars, due to the heavy use of the aromatic spice. ', CAST(90.00 AS Decimal(18, 2)), N'Restaurant')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'e094b040-e1a9-4728-8364-31da9f49c212', N'Engelsaugen', N'These delicious thumbprint cookies are an absolute must for Christmas in Germany. They are traditionally made with a simple shortcrust base that is filled with red currant jam. Interestingly, in some parts of Germany, engelsaugen (angel eyes) are called husarenkrapfen, which translates as Hussars'' donuts.', CAST(70.00 AS Decimal(18, 2)), N'Restaurant')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'9b71e4db-7b5a-4736-905e-4f8e74d948a8', N'Lebkuchen', N'This traditional German treat is mainly associated with Christmas and winter holidays. The term lebkuchen covers a wide variety of different types of honey or ginger-flavored cookies which are traditionally baked on oblaten (thin wafers), and glazed or coated with either dark chocolate or sugar icing.

Additional ingredients may include various spices like cloves, aniseed, nutmeg, coriander, and cardamom, with different types of nuts or candied fruit. Best-known varieties of this cookie include the heart-shaped lebkuchenherzen which are often decorated with icing and different inscriptions, the German version of the gingerbread man called honigkuchenpferd (lit. honey cake horse), and hexenhausel (lit. witch’s house), while the most famous is the Nurnberger Elisenlebkuchen. ', CAST(100.00 AS Decimal(18, 2)), N'Michelin Star')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'c0417b49-a199-4e61-adcf-5e923d1a7e56', N'Springerle', N'Also known as little jumper or little knight, springerle is an anise-flavored biscuit made from wheat flour, eggs, and powdered sugar. In the past, the dough used to be leavened with hartshorn salt, but modern day recipes typically use baking powder.

The designs and images on top are made with flat molds, presses, and boards, or specially crafted wooden rolling pins. The origin of these biscuits can be traced back to southwestern Germany and the 14th century. The oldest mold for springerle comes from Switzerland, and it was also made in the 14th century.', CAST(50.00 AS Decimal(18, 2)), N'Restaurant')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'35474a39-4ace-4497-bbe7-bc1ccdbab405', N'Pfeffernuss', N'Pfeffernuss is a spiced German Christmas cookie. Although there is no set recipe and the variations appear throughout the country, the dough is usually heavily spiced with several spices. The list typically includes white pepper, which gives the cookie its name, cinnamon, cardamom, anise, cloves, allspice, ground ginger, and nutmeg.

Sometimes, they are made with a gingerbread spice blend known as lebkuchengewürz, while some variations also use orange or lemon zest. These cookies are a Christmas staple, mostly prepared and enjoyed throughout the Christmas period.

They are typically round and often dusted in sugar or covered in icing glaze. ', CAST(60.00 AS Decimal(18, 2)), N'Restaurant')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'e528d38b-243c-4f9d-a6ae-d4c9e0f92b43', N'Bethmännchen', N'Bethmännchen have been a Christmas staple in Germany for over 200 years. Hailing from Frankfurt, these cookies are made from marzipan dough that is infused with rose water. Before they are glazed and baked, each cookie is decorated with three almond halves.

Bethmännchen are closely associated with the once influential Bethmann family. The legend says that the family’s pastry chef, who was inspired by a similar dessert known as Frankfurter Brenten, invented the cookies in 1838 and decorated them to honor Bethmann''s four sons. ', CAST(40.00 AS Decimal(18, 2)), N'Home')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Producer]) VALUES (N'8ff6f46d-5042-4fb7-a179-e06c8d677eeb', N'Aachener Printen', N'This is a special variety of brown, firm gingerbread that has been baked in Aachen since the 1820s. The term printe refers to the use of carved wooden molds into which the dough is pressed during production. Having been regionally protected, the Aachener Printen may only be produced in Aachen and a few neighboring villages.

The dough is made from flour, sugar, and exotic spices such as cinnamon, aniseed, or ginger, which impart a unique flavor to the gingerbread. Freshly baked Printen are always firm, but because many people prefer their pastries tender, they are also offered in a soft version. ', CAST(30.00 AS Decimal(18, 2)), N'Home')
GO
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'bc48bd3f-bc75-44ca-8fda-2c0ab68c7379', N'e094b040-e1a9-4728-8364-31da9f49c212', 48)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'39ff1103-7ef7-4b5a-b4c0-3113b5113d7d', N'e528d38b-243c-4f9d-a6ae-d4c9e0f92b43', 28)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'2e978c32-2e8e-4bc6-99f6-430746cb9ce3', N'e686dff2-bf78-49e2-b7c5-24951f466c64', 37)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'70f7b165-7153-4e1c-baf0-520fec58e8ae', N'9fb84549-cc23-4d9f-84f2-275d79fe7584', 64)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'd691eb82-332b-4e54-84e4-856e123cf089', N'6b10bc44-bb6e-49e0-a9de-2998da152fa0', 12)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'e4f79219-6acd-49f6-95cd-88ca5565df82', N'9b71e4db-7b5a-4736-905e-4f8e74d948a8', 5)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'596d926f-d174-4f36-a74d-ad2a57eb8704', N'35474a39-4ace-4497-bbe7-bc1ccdbab405', 79)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'da0f63b8-a32a-46af-9a50-b51b34721456', N'c0417b49-a199-4e61-adcf-5e923d1a7e56', 121)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'b01045ab-2086-43c9-809e-c83fd94b0246', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', 56)
INSERT [dbo].[Stocks] ([StockId], [ProductId], [Quantity]) VALUES (N'f0393c32-dfcd-4028-888f-ca29f08c17ad', N'8ff6f46d-5042-4fb7-a179-e06c8d677eeb', 30)
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [IsAdmin]) VALUES (N'2c60a581-0f43-44b6-a4d8-200557638b76', N'Adrian', N'1234567890', 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [IsAdmin]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa6', N'Carmen', N'456789', 1)
INSERT [dbo].[User] ([UserId], [Username], [Password], [IsAdmin]) VALUES (N'7ce9ea99-3378-4baa-9658-7fe03a7ec0a8', N'Amalia', N'234567', 0)
INSERT [dbo].[User] ([UserId], [Username], [Password], [IsAdmin]) VALUES (N'dd015643-4a03-4731-a4c2-d347f4037e3b', N'Ema', N'345678', 0)
GO
/****** Object:  Index [IX_Stocks]    Script Date: 9/8/2022 3:01:02 PM ******/
ALTER TABLE [dbo].[Stocks] ADD  CONSTRAINT [IX_Stocks] UNIQUE NONCLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ProductId]  DEFAULT (newid()) FOR [ProductId]
GO
ALTER TABLE [dbo].[Stocks] ADD  CONSTRAINT [DF_Stocks_StockId]  DEFAULT (newid()) FOR [StockId]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserId]  DEFAULT (newid()) FOR [UserId]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Products]
GO
USE [master]
GO
ALTER DATABASE [KronsoftFarma] SET  READ_WRITE 
GO
