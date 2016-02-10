
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/10/2016 15:00:42
-- Generated from EDMX file: D:\USERS\Saroko_P31014\My_DZ\C#\ADO.NET\DateTimeEntityTest\DateTimeEntityTest\ModelWithDateTime.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DateTimeTestEntityDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PeoplesJobs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobsSet] DROP CONSTRAINT [FK_PeoplesJobs];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PeoplesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PeoplesSet];
GO
IF OBJECT_ID(N'[dbo].[JobsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PeoplesSet'
CREATE TABLE [dbo].[PeoplesSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BirthDate] datetime  NOT NULL
);
GO

-- Creating table 'JobsSet'
CREATE TABLE [dbo].[JobsSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [JobName] nvarchar(max)  NOT NULL,
    [PeoplesID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'PeoplesSet'
ALTER TABLE [dbo].[PeoplesSet]
ADD CONSTRAINT [PK_PeoplesSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'JobsSet'
ALTER TABLE [dbo].[JobsSet]
ADD CONSTRAINT [PK_JobsSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PeoplesID] in table 'JobsSet'
ALTER TABLE [dbo].[JobsSet]
ADD CONSTRAINT [FK_PeoplesJobs]
    FOREIGN KEY ([PeoplesID])
    REFERENCES [dbo].[PeoplesSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PeoplesJobs'
CREATE INDEX [IX_FK_PeoplesJobs]
ON [dbo].[JobsSet]
    ([PeoplesID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------