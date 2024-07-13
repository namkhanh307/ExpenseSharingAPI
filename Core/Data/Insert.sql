USE [ExpenseSharingAPICF]
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (5, N'Bảo Nam', N'0919385156', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (6, N'Quỳnh Giang', N'0919238129', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (7, N'Bích Thúy', N'0911050267', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (8, N'Viết Hương', N'0348280179', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (9, N'Quốc Hường ', N'0972502741', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (10, N'Quốc Thái', N'0938472737', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (11, N'Khánh Trình ', N'0939392838', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (12, N'Đức Quang ', N'0939482828', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (13, N'Đăng Khoa ', N'0929383883', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (14, N'Đăng Minh', N'0939393288', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (15, N'Nguyên Lâm', N'0929292923', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (16, N'Tuấn Khang ', N'0939393933', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Person] ([ID], [name], [phone], [password],[CreatedTime], [LastUpdatedTime]) VALUES (17, N'Nam Khánh', N'0929929239', N'123456','2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 
GO
INSERT [dbo].[Group] ([ID], [name], [size], [type],[CreatedTime], [LastUpdatedTime]) VALUES (2, N'Hội chị em ', 8, 1,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Group] ([ID], [name], [size], [type],[CreatedTime], [LastUpdatedTime]) VALUES (3, N'Nhà của Khánh ', 6, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Report] ON 
GO
INSERT [dbo].[Report] ([ID], [name], [groupID],[CreatedTime], [LastUpdatedTime]) VALUES (1, N'Tháng 1', 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Report] ([ID], [name], [groupID],[CreatedTime], [LastUpdatedTime]) VALUES (2, N'Đi vũng tàu', 2,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
SET IDENTITY_INSERT [dbo].[Report] OFF
GO
SET IDENTITY_INSERT [dbo].[Expense] ON 
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (5, N'Tiền điện', N'0', 100000,  1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (6, N'Tiền dịch vụ', N'0', 500000, 1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (8, N'Tiền xe của Quang', N'1', 150000,  1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (9, N'Mua rau ', N'2', 1000,  1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (10, N'Mua thịt ', N'2', 30000,  1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[Expense] ([ID], [expenseName], [expenseType], [amount], [reportID], [invoiceImage],[CreatedTime], [LastUpdatedTime]) VALUES (11, N'Mua gạo ', N'2', 170000,  1, NULL,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
SET IDENTITY_INSERT [dbo].[Expense] OFF
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 13, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 14, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 15, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 16, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (5, 17, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 13, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 14, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 15, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 16, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (6, 17, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (8, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (9, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (9, 17, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (10, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (10, 17, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (11, 12, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonExpense] ([ExpenseID], [PersonID], [GroupID],[CreatedTime], [LastUpdatedTime]) VALUES (11, 17, 3,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (5, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (6, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (7, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (8, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (9, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (10, 2, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (11, 2, 1,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (12, 3, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (13, 3, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (14, 3, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (15, 3, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (16, 3, 0,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
INSERT [dbo].[PersonGroup] ([personID], [groupID], [isAdmin],[CreatedTime], [LastUpdatedTime]) VALUES (17, 3, 1,'2024-07-09 11:43:52.4846979','2024-07-09 11:43:52.4846979')
GO
