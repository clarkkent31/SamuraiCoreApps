IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Department] (
    [DepartmentId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([DepartmentId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130122953_EFCoreTstDb_1', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Department] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130123052_EFCoreTstDb_2', N'2.1.4-rtm-31024');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Department] ALTER COLUMN [Description] nvarchar(250) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130123143_EFCoreTstDb_3', N'2.1.4-rtm-31024');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'CreatedDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Department] ALTER COLUMN [CreatedDate] datetime2 NOT NULL;
ALTER TABLE [Department] ADD DEFAULT '2018-11-30T14:34:48.0080000+02:00' FOR [CreatedDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130123448_EFCoreTstDb_4', N'2.1.4-rtm-31024');

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'Name');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Department] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'CreatedDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Department] ALTER COLUMN [CreatedDate] datetime2 NOT NULL;
ALTER TABLE [Department] ADD DEFAULT '2018-11-30T14:40:34.8820000+02:00' FOR [CreatedDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130124035_EFCoreTstDb_5', N'2.1.4-rtm-31024');

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'Name');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Department] ALTER COLUMN [Name] nvarchar(250) NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'CreatedDate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Department] ALTER COLUMN [CreatedDate] datetime2 NOT NULL;
ALTER TABLE [Department] ADD DEFAULT '2018-11-30T14:57:11.0840000+02:00' FOR [CreatedDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130125711_EFCoreTstDb_6', N'2.1.4-rtm-31024');

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Department]') AND [c].[name] = N'CreatedDate');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Department] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Department] ALTER COLUMN [CreatedDate] datetime2 NOT NULL;
ALTER TABLE [Department] ADD DEFAULT '2018-11-30T15:02:45.5120000+02:00' FOR [CreatedDate];

GO

CREATE TABLE [Employee] (
    [EmployeeId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Gender] nvarchar(10) NOT NULL DEFAULT N'Male',
    CONSTRAINT [PK_Employee] PRIMARY KEY ([EmployeeId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181130130245_EFCoreTstDb_7', N'2.1.4-rtm-31024');

GO

