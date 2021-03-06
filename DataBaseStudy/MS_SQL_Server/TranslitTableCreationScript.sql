USE [Internet_Shop]
GO
/****** Object:  Table [dbo].[TransliterationTable]    Script Date: 22.12.2015 10:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransliterationTable](
	[Ru] [nvarchar](2) NULL,
	[En] [nvarchar](4) NULL
) ON [PRIMARY]

GO
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ый', N'y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ЫЙ', N'Y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'а', N'a')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'б', N'b')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'в', N'v')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'г', N'g')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'д', N'd')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'е', N'e')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ё', N'yo')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ж', N'zh')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'з', N'z')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'и', N'i')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'й', N'y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'к', N'k')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'л', N'l')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'м', N'm')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'н', N'n')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'о', N'o')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'п', N'p')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'р', N'r')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'с', N's')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'т', N't')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'у', N'u')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ф', N'f')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'х', N'kh')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ц', N'c')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ч', N'ch')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ш', N'sh')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'щ', N'shch')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ъ', N'')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ы', N'y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ь', N'''')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'э', N'e')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'ю', N'yu')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'я', N'ya')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'А', N'A')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Б', N'B')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'В', N'V')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Г', N'G')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Д', N'D')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Е', N'E')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ё', N'YO')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ж', N'ZH')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'З', N'Z')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'И', N'I')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Й', N'Y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'К', N'K')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Л', N'L')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'М', N'M')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Н', N'N')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'О', N'O')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'П', N'P')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Р', N'R')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'С', N'S')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Т', N'T')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'У', N'U')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ф', N'F')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Х', N'KH')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ц', N'C')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ч', N'CH')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ш', N'SH')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Щ', N'SHCH')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ъ', N'')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ы', N'Y')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ь', N'''')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Э', N'E')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Ю', N'YU')
INSERT [dbo].[TransliterationTable] ([Ru], [En]) VALUES (N'Я', N'YA')
