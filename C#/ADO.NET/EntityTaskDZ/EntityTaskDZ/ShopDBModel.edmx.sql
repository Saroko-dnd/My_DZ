
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/10/2016 16:03:42
-- Generated from EDMX file: D:\USERS\Saroko_P31014\My_DZ\C#\ADO.NET\EntityTaskDZ\EntityTaskDZ\ShopDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBFromModelTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OfficeAssignmentInstructor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OfficeAssignmentSet] DROP CONSTRAINT [FK_OfficeAssignmentInstructor];
GO
IF OBJECT_ID(N'[dbo].[FK_InstructorCourse_Instructor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstructorCourse] DROP CONSTRAINT [FK_InstructorCourse_Instructor];
GO
IF OBJECT_ID(N'[dbo].[FK_InstructorCourse_Course]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstructorCourse] DROP CONSTRAINT [FK_InstructorCourse_Course];
GO
IF OBJECT_ID(N'[dbo].[FK_EnrollmentCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnrollmentSet] DROP CONSTRAINT [FK_EnrollmentCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseSet] DROP CONSTRAINT [FK_CourseDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentEnrollment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnrollmentSet] DROP CONSTRAINT [FK_StudentEnrollment];
GO
IF OBJECT_ID(N'[dbo].[FK_InstructorDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentSet] DROP CONSTRAINT [FK_InstructorDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_Instructor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Instructor] DROP CONSTRAINT [FK_Instructor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Student] DROP CONSTRAINT [FK_Student_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[OfficeAssignmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfficeAssignmentSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[EnrollmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnrollmentSet];
GO
IF OBJECT_ID(N'[dbo].[CourseSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseSet];
GO
IF OBJECT_ID(N'[dbo].[DepartmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Instructor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Instructor];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Student];
GO
IF OBJECT_ID(N'[dbo].[InstructorCourse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstructorCourse];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OfficeAssignmentSet'
CREATE TABLE [dbo].[OfficeAssignmentSet] (
    [InstructorID] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Instructor_ID] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [FirstMidName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EnrollmentSet'
CREATE TABLE [dbo].[EnrollmentSet] (
    [EnrollmentID] int IDENTITY(1,1) NOT NULL,
    [CourseID] int  NOT NULL,
    [Grade] int  NOT NULL,
    [StudentID] int  NOT NULL,
    [Course_CourseID] int  NOT NULL
);
GO

-- Creating table 'CourseSet'
CREATE TABLE [dbo].[CourseSet] (
    [CourseID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Credits] nvarchar(max)  NOT NULL,
    [DepartmentID] int  NOT NULL,
    [Department_DepartmentID] int  NOT NULL
);
GO

-- Creating table 'DepartmentSet'
CREATE TABLE [dbo].[DepartmentSet] (
    [DepartmentID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Budget] float  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [RowVersion] nvarchar(max)  NOT NULL,
    [InstructorID] int  NULL
);
GO

-- Creating table 'PersonSet_Instructor'
CREATE TABLE [dbo].[PersonSet_Instructor] (
    [HireDate] datetime  NULL,
    [ID] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Student'
CREATE TABLE [dbo].[PersonSet_Student] (
    [EnrollmentDate] datetime  NOT NULL,
    [ID] int  NOT NULL
);
GO

-- Creating table 'InstructorCourse'
CREATE TABLE [dbo].[InstructorCourse] (
    [Instructor_ID] int  NOT NULL,
    [Course_CourseID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [InstructorID] in table 'OfficeAssignmentSet'
ALTER TABLE [dbo].[OfficeAssignmentSet]
ADD CONSTRAINT [PK_OfficeAssignmentSet]
    PRIMARY KEY CLUSTERED ([InstructorID] ASC);
GO

-- Creating primary key on [ID] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EnrollmentID] in table 'EnrollmentSet'
ALTER TABLE [dbo].[EnrollmentSet]
ADD CONSTRAINT [PK_EnrollmentSet]
    PRIMARY KEY CLUSTERED ([EnrollmentID] ASC);
GO

-- Creating primary key on [CourseID] in table 'CourseSet'
ALTER TABLE [dbo].[CourseSet]
ADD CONSTRAINT [PK_CourseSet]
    PRIMARY KEY CLUSTERED ([CourseID] ASC);
GO

-- Creating primary key on [DepartmentID] in table 'DepartmentSet'
ALTER TABLE [dbo].[DepartmentSet]
ADD CONSTRAINT [PK_DepartmentSet]
    PRIMARY KEY CLUSTERED ([DepartmentID] ASC);
GO

-- Creating primary key on [ID] in table 'PersonSet_Instructor'
ALTER TABLE [dbo].[PersonSet_Instructor]
ADD CONSTRAINT [PK_PersonSet_Instructor]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PersonSet_Student'
ALTER TABLE [dbo].[PersonSet_Student]
ADD CONSTRAINT [PK_PersonSet_Student]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Instructor_ID], [Course_CourseID] in table 'InstructorCourse'
ALTER TABLE [dbo].[InstructorCourse]
ADD CONSTRAINT [PK_InstructorCourse]
    PRIMARY KEY CLUSTERED ([Instructor_ID], [Course_CourseID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Instructor_ID] in table 'OfficeAssignmentSet'
ALTER TABLE [dbo].[OfficeAssignmentSet]
ADD CONSTRAINT [FK_OfficeAssignmentInstructor]
    FOREIGN KEY ([Instructor_ID])
    REFERENCES [dbo].[PersonSet_Instructor]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OfficeAssignmentInstructor'
CREATE INDEX [IX_FK_OfficeAssignmentInstructor]
ON [dbo].[OfficeAssignmentSet]
    ([Instructor_ID]);
GO

-- Creating foreign key on [Instructor_ID] in table 'InstructorCourse'
ALTER TABLE [dbo].[InstructorCourse]
ADD CONSTRAINT [FK_InstructorCourse_Instructor]
    FOREIGN KEY ([Instructor_ID])
    REFERENCES [dbo].[PersonSet_Instructor]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Course_CourseID] in table 'InstructorCourse'
ALTER TABLE [dbo].[InstructorCourse]
ADD CONSTRAINT [FK_InstructorCourse_Course]
    FOREIGN KEY ([Course_CourseID])
    REFERENCES [dbo].[CourseSet]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstructorCourse_Course'
CREATE INDEX [IX_FK_InstructorCourse_Course]
ON [dbo].[InstructorCourse]
    ([Course_CourseID]);
GO

-- Creating foreign key on [Course_CourseID] in table 'EnrollmentSet'
ALTER TABLE [dbo].[EnrollmentSet]
ADD CONSTRAINT [FK_EnrollmentCourse]
    FOREIGN KEY ([Course_CourseID])
    REFERENCES [dbo].[CourseSet]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnrollmentCourse'
CREATE INDEX [IX_FK_EnrollmentCourse]
ON [dbo].[EnrollmentSet]
    ([Course_CourseID]);
GO

-- Creating foreign key on [Department_DepartmentID] in table 'CourseSet'
ALTER TABLE [dbo].[CourseSet]
ADD CONSTRAINT [FK_CourseDepartment]
    FOREIGN KEY ([Department_DepartmentID])
    REFERENCES [dbo].[DepartmentSet]
        ([DepartmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseDepartment'
CREATE INDEX [IX_FK_CourseDepartment]
ON [dbo].[CourseSet]
    ([Department_DepartmentID]);
GO

-- Creating foreign key on [StudentID] in table 'EnrollmentSet'
ALTER TABLE [dbo].[EnrollmentSet]
ADD CONSTRAINT [FK_StudentEnrollment]
    FOREIGN KEY ([StudentID])
    REFERENCES [dbo].[PersonSet_Student]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentEnrollment'
CREATE INDEX [IX_FK_StudentEnrollment]
ON [dbo].[EnrollmentSet]
    ([StudentID]);
GO

-- Creating foreign key on [InstructorID] in table 'DepartmentSet'
ALTER TABLE [dbo].[DepartmentSet]
ADD CONSTRAINT [FK_InstructorDepartment]
    FOREIGN KEY ([InstructorID])
    REFERENCES [dbo].[PersonSet_Instructor]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstructorDepartment'
CREATE INDEX [IX_FK_InstructorDepartment]
ON [dbo].[DepartmentSet]
    ([InstructorID]);
GO

-- Creating foreign key on [ID] in table 'PersonSet_Instructor'
ALTER TABLE [dbo].[PersonSet_Instructor]
ADD CONSTRAINT [FK_Instructor_inherits_Person]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[PersonSet]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID] in table 'PersonSet_Student'
ALTER TABLE [dbo].[PersonSet_Student]
ADD CONSTRAINT [FK_Student_inherits_Person]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[PersonSet]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------