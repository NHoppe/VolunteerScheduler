/*
The MIT License (MIT)

Copyright (c) 2015 Pat McGee

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

DROP TABLE TaskAssignment
DROP TABLE Schedule
DROP TABLE Volunteer
GO
CREATE TABLE Schedule(
	monthAssigned VARCHAR(15) PRIMARY KEY,
);
CREATE TABLE Volunteer(
	volunteerID  INT IDENTITY PRIMARY KEY,
	firstName VARCHAR(25),
	lastName 	VARCHAR(25)
);
CREATE TABLE TaskAssignment (
		volunteerID INT,
		monthAssigned VARCHAR(15),
		
		PRIMARY KEY(volunteerID, monthAssigned),
		FOREIGN KEY (volunteerID) REFERENCES Volunteer(volunteerID),
		FOREIGN KEY (monthAssigned) REFERENCES Schedule(monthAssigned)
);
INSERT INTO Schedule VALUES('December 2015');
INSERT INTO Schedule VALUES('January 2016');
INSERT INTO Schedule VALUES('February 2016');
INSERT INTO Schedule VALUES('March 2016');
INSERT INTO Schedule VALUES('April 2016');
INSERT INTO Schedule VALUES('May 2016');