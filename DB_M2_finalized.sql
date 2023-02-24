CREATE DATABASE M2_;



select * from stadium
select * from club
select * from fan


exec createAllTables


GO;
CREATE PROCEDURE createAllTables
AS


CREATE TABLE SystemUser
(
username varchar(20) PRIMARY KEY,
password varchar(20)
);



CREATE TABLE Fan
(
national_id VARCHAR(20) PRIMARY KEY,  
phone_num INT,
name VARCHAR(20),
address VARCHAR(20),
status BIT DEFAULT '1',  --blocked or unblocked
birthdate DATETIME,
super_id VARCHAR(20) NOT NULL,
CONSTRAINT F_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE Sports_Association_Manager
(
id INT  PRIMARY KEY IDENTITY,   
name VARCHAR(20),
super_id VARCHAR(20) NOT NULL,

CONSTRAINT SAM_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE System_Admin
(
id INT  PRIMARY KEY IDENTITY,  
name VARCHAR(20),
super_id VARCHAR(20) NOT NULL,

CONSTRAINT SA_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE Stadium
(
id INT  PRIMARY KEY IDENTITY,
name VARCHAR(20),
capacity INT,
location VARCHAR(20),
status BIT DEFAULT '1',
);



CREATE TABLE Club              
(
id INT PRIMARY KEY IDENTITY,
name VARCHAR(20),
location VARCHAR(20),

);

CREATE TABLE Match
(
id INT PRIMARY KEY IDENTITY,
start_time DATETIME ,
end_time DATETIME,
host_id INT NOT NULL,
guest_id INT NOT NULL,
host_stadium_id INT 

CONSTRAINT M_FK1 FOREIGN KEY (host_id) REFERENCES CLUB(id)  ,
CONSTRAINT M_FK2 FOREIGN KEY (guest_id) REFERENCES CLUB(id) ,
CONSTRAINT M_FK3 FOREIGN KEY (host_stadium_id) REFERENCES Stadium(id)  
);



CREATE TABLE Stadium_Manager
(
id INT  PRIMARY KEY IDENTITY, --UNIQUE WITHIN THE TABLE (CHECK LATER!!)
name VARCHAR(20),
super_id VARCHAR(20) NOT NULL, 
stadium_id int,

CONSTRAINT SM_FK1 FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT SM_FK2 FOREIGN KEY(stadium_id) REFERENCES Stadium(id) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE Club_Representative
(
id INT PRIMARY KEY IDENTITY,  --UNIQUE WITHIN THE TABLE (CHECK LATER!!)
name VARCHAR(20),
super_id VARCHAR(20) NOT NULL ,
club_id int ,

CONSTRAINT CR_FK1 FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT CR_FK2 FOREIGN KEY(club_id) REFERENCES Club(id) ON UPDATE CASCADE ON DELETE CASCADE 
);



CREATE TABLE Host_Request
(
id INT PRIMARY KEY IDENTITY,
match_id INT,
status  INT DEFAULT -1,              --request can take 3 different values must change the datatype later (unhandled, accepted or rejected)
stadium_manager_id int   NOT NULL,       --STADIUM_MANAGER PRIMARY KEY IS USERNAME(CHACK LATER!!)
club_representative_id int NOT NULL,   --CLUB_REPRESENTATIVE PRIMARY KEY IS USERNAME(CHACK LATER!!)

CONSTRAINT HR_FK1 FOREIGN KEY (stadium_manager_id) REFERENCES Stadium_Manager(id) ON UPDATE CASCADE ,
CONSTRAINT HR_FK2 FOREIGN KEY (club_representative_id) REFERENCES Club_Representative(id) ON DELETE CASCADE 
);



CREATE TABLE Ticket
(
id INT PRIMARY KEY IDENTITY,
status BIT,
match_id INT NOT NULL

CONSTRAINT T_FK FOREIGN KEY (match_id) REFERENCES Match(id) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE Tickets_Bought
(
fan_id VARCHAR(20) NOT NULL,   
ticket_id INT  NOT NULL

CONSTRAINT TB_FK1 FOREIGN KEY (fan_id) REFERENCES Fan(national_id) ON UPDATE CASCADE ON DELETE CASCADE ,
CONSTRAINT TB_FK2 FOREIGN KEY (ticket_id) REFERENCES Ticket(id) ON UPDATE CASCADE ON DELETE CASCADE,
CONSTRAINT TB_PK PRIMARY KEY(fan_id,ticket_id)

)



GO;




GO;
CREATE PROCEDURE dropAllTables
AS
 
 DROP TABLE Tickets_Bought
 DROP TABLE Ticket
 DROP TABLE Host_Request
 DROP TABLE Club_Representative
 DROP TABLE Stadium_Manager
 DROP TABLE Match
 DROP TABLE Club
 DROP TABLE Stadium
 DROP TABLE System_Admin
 DROP TABLE Sports_Association_Manager
 DROP TABLE Fan
 DROP TABLE SystemUser
GO;





GO;
CREATE PROCEDURE dropAllProceduresFunctionsViews
AS
drop proc acceptRequestKareem;
drop proc rejectRequestKareem;
drop FUNCTION allPendingRequests1;
drop FUNCTION allRequests1;
drop function allUpcomingMatchesForSports_Association_Manager;
drop function PlayedMatchesForSports_Association_Manager;
drop FUNCTION  availableMatchesToAttend1;
drop PROCEDURE deleteMatch1;

DROP PROCEDURE createAllTables;
DROP PROCEDURE dropAllTables;
DROP PROCEDURE clearAllTables;
DROP PROCEDURE allAssociationManager;
DROP PROCEDURE addNewMatch;
DROP PROCEDURE deleteMatch;
DROP PROCEDURE deleteMatchesOnStadium;
DROP PROCEDURE addClub;
DROP PROCEDURE addTicket;
DROP PROCEDURE deleteClub;
DROP PROCEDURE addStadium;
DROP PROCEDURE deleteStadium;
DROP PROCEDURE blockFan;
DROP PROCEDURE unblockFan;
DROP PROCEDURE addRepresentative;
DROP PROCEDURE addHostRequest;
DROP PROCEDURE addStadiumManager;
DROP PROCEDURE acceptRequest;
DROP PROCEDURE rejectRequest;
DROP PROCEDURE addFan;
DROP PROCEDURE purchaseTicket;
DROP PROCEDURE updateMatchHost;
DROP PROCEDURE deleteMatchesOnStadium;


DROP FUNCTION allUnassignedMatches;
DROP FUNCTION allPendingRequests;
DROP FUNCTION upcomingMatchesOfClub;
DROP FUNCTION availableMatchesToAttend;
DROP FUNCTION clubsNeverPlayed;
DROP FUNCTION matchWithHighestAttendance;
DROP FUNCTION matchesRankedByAttendance;
DROP FUNCTION requestsFromClub;


DROP VIEW allAssocManagers;
DROP VIEW allClubRepresentatives;
DROP VIEW allStadiumManagers;
DROP VIEW allFans;
DROP VIEW allMatches;
DROP VIEW allTickets;
DROP VIEW allCLubs;
DROP VIEW allStadiums;
DROP VIEW allRequests;
DROP VIEW clubsWithNoMatches;
DROP VIEW matchesPerTeam;
DROP VIEW clubsNeverMatched;



GO;



GO;
CREATE PROCEDURE clearAllTables
AS

ALTER TABLE Fan
DROP CONSTRAINT F_FK;


ALTER TABLE Sports_Association_Manager
DROP CONSTRAINT SAM_FK;


ALTER TABLE System_Admin
DROP CONSTRAINT SA_FK;


ALTER TABLE Match
DROP CONSTRAINT M_FK1;
ALTER TABLE Match
DROP CONSTRAINT M_FK2;
ALTER TABLE Match
DROP CONSTRAINT M_FK3;


ALTER TABLE Stadium_Manager
DROP CONSTRAINT SM_FK1;
ALTER TABLE Stadium_Manager
DROP CONSTRAINT SM_FK2;


ALTER TABLE Club_Representative
DROP CONSTRAINT CR_FK1;
ALTER TABLE Club_Representative
DROP CONSTRAINT CR_FK2;


ALTER TABLE Host_Request
DROP CONSTRAINT HR_FK1;
ALTER TABLE Host_Request
DROP CONSTRAINT HR_FK2;


ALTER TABLE Ticket
DROP CONSTRAINT T_FK;


ALTER TABLE Tickets_Bought
DROP CONSTRAINT TB_FK1;
ALTER TABLE Tickets_Bought
DROP CONSTRAINT TB_FK2;




TRUNCATE TABLE Match
TRUNCATE TABLE Tickets_Bought
TRUNCATE TABLE Ticket
TRUNCATE TABLE Host_Request
TRUNCATE TABLE Club_Representative
TRUNCATE TABLE Stadium_Manager
TRUNCATE TABLE Club
TRUNCATE TABLE Stadium
TRUNCATE TABLE System_Admin
TRUNCATE TABLE Sports_Association_Manager
TRUNCATE TABLE Fan
TRUNCATE TABLE SystemUser




ALTER TABLE Fan
ADD CONSTRAINT F_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE;


ALTER TABLE Sports_Association_Manager
ADD CONSTRAINT SAM_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE;


ALTER TABLE System_Admin
ADD CONSTRAINT SA_FK FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE;


ALTER TABLE Match
ADD CONSTRAINT M_FK1 FOREIGN KEY (host_id) REFERENCES CLUB(id);
ALTER TABLE Match
ADD CONSTRAINT M_FK2 FOREIGN KEY (guest_id) REFERENCES CLUB(id);
ALTER TABLE Match
ADD CONSTRAINT M_FK3 FOREIGN KEY (host_stadium_id) REFERENCES Stadium(id);


ALTER TABLE Stadium_Manager
ADD CONSTRAINT SM_FK1 FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE Stadium_Manager
ADD CONSTRAINT SM_FK2 FOREIGN KEY(stadium_id) REFERENCES Stadium(id) ON UPDATE CASCADE ON DELETE CASCADE;


ALTER TABLE Club_Representative
ADD CONSTRAINT CR_FK1 FOREIGN KEY(super_id) REFERENCES SystemUser(username) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE Club_Representative
ADD CONSTRAINT CR_FK2 FOREIGN KEY(club_id) REFERENCES Club(id) ON UPDATE CASCADE ON DELETE CASCADE ;


ALTER TABLE Host_Request
ADD CONSTRAINT HR_FK1 FOREIGN KEY (stadium_manager_id) REFERENCES Stadium_Manager(id) ON UPDATE CASCADE ;
ALTER TABLE Host_Request
ADD CONSTRAINT HR_FK2 FOREIGN KEY (club_representative_id) REFERENCES Club_Representative(id) ON DELETE CASCADE ;


ALTER TABLE Ticket
ADD CONSTRAINT T_FK FOREIGN KEY (match_id) REFERENCES Match(id) ON UPDATE CASCADE ON DELETE CASCADE;


ALTER TABLE Tickets_Bought
ADD CONSTRAINT TB_FK1 FOREIGN KEY (fan_id) REFERENCES Fan(national_id) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE Tickets_Bought
ADD CONSTRAINT TB_FK2 FOREIGN KEY (ticket_id) REFERENCES Ticket(id) ON UPDATE CASCADE ON DELETE CASCADE;




GO;




GO;
CREATE VIEW allAssocManagers AS     

SELECT SU.username,SU.password,SAM.name
FROM Sports_Association_Manager SAM
INNER JOIN SystemUser SU
ON SAM.super_id=SU.username   
GO;





GO;
CREATE VIEW allClubRepresentatives AS

SELECT CR.super_id AS 'CLUB REPRESENTATIVE USERNAME' , SU.password AS 'PASSWORD' , CR.name AS 'CLUB REPRESENTATIVE NAME',C.name AS 'CLUB NAME'
FROM Club_Representative CR
INNER JOIN CLUB C
ON C.id=CR.club_id
INNER JOIN SystemUser SU
ON CR.super_id=SU.username
GO;





GO;
CREATE VIEW allStadiumManagers AS
SELECT SM.super_id AS MANAGER_USERNAME , SU.password AS PASSWORD , SM.name AS MANAGER_NAME , S.name AS STADIUM_NAME
FROM Stadium S
INNER JOIN Stadium_Manager SM
ON S.id=SM.stadium_id
INNER JOIN SystemUser SU
ON SM.super_id=SU.username
GO;



GO;
CREATE VIEW allFans AS

SELECT SU.username AS FAN_USERNAME,SU.password AS FAN_PASSWORD,  F.name, F.national_id , F.birthdate , F.status --status is represented by a bit not in this format (blocked or unblocked) !!

FROM Fan F
INNER JOIN SystemUser SU
ON F.super_id=SU.username
GO;




GO;
CREATE VIEW allMatches AS

SELECT C1.name AS HOST_CLUB, C2.name AS GUEST_CLUB , M.start_time   --THERE IS PROBABLY A TYPO IN THE MILESTONE (MUST BE CHECKED AGAIN !!)
FROM MATCH M
INNER JOIN CLUB C1
ON M.host_id=C1.id
INNER JOIN CLUB C2
ON M.guest_id=C2.id
GO;




GO;
CREATE VIEW allTickets AS
SELECT C1.name AS HOST_CLUB , C2.name AS GUEST_CLUB, S.name AS STADIUM_NAME , M.start_time
FROM Ticket T
INNER JOIN  Match M
ON T.match_id=M.id
INNER JOIN CLUB C1
ON M.host_id=C1.id
INNER JOIN CLUB C2
ON M.guest_id=C2.id
INNER JOIN STADIUM S
ON M.host_stadium_id=S.id
GO;




GO;
CREATE VIEW allCLubs AS    --SCHMEA OF THE UNI DOESNT PROVIDE ANY DETAILS FOR THE CLUB ????
SELECT C.name AS CLUB_NAME , C.location AS CLUB_LOCATION
FROM CLUB C
GO;




GO;
CREATE VIEW allStadiums AS
SELECT S.name AS STADIUM_NAME , S.location AS STADIUM_LOCATION , S.capacity AS STADIUM_CAPACITY , S.status AS STADIUM_STATUS --STADIUM STATUS IS REPRESENTED BY A BIT NOT LIKE (available or unavailable) ????
FROM Stadium S
GO;




GO;
CREATE VIEW allRequests AS
SELECT CR.super_id AS 'CLUB REPRESENTATIVE USER NAME' , SM.super_id AS 'STADIUM MANAGER USER NAME' , HR.status
FROM Host_Request HR
INNER JOIN Club_Representative CR
ON HR.club_representative_id=CR.id
INNER JOIN Stadium_Manager SM
ON HR.stadium_manager_id=SM.id

GO;




GO;
CREATE PROCEDURE addAssociationManager
@name varchar(20), 
@user_name varchar(20),
@password varchar(20)

AS

INSERT INTO SystemUser(username,password)
VALUES(@user_name,@password)

INSERT INTO Sports_Association_Manager(name,super_id)
VALUES(@name,@user_name)
GO;




GO;
CREATE PROCEDURE addNewMatch  --HOW IS THE MATCH START AND END TIME REPRESENTED IN ONLY A DATE TIME??????? ALSO NO HOST STADIUM IS PROVIDED
@name_host_club varchar(20),
@name_guest_club varchar(20),
@start_time_match datetime,
@end_time_match datetime

AS
DECLARE @id_host_club int
DECLARE @id_guest_club int

SELECT @id_host_club=C.id    --GET ID OF THE HOST CLUB
FROM Club C
WHERE C.name=@name_host_club


SELECT @id_guest_club=C.id   --GET ID OF THE GUEST CLUB
FROM CLUB C
WHERE C.name=@name_guest_club


INSERT INTO Match(start_time,end_time,host_id,guest_id,host_stadium_id)
VALUES(@start_time_match,@end_time_match,@id_host_club,@id_guest_club,NULL);  --**STADIUM ID IS NULL
GO;




GO;
CREATE VIEW clubsWithNoMatches AS

SELECT C.name AS CLUB_NAME_NO_MATCHES
FROM CLUB C
LEFT OUTER JOIN Match M
ON C.id=M.host_id OR C.id=M.guest_id
WHERE M.host_id IS NULL AND M.guest_id IS NULL
GO;




GO;
CREATE PROCEDURE deleteMatch    -- NOT SURE OF THIS ANSWER MUST BE CHECKED AGAIN ** milestone got updated

@name_host_club varchar(20),
@name_guest_club varchar(20)

AS

DECLARE @match_id INT 
DECLARE @host_club_id INT
DECLARE @guest_club_id INT



SELECT @host_club_id= C.id   --GET ID OF THE HOST CLUB  canbe put in a function cuz its repeated many times
FROM Club C
WHERE C.name=@name_host_club


SELECT @name_guest_club= C.id  --GET ID OF THE GUEST CLUB
FROM Club C
WHERE C.name=@name_guest_club


SELECT @match_id= M.id
FROM MATCH M 
INNER JOIN CLUB C1
ON C1.id=M.host_id
INNER JOIN CLUB C2
ON C2.id=M.guest_id
WHERE C1.id=@host_club_id AND C2.id=@name_guest_club

DELETE FROM MATCH 
WHERE MATCH.id=@match_id
  
  

GO;




GO;
CREATE PROCEDURE deleteMatchesOnStadium   --NOT SURE of the current timestamp part
@name_stadium varchar(20) 

AS

DELETE FROM MATCH
WHERE MATCH.id=
     (
     SELECT M.ID
     FROM Stadium S
     INNER JOIN Match M
     ON S.id=M.host_stadium_id
     WHERE @name_stadium=S.name AND M.start_time>CURRENT_TIMESTAMP
     )

GO;




GO;
CREATE PROCEDURE addClub
@name_club varchar(20),  
@location_club varchar(20)

AS
INSERT INTO CLUB(name,location)       --the representatove of the club is null???????????      NO I CHANGED THE SCHEMA
VALUES(@name_club,@location_club)
GO;




GO;
CREATE PROCEDURE addTicket
@name_host_club varchar(20), 
@name_guest_club varchar(20), 
@starting_time_match DATETIME     --will be changed to date time !!!!!!!!!!!!   DONE

AS
DECLARE @match_id INT


SELECT @match_id=M.id
FROM MATCH M
INNER JOIN CLUB C1
ON M.host_id=C1.id
INNER JOIN CLUB C2
ON M.guest_id=C2.id
WHERE C1.name=@name_host_club AND C2.name=@name_guest_club AND M.start_time=@starting_time_match  --STARTING TIME MOST PROBABLY WILL BE CHANGEDDDD!!!

INSERT INTO Ticket(status,match_id)
VALUES(1,@match_id)    --1 MEANS AVAILABLE  WILL BE REMOVED AND ADDED AS A DEFAULT VALUE IN TABLE CREATION

GO;




GO;
CREATE PROCEDURE deleteClub    --should delete any foreign key that refrences this club
@name_club varchar(20)
  
AS

DECLARE @club_id INT
DECLARE @club_representative INT
DECLARE @USERNAME VARCHAR(20)


SELECT @club_id= id     --get the id of the club
FROM CLUB 
WHERE name=@name_club


SELECT @club_representative =CR.id , @USERNAME = CR.super_id
FROM Club_Representative CR
WHERE CR.club_id=@club_id


DELETE FROM Match       --DELETE ANY MATCH THAT THE CLUB PLAYED IN  / YOU CAN SET IT TO NULL TO KEEP THE MATCH ID
WHERE host_id=@club_id OR guest_id=@club_id



DELETE FROM Host_Request
WHERE @club_representative=@club_representative  --DELETE HOST REQUEST FROM CLUB REPRESENTATIVE


DELETE FROM SystemUser   --delete username
WHERE username=@USERNAME

DELETE FROM CLUB       --delete club
WHERE CLUB.name =@name_club     

GO;




GO;
CREATE PROCEDURE addStadium
@name_stadium VARCHAR(20), 
@location_stadium VARCHAR(20), 
@capacity_stadium  INT

AS

INSERT INTO Stadium(name,location,capacity,status)  --STATUS OF 1 MEANS STADIUM IS AVAILABLE
VALUES(@name_stadium,@location_stadium,@capacity_stadium,'1')    --STADIUM MANGER IS NULL!!!!   NO I CHANGED THE SCHEMA
GO;




GO;
CREATE PROCEDURE deleteStadium  --SHOULD DELETE ANY FOREIGN KEY THAT REFRENCES STADIUM   IN MATCH , IN STADIUM MANAGER , IN HOST_REQUEST  
@name_stadium varchar(20)
AS

DECLARE @stadium_id INT
DECLARE @stadium_manager_id INT
DECLARE @USERNAME VARCHAR(20)
DECLARE @match_id int


SELECT @stadium_id = id           --get the id of the stadium
FROM Stadium
WHERE name=@name_stadium


SELECT @stadium_manager_id = id , @USERNAME=super_id   --get the id of the stadium manager  AND THE USERNAME OF STADIU MANAGER
FROM Stadium_Manager
WHERE stadium_id=@stadium_id

/*
--************************************************************************************************  shofha brthaya2ly msh lazem ne3malha  e3melha comment 
DELETE FROM Ticket         --DELETE ANY TICKET BELONGS TO A MATCH PLAYED ON A STADIUM
WHERE match_id in (
select M.id    ---get id of match
from Match M
where host_stadium_id=@stadium_id
        )
--****************************************************************************************************momken yeb2a fe match bas lesa mafesh stadium
*/


UPDATE Match              --SET THE ID OF THE STADIUM IN THE MATCH TO NULL
SET host_stadium_id= NULL
WHERE host_stadium_id=@stadium_id





DELETE FROM Host_Request   --delete the host request sent to the stadium manager who manages the stadium that will be deleted
WHERE stadium_manager_id=@stadium_manager_id

   
DELETE FROM SystemUser     --DELETE USERNAME
WHERE username=@USERNAME





DELETE FROM Stadium       --delete the stadium
where name=@name_stadium
GO;





GO;
CREATE PROCEDURE blockFan
@national_id_number_fan varchar(20)

AS
UPDATE Fan
set status='0'   --0 MEANS BLOCKED
where national_id=@national_id_number_fan

GO;




CREATE PROCEDURE unblockFan
@national_id_number_fan varchar(20)

AS
UPDATE Fan
set status='1'  --1 MEANS UNBLOCKED
where national_id=@national_id_number_fan

GO;


select * from club
go;
CREATE PROCEDURE addRepresentative  --not complete the name of the club where to put  it  might change the schema of the club_representative to incluse nameof club or id???
@name varchar(20),                   -- i changed the schema
@club_name varchar(20),
@user_name varchar(20),
@password varchar(20) 

AS
DECLARE @club_id INT


INSERT INTO SystemUser (username,password)
VALUES(@user_name,@password);



SELECT @club_id=C.id     --GET THE CLUB ID FROM THE INPUT CLUB NAME
FROM Club C
WHERE C.name=@club_name


INSERT INTO Club_Representative(name,super_id,club_id)
VALUES(@name,@user_name,@club_id)


GO;

DROP FUNCTION viewAvailableStadiumsOn
select * from dbo.viewAvailableStadiumsOn('1/1/2929')

GO;
CREATE FUNCTION viewAvailableStadiumsOn (@datetime DATETIME)   --CHECK AGAIN NOT SURE !!!!!
RETURNS @output_table TABLE
(
STADIUM_NAME VARCHAR(20),
STADIUM_LOCATION VARCHAR(20),
STADIUM_CAPACITY INT
)
AS
Begin
Insert into @output_table
  SELECT S.name AS STADIUM_NAME,S.location AS STADIUM_LOCATION,S.capacity AS STADIUM_CAPACITY
  FROM Stadium S
  LEFT OUTER JOIN Match M
  ON S.id=M.host_stadium_id
  WHERE S.status='1' AND  (@datetime NOT BETWEEN M.start_time AND M.end_time)  OR (M.id IS NULL)
  --THE START AND ENDING TIMES OF MATCH MUST BE CHECKED IF THEY WILL BE CHANGE TO ANOTHER DATATYPE     done

  RETURN;
  END;       
GO;
select * from Match
select * from Stadium
select * from Stadium_Manager
select * from Club_Representative
Exec addHostRequest 'barcelona' , 'real madrid stadium' , '06/06/2025 05:00:00 AM'
select * from Host_Request

go;
CREATE PROCEDURE addHostRequest    --NOT SURE OF THE LAST PARTS!!!!
@club_name varchar(20),
@stadium_name varchar(20),
@starting_time_match datetime
AS

DECLARE @club_represenative_id INT
DECLARE @stadium_manager_id INT
DECLARE @match_id INT
DECLARE @host_club_id INT

SELECT @club_represenative_id=CR.id  ,@host_club_id=C.id    --GOT THE ID OF THE REPRESENTATIVE OF THE CLUB
FROM CLUB C
INNER JOIN Club_Representative CR
ON C.id=CR.club_id
WHERE C.name=@club_name


SELECT @stadium_manager_id=SM.id        --GOT THE ID OF THE MANAGER OF THE STADIUM
FROM Stadium S
INNER JOIN Stadium_Manager SM
ON S.id=SM.stadium_id
WHERE S.name=@stadium_name


SELECT @match_id =M.id                  --GOT THE SPECIFIC MATCH THE REQUEST WANTS ACCORDING TO THE STARTING TIME
FROM Match M
WHERE M.start_time=@starting_time_match AND M.host_id=@host_club_id   --not sure of these!!!!!


--NOW WE ADD A REQUEST FROM THIS CLUB REPRESENTATIVE TO THE STADIUM MANAGER


INSERT INTO Host_Request (match_id,status,stadium_manager_id,club_representative_id)
VALUES(@match_id,-1,@stadium_manager_id,@club_represenative_id)                      --status is unhandled so -1????

GO;



GO;
CREATE FUNCTION allUnassignedMatches (@club_name VARCHAR(20))   ---NOT FINISHED
RETURNS @output_table TABLE
(
name_guest_club VARCHAR(20),
start_time_match DATETIME
)

AS
BEGIN

    DECLARE @club_id int



    SELECT @club_id=C.id   --GET THE ID OF THE CLUB NAME
    FROM CLUB C
    WHERE C.name=@club_name




    INSERT INTO @output_table
    SELECT C2.name AS GUEST_CLUB , M.start_time        --WHAT IS MEANT BY COMPETING CLUB!!!!!!!!!  decription changed to guest club
    FROM MATCH M
    LEFT OUTER JOIN CLUB C1 
    ON M.host_id=C1.id
    LEFT OUTER JOIN CLUB C2
    ON M.guest_id=C2.id
    LEFT OUTER JOIN Stadium S
    ON S.id=M.host_stadium_id
    WHERE C1.id=@club_id AND  S.id IS NULL            -- ??? NOT SURE OF THIS        S.status='0'       --='0' OR IS NULL    MUST BE CHECKED !!!!!!!!!!!
    



    RETURN;

END;

GO;




select * from Stadium
select * from Stadium
select * from Club

GO;

CREATE PROCEDURE addStadiumManager 
@name varchar(20),
@stadium_name varchar(20),
@user_name varchar(20),
@password varchar(20)

AS

DECLARE @stadium_id INT

SELECT @stadium_id= id      --get id of the stdium fromthe given name
FROM Stadium
WHERE name=@stadium_name


INSERT INTO SystemUser(username,password)
VALUES(@user_name,@password)


INSERT INTO Stadium_Manager(name,super_id,stadium_id)
VALUES(@name,@user_name,@stadium_id)

GO;


Select * from dbo.allPendingRequests('mohf')
Select * from dbo.allPendingRequests1('mohf')


drop function allPendingRequests1

go;
CREATE FUNCTION allPendingRequests1(@stadium_manager_user_name VARCHAR(20))
RETURNS @output_table TABLE
(
CR_NAME varchar(20),
G_NAME  VARCHAR(20),
H_NAME  VARCHAR(20),
M_START_TIME DATETIME,
M_END_TIME DATETIME

)
AS
BEGIN
  DECLARE @stadium_manger_id VARCHAR(20)


  SELECT @stadium_manger_id=S.id        
  FROM Stadium_Manager S                 
  WHERE S.super_id=@stadium_manager_user_name


  INSERT INTO @output_table
    SELECT  CR.name AS 'CLUB REPRESENTATIVE NAME', C.name AS 'GUEST CLUB',C2.name AS 'HOST CLUB' ,start_time AS 'MATCH START TIME' , M.end_time AS 'MATCH END TIME'
    FROM Host_Request HR
    INNER JOIN Club_Representative CR
    ON HR.club_representative_id=CR.id
    INNER JOIN MATCH M
    ON HR.match_id=M.id
    INNER JOIN CLUB C
    ON M.guest_id=C.id
    INNER JOIN CLUB C2
    ON M.host_id=C2.id
    WHERE HR.stadium_manager_id=@stadium_manger_id  AND HR.status=-1  --STILL UNHANDELED
   

  RETURN


END
GO;








go;
CREATE FUNCTION allPendingRequests(@stadium_manager_user_name VARCHAR(20))
RETURNS @output_table TABLE
(
CR_NAME varchar(20),
C_NAME  VARCHAR(20),
M_START_TIME DATETIME
)
AS
BEGIN
  DECLARE @stadium_manger_id VARCHAR(20)


  SELECT @stadium_manger_id=S.id         --THIS CAN BE PUT IN A SEPERATE FINCTION USED MULTIPLE TIMES
  FROM Stadium_Manager S                 --get the stadium manager id based on the given stadium manager username
  WHERE S.super_id=@stadium_manager_user_name


  INSERT INTO @output_table
    SELECT  CR.name AS 'CLUB REPRESENTATIVE NAME', C.name AS 'GUEST CLUB',M.start_time AS 'MATCH START TIME'
    FROM Host_Request HR
    INNER JOIN Club_Representative CR
    ON HR.club_representative_id=CR.id
    INNER JOIN MATCH M
    ON HR.match_id=M.id
    INNER JOIN CLUB C
    ON M.guest_id=C.id
    WHERE HR.stadium_manager_id=@stadium_manger_id AND HR.status=-1  --NOT SURE OF THE STATUS    -1 because unhandled is pending also?
   

  RETURN


END
GO;


select * from Host_Request
Select * from dbo.allPendingRequests('mohf')
select * from dbo.allRequests1('mohf')
Drop function allRequests1

go;
CREATE FUNCTION allRequests1(@stadium_manager_user_name VARCHAR(20))
RETURNS @output_table TABLE
(
CR_NAME varchar(20),
G_NAME  VARCHAR(20),
H_NAME  VARCHAR(20),
M_START_TIME DATETIME,
M_END_TIME DATETIME,
Status_of_request int --- value of -1 means request is unhandled
)
AS
BEGIN
  DECLARE @stadium_manger_id VARCHAR(20)


  SELECT @stadium_manger_id=S.id        
  FROM Stadium_Manager S                 
  WHERE S.super_id=@stadium_manager_user_name


  INSERT INTO @output_table
    SELECT  CR.name AS 'CLUB REPRESENTATIVE NAME', C.name AS 'GUEST CLUB',C2.name AS 'HOST CLUB' ,start_time AS 'MATCH START TIME' , M.end_time AS 'MATCH END TIME',HR.status AS 'STATUS OF REQUEST'
    FROM Host_Request HR
    INNER JOIN Club_Representative CR
    ON HR.club_representative_id=CR.id
    INNER JOIN MATCH M
    ON HR.match_id=M.id
    INNER JOIN CLUB C
    ON M.guest_id=C.id
    INNER JOIN CLUB C2
    ON M.host_id=C2.id
    WHERE HR.stadium_manager_id=@stadium_manger_id  
   

  RETURN


END
GO;








select * from Club_Representative

select * from club
SELECT * from Match

select * from SystemUser
SELECT * FROM System_Admin
select * from Stadium_Manager



select * from Host_Request
exec acceptRequest 'ahmed', 'testclub' ,'testclub2' ,'3/31/3333 3:33:00 pm'
select * from Host_Request

GO;
CREATE PROCEDURE acceptRequest 
@stadium_manager_user_name varchar(20),
@hosting_club_name varchar(20),
@guest_club_name varchar(20),
@starting_time_match DATETIME

AS
DECLARE @stadium_manager_id VARCHAR(20)
DECLARE @hosting_club_id INT
DECLARE @guest_club_id INT  --GUEST CLUB???
DECLARE @club_representative_id VARCHAR(20)
DECLARE @stadium_id INT
DECLARE @match_id INT
DECLARE @host_request_id INT
 
--THESE STATEMENTS ARE REPEATED CAN BE PUT IN A FUNCTION LATER**
  

SELECT @stadium_manager_id=SM.id,@stadium_id=SM.stadium_id   --GET THE ID OF THE STADIUM MANAGER BASED ON INPUT STADIUM MANAGER USERNAME AND THE ID OF THE STADIUM
FROM Stadium_Manager SM
WHERE SM.super_id=@stadium_manager_user_name  


SELECT @hosting_club_id=C.id                                 --GET THE ID OF THE HOST CLUB ID BASED ON THE INPUT CLUB NAME
FROM CLUB C
WHERE C.name=@hosting_club_name 

  
SELECT @guest_club_id =C.id                                   --GET THE ID OF THE GUEST CLUB ID BASED ON THE INPUT CLUB NAME
FROM CLUB C
WHERE C.name=@guest_club_name 


SELECT @club_representative_id= CR.id                         --GET THE ID OF THE CLUB REPRESENTATIVE BASED ON THE HOSTING CLUB ID
FROM Club_Representative CR
WHERE CR.club_id=@hosting_club_id

 
SELECT @match_id=M.id                                         --GET THE ID IF THE MATCH BASED ON THESE CONDITIONS
FROM Match M
WHERE M.host_id=@hosting_club_id AND M.guest_id=@guest_club_id AND
      M.host_stadium_id=@stadium_id AND M.start_time=@starting_time_match

      

SELECT @host_request_id=HR.id                             --GET ID OF THE REQUEST 
FROM Host_Request HR
WHERE HR.match_id=@match_id  AND HR.stadium_manager_id=@stadium_manager_id  AND
      HR.club_representative_id=@club_representative_id


UPDATE Host_Request   
set status=1             --dont know these values yet!!!!!!!    1 means accepted
WHERE id=@host_request_id   --UPDATE REQUEST TABLE

GO;


select * from Host_Request

GO;
CREATE PROCEDURE rejectRequest
@stadium_manager_user_name varchar(20),
@hosting_club_name varchar(20),
@guest_club_name varchar(20),
@starting_time_match DATETIME

AS
DECLARE @stadium_manager_id VARCHAR(20)
DECLARE @hosting_club_id INT
DECLARE @guest_club_id INT  --GUEST CLUB???
DECLARE @club_representative_id VARCHAR(20)
DECLARE @stadium_id INT
DECLARE @match_id INT
DECLARE @host_request_id INT
 
--THESE STATEMENTS ARE REPEATED CAN BE PUT IN A FUNCTION LATER**
  

SELECT @stadium_manager_id=SM.id,@stadium_id=SM.stadium_id   --GET THE ID OF THE STADIUM MANAGER BASED ON INPUT STADIUM MANAGER USERNAME AND THE ID OF THE STADIUM
FROM Stadium_Manager SM
WHERE SM.super_id=@stadium_manager_user_name  


SELECT @hosting_club_id=C.id                                 --GET THE ID OF THE HOST CLUB ID BASED ON THE INPUT CLUB NAME
FROM CLUB C
WHERE C.name=@hosting_club_name 

  
SELECT @guest_club_id =C.id                                   --GET THE ID OF THE GUEST CLUB ID BASED ON THE INPUT CLUB NAME
FROM CLUB C
WHERE C.name=@guest_club_name 


SELECT @club_representative_id= CR.id                         --GET THE ID OF THE CLUB REPRESENTATIVE BASED ON THE HOSTING CLUB ID
FROM Club_Representative CR
WHERE CR.club_id=@hosting_club_id

 
SELECT @match_id=M.id                                         --GET THE ID IF THE MATCH BASED ON THESE CONDITIONS
FROM Match M
WHERE M.host_id=@hosting_club_id AND M.guest_id=@guest_club_id AND
      M.host_stadium_id=@stadium_id AND M.start_time=@starting_time_match

      

SELECT @host_request_id=HR.id                                --GET ID OF THE REQUEST 
FROM Host_Request HR
WHERE HR.match_id=@match_id  AND HR.stadium_manager_id=@stadium_manager_id  AND
      HR.club_representative_id=@club_representative_id


UPDATE Host_Request   
set status=0              --dont know these values yet!!!!!!!   0 means rejected
WHERE id=@host_request_id   --UPDATE REQUEST TABLE

GO;

select * from fan
drop procedure addFan

go;
CREATE PROCEDURE addFan
@name VARCHAR(20), 
@username VARCHAR(20), 
@password VARCHAR(20),
@national_id_number VARCHAR(20), 
@birth_date DATETIME,
@address VARCHAR(20), 
@phone_number  INT 


AS

INSERT INTO SystemUser(username,password)
VALUES(@username,@password)

INSERT INTO Fan(super_id,name,national_id,birthdate,address,phone_num,status)   --DEFAULR OF STATUS IS 1 (UNBLOCKED)   
values(@username,@name,@national_id_number,@birth_date,@address,@phone_number,'1')

GO;

Exec addFan 'bbbb','bbbb','pass','1233','2/1/2134','mansoura',123;

select * from fan



SELECT * FROM allUpcomingMatchesForSports_Association_Manager()


GO;
CREATE function allUpcomingMatchesForSports_Association_Manager()
RETURNS @OUTPUT_TABLE TABLE
(
host_club_name VARCHAR (20),
guest_club_name VARCHAR(20) ,
start_time_match DATETIME,
end_time_match DATETIME
)
AS
BEGIN

INSERT INTO @OUTPUT_TABLE
SELECT C1.name , C2.name ,M.start_time , M.end_time
FROM MATCH M
INNER JOIN CLUB C1
ON M.host_id=C1.id
INNER JOIN CLUB C2
ON M.guest_id=C2.id
where M.start_time>CURRENT_TIMESTAMP

RETURN;
END
GO;


select * from  PlayedMatchesForSports_Association_Manager()


GO;
CREATE function PlayedMatchesForSports_Association_Manager()
RETURNS @OUTPUT_TABLE TABLE
(
host_club_name VARCHAR (20),
guest_club_name VARCHAR(20) ,
start_time_match DATETIME,
end_time_match DATETIME
)
AS
BEGIN

INSERT INTO @OUTPUT_TABLE
SELECT C1.name , C2.name ,M.start_time , M.end_time
FROM MATCH M
INNER JOIN CLUB C1
ON M.host_id=C1.id
INNER JOIN CLUB C2
ON M.guest_id=C2.id
where M.end_time<CURRENT_TIMESTAMP

RETURN;
END
GO;











GO;
CREATE FUNCTION upcomingMatchesOfClub(@club_name varchar(20))  --GIVEN CLUB IS THE HOST????OR NOT KNOWN??
RETURNS @OUTPUT_TABLE TABLE
(
club_name VARCHAR (20),
competing_club_name VARCHAR(20) ,
starting_time_match DATETIME,
stadium_hosting_match VARCHAR(20)
)


AS
BEGIN
DECLARE @club_id INT

SELECT @club_id=c.id     --get the id of the given club
FROM CLub c
where c.name=@club_name  


INSERT INTO @OUTPUT_TABLE
SELECT
        CASE                           --CASE STATEMENTS SO THE OUTPUT TABLE HAS FIRST COLUMN AS THE GIVEN CLUB NAME AND SECOND COLUMN THE COMPETING CLUB NAME
            WHEN C1.name=@club_name
                  THEN C1.name
                  ELSE C2.name
            END AS club_name,
         
         CASE
               WHEN C1.name=@club_name
                      THEN C2.name
                      ELSE C1.name
               END AS competing_club_name,
                
                M.start_time , S.name

FROM Match M
INNER JOIN Club C1
ON C1.id=M.host_id  
INNER JOIN CLUB C2
ON C2.id=M.guest_id
Left outer JOIN Stadium S
ON M.host_stadium_id=S.id
WHERE M.start_time>CURRENT_TIMESTAMP AND (C1.id=@club_id OR C2.id=@club_id)

RETURN;

END
GO;


SELECT * FROM Match
SELECT * FROM availableMatchesToAttend1('11/11/2023 8:00:00 PM')


GO;
CREATE FUNCTION availableMatchesToAttend1(@date DATETIME)
RETURNS @OUTPUT_TABLE TABLE
(
host_club_name VARCHAR(20),
guest_club_name VARCHAR(20),
starting_time_match DATETIME, 
stadium_name_hosting_match VARCHAR(20),
stadium_location_hosting_match VARCHAR(20)
)

AS
BEGIN


  INSERT INTO @OUTPUT_TABLE
  SELECT C1.name AS 'HOST CLUB', C2.name AS 'GUEST CLUB',M.start_time,S.name AS 'STADIUM NAME',S.location AS 'STADIUM LOCATION'
  FROM Match M
  INNER JOIN CLUB C1 
  ON M.host_id=C1.id
  INNER JOIN CLUB C2
  ON  M.guest_id=C2.id
  INNER JOIN Stadium s
  ON M.host_stadium_id=S.id
  WHERE  M.start_time>=@date                    --GREATER THAN THIS DATE OR AT THIS SPECIFIC DATE????
  AND EXISTS
            (
            SELECT *
            FROM
            Ticket T
            INNER JOIN Match M2
            ON T.match_id=M2.id
            WHERE M2.id=M.id AND T.status='1'    --STATUS IS 1 BECAUSE IT IS AVAILABLE
            )
                           
  RETURN


END
GO;




SELECT * FROM DBO.viewavailabletickets ('11/11/2023 8:00 PM')

GO;
CREATE FUNCTION viewavailabletickets(@date DATETIME)
RETURNS @OUTPUT_TABLE TABLE
(
host_club_name VARCHAR(20),
guest_club_name VARCHAR(20),
starting_time_match DATETIME

)

AS
BEGIN


  INSERT INTO @OUTPUT_TABLE
  SELECT C1.name AS 'HOST CLUB', C2.name AS 'GUEST CLUB',M.start_time
  FROM Match M
  INNER JOIN CLUB C1 
  ON M.host_id=C1.id
  INNER JOIN CLUB C2
  ON  M.guest_id=C2.id
  --INNER JOIN Stadium s
  --ON M.host_stadium_id=S.id
  --WHERE  M.start_time>=@date                    --GREATER THAN THIS DATE OR AT THIS SPECIFIC DATE????
  inner join Ticket t 
  on m.id=t.match_id
                           
  RETURN


END








GO;
CREATE FUNCTION availableMatchesToAttend(@date DATETIME)
RETURNS @OUTPUT_TABLE TABLE
(
host_club_name VARCHAR(20),
guest_club_name VARCHAR(20),
starting_time_match DATETIME, 
stadium_hosting_match VARCHAR(20)
)

AS
BEGIN


  INSERT INTO @OUTPUT_TABLE
  SELECT C1.name AS 'HOST CLUB', C2.name AS 'GUEST CLUB',M.start_time,S.name AS 'STADIUM NAME'
  FROM Match M
  INNER JOIN CLUB C1 
  ON M.host_id=C1.id
  INNER JOIN CLUB C2
  ON  M.guest_id=C2.id
  INNER JOIN Stadium s
  ON M.host_stadium_id=S.id
  WHERE  M.start_time>=@date                    --GREATER THAN THIS DATE OR AT THIS SPECIFIC DATE????
  AND EXISTS
            (
            SELECT *
            FROM
            Ticket T
            INNER JOIN Match M2
            ON T.match_id=M2.id
            WHERE M2.id=M.id AND T.status='1'    --STATUS IS 1 BECAUSE IT IS AVAILABLE
            )
                           
  RETURN


END
GO;


Select * from Tickets_Bought
truncate table tickets_bought

GO;
CREATE PROCEDURE purchaseTicket
@national_id_number_fan VARCHAR(20),
@hosting_club_name VARCHAR(20),
@guest_club_name VARCHAR(20),
@start_time_match DATETIME
AS

DECLARE @hosting_club_id INT 
DECLARE @guest_club_id INT
DECLARE @match_id INT
DECLARE @ticket_id INT



SELECT @hosting_club_id=C.id        --GET ID OF THE HOSTING CLUB
FROM Club C
WHERE C.name=@hosting_club_name   

 
SELECT @guest_club_id=C.id         --GET ID OF THE GUEST CLUB
FROM Club C
WHERE C.name=@guest_club_name   


SELECT @match_id=M.id             --GET ID OF THE MATCH BASED ON THESE CONDITIONS
FROM Match M
WHERE M.host_id=@hosting_club_id AND M.guest_id=@guest_club_id AND M.start_time=@start_time_match 


SELECT @ticket_id = T.id         --get ticket id for the specific match id
FROM Ticket T
WHERE T.match_id=@match_id   



INSERT INTO Tickets_Bought(fan_id,ticket_id)  --FAN PURCHSES THE TICKET
VALUES(@national_id_number_fan,@ticket_id)



GO;




GO;
CREATE PROCEDURE updateMatchHost       --not sure of this!
@hosting_club_name varchar(20),
@guest_club_name varchar(20),
@start_time_match DATETIME
AS

DECLARE @hosting_club_id INT
DECLARE @guest_club_id INT
DECLARE @match_id INT
DECLARE @TEMP INT

--CAN BE PUT IN FUNCTIONS REPEARED MANY TIMES

SELECT @hosting_club_id=C.id,@TEMP=C.id           --GET ID OF THE HOSTING CLUB
FROM Club C
WHERE C.name=@hosting_club_name 


SELECT @guest_club_id=C.id                       --GET ID OF THE GUEST CLUB
FROM Club C
WHERE C.name=@guest_club_name    

SELECT @match_id=M.id                            --GET ID OF THE MATCH BASED ON THESE CONDITIONS
FROM Match M
WHERE M.host_id=@hosting_club_id AND M.guest_id=@guest_club_id AND M.start_time=@start_time_match



UPDATE MATCH
SET host_id=guest_id,
    guest_id=@TEMP      --switch the host and guest ids
WHERE id=@match_id



GO;



GO;
CREATE PROCEDURE deleteMatchesOnStadium          --where is this shit in the milestone??
@stadium_name varchar(20)

AS
DECLARE @stadium_id INT



SELECT @stadium_id=S.id
FROM Stadium S
WHERE S.name=@stadium_name   --GET STADIUM ID


DELETE M
  FROM MATCH M
  INNER JOIN Stadium S
  ON M.host_stadium_id=S.id
  WHERE S.id=@stadium_id AND M.start_time>CURRENT_TIMESTAMP   --delete records based on these conditions


GO;




CREATE VIEW matchesPerTeam AS 

SELECT C.name AS 'CLUB NAME ' , COUNT(*) AS 'NUMBER OF MATCHES PLAYED'
FROM Club C
INNER JOIN Match M
ON C.id=M.host_id OR C.id=M.guest_id
WHERE M.end_time<CURRENT_TIMESTAMP
GROUP BY C.name


GO;


select * from clubsNeverMatched

GO;
CREATE VIEW clubsNeverMatched AS


SELECT C1.name AS 'CLUB1' , C2.name AS 'CLUB2'
FROM CLUB C1,CLUB C2
WHERE NOT EXISTS(
               SELECT *
               FROM Match M
               WHERE (M.host_id=C1.id AND M.guest_id=C2.id) OR (M.host_id=C2.id AND M.guest_id=C1.id)
               )


GO;
CREATE VIEW clubsNeverMatched AS


SELECT C1.name AS 'CLUB1' , C2.name AS 'CLUB2'
FROM CLUB C1,CLUB C2
WHERE NOT EXISTS(
               SELECT *
               FROM Match M
               WHERE (M.host_id=C1.id AND M.guest_id=C2.id) OR (M.host_id=C2.id AND M.guest_id=C1.id)
               )


GO;




GO;
CREATE FUNCTION clubsNeverPlayed(@club_name varchar(20))  
RETURNS @OUTPUT_TABLE TABLE
(
CLUB_NAME VARCHAR(20)
)
AS
BEGIN
DECLARE @club_id INT


SELECT @club_id=C.id       --GET THE ID OF THE GIVEN CLUB
FROM Club C
WHERE C.name=@club_name   



INSERT INTO @OUTPUT_TABLE

  SELECT C.name          --GET ALL CLUB NAMES
  FROM Club C

  EXCEPT                --REMOVE CLUBS WHERE THE GIVEN CLUB WAS THE HOST OR THE GUEST OF THE MATCH (removenay club the given club played against as the host or as the guest)
  (
    (
    SELECT C2.name
    FROM Match M
    INNER JOIN CLUB C1
    ON M.host_id=C1.id AND M.host_id=@club_id
    INNER JOIN CLUB C2
    ON M.guest_id=C2.id
    )
    UNION
    (
    SELECT C1.name
    FROM Match M
    INNER JOIN CLUB C1
    ON M.host_id=C1.id
    INNER JOIN CLUB C2
    ON M.guest_id=C2.id AND M.guest_id=@club_id
    )
  )





RETURN;
END

GO;




GO;
CREATE FUNCTION matchWithHighestAttendance()

RETURNS @OUTPUT_TABLE TABLE
(
name_hosting_club VARCHAR(20),
name_guest_club VARCHAR(20)
)

AS
BEGIN

  
  INSERT INTO @OUTPUT_TABLE 
  SELECT C1.name,C2.name
  FROM Tickets_Bought TB
  INNER JOIN Ticket T
  ON TB.ticket_id=T.id
  INNER JOIN Match M
  ON T.match_id=M.id
  INNER JOIN Club C1
  ON M.host_id=C1.id
  INNER JOIN Club C2
  ON M.guest_id=C2.id
  GROUP BY M.id,C1.name,C2.name
  HAVING COUNT(*)=(
                   SELECT MAX (X)
                   FROM
                   ( 
                     SELECT COUNT(*) AS X
                     FROM Tickets_Bought TB1
                     INNER JOIN Ticket T1
                     ON TB1.ticket_id=T1.id
                     INNER JOIN Match M1
                     ON T1.match_id=M1.id
                     GROUP BY M1.id

                   ) AS T        --WHY SHOULD WE PUT THIS IF REMOVED GIVES AN ERROR
                  )
                   
                 
RETURN
END
GO;




CREATE FUNCTION matchesRankedByAttendance()     --not sure of this!

RETURNS @OUTPUT_TABLE TABLE
(
name_hosting_club VARCHAR(20),
name_competing_club VARCHAR(20)
)

AS
BEGIN 


 INSERT INTO @OUTPUT_TABLE 
  SELECT C1.name,C2.name
  FROM Tickets_Bought TB
  INNER JOIN Ticket T
  ON TB.ticket_id=T.id
  RIGHT OUTER JOIN Match M
  ON T.match_id=M.id
  INNER JOIN Club C1
  ON M.host_id=C1.id
  INNER JOIN Club C2
  ON M.guest_id=C2.id
  GROUP BY M.id,C1.name,C2.name
  ORDER BY COUNT(*)  DESC        --ORDER BY THE NUMBER OF ROWS FOR EACH GROUP of match id , club1 ,club2



RETURN
END
GO;




GO;
CREATE FUNCTION requestsFromClub   --not sure of this!!!
(
@name_stadium varchar(20),
@name_club varchar(20)
)


RETURNS @OUTPUT_TABLE TABLE
(
name_hosting_club VARCHAR(20),
name_guest_club VARCHAR(20)
)


AS
BEGIN


  DECLARE @stadium_id INT
  DECLARE @stadium_manager_id VARCHAR(20)
  DECLARE @club_id INT
  DECLARE @club_represenative_id VARCHAR(20)



  SELECT @stadium_id=S.id              --get the stadium id from stadium name
  FROM Stadium S
  WHERE S.name=@name_stadium



  SELECT @stadium_manager_id = SM.id   --get the stadium manager id from the stadium id
  FROM Stadium_Manager SM
  WHERE SM.stadium_id=@stadium_id



  SELECT @club_id=C.id                 --get the id of the club from the club name
  FROM Club C
  WHERE C.name=@name_club



  SELECT @club_represenative_id=CR.id  --get the id of the club representative from the club id
  FROM Club_Representative CR
  WHERE CR.club_id=@club_id




  INSERT INTO @OUTPUT_TABLE
  SELECT C1.name AS 'HOSTING CLUB',C2.name AS 'GUEST'
  FROM Match M 
  INNER JOIN Club C1
  ON M.host_id=C1.id
  INNER JOIN Club C2
  ON M.guest_id=C2.id
  WHERE M.id IN (
                SELECT HR.match_id
                FROM Host_Request HR
                WHERE HR.stadium_manager_id=@stadium_manager_id AND
                HR.club_representative_id=@club_represenative_id
                )






RETURN
END
GO;








SET IDENTITY_INSERT club on;
INSERT INTO Club(ID,name,location)
values
(1,'barcelona','spain'),
(2,'madrid','spain');
SET IDENTITY_INSERT club off;




SET IDENTITY_INSERT Stadium on;
Insert INTO Stadium(id,status,capacity,name)
VALUES
(1,1,100,'camp nou'),
(2,1,100,'real madrid stadium');
SET IDENTITY_INSERT Stadium off;





SET IDENTITY_INSERT Match on;
INSERT INTO Match(id,start_time,end_time,host_id,guest_id,host_stadium_id)
VALUES(1,'10/2/2023','10/3/2023',1,2,1)
SET IDENTITY_INSERT Match off;





INSERT INTO SystemUser(username,password)
VALUES
('ahmed','123'),
('youssef','654'),
('mohammed','789');




SET IDENTITY_INSERT STADIUM_MANAGER on;
INSERT INTO Stadium_Manager(id,name,super_id,stadium_id)
VALUES
(1,'hamed','ahmed',1),
(2,'bayomi','youssef',2);

SET IDENTITY_INSERT STADIUM_MANAGER OFF;






SET IDENTITY_INSERT CLUB_REPRESENTATIVE on;
INSERT INTO Club_Representative(id,name,super_id,club_id)
VALUES
(3,'fayzallah','mohammed',1)
SET IDENTITY_INSERT CLUB_REPRESENTATIVE OFF;



SET IDENTITY_INSERT Host_Request on;
INSERT INTO Host_Request(id,status,match_id,stadium_manager_id,club_representative_id)
values
(1,0,1,1,3)
SET IDENTITY_INSERT Host_Request OFF;




SET IDENTITY_INSERT Ticket on;
insert into Ticket (id,status,match_id)
values(1,'0',1)

SET IDENTITY_INSERT Ticket off;





EXEC dropAllTables
exec clearAllTables
EXEC CREATEALLTABLES







SELECT * FROM CLUB





go;






EXEC clearAllTables

EXEC addAssociationManager 'Kareem' , 'KareemEladl' , 'ANAPASS';
SELECT * FROM Sports_Association_Manager
SELECT * FROM System_Users

EXEC addClub 'Barcelona' , 'Spain'
EXEC addClub 'real madrid' , 'Spain'
EXEC addClub 'atletico madrid' , 'Spain'
EXEC addClub 'bayern' , 'germany'
EXEC addClub 'chelsea' , 'england'
EXEC addClub 'dortmund' , 'germany'


-- You will want to use the YYYYMMDD for unambiguous date determination in SQL Server.
--insert into table1(approvaldate)values('20120618 10:34:09 AM');
EXEC addNewMatch 'Barcelona' , 'real madrid' , '06/06/2025 05:00:00 AM'
, '06/30/2025 07:00:00 AM'

EXEC addNewMatch 'bayern' , 'dortmund' , '06/06/2021 05:00:00 AM'
, '06/06/2021 07:00:00 AM'

SELECT * FROM Matches

SELECT * FROM clubsWithNoMatches

EXEC deleteMatch 'bayern' , 'dortmund'

EXEC addStadium 'Camp Nou' , 'Spain' , 1000
EXEC addStadium 'santiago' , 'Spain' , 400
EXEC addStadium 'stamford' , 'england' , 500
EXEC addStadium 'alianz' , 'germany' , 600

SELECT * FROM Stadium

SELECT * FROM Club

INSERT INTO Match VALUES('06/06/2021 05:00:00 AM'
, '06/06/2021 07:00:00 AM' , 1 , 2 , 1 )

INSERT INTO Match VALUES('06/06/2021 05:00:00 AM'
, '06/06/2021 07:00:00 AM' , 1 , 2 , 1 )
INSERT INTO Match VALUES('06/06/2023 05:00:00 AM'
, '06/06/2023 07:00:00 AM' , 1 , 2 , 1 )

select * from club
select * from Ticket
EXEC deleteMatchesOnStadium 'Camp Nou'

EXEC addTicket 'Barcelona' , 'real madrid' , '06/06/2023 05:00:00 AM'

EXEC addRepresentative 'kareem' , 'Barcelona' , 'AhmedEE' , "anaeishhena"

SELECT * FROM Club_Representative
SELECT * FROM System_Users
SELECT * FROM Club


EXEC deleteClub 'dortmund'

EXEC deleteStadium 'Camp Nou'

UPDATE Matches SET staduim_id = 2 WHERE id = 3;


SELECT * FROM club
select * from Match
select * from Stadium
select * from Host_Request
select * from systemuser
select * from club_Representative
SELECT * FROM Fan
select * from Stadium_Manager
select * from Stadium

EXEC addFan 'fayzallah' , 'Mohammedf' , 'pass' , '1234' ,  --ERROR
'5/1/1999 05:00:00 AM' , 'Mansoura' , 842

select * from fan

EXEC blockFan '12345'

EXEC unblockFan '12345'

SELECT * FROM Club

SELECT * FROM viewAvailableStadiumsOn('20230730 05:00:00 AM');


EXEC addStadiumManager 'Abdo' , 'santiago' , 'aboda' , '123'

-- dont forget to test delete stadium to make sure stadium manager
-- removed from sys users

SELECT * FROM Matches

SELECT * FROM Club_Representative

EXEC addHostRequest 'barcelona' , 'camp nou' , '7/7/2727 05:00:00 AM'


select * from Club
select * from match

SELECT * FROM Host_Request
Select * from dbo.allRequests('ahmed')

SELECT * FROM allUnassignedMatches('Barcelona')

SELECT * FROM allPendingRequests('aboda')

EXEC acceptRequest 'ahned'  , 'Barcelona'  , 'real madrid' ,  --NOT EXECUTED
'20230630 05:00:00 AM'

SELECT * FROM Host_Request





EXEC createAllTables
exec clearAllTables


select * from dbo.upcomingMatchesOfClub('barcelona')

select * from match

exec ad
EXEC addNewMatch 'barcelona' , 'madrid' , '06/06/2025 05:00:00 AM'
, '06/30/2025 07:00:00 AM'




Go;
CREATE PROCEDURE deleteMatch1  --HOW IS THE MATCH START AND END TIME REPRESENTED IN ONLY A DATE TIME??????? ALSO NO HOST STADIUM IS PROVIDED
@name_host_club varchar(20),
@name_guest_club varchar(20),
@start_time_match datetime,
@end_time_match datetime

AS
DECLARE @id_host_club int
DECLARE @id_guest_club int

SELECT @id_host_club=C.id    --GET ID OF THE HOST CLUB
FROM Club C
WHERE C.name=@name_host_club


SELECT @id_guest_club=C.id   --GET ID OF THE GUEST CLUB
FROM CLUB C
WHERE C.name=@name_guest_club


DELETE MATCH 
WHERE host_id=@id_host_club AND guest_id=@id_guest_club AND start_time=@start_time_match AND end_time=@end_time_match
GO;




GO;
CREATE VIEW clubsWithNoMatches AS

SELECT C.name AS CLUB_NAME_NO_MATCHES
FROM CLUB C
LEFT OUTER JOIN Match M
ON C.id=M.host_id OR C.id=M.guest_id
WHERE M.host_id IS NULL AND M.guest_id IS NULL
GO;








select * from Ticket

drop procedure acceptRequestKareem

drop proc acceptRequestKareem


go;
CREATE PROC acceptRequestKareem
@username VARCHAR(20),
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME
AS
DECLARE @hostid INT
DECLARE @guestid INT
DECLARE @smid INT
DECLARE @rid INT
DECLARE @matchid INT
DECLARE @capacity INT
DECLARE @sid INT

SELECT @smid = id FROM 
Stadium_Manager WHERE super_id = @username;

SELECT @sid = stadium_id FROM
Stadium_Manager WHERE stadium_id = @smid


Select @capacity=capacity
from Stadium
where id=@sid



SELECT @hostid = id FROM 
Club WHERE name = @hostname

SELECT @guestid = id FROM 
Club WHERE name = @guestname;

SELECT @matchid = M.id 
FROM Match M 
WHERE M.guest_id = @guestid AND M.host_id = @hostid AND M.start_time = @start_time 

SELECT @rid = R.id 
FROM Club_Representative R INNER JOIN Club C ON R.club_id = C.id
WHERE C.id = @hostid

UPDATE Host_Request 
SET status = 1 
WHERE club_representative_id = @rid 
AND stadium_manager_id = @smid 
AND match_id = @matchid;

Declare @i int=0
while @i<@capacity
begin
set @i=@i+1
Insert into Ticket (status,match_id)
values('1',@matchid)
end



GO;

SELECT * FROM Stadium_Manager
select * from Club
select * from match


go;
CREATE PROC rejectRequestKareem
@username VARCHAR(20),
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME
AS
DECLARE @hostid INT
DECLARE @guestid INT
DECLARE @smid INT
DECLARE @rid INT
DECLARE @matchid INT
DECLARE @capacity INT
DECLARE @sid INT

SELECT @smid = id  FROM 
Stadium_Manager WHERE super_id = @username;

SELECT @sid = stadium_id FROM
Stadium_Manager WHERE stadium_id = @smid


SELECT @hostid = id FROM 
Club WHERE name = @hostname

SELECT @guestid = id FROM 
Club WHERE name = @guestname;

SELECT @matchid = M.id 
FROM Match M 
WHERE M.guest_id = @guestid AND M.host_id = @hostid AND M.start_time = @start_time 

SELECT @rid = R.id 
FROM Club_Representative R INNER JOIN Club C ON R.club_id = C.id
WHERE C.id = @hostid

UPDATE Host_Request 
SET status = 0 
WHERE club_representative_id = @rid 
AND stadium_manager_id = @smid 
AND match_id = @matchid;




GO;