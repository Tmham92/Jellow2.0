
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2020 12:12:37
-- Generated from EDMX file: C:\Users\Gebruiker\source\repos\Jellow2.0\Jellow2.0\Database\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Jellow2.0];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompanyJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_CompanyJob];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsumerProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ConsumerProject];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_CompanyProject];
GO
IF OBJECT_ID(N'[dbo].[FK_FreelancerJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_FreelancerJob];
GO
IF OBJECT_ID(N'[dbo].[FK_FreelancerProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_FreelancerProject];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Consumers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consumers];
GO
IF OBJECT_ID(N'[dbo].[Jobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jobs];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[Freelancers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Freelancers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompanyID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [HasProjectsPosted] bit  NOT NULL,
    [HasJobsPosted] bit  NOT NULL
);
GO

-- Creating table 'Consumers'
CREATE TABLE [dbo].[Consumers] (
    [ConsumerID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [HasProjectPosted] bit  NOT NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [JobID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Experience] float  NOT NULL,
    [Salary] decimal(18,0)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [CompanyID] int  NOT NULL,
    [FreelancerID] int  NULL,
    [SkillRequirement] smallint  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Experience] float  NOT NULL,
    [Budget] decimal(18,0)  NOT NULL,
    [DueDate] nvarchar(max)  NOT NULL,
    [ConsumerID] int  NULL,
    [CompanyID] int  NULL,
    [FreelancerID] int  NULL,
    [SkillRequirement] smallint  NOT NULL
);
GO

-- Creating table 'Freelancers'
CREATE TABLE [dbo].[Freelancers] (
    [FreelancerID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsEmployed] bit  NOT NULL,
    [Experience] float  NOT NULL,
    [Skill] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CompanyID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompanyID] ASC);
GO

-- Creating primary key on [ConsumerID] in table 'Consumers'
ALTER TABLE [dbo].[Consumers]
ADD CONSTRAINT [PK_Consumers]
    PRIMARY KEY CLUSTERED ([ConsumerID] ASC);
GO

-- Creating primary key on [JobID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([JobID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [FreelancerID] in table 'Freelancers'
ALTER TABLE [dbo].[Freelancers]
ADD CONSTRAINT [PK_Freelancers]
    PRIMARY KEY CLUSTERED ([FreelancerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_CompanyJob]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[Companies]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyJob'
CREATE INDEX [IX_FK_CompanyJob]
ON [dbo].[Jobs]
    ([CompanyID]);
GO

-- Creating foreign key on [ConsumerID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ConsumerProject]
    FOREIGN KEY ([ConsumerID])
    REFERENCES [dbo].[Consumers]
        ([ConsumerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsumerProject'
CREATE INDEX [IX_FK_ConsumerProject]
ON [dbo].[Projects]
    ([ConsumerID]);
GO

-- Creating foreign key on [CompanyID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_CompanyProject]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[Companies]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyProject'
CREATE INDEX [IX_FK_CompanyProject]
ON [dbo].[Projects]
    ([CompanyID]);
GO

-- Creating foreign key on [FreelancerID] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_FreelancerJob]
    FOREIGN KEY ([FreelancerID])
    REFERENCES [dbo].[Freelancers]
        ([FreelancerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FreelancerJob'
CREATE INDEX [IX_FK_FreelancerJob]
ON [dbo].[Jobs]
    ([FreelancerID]);
GO

-- Creating foreign key on [FreelancerID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_FreelancerProject]
    FOREIGN KEY ([FreelancerID])
    REFERENCES [dbo].[Freelancers]
        ([FreelancerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FreelancerProject'
CREATE INDEX [IX_FK_FreelancerProject]
ON [dbo].[Projects]
    ([FreelancerID]);
GO



INSERT INTO Freelancers VALUES ('Tobias Ham', 'false', '1', '4');
INSERT INTO Freelancers VALUES ('Henk Jansen', 'false', '5', '1');
INSERT INTO Freelancers VALUES ('Marieke Naarde', 'false', '3.5', '5');
INSERT INTO Freelancers VALUES ('Angela', 'false', '3', '6');
INSERT INTO Freelancers VALUES ('Johnny Bravo', 'false', '4.5', '2');

INSERT INTO Companies VALUES ('Facebook', 'false', 'false');
INSERT INTO Companies VALUES ('Google', 'false', 'false');
INSERT INTO Companies VALUES ('Apple', 'false', 'false');
INSERT INTO Companies VALUES ('Bol.com', 'false', 'false');
INSERT INTO Companies VALUES ('WindowCleaners.Inc', 'false', 'false');
INSERT INTO Companies VALUES ('Pizzaria Mama Mia', 'false', 'false');

INSERT INTO Consumers VALUES ('Tobias Ham', 'false');
INSERT INTO Consumers VALUES ('Carla Hoogwater', 'false');
INSERT INTO Consumers VALUES ('Reda Haroun', 'false');
INSERT INTO Consumers VALUES ('Quinten Kolver', 'false');

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------