use EduhubDB;


CREATE TABLE Enrollments (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    CourseId INT,
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    status VARCHAR(20),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
CREATE TABLE Feedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    CourseId INT,
    Feedback TEXT,
    Date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);



CREATE PROCEDURE SP_ViewFeedbackByStudent
@UserId INT
AS
BEGIN
    SELECT f.FeedbackId, u.UserName AS StudentName, c.UserId, c.Title, f.Feedback, f.Date
    FROM Feedbacks f
    JOIN Courses c ON f.CourseId = c.CourseId
    JOIN Users u ON f.UserId = u.UserId
    WHERE c.UserId = @UserId;
END;
GO


Exec SP_ViewFeedbackByStudent @UserId=2;
SELECT 
    C.CourseId,
    C.Title AS CourseTitle,
    U.UserName AS StudentName
    
FROM 
    Enrollments E
JOIN 
    Courses C ON E.CourseId = C.CourseId
JOIN 
    Users U ON E.UserId = U.UserId
WHERE 
    U.UserId =1;

	select * from Enrollments



CREATE PROCEDURE MyStudentCoureses
@UserId INT
AS
BEGIN
    SELECT 
    C.CourseId,
    C.Title AS CourseTitle,
    U.UserName AS StudentName
    
		FROM 
    Enrollments E
			JOIN 
    Courses C ON E.CourseId = C.CourseId
			JOIN 
    Users U ON E.UserId = U.UserId
WHERE  U.UserId =@UserId;
END;
GO

Exec MyStudentCoureses @userId=1;


SELECT 
    C.Title AS CourseTitle,
    C.Description,
    C.CourseStartDate,
    C.CourseEndDate,
    F.Feedback
FROM 
    Courses C
JOIN 
    Feedbacks F ON C.CourseId = F.CourseId
ORDER BY 
    C.Title;




CREATE TABLE Materials (
    MaterialId INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT,
    Title VARCHAR(100),
    Description TEXT,
    URL VARCHAR(255),
    UploadDate DATETIME DEFAULT GETDATE(),
    ContentType VARCHAR(50),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
use EduhubDB;

SELECT 
    M.CourseId,
    M.Title AS MaterialTitle,
    M.Description AS MaterialDescription,
    M.URL
FROM 
    Materials M
JOIN 
    Courses C ON M.CourseId = C.CourseId
JOIN 
    Users U ON C.UserId = U.UserId
WHERE 
    U.UserId = 2;



UPDATE M
SET M.Description = 'New Description'
FROM Materials M
JOIN Courses C ON M.CourseId = C.CourseId
JOIN Users U ON C.UserId = U.UserId
WHERE U.UserId = 2 
AND M.CourseId = C.CourseId
AND C.CourseId = 2;


select * from Materials;
