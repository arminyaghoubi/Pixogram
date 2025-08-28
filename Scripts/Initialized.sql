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
CREATE TABLE [Post] (
    [Id] uniqueidentifier NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [DatePosted] datetime2 NOT NULL,
    [Likes] int NOT NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY ([Id])
);

CREATE TABLE [Comment] (
    [Id] uniqueidentifier NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [IsEdited] bit NOT NULL,
    [CommentDate] datetime2 NOT NULL,
    [PostId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comment_Post_PostId] FOREIGN KEY ([PostId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Comment_PostId] ON [Comment] ([PostId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250821081125_Initialized', N'9.0.8');

COMMIT;
GO

