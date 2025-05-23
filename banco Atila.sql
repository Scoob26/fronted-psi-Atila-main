USE [AndrezaMeloNeuropsicanalista]
GO
/****** Object:  User [admin]    Script Date: 19/02/2025 10:31:24 ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[TB_Agendamento]    Script Date: 19/02/2025 10:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Agendamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DTHoraAgendamento] [datetime] NOT NULL,
	[DataAtendimento] [date] NOT NULL,
	[Horario] [time](0) NOT NULL,
	[fk_Usuario_ID] [int] NOT NULL,
	[fk_Servico_ID] [int] NOT NULL,
 CONSTRAINT [PK_TB_Atendimento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Servico]    Script Date: 19/02/2025 10:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Servico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TipoServico] [varchar](50) NOT NULL,
	[Valor] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_TB_Servico] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Usuario]    Script Date: 19/02/2025 10:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefone] [varchar](50) NOT NULL,
	[Senha] [varchar](100) NOT NULL,
	[TipoUsuario] [int] NOT NULL,
	[DataHoraCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Tb_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Agendamento]    Script Date: 19/02/2025 10:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Agendamento]
AS
SELECT        dbo.TB_Agendamento.Id, dbo.TB_Agendamento.DTHoraAgendamento, dbo.TB_Agendamento.DataAtendimento, dbo.TB_Agendamento.Horario, dbo.TB_Servico.TipoServico, dbo.TB_Servico.Valor, dbo.TB_Usuario.Nome, 
                         dbo.TB_Usuario.Email, dbo.TB_Usuario.Telefone
FROM            dbo.TB_Agendamento INNER JOIN
                         dbo.TB_Servico ON dbo.TB_Agendamento.fk_Servico_ID = dbo.TB_Servico.id INNER JOIN
                         dbo.TB_Usuario ON dbo.TB_Agendamento.fk_Usuario_ID = dbo.TB_Usuario.Id
GO
ALTER TABLE [dbo].[TB_Usuario] ADD  CONSTRAINT [DF_TB_Usuario_DataHoraCadastro]  DEFAULT (getdate()) FOR [DataHoraCadastro]
GO
ALTER TABLE [dbo].[TB_Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_TB_Atendimento_TB_Servico] FOREIGN KEY([fk_Servico_ID])
REFERENCES [dbo].[TB_Servico] ([id])
GO
ALTER TABLE [dbo].[TB_Agendamento] CHECK CONSTRAINT [FK_TB_Atendimento_TB_Servico]
GO
ALTER TABLE [dbo].[TB_Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_TB_Atendimento_TB_Usuario] FOREIGN KEY([fk_Usuario_ID])
REFERENCES [dbo].[TB_Usuario] ([Id])
GO
ALTER TABLE [dbo].[TB_Agendamento] CHECK CONSTRAINT [FK_TB_Atendimento_TB_Usuario]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TB_Agendamento"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "TB_Servico"
            Begin Extent = 
               Top = 6
               Left = 281
               Bottom = 119
               Right = 477
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TB_Usuario"
            Begin Extent = 
               Top = 6
               Left = 515
               Bottom = 136
               Right = 711
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Agendamento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Agendamento'
GO
