--ill finish this by saturday thank u

CREATE TABLE Books (
    bookID INT PRIMARY KEY,
    title VARCHAR(100),
    authorID INT,
    isbn BIGINT,
    publishedYear INT,
    FOREIGN KEY (authorID) REFERENCES Authors(authorID)
);


CREATE TABLE Authors (
    authorID INT PRIMARY KEY,
    name VARCHAR(50),
    birthDate DATE,
    country VARCHAR(50)
);
--can change to any length, but i doubt any names/countries exceed 50 letters

CREATE TABLE Borrowers (
    borrowerID INT PRIMARY KEY,
    name VARCHAR(50),
    email VARCHAR(100),
    phone VARCHAR(20)
);


CREATE TABLE Loans (
    loanID INT PRIMARY KEY,
    bookID INT,
    borrowerID INT,
    loanDate DATE,
    returnDate DATE,
    returned BOOLEAN,
    FOREIGN KEY (bookID) REFERENCES Books(bookID),
    FOREIGN KEY (borrowerID) REFERENCES Borrowers(borrowerID)
);


INSERT INTO Authors (authorID, name, birthDate, country) VALUES
(1, 'Andre Aciman', '1951-01-02', 'Egypt'),
(2, 'William James', '1842-01-11', 'United States'),
(3, 'David Humes', '1711-05-07', 'United Kingdom'),
(4, 'Albert Camus', '1913-11-07', 'Algeria'),
(5, 'George Orwell', '1903-06-25', 'India');

INSERT INTO Borrowers (borrowerID, name, email, phone) VALUES
(1, 'Maher Bekdache', 'mmb86@mail.aub.edu', '81989771'),
(2, 'Amina Jaroudi', 'amina@gmail.com', '81632501'),
(3, 'Omar El Hajj', 'omar@gmail.com', '81459205'),
(4, 'Ali Hammoud', 'ali@gmail.com', '81838024'),
(5, 'Lea Azar', 'lea@gmail.com', '81762071');

INSERT INTO Books (bookID, title, authorID, isbn, publishedYear) VALUES
(1, 'Find Me', 1, 9780312426781, 2019),
(2, 'The Varieties of Religious Experience', 2, 9780140390346, 1902),
(3, 'A Treatise of Human Nature', 3, 9780486432501, 1739),
(4, 'The Stranger', 4, 9780679720201, 1942),
(5, 'Animal Farm', 5, 9780451524935, 1945);

INSERT INTO Loans (loanID, bookID, borrowerID, loanDate, returnDate, returned) VALUES
(1, 1, 1, '2025-06-01', '2025-07-01', TRUE),
(2, 2, 2, '2025-06-10', '2025-07-10', FALSE),
(3, 3, 1, '2025-06-05', '2025-07-05', TRUE),
(4, 4, 3, '2025-06-18', '2025-07-18', FALSE),
(5, 5, 4, '2025-06-20', '2025-07-20', FALSE);

--in the system, returnDate is the deadline to return the book, which is 30 days after loanDate.

SELECT * FROM Books WHERE publishedYear = 1949;

--assume the date today is July 20, to check overdues
SELECT * 
FROM Loans
WHERE returnDate < '2025-07-20' AND returned = FALSE;

SELECT B.title, A.name AS author
FROM Loans L
JOIN Books B ON L.bookID = B.bookID
JOIN Authors A ON B.authorID = A.authorID
WHERE L.borrowerID = 1;

SELECT COUNT(*) AS totalBooks FROM Books;

