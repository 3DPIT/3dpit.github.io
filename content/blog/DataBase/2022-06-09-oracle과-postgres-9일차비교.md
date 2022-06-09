---
title: oracle과-postgres-9일차비교 
date: 2022-06-09 15:39:00 +0900
category: DB
draft: false
---

## 2022-06-09-oracle과-postgres-9일차비교

## 목차

>00.사전 준비 테이블
>
>01.INNER JOIN
>
>>01.1 세 개 테이블 조인
>
>02.OUTER JOIN
>
>>02.1 LEFT/RIGHT/FULL OUTER JOIN 실습
>>
>>02.2 CROSS JOIN

## 00.사전 준비 테이블

- **Oracle**

  ```sql
  DROP TABLE buyTBL;
  DROP TABLE userTBL;
  CREATE TABLE userTBL -- 회원 테이블
  ( userID  	CHAR(8) NOT NULL PRIMARY KEY, -- 사용자 아이디(PK)
    userName  	NVARCHAR2(10) NOT NULL, -- 이름
    birthYear 	NUMBER(4) NOT NULL,  -- 출생년도
    addr	  	NCHAR(2) NOT NULL, -- 지역(경기,서울,경남 식으로 2글자만입력)
    mobile1	CHAR(3), -- 휴대폰의 국번(010, 011, 016, 017, 018, 019 등)
    mobile2	CHAR(8), -- 휴대폰의 나머지 전화번호(하이픈제외)
    height    	NUMBER(3),  -- 키
    mDate    	DATE  -- 회원 가입일
  );
  
  CREATE TABLE buyTBL -- 회원 구매 테이블
  (  idNum 	NUMBER(8) NOT NULL PRIMARY KEY, -- 순번(PK)
     userID  	CHAR(8) NOT NULL, -- 아이디(FK)
     prodName 	NCHAR(6) NOT NULL, --  물품명
     groupName 	NCHAR(4)  , -- 분류
     price     	NUMBER(8)  NOT NULL, -- 단가
     amount    	NUMBER(3)  NOT NULL, -- 수량
     FOREIGN KEY (userID) REFERENCES userTBL(userID)
  );
  
  INSERT INTO userTBL VALUES('LSG', '이승기', 1987, '서울', '011', '11111111', 182, '2008-8-8');
  INSERT INTO userTBL VALUES('KBS', '김범수', 1979, '경남', '011', '22222222', 173, '2012-4-4');
  INSERT INTO userTBL VALUES('KKH', '김경호', 1971, '전남', '019', '33333333', 177, '2007-7-7');
  INSERT INTO userTBL VALUES('JYP', '조용필', 1950, '경기', '011', '44444444', 166, '2009-4-4');
  INSERT INTO userTBL VALUES('SSK', '성시경', 1979, '서울', NULL  , NULL      , 186, '2013-12-12');
  INSERT INTO userTBL VALUES('LJB', '임재범', 1963, '서울', '016', '66666666', 182, '2009-9-9');
  INSERT INTO userTBL VALUES('YJS', '윤종신', 1969, '경남', NULL  , NULL      , 170, '2005-5-5');
  INSERT INTO userTBL VALUES('EJW', '은지원', 1972, '경북', '011', '88888888', 174, '2014-3-3');
  INSERT INTO userTBL VALUES('JKW', '조관우', 1965, '경기', '018', '99999999', 172, '2010-10-10');
  INSERT INTO userTBL VALUES('BBK', '바비킴', 1973, '서울', '010', '00000000', 176, '2013-5-5');
  
  DROP SEQUENCE idSEQ;
  CREATE SEQUENCE idSEQ; -- 순차번호 입력을 위해서 시퀀스 생성
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'KBS', '운동화', NULL   , 30,   2);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'KBS', '노트북', '전자', 1000, 1);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'JYP', '모니터', '전자', 200,  1);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'BBK', '모니터', '전자', 200,  5);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'KBS', '청바지', '의류', 50,   3);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'BBK', '메모리', '전자', 80,  10);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'SSK', '책'    , '서적', 15,   5);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'EJW', '책'    , '서적', 15,   2);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'EJW', '청바지', '의류', 50,   1);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'BBK', '운동화', NULL   , 30,   2);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'EJW', '책'    , '서적', 15,   1);
  INSERT INTO buyTBL VALUES(idSEQ.NEXTVAL, 'BBK', '운동화', NULL   , 30,   2);
  
  COMMIT;
  SELECT * FROM userTBL;
  SELECT * FROM buyTBL;
  ```

- **Postgres**

  ```sql
  drop table userTBL;
  drop table buyTBL;
  CREATE TABLE userTBL -- 회원 테이블
  (
  userID CHAR(8) NOT NULL PRIMARY KEY, --사용자 아이디(PK)
  userName varchar(10) NOT NULL, --이름
  birthYear numeric NOT NULL, --출생년도
  addr NCHAR(2) NOT NULL, --지역(경기, 서울, 경남 식으로 2글자만 입력) 
  mobile1 CHAR(3),-- 휴대폰의 국번(010, 011, 016, 017, 018, 019 등)
  mobile2 CHAR(8),-- 휴대폰의 나머지 전화번호(하이픈 제외)
  height numeric,
  mDate DATE --회원가입일
  );
  
  drop sequence seq_idNum;
  CREATE SEQUENCE seq_idNum START 1;
  
  CREATE TABLE buyTBL --회원 구매 테이블
  (
  idNum numeric NOT NULL PRIMARY KEY DEFAULT nextval('seq_idNum'), --순번(PK)
  userID CHAR(8) NOT NULL, --아이디(FK) 
  prodName NCHAR(6) NOT NULL, --물품명
  groupName NCHAR(4) , -- 분류
  price numeric NOT NULL, --단가
  amount numeric NOT NULL, --수량
  FOREIGN KEY (userID) REFERENCES userTBL(userID)
  );
  
  -- userTBL
  INSERT INTO userTBL VALUES('LSG','이승기',1987,'서울','011','11111111',182,'2008-08-08')
  ,('KBS','김범수',1979,'경남','011','22222222',173,'2012-04-04')
  ,('KKH','김경호',1971,'전남','019','33333333',177,'2007-07-07')
  ,('JYP','조용필',1950,'경기','011','44444444',166,'2009-04-04')
  ,('SSK','성시경',1979,'서울',NULL,NULL,186,'2013-12-12')
  ,('LJB','임재범',1963,'서울','016','66666666',182,'2009-09-09')
  ,('YJS','윤종신',1969,'경남',NULL,NULL,170,'2005-05-05')
  ,('EJW','은지원',1972,'경북','011','88888888',174,'2014-03-03')
  ,('JKW','조관우',1965,'경기','018','99999999',172,'2010-10-10')
  ,('BBK','바비킴',1973,'서울','010','00000000',176,'2013-05-05');
  
  -- buyTBL
  INSERT INTO buyTBL VALUES(nextval('seq_idNum'),'KBS','운동화',NULL,30,2)
  ,(nextval('seq_idNum'),'KBS','노트북','전자',1000,1)
  ,(nextval('seq_idNum'),'JYP','모니터','전자',200,1)
  ,(nextval('seq_idNum'),'BBK','모니터','전자',200,5)
  ,(nextval('seq_idNum'),'KBS','청바지','의류',50,3)
  ,(nextval('seq_idNum'),'BBK','메모리','전자',80,10)
  ,(nextval('seq_idNum'),'SSK','책','서적',15,5)
  ,(nextval('seq_idNum'),'EJW','책','서적',15,2)
  ,(nextval('seq_idNum'),'EJW','청바지','의류',50,1)
  ,(nextval('seq_idNum'),'BBK','운동화',NULL,30,2)
  ,(nextval('seq_idNum'),'EJW','책','서적',15,1)
  ,(nextval('seq_idNum'),'BBK','운동화',NULL,30,2);
  
  SELECT * FROM userTBL;
  SELECT * FROM buyTBL;
  ```

## 01.INNER JOIN

- **Oracle, Postgres**

```sql
SELECT * 
   FROM buyTbl
     INNER JOIN userTbl
        ON buyTbl.userID = userTbl.userID
   WHERE buyTbl.userID = 'JYP';

SELECT * 
   FROM buyTbl
     INNER JOIN userTbl
        ON buyTbl.userID = userTbl.userID;

SELECT userID, userName, prodName, addr, mobile1 || mobile2 AS "연락처" -- 같은 이름의 컬럼은 어디의 테이블인지 명시해야함
   FROM buyTbl
     INNER JOIN userTbl
        ON buyTbl.userID = userTbl.userID ;

SELECT buyTbl.userID, userName, prodName, addr, mobile1 || mobile2 AS "연락처"
   FROM buyTbl
     INNER JOIN userTbl
        ON buyTbl.userID = userTbl.userID ;

SELECT buyTbl.userID, userName, prodName, addr, mobile1 || mobile2 
   FROM buyTbl, userTbl
   WHERE buyTbl.userID = userTbl.userID ;

SELECT buyTbl.userID, userTbl.userName, buyTbl.prodName, userTbl.addr, 
         userTbl.mobile1 || userTbl.mobile2  AS "연락처"
   FROM buyTbl
     INNER JOIN userTbl
        ON buyTbl.userID = userTbl.userID;

SELECT B.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2  AS "연락처"
   FROM buyTbl B
     INNER JOIN userTbl U
        ON B.userID = U.userID;
        
SELECT B.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2  AS "연락처"
   FROM buyTbl B
     INNER JOIN userTbl U
        ON B.userID = U.userID
   WHERE B.userID = 'JYP';

SELECT U.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2  AS "연락처"
   FROM userTbl U
     INNER JOIN buyTbl B
        ON U.userID = B.userID 
   WHERE B.userID = 'JYP';

SELECT U.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2  AS "연락처"
   FROM userTbl U
     INNER JOIN buyTbl B
        ON U.userID = B.userID 
   ORDER BY U.userID;

SELECT DISTINCT U.userID, U.userName,  U.addr
   FROM userTbl U
     INNER JOIN buyTbl B
        ON U.userID = B.userID 
   ORDER BY U.userID ;

SELECT U.userID, U.userName,  U.addr
   FROM userTbl U
   WHERE EXISTS (
      SELECT * 
      FROM buyTbl B
      WHERE U.userID = B.userID );
```

### 01.1 세 개 테이블 조인

- **Oracle**

  ```sql
  CREATE TABLE stdTBL 
  ( stdName   NCHAR(5) NOT NULL PRIMARY KEY,
    addr	    NCHAR(2) NOT NULL
  );
  CREATE TABLE clubTBL 
  ( clubName    NCHAR(5) NOT NULL PRIMARY KEY,
    roomNo      NCHAR(4) NOT NULL
  );
  CREATE SEQUENCE stdclubSEQ;
  CREATE TABLE stdclubTBL
  (  idNum    NUMBER(5) NOT NULL PRIMARY KEY, 
     stdName  NCHAR(5) NOT NULL,
     clubName NCHAR(5) NOT NULL,
  FOREIGN KEY(stdName) REFERENCES stdTBL(stdName),
  FOREIGN KEY(clubName) REFERENCES clubTBL(clubName)
  );
  INSERT INTO stdTBL VALUES('김범수','경남');
  INSERT INTO stdTBL VALUES('성시경','서울');
  INSERT INTO stdTBL VALUES('조용필','경기');
  INSERT INTO stdTBL VALUES('은지원','경북');
  INSERT INTO stdTBL VALUES('바비킴','서울');
  INSERT INTO clubTBL VALUES('수영','101호');
  INSERT INTO clubTBL VALUES('바둑','102호');
  INSERT INTO clubTBL VALUES('축구','103호');
  INSERT INTO clubTBL VALUES('봉사','104호');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL, '김범수','바둑');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL,'김범수','축구');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL,'조용필','축구');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL,'은지원','축구');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL,'은지원','봉사');
  INSERT INTO stdclubTBL VALUES(stdclubSEQ.NEXTVAL,'바비킴','봉사');
  
  SELECT S.stdName, S.addr, C.clubName, C.roomNo
     FROM stdTBL S 
        INNER JOIN stdclubTBL SC
             ON S.stdName = SC.stdName
        INNER JOIN clubTBL C
  	  ON SC.clubName = C.clubName 
     ORDER BY S.stdName;
  
  SELECT C.clubName, C.roomNo, S.stdName, S.addr
     FROM  stdTBL S
        INNER JOIN stdclubTBL SC
             ON SC.stdName = S.stdName
        INNER JOIN clubTBL C
  	 ON SC.clubName = C.clubName
      ORDER BY C.clubName;
  ```

- **Postgres**

  ```sql
  DROP TABLE clubTBL;
  DROP TABLE stdclubTBL;
  DROP TABLE stdTBL;
  
   -- SQLINES LICENSE FOR EVALUATION USE ONLY
   CREATE TABLE stdTBL 
  ( stdName   CHAR(5) NOT NULL PRIMARY KEY,
    addr	    CHAR(4) NOT NULL
  );
  -- SQLINES LICENSE FOR EVALUATION USE ONLY
  CREATE TABLE clubTBL 
  ( clubName    CHAR(5) NOT NULL PRIMARY KEY,
    roomNo      CHAR(4) NOT NULL
  );
  
  drop sequence stdclubSEQ;
  CREATE SEQUENCE stdclubSEQ;
  -- SQLINES LICENSE FOR EVALUATION USE ONLY
  CREATE TABLE stdclubTBL
  (  idNum    INT NOT NULL PRIMARY KEY, 
     stdName  CHAR(5) NOT NULL,
     clubName CHAR(5) NOT NULL,
  FOREIGN KEY(stdName) REFERENCES stdTBL(stdName),
  FOREIGN KEY(clubName) REFERENCES clubTBL(clubName)
  );
  
  INSERT INTO stdTBL values
   ('김범수','경남')
  ,('성시경','서울')
  ,('조용필','경기')
  ,('은지원','경북')
  ,('바비킴','서울');
  INSERT INTO clubTBL values
   ('수영','101호')
  ,('바둑','102호')
  ,('축구','103호')
  ,('봉사','104호');
  
  INSERT INTO stdclubTBL values
   (NEXTVAL('stdclubSEQ'), '김범수','바둑')
  ,(NEXTVAL('stdclubSEQ'),'김범수','축구')
  ,(NEXTVAL('stdclubSEQ'),'조용필','축구')
  ,(NEXTVAL('stdclubSEQ'),'은지원','축구')
  ,(NEXTVAL('stdclubSEQ'),'은지원','봉사')
  ,(NEXTVAL('stdclubSEQ'),'바비킴','봉사');
  
  -- SQLINES LICENSE FOR EVALUATION USE ONLY
  SELECT S.stdName, S.addr, C.clubName, C.roomNo
     FROM stdTBL S 
        INNER JOIN stdclubTBL SC
             ON S.stdName = SC.stdName
        INNER JOIN clubTBL C
  	  ON SC.clubName = C.clubName 
     ORDER BY S.stdName;
  
  -- SQLINES LICENSE FOR EVALUATION USE ONLY
  SELECT C.clubName, C.roomNo, S.stdName, S.addr
     FROM  stdTBL S
        INNER JOIN stdclubTBL SC
             ON SC.stdName = S.stdName
        INNER JOIN clubTBL C
  	 ON SC.clubName = C.clubName
      ORDER BY C.clubName;         
  ```

## 02.OUTER JOIN

- **Oracle, Postgres**

  ```sql
   SELECT U.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2 AS "연락처"
     FROM userTbl U
        LEFT OUTER JOIN buyTbl B
           ON U.userID = B.userID 
     ORDER BY U.userID;
  
  SELECT U.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2 AS "연락처"
     FROM buyTbl B 
        RIGHT OUTER JOIN userTbl U
           ON U.userID = B.userID 
     ORDER BY U.userID;
  
  SELECT U.userID, U.userName, B.prodName, U.addr, U.mobile1 || U.mobile2 AS "연락처"
     FROM userTbl U
        LEFT  JOIN buyTbl B
           ON U.userID = B.userID 
     WHERE B.prodName IS NULL
     ORDER BY U.userID;  
  ```

### 02.1 LEFT/RIGHT/FULL OUTER JOIN 실습

- **Oracle, Postgres**

  ```sql
  SELECT S.stdName, S.addr, C.clubName, C.roomNo
     FROM stdTBL S 
        LEFT OUTER JOIN stdclubTBL SC
            ON S.stdName = SC.stdName
        LEFT OUTER JOIN clubTBL C
            ON SC.clubName = C.clubName
     ORDER BY S.stdName;
  
  SELECT C.clubName, C.roomNo, S.stdName, S.addr
     FROM  stdTBL S
        LEFT OUTER JOIN stdclubTBL SC
            ON SC.stdName = S.stdName
        RIGHT OUTER JOIN clubTBL C
            ON SC.clubName = C.clubName
     ORDER BY C.clubName;
  
  SELECT S.stdName, S.addr, C.clubName, C.roomNo
     FROM stdTBL S 
        LEFT OUTER JOIN stdclubTBL SC
            ON S.stdName = SC.stdName
        LEFT OUTER JOIN clubTBL C
            ON SC.clubName = C.clubName
  UNION 
  SELECT S.stdName, S.addr, C.clubName, C.roomNo
     FROM  stdTBL S
        LEFT OUTER JOIN stdclubTBL SC
            ON SC.stdName = S.stdName
        RIGHT OUTER JOIN clubTBL C
            ON SC.clubName = C.clubName;
  ```

### 02.2 CROSS JOIN

- 사전 준비

  ```sql
  CREATE TABLE EMPLOYEES
     (	
      EMPLOYEE_ID numeric, 
  	FIRST_NAME VARCHAR(20), 
  	LAST_NAME VARCHAR(25) NOT NULL , 
  	EMAIL VARCHAR(25) NOT NULL , 
  	PHONE_NUMBER VARCHAR(20), 
  	HIRE_DATE DATE NOT NULL , 
  	JOB_ID VARCHAR(10)   NOT NULL , 
  	SALARY numeric, 
  	COMMISSION_PCT numeric, 
  	MANAGER_ID numeric, 
  	DEPARTMENT_ID numeric, 
  	 CONSTRAINT EMPLOYEES_PK PRIMARY KEY
    	(
    		EMPLOYEE_ID
    	)
  	);
  
  CREATE TABLE countries(
     COUNTRY_ID   VARCHAR(2) NOT NULL PRIMARY KEY
    ,COUNTRY_NAME VARCHAR(24) NOT NULL
    ,REGION_ID    INTEGER  NOT NULL
  );
  
  INSERT INTO EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL,PHONE_NUMBER,HIRE_DATE,JOB_ID,SALARY,COMMISSION_PCT,MANAGER_ID,DEPARTMENT_ID) VALUES
  	 (100,'Steven','King','SKING','515.123.4567',TIMESTAMP'2003-06-17 00:00:00.0','AD_PRES',24000,NULL,NULL,90),
  	 (101,'Neena','Kochhar','NKOCHHAR','515.123.4568',TIMESTAMP'2005-09-21 00:00:00.0','AD_VP',17000,NULL,100,90),
  	 (102,'Lex','De Haan','LDEHAAN','515.123.4569',TIMESTAMP'2001-01-13 00:00:00.0','AD_VP',17000,NULL,100,90),
  	 (103,'Alexander','Hunold','AHUNOLD','590.423.4567',TIMESTAMP'2006-01-03 00:00:00.0','IT_PROG',9000,NULL,102,60),
  	 (104,'Bruce','Ernst','BERNST','590.423.4568',TIMESTAMP'2007-05-21 00:00:00.0','IT_PROG',6000,NULL,103,60),
  	 (105,'David','Austin','DAUSTIN','590.423.4569',TIMESTAMP'2005-06-25 00:00:00.0','IT_PROG',4800,NULL,103,60),
  	 (106,'Valli','Pataballa','VPATABAL','590.423.4560',TIMESTAMP'2006-02-05 00:00:00.0','IT_PROG',4800,NULL,103,60),
  	 (107,'Diana','Lorentz','DLORENTZ','590.423.5567',TIMESTAMP'2007-02-07 00:00:00.0','IT_PROG',4200,NULL,103,60),
  	 (108,'Nancy','Greenberg','NGREENBE','515.124.4569',TIMESTAMP'2002-08-17 00:00:00.0','FI_MGR',12008,NULL,101,100),
  	 (109,'Daniel','Faviet','DFAVIET','515.124.4169',TIMESTAMP'2002-08-16 00:00:00.0','FI_ACCOUNT',9000,NULL,108,100),
  	 (110,'John','Chen','JCHEN','515.124.4269',TIMESTAMP'2005-09-28 00:00:00.0','FI_ACCOUNT',8200,NULL,108,100),
  	 (111,'Ismael','Sciarra','ISCIARRA','515.124.4369',TIMESTAMP'2005-09-30 00:00:00.0','FI_ACCOUNT',7700,NULL,108,100),
  	 (112,'Jose Manuel','Urman','JMURMAN','515.124.4469',TIMESTAMP'2006-03-07 00:00:00.0','FI_ACCOUNT',7800,NULL,108,100),
  	 (113,'Luis','Popp','LPOPP','515.124.4567',TIMESTAMP'2007-12-07 00:00:00.0','FI_ACCOUNT',6900,NULL,108,100),
  	 (114,'Den','Raphaely','DRAPHEAL','515.127.4561',TIMESTAMP'2002-12-07 00:00:00.0','PU_MAN',11000,NULL,100,30),
  	 (115,'Alexander','Khoo','AKHOO','515.127.4562',TIMESTAMP'2003-05-18 00:00:00.0','PU_CLERK',3100,NULL,114,30),
  	 (116,'Shelli','Baida','SBAIDA','515.127.4563',TIMESTAMP'2005-12-24 00:00:00.0','PU_CLERK',2900,NULL,114,30),
  	 (117,'Sigal','Tobias','STOBIAS','515.127.4564',TIMESTAMP'2005-07-24 00:00:00.0','PU_CLERK',2800,NULL,114,30),
  	 (118,'Guy','Himuro','GHIMURO','515.127.4565',TIMESTAMP'2006-11-15 00:00:00.0','PU_CLERK',2600,NULL,114,30),
  	 (119,'Karen','Colmenares','KCOLMENA','515.127.4566',TIMESTAMP'2007-08-10 00:00:00.0','PU_CLERK',2500,NULL,114,30),
  	 (120,'Matthew','Weiss','MWEISS','650.123.1234',TIMESTAMP'2004-07-18 00:00:00.0','ST_MAN',8000,NULL,100,50),
  	 (121,'Adam','Fripp','AFRIPP','650.123.2234',TIMESTAMP'2005-04-10 00:00:00.0','ST_MAN',8200,NULL,100,50),
  	 (122,'Payam','Kaufling','PKAUFLIN','650.123.3234',TIMESTAMP'2003-05-01 00:00:00.0','ST_MAN',7900,NULL,100,50),
  	 (123,'Shanta','Vollman','SVOLLMAN','650.123.4234',TIMESTAMP'2005-10-10 00:00:00.0','ST_MAN',6500,NULL,100,50),
  	 (124,'Kevin','Mourgos','KMOURGOS','650.123.5234',TIMESTAMP'2007-11-16 00:00:00.0','ST_MAN',5800,NULL,100,50),
  	 (125,'Julia','Nayer','JNAYER','650.124.1214',TIMESTAMP'2005-07-16 00:00:00.0','ST_CLERK',3200,NULL,120,50),
  	 (126,'Irene','Mikkilineni','IMIKKILI','650.124.1224',TIMESTAMP'2006-09-28 00:00:00.0','ST_CLERK',2700,NULL,120,50),
  	 (127,'James','Landry','JLANDRY','650.124.1334',TIMESTAMP'2007-01-14 00:00:00.0','ST_CLERK',2400,NULL,120,50),
  	 (128,'Steven','Markle','SMARKLE','650.124.1434',TIMESTAMP'2008-03-08 00:00:00.0','ST_CLERK',2200,NULL,120,50),
  	 (129,'Laura','Bissot','LBISSOT','650.124.5234',TIMESTAMP'2005-08-20 00:00:00.0','ST_CLERK',3300,NULL,121,50),
  	 (130,'Mozhe','Atkinson','MATKINSO','650.124.6234',TIMESTAMP'2005-10-30 00:00:00.0','ST_CLERK',2800,NULL,121,50),
  	 (131,'James','Marlow','JAMRLOW','650.124.7234',TIMESTAMP'2005-02-16 00:00:00.0','ST_CLERK',2500,NULL,121,50),
  	 (132,'TJ','Olson','TJOLSON','650.124.8234',TIMESTAMP'2007-04-10 00:00:00.0','ST_CLERK',2100,NULL,121,50),
  	 (133,'Jason','Mallin','JMALLIN','650.127.1934',TIMESTAMP'2004-06-14 00:00:00.0','ST_CLERK',3300,NULL,122,50),
  	 (134,'Michael','Rogers','MROGERS','650.127.1834',TIMESTAMP'2006-08-26 00:00:00.0','ST_CLERK',2900,NULL,122,50),
  	 (135,'Ki','Gee','KGEE','650.127.1734',TIMESTAMP'2007-12-12 00:00:00.0','ST_CLERK',2400,NULL,122,50),
  	 (136,'Hazel','Philtanker','HPHILTAN','650.127.1634',TIMESTAMP'2008-02-06 00:00:00.0','ST_CLERK',2200,NULL,122,50),
  	 (137,'Renske','Ladwig','RLADWIG','650.121.1234',TIMESTAMP'2003-07-14 00:00:00.0','ST_CLERK',3600,NULL,123,50),
  	 (138,'Stephen','Stiles','SSTILES','650.121.2034',TIMESTAMP'2005-10-26 00:00:00.0','ST_CLERK',3200,NULL,123,50),
  	 (139,'John','Seo','JSEO','650.121.2019',TIMESTAMP'2006-02-12 00:00:00.0','ST_CLERK',2700,NULL,123,50),
  	 (140,'Joshua','Patel','JPATEL','650.121.1834',TIMESTAMP'2006-04-06 00:00:00.0','ST_CLERK',2500,NULL,123,50),
  	 (141,'Trenna','Rajs','TRAJS','650.121.8009',TIMESTAMP'2003-10-17 00:00:00.0','ST_CLERK',3500,NULL,124,50),
  	 (142,'Curtis','Davies','CDAVIES','650.121.2994',TIMESTAMP'2005-01-29 00:00:00.0','ST_CLERK',3100,NULL,124,50),
  	 (143,'Randall','Matos','RMATOS','650.121.2874',TIMESTAMP'2006-03-15 00:00:00.0','ST_CLERK',2600,NULL,124,50),
  	 (144,'Peter','Vargas','PVARGAS','650.121.2004',TIMESTAMP'2006-07-09 00:00:00.0','ST_CLERK',2500,NULL,124,50),
  	 (145,'John','Russell','JRUSSEL','011.44.1344.429268',TIMESTAMP'2004-10-01 00:00:00.0','SA_MAN',14000,0.4,100,80),
  	 (146,'Karen','Partners','KPARTNER','011.44.1344.467268',TIMESTAMP'2005-01-05 00:00:00.0','SA_MAN',13500,0.3,100,80),
  	 (147,'Alberto','Errazuriz','AERRAZUR','011.44.1344.429278',TIMESTAMP'2005-03-10 00:00:00.0','SA_MAN',12000,0.3,100,80),
  	 (148,'Gerald','Cambrault','GCAMBRAU','011.44.1344.619268',TIMESTAMP'2007-10-15 00:00:00.0','SA_MAN',11000,0.3,100,80),
  	 (149,'Eleni','Zlotkey','EZLOTKEY','011.44.1344.429018',TIMESTAMP'2008-01-29 00:00:00.0','SA_MAN',10500,0.2,100,80),
  	 (150,'Peter','Tucker','PTUCKER','011.44.1344.129268',TIMESTAMP'2005-01-30 00:00:00.0','SA_REP',10000,0.3,145,80),
  	 (151,'David','Bernstein','DBERNSTE','011.44.1344.345268',TIMESTAMP'2005-03-24 00:00:00.0','SA_REP',9500,0.25,145,80),
  	 (152,'Peter','Hall','PHALL','011.44.1344.478968',TIMESTAMP'2005-08-20 00:00:00.0','SA_REP',9000,0.25,145,80),
  	 (153,'Christopher','Olsen','COLSEN','011.44.1344.498718',TIMESTAMP'2006-03-30 00:00:00.0','SA_REP',8000,0.2,145,80),
  	 (154,'Nanette','Cambrault','NCAMBRAU','011.44.1344.987668',TIMESTAMP'2006-12-09 00:00:00.0','SA_REP',7500,0.2,145,80),
  	 (155,'Oliver','Tuvault','OTUVAULT','011.44.1344.486508',TIMESTAMP'2007-11-23 00:00:00.0','SA_REP',7000,0.15,145,80),
  	 (156,'Janette','King','JKING','011.44.1345.429268',TIMESTAMP'2004-01-30 00:00:00.0','SA_REP',10000,0.35,146,80),
  	 (157,'Patrick','Sully','PSULLY','011.44.1345.929268',TIMESTAMP'2004-03-04 00:00:00.0','SA_REP',9500,0.35,146,80),
  	 (158,'Allan','McEwen','AMCEWEN','011.44.1345.829268',TIMESTAMP'2004-08-01 00:00:00.0','SA_REP',9000,0.35,146,80),
  	 (159,'Lindsey','Smith','LSMITH','011.44.1345.729268',TIMESTAMP'2005-03-10 00:00:00.0','SA_REP',8000,0.3,146,80),
  	 (160,'Louise','Doran','LDORAN','011.44.1345.629268',TIMESTAMP'2005-12-15 00:00:00.0','SA_REP',7500,0.3,146,80),
  	 (161,'Sarath','Sewall','SSEWALL','011.44.1345.529268',TIMESTAMP'2006-11-03 00:00:00.0','SA_REP',7000,0.25,146,80),
  	 (162,'Clara','Vishney','CVISHNEY','011.44.1346.129268',TIMESTAMP'2005-11-11 00:00:00.0','SA_REP',10500,0.25,147,80),
  	 (163,'Danielle','Greene','DGREENE','011.44.1346.229268',TIMESTAMP'2007-03-19 00:00:00.0','SA_REP',9500,0.15,147,80),
  	 (164,'Mattea','Marvins','MMARVINS','011.44.1346.329268',TIMESTAMP'2008-01-24 00:00:00.0','SA_REP',7200,0.1,147,80),
  	 (165,'David','Lee','DLEE','011.44.1346.529268',TIMESTAMP'2008-02-23 00:00:00.0','SA_REP',6800,0.1,147,80),
  	 (166,'Sundar','Ande','SANDE','011.44.1346.629268',TIMESTAMP'2008-03-24 00:00:00.0','SA_REP',6400,0.1,147,80),
  	 (167,'Amit','Banda','ABANDA','011.44.1346.729268',TIMESTAMP'2008-04-21 00:00:00.0','SA_REP',6200,0.1,147,80),
  	 (168,'Lisa','Ozer','LOZER','011.44.1343.929268',TIMESTAMP'2005-03-11 00:00:00.0','SA_REP',11500,0.25,148,80),
  	 (169,'Harrison','Bloom','HBLOOM','011.44.1343.829268',TIMESTAMP'2006-03-23 00:00:00.0','SA_REP',10000,0.2,148,80),
  	 (170,'Tayler','Fox','TFOX','011.44.1343.729268',TIMESTAMP'2006-01-24 00:00:00.0','SA_REP',9600,0.2,148,80),
  	 (171,'William','Smith','WSMITH','011.44.1343.629268',TIMESTAMP'2007-02-23 00:00:00.0','SA_REP',7400,0.15,148,80),
  	 (172,'Elizabeth','Bates','EBATES','011.44.1343.529268',TIMESTAMP'2007-03-24 00:00:00.0','SA_REP',7300,0.15,148,80),
  	 (173,'Sundita','Kumar','SKUMAR','011.44.1343.329268',TIMESTAMP'2008-04-21 00:00:00.0','SA_REP',6100,0.1,148,80),
  	 (174,'Ellen','Abel','EABEL','011.44.1644.429267',TIMESTAMP'2004-05-11 00:00:00.0','SA_REP',11000,0.3,149,80),
  	 (175,'Alyssa','Hutton','AHUTTON','011.44.1644.429266',TIMESTAMP'2005-03-19 00:00:00.0','SA_REP',8800,0.25,149,80),
  	 (176,'Jonathon','Taylor','JTAYLOR','011.44.1644.429265',TIMESTAMP'2006-03-24 00:00:00.0','SA_REP',8600,0.2,149,80),
  	 (177,'Jack','Livingston','JLIVINGS','011.44.1644.429264',TIMESTAMP'2006-04-23 00:00:00.0','SA_REP',8400,0.2,149,80),
  	 (178,'Kimberely','Grant','KGRANT','011.44.1644.429263',TIMESTAMP'2007-05-24 00:00:00.0','SA_REP',7000,0.15,149,NULL),
  	 (179,'Charles','Johnson','CJOHNSON','011.44.1644.429262',TIMESTAMP'2008-01-04 00:00:00.0','SA_REP',6200,0.1,149,80),
  	 (180,'Winston','Taylor','WTAYLOR','650.507.9876',TIMESTAMP'2006-01-24 00:00:00.0','SH_CLERK',3200,NULL,120,50),
  	 (181,'Jean','Fleaur','JFLEAUR','650.507.9877',TIMESTAMP'2006-02-23 00:00:00.0','SH_CLERK',3100,NULL,120,50),
  	 (182,'Martha','Sullivan','MSULLIVA','650.507.9878',TIMESTAMP'2007-06-21 00:00:00.0','SH_CLERK',2500,NULL,120,50),
  	 (183,'Girard','Geoni','GGEONI','650.507.9879',TIMESTAMP'2008-02-03 00:00:00.0','SH_CLERK',2800,NULL,120,50),
  	 (184,'Nandita','Sarchand','NSARCHAN','650.509.1876',TIMESTAMP'2004-01-27 00:00:00.0','SH_CLERK',4200,NULL,121,50),
  	 (185,'Alexis','Bull','ABULL','650.509.2876',TIMESTAMP'2005-02-20 00:00:00.0','SH_CLERK',4100,NULL,121,50),
  	 (186,'Julia','Dellinger','JDELLING','650.509.3876',TIMESTAMP'2006-06-24 00:00:00.0','SH_CLERK',3400,NULL,121,50),
  	 (187,'Anthony','Cabrio','ACABRIO','650.509.4876',TIMESTAMP'2007-02-07 00:00:00.0','SH_CLERK',3000,NULL,121,50),
  	 (188,'Kelly','Chung','KCHUNG','650.505.1876',TIMESTAMP'2005-06-14 00:00:00.0','SH_CLERK',3800,NULL,122,50),
  	 (189,'Jennifer','Dilly','JDILLY','650.505.2876',TIMESTAMP'2005-08-13 00:00:00.0','SH_CLERK',3600,NULL,122,50),
  	 (190,'Timothy','Gates','TGATES','650.505.3876',TIMESTAMP'2006-07-11 00:00:00.0','SH_CLERK',2900,NULL,122,50),
  	 (191,'Randall','Perkins','RPERKINS','650.505.4876',TIMESTAMP'2007-12-19 00:00:00.0','SH_CLERK',2500,NULL,122,50),
  	 (192,'Sarah','Bell','SBELL','650.501.1876',TIMESTAMP'2004-02-04 00:00:00.0','SH_CLERK',4000,NULL,123,50),
  	 (193,'Britney','Everett','BEVERETT','650.501.2876',TIMESTAMP'2005-03-03 00:00:00.0','SH_CLERK',3900,NULL,123,50),
  	 (194,'Samuel','McCain','SMCCAIN','650.501.3876',TIMESTAMP'2006-07-01 00:00:00.0','SH_CLERK',3200,NULL,123,50),
  	 (195,'Vance','Jones','VJONES','650.501.4876',TIMESTAMP'2007-03-17 00:00:00.0','SH_CLERK',2800,NULL,123,50),
  	 (196,'Alana','Walsh','AWALSH','650.507.9811',TIMESTAMP'2006-04-24 00:00:00.0','SH_CLERK',3100,NULL,124,50),
  	 (197,'Kevin','Feeney','KFEENEY','650.507.9822',TIMESTAMP'2006-05-23 00:00:00.0','SH_CLERK',3000,NULL,124,50),
  	 (198,'Donald','OConnell','DOCONNEL','650.507.9833',TIMESTAMP'2007-06-21 00:00:00.0','SH_CLERK',2600,NULL,124,50),
  	 (199,'Douglas','Grant','DGRANT','650.507.9844',TIMESTAMP'2008-01-13 00:00:00.0','SH_CLERK',2600,NULL,124,50),
  	 (200,'Jennifer','Whalen','JWHALEN','515.123.4444',TIMESTAMP'2003-09-17 00:00:00.0','AD_ASST',4400,NULL,101,10),
  	 (201,'Michael','Hartstein','MHARTSTE','515.123.5555',TIMESTAMP'2004-02-17 00:00:00.0','MK_MAN',13000,NULL,100,20),
  	 (202,'Pat','Fay','PFAY','603.123.6666',TIMESTAMP'2005-08-17 00:00:00.0','MK_REP',6000,NULL,201,20),
  	 (203,'Susan','Mavris','SMAVRIS','515.123.7777',TIMESTAMP'2002-06-07 00:00:00.0','HR_REP',6500,NULL,101,40),
  	 (204,'Hermann','Baer','HBAER','515.123.8888',TIMESTAMP'2002-06-07 00:00:00.0','PR_REP',10000,NULL,101,70),
  	 (205,'Shelley','Higgins','SHIGGINS','515.123.8080',TIMESTAMP'2002-06-07 00:00:00.0','AC_MGR',12008,NULL,101,110),
  	 (206,'William','Gietz','WGIETZ','515.123.8181',TIMESTAMP'2002-06-07 00:00:00.0','AC_ACCOUNT',8300,NULL,205,110);
  	 
  INSERT INTO countries(COUNTRY_ID,COUNTRY_NAME,REGION_ID) VALUES
   ('AR','Argentina',2)
  ,('AU','Australia',3)
  ,('BE','Belgium',1)
  ,('BR','Brazil',2)
  ,('CA','Canada',2)
  ,('CH','Switzerland',1)
  ,('CN','China',3)
  ,('DE','Germany',1)
  ,('DK','Denmark',1)
  ,('EG','Egypt',4)
  ,('FR','France',1)
  ,('IL','Israel',4)
  ,('IN','India',3)
  ,('IT','Italy',1)
  ,('JP','Japan',3)
  ,('KW','Kuwait',4)
  ,('ML','Malaysia',3)
  ,('MX','Mexico',2)
  ,('NG','Nigeria',4)
  ,('NL','Netherlands',1)
  ,('SG','Singapore',3)
  ,('UK','United Kingdom',1)
  ,('US','United States of America',2)
  ,('ZM','Zambia',4)
  ,('ZW','Zimbabwe',4);	 
  ```

- **Oracle**

  ```sql
  SELECT * 
     FROM buyTbl 
       CROSS JOIN userTbl;
  
  SELECT * 
     FROM buyTbl , userTbl ;
  
  SELECT  COUNT(*) AS "데이터 개수"
     FROM HR.employees 
       CROSS JOIN HR.countries;
  
  CREATE TABLE empTbl (emp NCHAR(3), manager NCHAR(3), department NCHAR(3));
  INSERT INTO empTbl VALUES('나사장','없음','없음');
  INSERT INTO empTbl VALUES('김재무','나사장','재무부');
  INSERT INTO empTbl VALUES('김부장','김재무','재무부');
  INSERT INTO empTbl VALUES('이부장','김재무','재무부');
  INSERT INTO empTbl VALUES('우대리','이부장','재무부');
  INSERT INTO empTbl VALUES('지사원','이부장','재무부');
  INSERT INTO empTbl VALUES('이영업','나사장','영업부');
  INSERT INTO empTbl VALUES('한과장','이영업','영업부');
  INSERT INTO empTbl VALUES('최정보','나사장','정보부');
  INSERT INTO empTbl VALUES('윤차장','최정보','정보부');
  INSERT INTO empTbl VALUES('이주임','윤차장','정보부');
  
  SELECT A.emp AS "부하직원" , B.emp AS "직속상관", B.department AS "직속상관부서"
     FROM empTbl A
        INNER JOIN empTbl B
           ON A.manager = B.emp
     WHERE A.emp = '우대리';
  
  SELECT stdName, addr FROM stdTBL
     UNION ALL
  SELECT clubName, roomNo FROM clubTBL;
  
  SELECT userName, CONCAT(mobile1, mobile2) AS "전화번호" FROM userTbl
     WHERE userName NOT IN ( SELECT userName FROM userTbl WHERE mobile1 IS NULL);
  
  SELECT userName, CONCAT(mobile1, mobile2) AS "전화번호" FROM userTbl
     WHERE userName IN ( SELECT userName FROM userTbl WHERE mobile1 IS NULL);
  ```

- **Postgres**

  ```sql
  SELECT * 
     FROM buyTbl 
       CROSS JOIN userTbl
  order by userName, idNum;
  
  SELECT * 
     FROM buyTbl , userTbl 
  order by userName, idNum;
  
  SELECT  COUNT(*) AS "데이터 개수"
     FROM employees 
       CROSS JOIN countries;
  
  CREATE TABLE empTbl (emp CHAR(3), manager CHAR(3), department CHAR(3));
  INSERT INTO empTbl VALUES
   ('나사장','없음','없음')
  ,('김재무','나사장','재무부')
  ,('김부장','김재무','재무부')
  ,('이부장','김재무','재무부')
  ,('우대리','이부장','재무부')
  ,('지사원','이부장','재무부')
  ,('이영업','나사장','영업부')
  ,('한과장','이영업','영업부')
  ,('최정보','나사장','정보부')
  ,('윤차장','최정보','정보부')
  ,('이주임','윤차장','정보부');
  
  SELECT A.emp AS "부하직원" , B.emp AS "직속상관", B.department AS "직속상관부서"
     FROM empTbl A
        INNER JOIN empTbl B
           ON A.manager = B.emp
     WHERE A.emp = '우대리';
  
  SELECT stdName, addr FROM stdTBL
     UNION ALL
  SELECT clubName, roomNo FROM clubTBL;
  
  SELECT userName, CONCAT(mobile1, mobile2) AS "전화번호" FROM userTbl
     WHERE userName NOT IN ( SELECT userName FROM userTbl WHERE mobile1 IS NULL);
  
  SELECT userName, CONCAT(mobile1, mobile2) AS "전화번호" FROM userTbl
     WHERE userName IN ( SELECT userName FROM userTbl WHERE mobile1 IS NULL); 
  ```