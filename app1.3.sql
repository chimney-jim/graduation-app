-- MySQL dump 10.13  Distrib 5.1.61, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: Application
-- ------------------------------------------------------
-- Server version	5.1.61-0ubuntu0.11.10.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `Application` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `Application`;

--
-- Table structure for table `AppPreReq`
--

DROP TABLE IF EXISTS `AppPreReq`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AppPreReq` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ApplicationId` int(25) NOT NULL,
  `PreReqId` int(11) NOT NULL,
  `PreReqStatusId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_app_prereq` (`ApplicationId`,`PreReqId`),
  KEY `fk_prereq_app` (`PreReqId`),
  KEY `fk_prereqstatus` (`PreReqStatusId`),
  CONSTRAINT `fk_prereqstatus` FOREIGN KEY (`PreReqStatusId`) REFERENCES `PreReqStatus` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_app_prereq` FOREIGN KEY (`ApplicationId`) REFERENCES `Application` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_prereq_app` FOREIGN KEY (`PreReqId`) REFERENCES `PreReq` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppPreReq`
--

LOCK TABLES `AppPreReq` WRITE;
/*!40000 ALTER TABLE `AppPreReq` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppPreReq` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Application`
--

DROP TABLE IF EXISTS `Application`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Application` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `StudentFirstName` varchar(50) NOT NULL,
  `StudentLastName` varchar(50) NOT NULL,
  `ProgramId` int(25) NOT NULL,
  `AppTerm` varchar(10) NOT NULL,
  `AppYear` int(4) NOT NULL,
  `DateInputed` date NOT NULL,
  `AppDate` date NOT NULL,
  `TimesReviewed` int(25) DEFAULT NULL,
  `LastReviewed` date NOT NULL,
  `IsAccepted` tinyint(3) NOT NULL DEFAULT '0',
  `IsFinalized` tinyint(3) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Application`
--

LOCK TABLES `Application` WRITE;
/*!40000 ALTER TABLE `Application` DISABLE KEYS */;
/*!40000 ALTER TABLE `Application` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ApplicationReviewer`
--

DROP TABLE IF EXISTS `ApplicationReviewer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ApplicationReviewer` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ApplicationId` int(25) NOT NULL,
  `ReviewerId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_app_rev` (`ApplicationId`,`ReviewerId`),
  KEY `fk_rev_app` (`ReviewerId`),
  CONSTRAINT `fk_rev_app` FOREIGN KEY (`ReviewerId`) REFERENCES `Reviewer` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_app_rev` FOREIGN KEY (`ApplicationId`) REFERENCES `Application` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ApplicationReviewer`
--

LOCK TABLES `ApplicationReviewer` WRITE;
/*!40000 ALTER TABLE `ApplicationReviewer` DISABLE KEYS */;
/*!40000 ALTER TABLE `ApplicationReviewer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Comments`
--

DROP TABLE IF EXISTS `Comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Comments` (
  `Id` int(25) NOT NULL AUTO_INCREMENT,
  `ApplicationId` int(25) NOT NULL,
  `Body` text,
  PRIMARY KEY (`Id`),
  KEY `fk_comment_app` (`ApplicationId`),
  CONSTRAINT `fk_comment_app` FOREIGN KEY (`ApplicationId`) REFERENCES `Application` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Comments`
--

LOCK TABLES `Comments` WRITE;
/*!40000 ALTER TABLE `Comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `Comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Login`
--

DROP TABLE IF EXISTS `Login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Login` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserType` varchar(50) NOT NULL,
  `UserId` int(25) NOT NULL,
  `LoginName` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_userType_userId_loginName` (`UserType`,`UserId`,`LoginName`),
  KEY `fk_log_addmin` (`UserId`),
  CONSTRAINT `fk_log_addmin` FOREIGN KEY (`UserId`) REFERENCES `SysAdmin` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_log_rev` FOREIGN KEY (`UserId`) REFERENCES `Reviewer` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_log_sec` FOREIGN KEY (`UserId`) REFERENCES `Secretary` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Login`
--

LOCK TABLES `Login` WRITE;
/*!40000 ALTER TABLE `Login` DISABLE KEYS */;
/*!40000 ALTER TABLE `Login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PDF`
--

DROP TABLE IF EXISTS `PDF`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PDF` (
  `Id` int(25) NOT NULL AUTO_INCREMENT,
  `ApplicationId` int(25) NOT NULL,
  `FileName` varchar(250) NOT NULL DEFAULT '',
  `FileContent` longblob NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_pdf_app` (`ApplicationId`),
  CONSTRAINT `fk_pdf_app` FOREIGN KEY (`ApplicationId`) REFERENCES `Application` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PDF`
--

LOCK TABLES `PDF` WRITE;
/*!40000 ALTER TABLE `PDF` DISABLE KEYS */;
/*!40000 ALTER TABLE `PDF` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PreReq`
--

DROP TABLE IF EXISTS `PreReq`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PreReq` (
  `Id` int(25) NOT NULL AUTO_INCREMENT,
  `Name` varchar(80) NOT NULL,
  `Body` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PreReq`
--

LOCK TABLES `PreReq` WRITE;
/*!40000 ALTER TABLE `PreReq` DISABLE KEYS */;
/*!40000 ALTER TABLE `PreReq` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PreReqStatus`
--

DROP TABLE IF EXISTS `PreReqStatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PreReqStatus` (
  `Id` int(25) NOT NULL AUTO_INCREMENT,
  `Name` varchar(80) NOT NULL,
  `Body` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PreReqStatus`
--

LOCK TABLES `PreReqStatus` WRITE;
/*!40000 ALTER TABLE `PreReqStatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `PreReqStatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Program`
--

DROP TABLE IF EXISTS `Program`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Program` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(80) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ProgramPreReq`
--

DROP TABLE IF EXISTS `ProgramPreReq`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ProgramPreReq` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProgramId` int(25) NOT NULL,
  `PreReqId` int(25) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_program_prereq` (`ProgramId`,`PreReqId`),
  KEY `fk_req_pro` (`PreReqId`),
  CONSTRAINT `fk_req_pro` FOREIGN KEY (`PreReqId`) REFERENCES `PreReq` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_pro_req` FOREIGN KEY (`ProgramId`) REFERENCES `Program` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ProgramPreReq`
--

LOCK TABLES `ProgramPreReq` WRITE;
/*!40000 ALTER TABLE `ProgramPreReq` DISABLE KEYS */;
/*!40000 ALTER TABLE `ProgramPreReq` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ProgramReviewer`
--

DROP TABLE IF EXISTS `ProgramReviewer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ProgramReviewer` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProgramId` int(11) NOT NULL,
  `ReviewerId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `_prog_rev` (`ProgramId`,`ReviewerId`),
  KEY `fk_rev_pro` (`ReviewerId`),
  CONSTRAINT `fk_rev_pro` FOREIGN KEY (`ReviewerId`) REFERENCES `Reviewer` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_pro_rev` FOREIGN KEY (`ProgramId`) REFERENCES `Program` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ProgramReviewer`
--

LOCK TABLES `ProgramReviewer` WRITE;
/*!40000 ALTER TABLE `ProgramReviewer` DISABLE KEYS */;
/*!40000 ALTER TABLE `ProgramReviewer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Reviewer`
--

DROP TABLE IF EXISTS `Reviewer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Reviewer` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `IsDecider` tinyint(3) NOT NULL DEFAULT '0',
  `Email` varchar(80) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Reviewer`
--

LOCK TABLES `Reviewer` WRITE;
/*!40000 ALTER TABLE `Reviewer` DISABLE KEYS */;
/*!40000 ALTER TABLE `Reviewer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Secretary`
--

DROP TABLE IF EXISTS `Secretary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Secretary` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Email` varchar(80) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Secretary`
--

LOCK TABLES `Secretary` WRITE;
/*!40000 ALTER TABLE `Secretary` DISABLE KEYS */;
/*!40000 ALTER TABLE `Secretary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysAdmin`
--

DROP TABLE IF EXISTS `SysAdmin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `SysAdmin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Email` varchar(80) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysAdmin`
--

LOCK TABLES `SysAdmin` WRITE;
/*!40000 ALTER TABLE `SysAdmin` DISABLE KEYS */;
/*!40000 ALTER TABLE `SysAdmin` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2012-03-20 18:16:42


USE application;


/*------------------------------------*/
/* additional modification to app.sql */
/*------------------------------------*/
ALTER TABLE application
ADD CONSTRAINT fk_application_program 
            FOREIGN KEY (ProgramId) REFERENCES Program (Id);
            
ALTER TABLE application 
CHANGE COLUMN `IsAccepted` 
    `IsAccepted` TINYINT(3) NULL DEFAULT NULL;

ALTER TABLE application 
CHANGE COLUMN `LastReviewed` 
    `LastReviewed` DATETIME NOT NULL;


/*ALTER TABLE programprereq
ADD CONSTRAINT programprereq_FK_program
FOREIGN KEY (ProgramId) REFERENCES program(id);

ALTER TABLE programprereq
ADD CONSTRAINT programprereq_FK_prereq
FOREIGN KEY (PreReqId) REFERENCES prereq(id);
*/

ALTER TABLE programprereq AUTO_INCREMENT=1;



INSERT INTO program (Name)
VALUES('SWE');
INSERT INTO program (Name)
VALUES('CSE');
INSERT INTO program (Name)
VALUES('IT');
INSERT INTO program (Name)
VALUES('CGDD');


INSERT INTO prereq (Name)
VALUES('CSE_Prereq1');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq2');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq3');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq4');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq5');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq6');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq7');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq8');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq9');
INSERT INTO prereq(Name)
VALUES('CSE_Prereq10');


INSERT INTO prereq (Name)
VALUES('SWE Prereq1');
INSERT INTO prereq (Name)
VALUES('SWE Prereq2');
INSERT INTO prereq (Name)
VALUES('SWE Prereq3');
INSERT INTO prereq (Name)
VALUES('SWE Prereq4');
INSERT INTO prereq (Name)
VALUES('SWE Prereq5');
INSERT INTO prereq (Name)
VALUES('SWE Prereq6');
INSERT INTO prereq (Name)
VALUES('SWE Prereq7');
INSERT INTO prereq (Name)
VALUES('SWE Prereq8');
INSERT INTO prereq (Name)
VALUES('SWE Prereq9');
INSERT INTO prereq (Name)
VALUES('SWE Prereq10');

INSERT INTO prereq (Name)
VALUES('IT=Prereq1');
INSERT INTO prereq (Name)
VALUES('IT=Prereq2');
INSERT INTO prereq (Name)
VALUES('IT=Prereq3');
INSERT INTO prereq (Name)
VALUES('IT=Prereq4');
INSERT INTO prereq (Name)
VALUES('IT=Prereq5');
INSERT INTO prereq (Name)
VALUES('IT=Prereq6');
INSERT INTO prereq (Name)
VALUES('IT=Prereq7');
INSERT INTO prereq (Name)
VALUES('IT=Prereq8');
INSERT INTO prereq (Name)
VALUES('IT=Prereq9');
INSERT INTO prereq (Name)
VALUES('IT=Prereq10');

INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,11);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,12);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,13);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,14);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,15);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,16);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,17);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,18);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,19);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(1,20);

INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,1);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,2);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,3);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,4);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,5);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,6);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,7);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,8);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,9);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(2,10);

INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,21);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,22);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,23);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,24);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,25);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,26);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,27);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,28);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,29);
INSERT INTO programprereq (ProgramId, PreReqId)
VALUES(3,30);

INSERT INTO prereqstatus (Name, Body)
VALUES('PC', 'Possible Credit');
INSERT INTO prereqstatus (Name, Body)
VALUES('PT', 'Possible Transfer');
INSERT INTO prereqstatus (Name, Body)
VALUES('RQ', 'Required');
INSERT INTO prereqstatus (Name, Body)
VALUES('NN', 'Not Needed');
INSERT INTO prereqstatus (Name, Body)
VALUES('RE', 'Recommended');

/*-------------------*/
/* STORED PROCEDURES */
/*-------------------*/
DELIMITER //
CREATE PROCEDURE getDDLProgram()
    BEGIN
        SELECT * FROM program;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getPrerequisiteCourses(IN progid INT)
    BEGIN
        SELECT preq.Id AS 'Id', preq.Name AS 'pre-requisite'
        FROM prereq AS preq JOIN programprereq AS progpreq ON (preq.Id=progpreq.PreReqId)
            JOIN program AS prog ON (progpreq.ProgramID = prog.Id)
        WHERE prog.id = progid;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE setApplication (IN firstN VARCHAR(50), IN lastN VARCHAR(50), IN progId INT(25),
                                 IN term VARCHAR(10), IN year INT(4), IN dateInput DATE,
                                 IN app_Date DATE, IN lastViewed DATE,
                                 IN isAccept TINYINT(3), IN isFinal TINYINT(3))
    BEGIN
        INSERT INTO application (StudentFirstName, StudentLastName, ProgramId, 
                                 AppTerm, AppYear, DateInputed, 
                                 AppDate, timesReviewed, LastReviewed, 
                                 IsAccepted, IsFinalized)
        VALUES (firstN, lastN, progId,
                term, year, dateInput,
                app_Date,1, lastViewed,
                isAccept, isFinal);
    END//                                 
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getNewApplicationId()
    BEGIN
        SELECT MAX(Id) as 'maxId'
        FROM application;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE setAppPreReq (IN appId INT, IN PReqId INT, IN PReqStatusId INT)
    BEGIN
        INSERT INTO appprereq (ApplicationId, PreReqId, PreReqStatusId)
        VALUES (appId, PReqId, PReqStatusId);
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getMinApplicationId()
    BEGIN
        SELECT Min(Id) as 'minId'
        FROM application;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getMaxApplicationId()
    BEGIN
        SELECT MAX(Id) as 'maxId'
        FROM application;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE loadApplication(IN appId INT)
    BEGIN
        SELECT * FROM application
        WHERE Id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE loadAppPreReq(IN appId INT)
    BEGIN
        SELECT appPReq.PreReqId, pReq.Name, appPReq.PreReqStatusId
        FROM appprereq AS appPReq JOIN prereq AS pReq ON (appPReq.PreReqId = pReq.Id)
        WHERE appPReq.ApplicationId = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE clearListBoxes(IN appId INT)
    BEGIN
        DELETE FROM appprereq
        WHERE ApplicationId = appId;
    END//
DELIMITER ;


DELIMITER //
CREATE PROCEDURE updateAppIsFinal(IN appId INT, IN isAccept INT,
                                  IN isFinal INT)
    BEGIN
        UPDATE application
        SET isAccepted = isAccept, IsFinalized = isFinal
        WHERE Id = appId;
    END//
DELIMITER ;


DELIMITER //
CREATE PROCEDURE submitComment(IN appId INT, IN commentBody TEXT)
    BEGIN
        INSERT INTO comments(ApplicationId, Body)
        VALUES(appId, commentBody);
    END//
DELIMITER ;


DELIMITER //
CREATE PROCEDURE checkIfAtLeastOneAppExists()
    BEGIN
        SELECT *
        FROM application;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE re_populateThreeRadioButtons(IN appId INT)
    BEGIN
        SELECT isAccepted
        FROM application
        WHERE Id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE loadComments(IN appId INT)
    BEGIN
        SELECT Id, Body
        FROM comments
        WHERE ApplicationId = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE loadLastViewed(IN appId INT)
    BEGIN
        SELECT LastReviewed
        FROM application
        WHERE id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE setLastViewed(IN appId INT)
    BEGIN
        UPDATE application
        SET lastReviewed = NOW()
        WHERE id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE setTimesReviewed(IN appId INT)
    BEGIN
        UPDATE application
        SET TimesReviewed = TimesReviewed + 1
        WHERE Id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE loadTimesReviewed(IN appId INT)
    BEGIN
        SELECT TimesReviewed
        FROM application
        WHERE Id = appId;
    END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE LoadApplicationsToGridView ()
    BEGIN
        SELECT appl.Id,
               appl.StudentFirstName AS FirstName, appl.StudentLastName AS LastName,
               prog.Name AS ProgName, appl.AppTerm AS Term, appl.AppYear AS Year,
               appl.DateInputed AS DateSubmitted, 
               appl.AppDate, appl.TimesReviewed AS Reviewed, appl.LastReviewed       
        FROM application AS appl JOIN program AS prog ON (appl.ProgramId = prog.Id)
        WHERE appl.isFinalized != 2;
    END//
DELIMITER ;


