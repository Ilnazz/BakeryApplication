USE [bakery_database]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[Salary] [money] NOT NULL,
	[GenderId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [int] NOT NULL,
	[MaterialSpecification] [int] NOT NULL,
	[MaterialsPurchasePlanId] [int] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialSpecification]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialSpecification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeasureUnitId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MaterialSpecification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsPurchasePlan]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsPurchasePlan](
	[Id] [int] NOT NULL,
	[PlanStateId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_MaterialsPurchasePlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsPurchasePlan_ResponsibleEmployees]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsPurchasePlan_ResponsibleEmployees](
	[MaterialsPurchasePlanId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_MaterialsPurchasePlan_ResponsibleEmployees] PRIMARY KEY CLUSTERED 
(
	[MaterialsPurchasePlanId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification](
	[MaterialsPurchasePlanId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[MaterialSpecificationId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_MaterialsPurchasePlan_SupplierAndMaterialSpecification] PRIMARY KEY CLUSTERED 
(
	[MaterialsPurchasePlanId] ASC,
	[SupplierId] ASC,
	[MaterialSpecificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasureUnit]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasureUnit](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_MeasureUnit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanState]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanState](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PlanState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] NOT NULL,
	[ProductSpecificationId] [int] NOT NULL,
	[ProductionPlanId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionPlan]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionPlan](
	[Id] [int] NOT NULL,
	[PlanStateId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductionPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionPlan_Product]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionPlan_Product](
	[ProductionPlanid] [int] NOT NULL,
	[ProductSpecificationId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductionPlan_Product] PRIMARY KEY CLUSTERED 
(
	[ProductionPlanid] ASC,
	[ProductSpecificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionPlan_ResponsibleEmployees]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionPlan_ResponsibleEmployees](
	[ProductionPlanId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_ProductionPlan_ResponsibleEmployees] PRIMARY KEY CLUSTERED 
(
	[ProductionPlanId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductRecipe]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductRecipe](
	[ProductSpecificationId] [int] NOT NULL,
	[MaterialSpecificationId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductRecipe] PRIMARY KEY CLUSTERED 
(
	[ProductSpecificationId] ASC,
	[MaterialSpecificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSpecification]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSpecification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [money] NOT NULL,
	[Weight] [int] NOT NULL,
	[WeightMeasureUnitId] [int] NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_ProductSpecification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Realization]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Realization](
	[Id] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Realization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Realization_Product]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Realization_Product](
	[RealizationId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Realization_Product] PRIMARY KEY CLUSTERED 
(
	[RealizationId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Realization_ResponsibleEmployees]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Realization_ResponsibleEmployees](
	[RealizationId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_Realization_ResponsibleEmployees] PRIMARY KEY CLUSTERED 
(
	[RealizationId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14.12.2022 4:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Salary], [GenderId], [RoleId]) VALUES (1, N'Марков', N'Дмитрий', N'Викторович', 100000.0000, 1, 1)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Salary], [GenderId], [RoleId]) VALUES (2, N'Воробьёв', N'Илья', N'Ярославович', 50000.0000, 1, 2)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Salary], [GenderId], [RoleId]) VALUES (3, N'Антонова', N'Мария', N'Олеговна', 75000.0000, 2, 3)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Salary], [GenderId], [RoleId]) VALUES (4, N'Верещагина', N'Елена', N'Данииловна', 50000.0000, 2, 4)
INSERT [dbo].[Employee] ([Id], [Surname], [Name], [Patronymic], [Salary], [GenderId], [RoleId]) VALUES (5, N'Галимов', N'Динар', N'Олегович', 25000.0000, 1, 2)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[Gender] ([Id], [Title]) VALUES (1, N'Мужской')
INSERT [dbo].[Gender] ([Id], [Title]) VALUES (2, N'Женский')
GO
SET IDENTITY_INSERT [dbo].[MaterialSpecification] ON 

INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (1, 1, N'Мука пшеничная высшего сорта')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (2, 1, N'Мука пшеничная 1-го сорта')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (3, 1, N'Мука пшеничная 2-го сорта')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (4, 1, N'Мука пшеничная обойная')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (5, 1, N'Мука ржаная сеяная')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (6, 1, N'Мука ржаная обдирная')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (7, 1, N'Мука ржаная обойная')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (8, 1, N'Дрожжи хлебопекарные')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (9, 1, N'Соль')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (10, 3, N'Вода питьевая')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (11, 1, N'Солод')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (12, 1, N'Сахар-песок')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (13, 1, N'Патока')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (14, 3, N'Мёд натуральный')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (15, 1, N'Масло сливочное')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (16, 1, N'Маргарин')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (17, 1, N'Кондитерский жир')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (18, 5, N'Яйца куриные')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (19, 3, N'Молоко')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (20, 3, N'Молочная сыворотка')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (21, 1, N'Тыква')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (22, 1, N'Картофель')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (23, 1, N'Мак')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (24, 1, N'Семена подсолнуха')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (25, 1, N'Семена чиа')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (26, 1, N'Семена льна')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (27, 1, N'Кунжут')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (28, 1, N'Отруби пшеничные')
INSERT [dbo].[MaterialSpecification] ([Id], [MeasureUnitId], [Title]) VALUES (29, 2, N'Отруби ржание')
SET IDENTITY_INSERT [dbo].[MaterialSpecification] OFF
GO
INSERT [dbo].[MeasureUnit] ([Id], [Title]) VALUES (1, N'гр')
INSERT [dbo].[MeasureUnit] ([Id], [Title]) VALUES (2, N'кг')
INSERT [dbo].[MeasureUnit] ([Id], [Title]) VALUES (3, N'мл')
INSERT [dbo].[MeasureUnit] ([Id], [Title]) VALUES (4, N'л')
INSERT [dbo].[MeasureUnit] ([Id], [Title]) VALUES (5, N'шт')
GO
INSERT [dbo].[PlanState] ([Id], [Title]) VALUES (1, N'Создан')
INSERT [dbo].[PlanState] ([Id], [Title]) VALUES (2, N'В работе')
INSERT [dbo].[PlanState] ([Id], [Title]) VALUES (3, N'Завершён')
GO
INSERT [dbo].[ProductRecipe] ([ProductSpecificationId], [MaterialSpecificationId], [Quantity]) VALUES (1, 1, 150)
INSERT [dbo].[ProductRecipe] ([ProductSpecificationId], [MaterialSpecificationId], [Quantity]) VALUES (1, 9, 10)
INSERT [dbo].[ProductRecipe] ([ProductSpecificationId], [MaterialSpecificationId], [Quantity]) VALUES (1, 10, 50)
INSERT [dbo].[ProductRecipe] ([ProductSpecificationId], [MaterialSpecificationId], [Quantity]) VALUES (1, 12, 25)
INSERT [dbo].[ProductRecipe] ([ProductSpecificationId], [MaterialSpecificationId], [Quantity]) VALUES (1, 15, 10)
GO
SET IDENTITY_INSERT [dbo].[ProductSpecification] ON 

INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (1, N'Багет пшеничный', N'Французский пшеничный багет с хрустящей 
корочкой.', 50.0000, 250, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (2, N'Батон нарезной', N'адиционный батон из пшеничной муки высшего сорта.', 25.0000, 350, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (3, N'Хлеб Деревенский', N'рмовой пшеничный хлеб на опаре. Обладает насыщенным вкусом и ярким ароматом.', 75.0000, 540, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (4, N'Хлеб Домашний на закваске', N'еб ручной работы из пшеничной муки высшего сорта, приготовленный по старинной технологии — заквашивается с применением натуральной закваски, без хлебопекарных дрожжей 
и выпекается на каменной поверхности-поду.', 100.0000, 500, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (5, N'Хлеб с отрубями', N'леб из пшеничной муки высшего сорта и муки
ржаной обдирной с добавлением пшеничных
отрубей. ', 60.0000, 310, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (6, N'Хлеб Бородинский', N'Оригинальный заварной хлеб из ржаной муки
с добавление солода и кориандра. Насыщенный, с характерным ароматом — максимум 
пользы и вкуса.', 75.0000, 350, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (7, N'Хлеб Ржаной', N'Хлеб из пшеничной муки высшего сорта и муки
ржаной обдирной. Выпекается на каменной
поверхности, благодаря чему обладает более
выраженным вкусом и дольше остается свежим.', 50.0000, 500, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (8, N'Хлеб Украина', N'радиционный формовой круглый хлеб. Сочетает в себе вкус и пользу ржаной и пшеничной 
муки.', 65.0000, 400, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (9, N'Хлеб Картофельный', N'Ароматный формовой картофельный хлеб
с репчатым луком.', 75.0000, 300, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (10, N'Хлеб Зерновой', N'Хлеб из пшеничной муки высшего сорта и муки
ржаной обдирной с семенами кунжута, льна,
подсолнуха, тыквы и овсяными хлопьями.', 100.0000, 350, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (11, N'Хлеб Спортивный', N'Хлеб из пшеничной муки высшего сорта и муки
ржаной обдирной с добавлением злаков (лен,
кунжут, подсолнечные зерна).', 75.0000, 300, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (12, N'Хлеб Изобилие', N'Хлеб с хрустящей корочкой, нежным мякишем 
и богатейшим составом. Содержит множество 
злаков, семян, зародыши пшеницы, отруби, 
мак.', 100.0000, 300, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (13, N'Хлеб Тыквенный', N'Оригинальный хлеб на основе пшеничной муки 
с добавлением ядер подсолнечника, семян 
тыквы и отрубей.', 125.0000, 300, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (14, N'Чиабатта пшеничная', N'менитый итальянский белый хлеб с хрустящей корочкой, мягкой пористой структурой
и насыщенным вкусом, изготовленный
из пшеничной муки по старинному рецепту.', 75.0000, 200, 1, NULL)
INSERT [dbo].[ProductSpecification] ([Id], [Title], [Description], [Price], [Weight], [WeightMeasureUnitId], [Photo]) VALUES (15, N'Чиабатта ржаная', N'Знаменитый итальянский хлеб с хрустящей
корочкой, мягкой пористой структурой и насыщенным вкусом, изготовленный из пшенично-ржаной муки по старинному рецепту.', 75.0000, 200, 1, NULL)
SET IDENTITY_INSERT [dbo].[ProductSpecification] OFF
GO
INSERT [dbo].[Role] ([Id], [Title]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (2, N'Кладовщик')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (3, N'Пекарь')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (4, N'Продавец')
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([Id], [Title]) VALUES (1, N'СоюзПищеПром')
INSERT [dbo].[Supplier] ([Id], [Title]) VALUES (2, N'ЭлитФуд')
INSERT [dbo].[Supplier] ([Id], [Title]) VALUES (3, N'КазаньХлебПром')
INSERT [dbo].[Supplier] ([Id], [Title]) VALUES (4, N'Неос Ингридиентс')
INSERT [dbo].[Supplier] ([Id], [Title]) VALUES (5, N'ООО Молочные продукты')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [EmployeeId], [Login], [Password]) VALUES (1, 1, N'dmitriy', N'dmitriy')
INSERT [dbo].[User] ([Id], [EmployeeId], [Login], [Password]) VALUES (2, 2, N'ilya', N'ilya')
INSERT [dbo].[User] ([Id], [EmployeeId], [Login], [Password]) VALUES (3, 3, N'mariya', N'mariya')
INSERT [dbo].[User] ([Id], [EmployeeId], [Login], [Password]) VALUES (4, 4, N'elena', N'elena')
INSERT [dbo].[User] ([Id], [EmployeeId], [Login], [Password]) VALUES (5, 5, N'dinar', N'dinar')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Gender]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Role]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_MaterialSpecification] FOREIGN KEY([MaterialSpecification])
REFERENCES [dbo].[MaterialSpecification] ([Id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_MaterialSpecification]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_MaterialsPurchasePlan] FOREIGN KEY([MaterialsPurchasePlanId])
REFERENCES [dbo].[MaterialsPurchasePlan] ([Id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_MaterialsPurchasePlan]
GO
ALTER TABLE [dbo].[MaterialSpecification]  WITH CHECK ADD  CONSTRAINT [FK_MaterialSpecification_MeasureUnit] FOREIGN KEY([MeasureUnitId])
REFERENCES [dbo].[MeasureUnit] ([Id])
GO
ALTER TABLE [dbo].[MaterialSpecification] CHECK CONSTRAINT [FK_MaterialSpecification_MeasureUnit]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_PlanState] FOREIGN KEY([PlanStateId])
REFERENCES [dbo].[PlanState] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_PlanState]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_ResponsibleEmployees_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_ResponsibleEmployees] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_ResponsibleEmployees_Employee]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_ResponsibleEmployees_MaterialsPurchasePlan] FOREIGN KEY([MaterialsPurchasePlanId])
REFERENCES [dbo].[MaterialsPurchasePlan] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_ResponsibleEmployees] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_ResponsibleEmployees_MaterialsPurchasePlan]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_MaterialSpecification] FOREIGN KEY([MaterialSpecificationId])
REFERENCES [dbo].[MaterialSpecification] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_MaterialSpecification]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_MaterialsPurchasePlan] FOREIGN KEY([MaterialsPurchasePlanId])
REFERENCES [dbo].[MaterialsPurchasePlan] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_MaterialsPurchasePlan]
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[MaterialsPurchasePlan_SupplierAndMaterialSpecification] CHECK CONSTRAINT [FK_MaterialsPurchasePlan_SupplierAndMaterialSpecification_Supplier]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductionPlan] FOREIGN KEY([ProductionPlanId])
REFERENCES [dbo].[ProductionPlan] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductionPlan]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSpecification] FOREIGN KEY([ProductSpecificationId])
REFERENCES [dbo].[ProductSpecification] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductSpecification]
GO
ALTER TABLE [dbo].[ProductionPlan]  WITH CHECK ADD  CONSTRAINT [FK_ProductionPlan_PlanState] FOREIGN KEY([PlanStateId])
REFERENCES [dbo].[PlanState] ([Id])
GO
ALTER TABLE [dbo].[ProductionPlan] CHECK CONSTRAINT [FK_ProductionPlan_PlanState]
GO
ALTER TABLE [dbo].[ProductionPlan_Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductionPlan_Product_ProductionPlan] FOREIGN KEY([ProductionPlanid])
REFERENCES [dbo].[ProductionPlan] ([Id])
GO
ALTER TABLE [dbo].[ProductionPlan_Product] CHECK CONSTRAINT [FK_ProductionPlan_Product_ProductionPlan]
GO
ALTER TABLE [dbo].[ProductionPlan_Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductionPlan_Product_ProductSpecification] FOREIGN KEY([ProductSpecificationId])
REFERENCES [dbo].[ProductSpecification] ([Id])
GO
ALTER TABLE [dbo].[ProductionPlan_Product] CHECK CONSTRAINT [FK_ProductionPlan_Product_ProductSpecification]
GO
ALTER TABLE [dbo].[ProductionPlan_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_ProductionPlan_ResponsibleEmployees_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[ProductionPlan_ResponsibleEmployees] CHECK CONSTRAINT [FK_ProductionPlan_ResponsibleEmployees_Employee]
GO
ALTER TABLE [dbo].[ProductionPlan_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_ProductionPlan_ResponsibleEmployees_ProductionPlan] FOREIGN KEY([ProductionPlanId])
REFERENCES [dbo].[ProductionPlan] ([Id])
GO
ALTER TABLE [dbo].[ProductionPlan_ResponsibleEmployees] CHECK CONSTRAINT [FK_ProductionPlan_ResponsibleEmployees_ProductionPlan]
GO
ALTER TABLE [dbo].[ProductRecipe]  WITH CHECK ADD  CONSTRAINT [FK_ProductRecipe_MaterialSpecification] FOREIGN KEY([MaterialSpecificationId])
REFERENCES [dbo].[MaterialSpecification] ([Id])
GO
ALTER TABLE [dbo].[ProductRecipe] CHECK CONSTRAINT [FK_ProductRecipe_MaterialSpecification]
GO
ALTER TABLE [dbo].[ProductRecipe]  WITH CHECK ADD  CONSTRAINT [FK_ProductRecipe_ProductSpecification] FOREIGN KEY([ProductSpecificationId])
REFERENCES [dbo].[ProductSpecification] ([Id])
GO
ALTER TABLE [dbo].[ProductRecipe] CHECK CONSTRAINT [FK_ProductRecipe_ProductSpecification]
GO
ALTER TABLE [dbo].[ProductSpecification]  WITH CHECK ADD  CONSTRAINT [FK_ProductSpecification_MeasureUnit1] FOREIGN KEY([WeightMeasureUnitId])
REFERENCES [dbo].[MeasureUnit] ([Id])
GO
ALTER TABLE [dbo].[ProductSpecification] CHECK CONSTRAINT [FK_ProductSpecification_MeasureUnit1]
GO
ALTER TABLE [dbo].[Realization_Product]  WITH CHECK ADD  CONSTRAINT [FK_Realization_Product_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Realization_Product] CHECK CONSTRAINT [FK_Realization_Product_Product]
GO
ALTER TABLE [dbo].[Realization_Product]  WITH CHECK ADD  CONSTRAINT [FK_Realization_Product_Realization] FOREIGN KEY([RealizationId])
REFERENCES [dbo].[Realization] ([Id])
GO
ALTER TABLE [dbo].[Realization_Product] CHECK CONSTRAINT [FK_Realization_Product_Realization]
GO
ALTER TABLE [dbo].[Realization_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Realization_ResponsibleEmployees_Employee] FOREIGN KEY([RealizationId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Realization_ResponsibleEmployees] CHECK CONSTRAINT [FK_Realization_ResponsibleEmployees_Employee]
GO
ALTER TABLE [dbo].[Realization_ResponsibleEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Realization_ResponsibleEmployees_Realization] FOREIGN KEY([RealizationId])
REFERENCES [dbo].[Realization] ([Id])
GO
ALTER TABLE [dbo].[Realization_ResponsibleEmployees] CHECK CONSTRAINT [FK_Realization_ResponsibleEmployees_Realization]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Employee]
GO
