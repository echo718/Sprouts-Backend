IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809113019_Initial')
BEGIN
    CREATE TABLE [Kids] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Age] nvarchar(max) NULL,
        [GitHub] nvarchar(max) NOT NULL,
        [ImageURI] nvarchar(max) NULL,
        CONSTRAINT [PK_Kids] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809113019_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210809113019_Initial', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809114624_AddStudy')
BEGIN
    CREATE TABLE [Studies] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NOT NULL,
        [Language] nvarchar(max) NOT NULL,
        [ImageURI] int NOT NULL,
        [KidId] int NOT NULL,
        [Modified] datetime2 NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Studies] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Studies_Kids_KidId] FOREIGN KEY ([KidId]) REFERENCES [Kids] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809114624_AddStudy')
BEGIN
    CREATE INDEX [IX_Studies_KidId] ON [Studies] ([KidId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210809114624_AddStudy')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210809114624_AddStudy', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810003710_changeConnection')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Studies]') AND [c].[name] = N'ImageURI');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Studies] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Studies] ALTER COLUMN [ImageURI] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810003710_changeConnection')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Kids]') AND [c].[name] = N'ImageURI');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Kids] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Kids] ALTER COLUMN [ImageURI] nvarchar(max) NOT NULL;
    ALTER TABLE [Kids] ADD DEFAULT N'' FOR [ImageURI];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810003710_changeConnection')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Kids]') AND [c].[name] = N'Age');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Kids] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Kids] ALTER COLUMN [Age] nvarchar(max) NOT NULL;
    ALTER TABLE [Kids] ADD DEFAULT N'' FOR [Age];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810003710_changeConnection')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210810003710_changeConnection', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810013157_AddSelf')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210810013157_AddSelf', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210810020028_AddSelf1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210810020028_AddSelf1', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822044804_DelStudy')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210822044804_DelStudy', N'5.0.8');
END;
GO

COMMIT;
GO

