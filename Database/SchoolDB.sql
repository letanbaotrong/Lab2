CREATE DATABASE SchoolDB;
GO

USE SchoolDB;
GO

CREATE TABLE Students
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(150) NULL
);
GO

INSERT INTO Students (Name, Age, Email) VALUES
(N'Nguyen Van An', 21, N'an@example.com'),
(N'Tran Thi Binh', 22, N'binh@example.com'),
(N'Le Minh Chau', 20, N'chau@example.com');
GO

CREATE TABLE Courses
(
    Id INT PRIMARY KEY IDENTITY,
    CourseName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE StudentCourses
(
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);
GO

INSERT INTO Courses (CourseName) VALUES
(N'Lap trinh .NET'),
(N'Co so du lieu'),
(N'Kien truc phan mem');
GO

INSERT INTO StudentCourses (StudentId, CourseId) VALUES
(1, 1), (1, 2),
(2, 2), (2, 3),
(3, 1), (3, 3);
GO
