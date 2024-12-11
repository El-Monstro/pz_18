CREATE DATABASE Demos2
USE Demos2

CREATE TABLE UserTypes (
    typeID INT PRIMARY KEY IDENTITY(1,1),
    typeName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO UserTypes (typeName)
VALUES 
    (N'Менеджер'),
    (N'Мастер'),
    (N'Оператор'),
    (N'Заказчик');

CREATE TABLE Users (
    userID INT PRIMARY KEY IDENTITY(1,1),
    fio NVARCHAR(255) NOT NULL,
    phone NVARCHAR(15) NOT NULL,
    typeID INT NOT NULL,
    CONSTRAINT FK_UserType FOREIGN KEY (typeID) REFERENCES UserTypes(typeID)
);

CREATE TABLE UserAuth (
    authID INT PRIMARY KEY IDENTITY(1,1),
    userID INT NOT NULL UNIQUE,
    login NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL, -- Рекомендуется хранить хешированные пароли
    CONSTRAINT FK_UserAuth_User FOREIGN KEY (userID) REFERENCES Users(userID)
);

CREATE TABLE HomeTechTypes (
    techTypeID INT PRIMARY KEY IDENTITY(1,1),
    techTypeName NVARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO HomeTechTypes (techTypeName)
VALUES 
    (N'Фен'),
    (N'Тостер'),
    (N'Холодильник'),
    (N'Стиральная машина'),
    (N'Мультиварка');

CREATE TABLE RequestStatuses (
    statusID INT PRIMARY KEY IDENTITY(1,1),
    statusName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO RequestStatuses (statusName)
VALUES 
    (N'Новая заявка'),
    (N'В процессе ремонта'),
    (N'Готова к выдаче');

CREATE TABLE Requests (
    requestID INT PRIMARY KEY IDENTITY(1,1),
    startDate DATE NOT NULL,
    techTypeID INT NOT NULL,
    homeTechModel NVARCHAR(255) NOT NULL,
    problemDescription TEXT NOT NULL,
    currentStatusID INT NOT NULL,
    masterID INT,
    clientID INT NOT NULL,
    CONSTRAINT FK_RequestTechType FOREIGN KEY (techTypeID) REFERENCES HomeTechTypes(techTypeID),
    CONSTRAINT FK_RequestMaster FOREIGN KEY (masterID) REFERENCES Users(userID),
    CONSTRAINT FK_RequestClient FOREIGN KEY (clientID) REFERENCES Users(userID),
    CONSTRAINT FK_RequestStatus FOREIGN KEY (currentStatusID) REFERENCES RequestStatuses(statusID)
);

CREATE TABLE RequestStatusHistory (
    historyID INT PRIMARY KEY IDENTITY(1,1),
    requestID INT NOT NULL,
    statusID INT NOT NULL,
    changeDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_HistoryRequest FOREIGN KEY (requestID) REFERENCES Requests(requestID),
    CONSTRAINT FK_HistoryStatus FOREIGN KEY (statusID) REFERENCES RequestStatuses(statusID)
);

CREATE TABLE RepairParts (
    partID INT PRIMARY KEY IDENTITY(1,1),
    requestID INT NOT NULL,
    partName NVARCHAR(255) NOT NULL,
    quantity INT DEFAULT 1,
    CONSTRAINT FK_RepairPartsRequest FOREIGN KEY (requestID) REFERENCES Requests(requestID)
);

CREATE TABLE Comments (
    commentID INT PRIMARY KEY IDENTITY(1,1),
    message TEXT NOT NULL,
    masterID INT,
    requestID INT,
    CONSTRAINT FK_CommentMaster FOREIGN KEY (masterID) REFERENCES Users(userID),
    CONSTRAINT FK_CommentRequest FOREIGN KEY (requestID) REFERENCES Requests(requestID)
);

INSERT INTO Users (fio, phone, typeID)
VALUES 
    (N'Трубин Никита Юрьевич', '89210563128', 1),
    (N'Мурашов Андрей Юрьевич', '89535078985', 2),
    (N'Степанов Андрей Викторович', '89210673849', 2),
    (N'Перина Анастасия Денисовна', '89990563748', 3);

INSERT INTO UserAuth (userID, login, password)
VALUES 
    (1, 'kasoo', 'root'),
    (2, 'murashov123', 'qwerty'),
    (3, 'test1', 'test1'),
    (4, 'perinaAD', '250519');

INSERT INTO Requests (startDate, techTypeID, homeTechModel, problemDescription, currentStatusID, masterID, clientID)
VALUES 
    ('2023-06-06', 1, N'Ладомир ТА112 белый', N'Перестал работать', 2, 2, 4),
    ('2023-05-05', 2, N'Redmond RT-437 черный', N'Перестал работать', 2, 3, 4);

INSERT INTO RequestStatusHistory (requestID, statusID)
VALUES 
    (1, 1),
    (1, 2),
    (2, 1),
    (2, 2);
