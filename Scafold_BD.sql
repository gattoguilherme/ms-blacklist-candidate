CREATE DATABASE BLACKLISTDB;

CREATE TABLE candidates (
ID VARCHAR(50),
NAME VARCHAR (100),
IDMENTOR VARCHAR (50),
EMAIL VARCHAR (200),
CITY VARCHAR (50),
UF VARCHAR (2),
CONDIT VARCHAR (50),
STATUS BOOL,
SCORE INTEGER,
PHONENUMBER VARCHAR (15),
PRIMARY KEY (ID)
);

CREATE TABLE skills (
ID_SKILL VARCHAR(50),
NAME VARCHAR(100),
PRIMARY KEY (ID_SKILL)
);

CREATE TABLE candidates_skills (
ID VARCHAR(50),
ID_CANDIDATE VARCHAR(50),
ID_SKILL VARCHAR(50),
PRIMARY KEY (ID),
FOREIGN KEY (ID_CANDIDATE) REFERENCES candidates(ID),
FOREIGN KEY (ID_SKILL) REFERENCES skills(ID_SKILL)
);