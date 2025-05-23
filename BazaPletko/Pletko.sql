USE master
GO
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name =
N'Pletko')
DROP DATABASE Pletko
GO
CREATE DATABASE Pletko
GO
Use Pletko
GO
GO
CREATE TABLE [dbo].[Kategorija](
	[Kategorija_ID] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Kategorija] PRIMARY KEY CLUSTERED 
(
	[Kategorija_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Korisnik]    Script Date: 03-Apr-25 23:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Korisnik](
	[Korisnik_ID] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](35) NOT NULL,
	[Prezime] [nvarchar](35) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Adresa] [nvarchar](50) NOT NULL,
	[DatumRodjenja] [date] NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[BrojTelefona] [nchar](15) NOT NULL,
	[Status_ID] [int] NOT NULL,
	[Ponuda_ID] [int] NULL,
 CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED 
(
	[Korisnik_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Narudzbina]    Script Date: 03-Apr-25 23:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Narudzbina](
	[Narudzbina_ID] [int] IDENTITY(1,1) NOT NULL,
	[DatumNarudzbine] [datetime] NOT NULL,
	[Broj_Kartice] [nvarchar](19) NOT NULL,
	[Korisnik_ID] [int] NOT NULL,
	[Proizvod_ID] [int] NOT NULL,
	[Kolicina] [int] NOT NULL,
	[UkupnaCena] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Narudzbina] PRIMARY KEY CLUSTERED 
(
	[Narudzbina_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proizvod]    Script Date: 03-Apr-25 23:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proizvod](
	[Proizvod_id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Cena] [decimal](10, 2) NOT NULL,
	[Kolicina] [int] NOT NULL,
	[Opis] [nvarchar](250) NOT NULL,
	[Korisnik_ID] [int] NULL,
	[Kategorija_ID] [int] NOT NULL,
 CONSTRAINT [PK_Proizvod] PRIMARY KEY CLUSTERED 
(
	[Proizvod_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecijalnaPonuda]    Script Date: 03-Apr-25 23:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecijalnaPonuda](
	[Ponuda_ID] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Popust] [decimal](10, 2) NOT NULL,
	[Opis] [nvarchar](250) NOT NULL,
	[Korisnik_ID] [int] NOT NULL,
 CONSTRAINT [PK_SpecijalnaPonuda] PRIMARY KEY CLUSTERED 
(
	[Ponuda_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 03-Apr-25 23:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[Status_id] [int] NOT NULL,
	[Naziv] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[Status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Kategorija] ON 

INSERT [dbo].[Kategorija] ([Kategorija_ID], [Naziv]) VALUES (1, N'Kape')
INSERT [dbo].[Kategorija] ([Kategorija_ID], [Naziv]) VALUES (2, N'Salovi')
INSERT [dbo].[Kategorija] ([Kategorija_ID], [Naziv]) VALUES (3, N'Džemperi')
INSERT [dbo].[Kategorija] ([Kategorija_ID], [Naziv]) VALUES (4, N'Rukavice')
INSERT [dbo].[Kategorija] ([Kategorija_ID], [Naziv]) VALUES (5, N'Čarape')
SET IDENTITY_INSERT [dbo].[Kategorija] OFF
GO
SET IDENTITY_INSERT [dbo].[Korisnik] ON 

INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (3, N'Lazar', N'Kocmanović', N'tatanakod@gmail.com', N'Bulevar Kralja Aleksandra 35A', CAST(N'2001-01-24' AS Date), N'123456789', N'0652314244     ', 333, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (4, N'Aleksa', N'Halic', N'aleksa@gmail.com', N'Bulevar Kralja Aleksandra 35A', CAST(N'2012-04-22' AS Date), N'123', N'0652314244     ', 111, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (5, N'Petar', N'Nedić', N'petar@gmail.com', N'Novopazarska 26A', CAST(N'2001-04-20' AS Date), N'123', N'0653432146565  ', 333, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1010, N'Filip', N'Ugrenović', N'ugrenovic@gmail.com', N'Ruzveltova 25', CAST(N'1984-05-24' AS Date), N'12345', N'0613432146565  ', 333, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1011, N'Milica', N'Ignjatovic', N'Ignjatovic@gmail.com', N'Durlanska', CAST(N'2002-01-23' AS Date), N'1234', N'0652314244     ', 222, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1012, N'Milica', N'Ignjatovic', N'Ignjatovic2@gmail.com', N'Durlanska', CAST(N'2002-01-23' AS Date), N'1234', N'0652314244     ', 222, 4)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1014, N'Uros', N'Halic', N'Uros@gmail.com', N'Bulevar Kralja Aleksandra 35A', CAST(N'2025-04-02' AS Date), N'4321', N'0652314244     ', 333, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1015, N'Andrija', N'Filipovic', N'andrija@gmail.com', N'Grdalička 15B', CAST(N'2004-02-23' AS Date), N'12345', N'0613432146565  ', 333, NULL)
INSERT [dbo].[Korisnik] ([Korisnik_ID], [Ime], [Prezime], [Email], [Adresa], [DatumRodjenja], [Password], [BrojTelefona], [Status_ID], [Ponuda_ID]) VALUES (1016, N'Sanjica', N'Peric', N'joZ321o@gmail.com', N'Bulevar Kralja Aleksandra 35A', CAST(N'2025-04-11' AS Date), N'1234', N'0652314244     ', 333, NULL)
SET IDENTITY_INSERT [dbo].[Korisnik] OFF
GO
SET IDENTITY_INSERT [dbo].[Narudzbina] ON 

INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1020, CAST(N'2025-04-02T20:58:12.657' AS DateTime), N'2345 5456 4345 3345', 3, 1, 2, CAST(2400.00 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1021, CAST(N'2025-04-02T20:58:30.083' AS DateTime), N'2345 5556 4745 3445', 3, 3, 4, CAST(2800.00 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1022, CAST(N'2025-04-02T22:23:25.553' AS DateTime), N'2345 5456 4345 3345', 4, 1, 3, CAST(3600.00 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1023, CAST(N'2025-04-03T20:19:44.993' AS DateTime), N'2345 5456 4345 3345', 1015, 9, 2, CAST(2772.28 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1024, CAST(N'2025-04-03T20:24:29.033' AS DateTime), N'2345 5456 4345 3345', 1012, 9, 1, CAST(1386.14 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1026, CAST(N'2025-04-03T22:22:17.113' AS DateTime), N'2345 5456 4345 3345', 1011, 2, 1, CAST(2500.00 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1027, CAST(N'2025-04-03T22:22:24.387' AS DateTime), N'2345 5456 4345 3345', 1011, 10, 2, CAST(1600.00 AS Decimal(10, 2)))
INSERT [dbo].[Narudzbina] ([Narudzbina_ID], [DatumNarudzbine], [Broj_Kartice], [Korisnik_ID], [Proizvod_ID], [Kolicina], [UkupnaCena]) VALUES (1028, CAST(N'2025-04-03T22:22:32.580' AS DateTime), N'2345 5456 4345 3345', 1011, 12, 2, CAST(1500.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Narudzbina] OFF
GO
SET IDENTITY_INSERT [dbo].[Proizvod] ON 

INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (1, N'Pletena kapa', CAST(1200.00 AS Decimal(10, 2)), 6, N'Plava kapa, ispletena najkvalitetnijom vunom', 4, 1)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (2, N'Vuneni dzemper', CAST(2500.00 AS Decimal(10, 2)), 2, N'Vuneni dzemper najboji na trzistu.', 4, 3)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (3, N'Vuneni sal', CAST(700.00 AS Decimal(10, 2)), 5, N'Vuneni sal plave boje', 4, 2)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (4, N'Narandžasta vunena kapa', CAST(1200.00 AS Decimal(10, 2)), 4, N'Narandžasta vunena kapa napravljena od najboljih materijala.', 4, 1)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (8, N'Ljubičasta vunena kapa sa punđom', CAST(1250.00 AS Decimal(10, 2)), 5, N'Ljubičasta vunena kapa napravljena od najboljih materijala.', 4, 1)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (9, N'Pletena kapa', CAST(1400.00 AS Decimal(10, 2)), 2, N'Plava vunen kapa za svaki dan u vasem zivotu.', 4, 1)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (10, N'Sive vunene čarape', CAST(800.00 AS Decimal(10, 2)), 5, N'Sive vunene čarape, za toplije zimske dane.', 4, 5)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (11, N'Vuneni dzemper plavo sivi', CAST(3200.00 AS Decimal(10, 2)), 5, N'Vuneni dzemper najboji na trzistu.', 4, 3)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (12, N'Crne vunene rukavice', CAST(750.00 AS Decimal(10, 2)), 2, N'Vunene rukavice za hladne zimske dane.', 4, 4)
INSERT [dbo].[Proizvod] ([Proizvod_id], [Naziv], [Cena], [Kolicina], [Opis], [Korisnik_ID], [Kategorija_ID]) VALUES (14, N'Braon vunene rukavice', CAST(600.00 AS Decimal(10, 2)), 4, N'Braon vunene rukavice. Pogodne za prolecne hladne dane kao hladne zimske dane.', 4, 3)
SET IDENTITY_INSERT [dbo].[Proizvod] OFF
GO
SET IDENTITY_INSERT [dbo].[SpecijalnaPonuda] ON 

INSERT [dbo].[SpecijalnaPonuda] ([Ponuda_ID], [Naziv], [Popust], [Opis], [Korisnik_ID]) VALUES (1, N'Tvoj specijalni popust', CAST(10.00 AS Decimal(10, 2)), N'Specijalna ponuda od 10% za bilo koji nas proizvod!', 1011)
INSERT [dbo].[SpecijalnaPonuda] ([Ponuda_ID], [Naziv], [Popust], [Opis], [Korisnik_ID]) VALUES (4, N'Tvoj specijalni popust', CAST(7.00 AS Decimal(10, 2)), N'Specijalna ponuda od 10% za bilo koji nas proizvod!', 1011)
SET IDENTITY_INSERT [dbo].[SpecijalnaPonuda] OFF
GO
INSERT [dbo].[UserStatus] ([Status_id], [Naziv]) VALUES (111, N'Admin')
INSERT [dbo].[UserStatus] ([Status_id], [Naziv]) VALUES (222, N'Moderator')
INSERT [dbo].[UserStatus] ([Status_id], [Naziv]) VALUES (333, N'User')
GO
ALTER TABLE [dbo].[Korisnik] ADD  CONSTRAINT [DF_Korisnik_Status_ID]  DEFAULT ((333)) FOR [Status_ID]
GO
ALTER TABLE [dbo].[Narudzbina] ADD  CONSTRAINT [DF_Narudzbina_DatumNarudzbine]  DEFAULT (sysdatetime()) FOR [DatumNarudzbine]
GO
ALTER TABLE [dbo].[Korisnik]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_SpecijalnaPonuda] FOREIGN KEY([Ponuda_ID])
REFERENCES [dbo].[SpecijalnaPonuda] ([Ponuda_ID])
GO
ALTER TABLE [dbo].[Korisnik] CHECK CONSTRAINT [FK_Korisnik_SpecijalnaPonuda]
GO
ALTER TABLE [dbo].[Korisnik]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_UserStatus] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[UserStatus] ([Status_id])
GO
ALTER TABLE [dbo].[Korisnik] CHECK CONSTRAINT [FK_Korisnik_UserStatus]
GO
ALTER TABLE [dbo].[Narudzbina]  WITH CHECK ADD  CONSTRAINT [FK_Narudzbina_Korisnik] FOREIGN KEY([Korisnik_ID])
REFERENCES [dbo].[Korisnik] ([Korisnik_ID])
GO
ALTER TABLE [dbo].[Narudzbina] CHECK CONSTRAINT [FK_Narudzbina_Korisnik]
GO
ALTER TABLE [dbo].[Narudzbina]  WITH CHECK ADD  CONSTRAINT [FK_Narudzbina_Proizvod] FOREIGN KEY([Proizvod_ID])
REFERENCES [dbo].[Proizvod] ([Proizvod_id])
GO
ALTER TABLE [dbo].[Narudzbina] CHECK CONSTRAINT [FK_Narudzbina_Proizvod]
GO
ALTER TABLE [dbo].[Proizvod]  WITH CHECK ADD  CONSTRAINT [FK_Proizvod_Kategorija] FOREIGN KEY([Kategorija_ID])
REFERENCES [dbo].[Kategorija] ([Kategorija_ID])
GO
ALTER TABLE [dbo].[Proizvod] CHECK CONSTRAINT [FK_Proizvod_Kategorija]
GO
USE [master]
GO
ALTER DATABASE [Pletko] SET  READ_WRITE 
GO
