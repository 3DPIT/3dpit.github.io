---
title: oracle과 postgres 2일차비교
date: 2022-05-30 08:25:00 +0900
category: DB
draft: false
---

## 2022-05-30-oracle과-postgres-2일차비교

## 목차

 >
 >01.스키마 생성
 >
 >02.테이블 생성
 >
 >03.데이터 삽입
 >
 >04.데이터 조회

## 01.스키마 생성

- **Oracle**

  ``` sql
  -- USER SQL
  CREATE USER Shop IDENTIFIED BY "1234"
  DEFAULT TABLESPACE "USERS"
  TEMPORARY TABLESPACE "TEMP";
  
  -- QUORAS
  ALTER USER Shop QUOTA 10M ON USERS;
  
  -- ROLES
  GRANT "CONNECT" TO Shop;
  GRANT "RESOURCE" TO Shop;
  GRANT "DBA" TO Shop;
  
  ALTER SESSION SET CURRENT_SCHEMA = Shop;
  ```

- **Postgres**

  ```sql
  create user Shop with password '1234';
  
  create database Shop with owner Shop;
  
  create schema Shop authorization Shop;
  
  set search_path to "$user", Shop;
  ```

## 02.테이블 생성

- **Oracle**

  ```sql
  CREATE TABLE MEMBERTBL
  (
  	MEMBERID CHAR(8) NOT NULL
     ,MEMBERNAME NCHAR(5) NOT NULL
     ,MEMBERADDRESS NVARCHAR2(20) NOT NULL
     ,CONSTRAINT MEMBERTBL_PK PRIMARY KEY
  	(
  		MEMBERID
  	)
  	ENABLE
  );
  
  CREATE TABLE PRODUCTTBL
  (
  	PRODUCTNAME NCHAR(4) NOT NULL
  	,COST number(7) NOT NULL
  	,MAKEDATE DATE
  	,COMPANY nchar(5)
  	,AMOUNT number(3) NOT NULL
  	,CONSTRAINT PRODUCTTBL_PK PRIMARY KEY
  	(
  		PRODUCTNAME
  	)
  	ENABLE
  );
  ```

- **Postgres**

  ```sql
  CREATE TABLE MEMBERTBL
  (
  	MEMBERID char(8) NOT NULL
     ,MEMBERNAME nchar(5) NOT NULL
     ,MEMBERADDRESS varchar(20) NOT NULL
     ,CONSTRAINT MEMBERTBL_PK PRIMARY KEY
  	(
  		MEMBERID
  	)
  );
  
  CREATE TABLE PRODUCTTBL
  (
  	PRODUCTNAME nchar(4) NOT NULL
  	,COST numeric NOT NULL
  	,MAKEDATE date
  	,COMPANY nchar(5)
  	,AMOUNT numeric NOT NULL
  	,CONSTRAINT PRODUCTTBL_PK PRIMARY KEY
  	(
  		PRODUCTNAME
  	)
  );
  ```

## 03.데이터 삽입

- **Oracle**

  ```sql
  -- MEMBERTBL 데이터 삽입
  INSERT INTO MEMBERTBL (MEMBERID, MEMBERNAME, MEMBERADDRESS) VALUES ('Dang', '당탕이', '경기도 부천시 중동');
  INSERT INTO MEMBERTBL (MEMBERID, MEMBERNAME, MEMBERADDRESS) VALUES ('Jee', '지운이', '서울 은평구 중산동');
  
  -- PRODUCTTBL 데이터 삽입
  INSERT INTO PRODUCTTBL (PRODUCTNAME, COST, MAKEDATE, COMPANY, AMOUNT) VALUES ('컴퓨터', '10', '2022-08-17', '삼성', 17);
  INSERT INTO PRODUCTTBL (PRODUCTNAME, COST, MAKEDATE, COMPANY, AMOUNT) VALUES ('세탁기', '5', '2021-5-16', 'LG', 5);
  ```

- **Postgres**

  ```sql
  -- MEMBERTBL 데이터 삽입
  INSERT INTO MEMBERTBL (MEMBERID, MEMBERNAME, MEMBERADDRESS) values
   ('Dang', '당탕이', '경기도 부천시 중동')
  ,('Jee', '지운이', '서울 은평구 중산동');
  
  -- PRODUCTTBL 데이터 삽입
  INSERT INTO PRODUCTTBL (PRODUCTNAME, COST, MAKEDATE, COMPANY, AMOUNT) values
    ('컴퓨터', '10', '2022-08-17', '삼성', 17)
   ,('세탁기', '5', '2021-5-16', 'LG', 5);
  ```

## 04.데이터 조회

- **Oracle**

  ```sql
  SELECT * FROM MEMBERTBL;
  SELECT * FROM PRODUCTTBL;
  ```

- **Postgres**

  ```sql
  SELECT * FROM MEMBERTBL;
  SELECT * FROM PRODUCTTBL;
  ```

  

