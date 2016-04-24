
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/02/2016 12:56:10
-- Generated from EDMX file: D:\USERS\Saroko_P31014\My_DZ\C#\WCF\ConsoleApplication1\ConsoleApplication2\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NewDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AutoSet'
CREATE TABLE [dbo].[AutoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Model] nvarchar(max)  NULL,
    [OwnerID] int  NOT NULL
);
GO

-- Creating table 'AutoOwnerSet'
CREATE TABLE [dbo].[AutoOwnerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] int  NULL,
    [Auto_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AutoSet'
ALTER TABLE [dbo].[AutoSet]
ADD CONSTRAINT [PK_AutoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AutoOwnerSet'
ALTER TABLE [dbo].[AutoOwnerSet]
ADD CONSTRAINT [PK_AutoOwnerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Auto_Id] in table 'AutoOwnerSet'
ALTER TABLE [dbo].[AutoOwnerSet]
ADD CONSTRAINT [FK_AutoAutoOwner]
    FOREIGN KEY ([Auto_Id])
    REFERENCES [dbo].[AutoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutoAutoOwner'
CREATE INDEX [IX_FK_AutoAutoOwner]
ON [dbo].[AutoOwnerSet]
    ([Auto_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------