USE [MusicDb]
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 26.8.2019 10:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Albums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[ArtistID] [int] NOT NULL,
	[YearReleased] [int] NULL,
	[Label] [nvarchar](50) NULL,
	[Cover] [varbinary](max) NULL,
 CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bands]    Script Date: 26.8.2019 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bands](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Bio] [nvarchar](max) NULL,
	[YearFormed] [int] NULL,
	[Site] [nvarchar](max) NULL,
	[Origin] [nvarchar](255) NULL,
	[Image] [varbinary](max) NULL,
 CONSTRAINT [PK_Bands] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Albums] ON 

INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (1, N'Kill ''em All', 1, 1983, N'Megaforce', NULL)
INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (2, N'Ride The Lightning', 1, 1984, N'Megaforce', NULL)
INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (3, N'Master of Puppets', 1, 1986, N'Electra', NULL)
INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (4, N'Iron Maiden', 2, 1980, N'EMI Records', NULL)
INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (5, N'Killers', 2, 1981, N'EMI Records', NULL)
INSERT [dbo].[Albums] ([ID], [Title], [ArtistID], [YearReleased], [Label], [Cover]) VALUES (6, N'The Number of the Beast', 2, 1982, N'EMI Records', NULL)
SET IDENTITY_INSERT [dbo].[Albums] OFF
SET IDENTITY_INSERT [dbo].[Bands] ON 

INSERT [dbo].[Bands] ([ID], [Name], [Bio], [YearFormed], [Site], [Origin], [Image]) VALUES (1, N'Metallica', N'Metallica is an American heavy metal band. The band was formed in 1981 in Los Angeles by vocalist/guitarist James Hetfield and drummer Lars Ulrich, and has been based in San Francisco for most of its career.[1][2] The group''s fast tempos, instrumentals and aggressive musicianship made them one of the founding "big four" bands of thrash metal, alongside Megadeth, Anthrax and Slayer. Metallica''s current lineup comprises founding members and primary songwriters Hetfield and Ulrich, longtime lead guitarist Kirk Hammett and bassist Robert Trujillo. Guitarist Dave Mustaine (who went on to form Megadeth after being fired from the band) and bassists Ron McGovney, Cliff Burton (who died in a bus accident in Sweden in 1986) and Jason Newsted are former members of the band.', 1981, N'https://www.metallica.com', N'Los Angeles', NULL)
INSERT [dbo].[Bands] ([ID], [Name], [Bio], [YearFormed], [Site], [Origin], [Image]) VALUES (2, N'Iron Maiden', N'Iron Maiden are an English heavy metal band formed in Leyton, East London, in 1975 by bassist and primary songwriter Steve Harris. The band''s discography has grown to thirty-nine albums, including sixteen studio albums, twelve live albums, four EPs, and seven compilations.

Pioneers of the new wave of British heavy metal, Iron Maiden achieved initial success during the early 1980s. After several line-up changes, the band went on to release a series of UK and US platinum and gold albums, including 1982''s The Number of the Beast, 1983''s Piece of Mind, 1984''s Powerslave, 1985''s live release Live After Death, 1986''s Somewhere in Time and 1988''s Seventh Son of a Seventh Son. Since the return of lead vocalist Bruce Dickinson and guitarist Adrian Smith in 1999, the band has undergone a resurgence in popularity,[2] with their 2010 studio offering, The Final Frontier, peaking at No. 1 in 28 countries and receiving widespread critical acclaim. Their sixteenth studio album, The Book of Souls, was released on 4 September 2015 to similar success.', 1975, N'https://ironmaiden.com/', N'London', NULL)
SET IDENTITY_INSERT [dbo].[Bands] OFF
ALTER TABLE [dbo].[Albums]  WITH CHECK ADD  CONSTRAINT [FK_Albums_Bands] FOREIGN KEY([ArtistID])
REFERENCES [dbo].[Bands] ([ID])
GO
ALTER TABLE [dbo].[Albums] CHECK CONSTRAINT [FK_Albums_Bands]
GO
