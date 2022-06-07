---
title: oracle과 postgres 7일차비교
date: 2022-06-07 08:38:00 +0900
category: DB
draft: false
---

## 2022-06-07-oracle과-postgres-7일차비교

## 목차

## 01.데이터 형식

### 01.1 숫자 데이터 형식

| 자주사용 유무 | Oracle        | 바이트 수 | 숫자 범위              | 설명                                                         | PostgreSql                      | 설명                                                    |
| ------------- | ------------- | --------- | ---------------------- | ------------------------------------------------------------ | ------------------------------- | ------------------------------------------------------- |
| X             | BINARY_FLOAT  | 4         |                        | 32bit 부동 소수점                                            | real (별칭: float4)             | 최소 6자리 의정밀도 (적어도 1E-37 ~ 1E+37)              |
| X             | BINARY_DOUBLE | 8         |                        | 64bit 부동 소수점                                            | double precision (별칭: float8) | 최소 15 자리의 정밀도 (약 1E-307에서 1E + 308)          |
| O             | NUMBER(p,[s]) | 5 ~ 21    | p: 1~38<br />s: 84~127 | 전체 자리수 (p)와 소수점 이하 자릿수(s)를 가진 숫자형 (예: NUMBER(5,2)는 전체 자릿수를 5자리로 하되, 그 중 소수점 이하를 2자리로 하겠다는 의미) | DECIMAL (P,S)                   | 소수점 위 131072 자리까지 소수점 아래는 16,383 자리까지 |

### 01.2 문자 데이터 형식

| 자주사용 유무 | Oracle       | 바이트 수  | 설명                                                         | PostgreSql                |
| ------------- | ------------ | ---------- | ------------------------------------------------------------ | ------------------------- |
| O             | CHAR[(n)]    | 1~2000     | 고정 길이 문자형, 숫자 없이 사용시 CHAR(1)과 동일            | CHAR(n), VARCHAR(N), TEXT |
| O             | NCHAR[(n)]   | 2~2000     | 유니코드 고정길이 문자형, n을 1부터 1000까지 지정, 한글을 저장할 수 있음(한글자 당 2Byte사용됨), 숫자 없이 사용시 NCHAR(1)과 동일 | CHAR(n), VARCHAR(N), TEXT |
| O             | VARCHAR2(n)  | 1~4000     | 가변길이 문자형                                              | VARCHAR(n)                |
| O             | NVARCHAR2(n) | 2~4000     | 유니코드 가변길이 문자형, 한글을 저장할 수 있음, 한글자당 2Byte가 사용됨 | VARCHAR(n)                |
| O             | CLOB         | 최대 128TB | 대용량 덱스트의 데이터 타입(영문)                            | TEXT                      |
| O             | NCLOB        | 최대 128TB | 대용량 텍스트의 유니코드 데이터 타입(한글, 일본어, 한자등)   | TEXT                      |

### 01.3 이진 데이터 형식

| 자주사용 유무 | Oracle | 바이트 수                            | 설명                                                         | PostgreSql | 설명 |
| ------------- | ------ | ------------------------------------ | ------------------------------------------------------------ | ---------- | ---- |
| O             | BLOB   | 최대 128TB                           | 대용량 이진(Binary) 데이터를 저장할 수 있는 데이터 타입, Binary LOB의 약자 | -          | 없음 |
| X             | BFILE  | 운영체제에서 허용하는 크기(대개 4GB) | 대용량 이진(Binary)데이터를 파일 형태로 저장함, Oracle 내부에 저장하지 않고, 운영체제에 외부 파일 형태로 저장됨, Binary FILE의 약자 | -          | 없음 |

### 01.4 날짜와 시간 데이터 형식

| 자주사용 유무 | Oracle                         | 바이트 수 | 설명                                                         | PostgreSql                              |
| ------------- | ------------------------------ | --------- | ------------------------------------------------------------ | --------------------------------------- |
| O             | DATE                           | 7         | 날짜는 기원전 4712년 1월 1일 부터 9999년 12월 31일까지 저장됨(연, 월, 일 , 시, 분, 초가 저장됨) | date                                    |
| X             | TIMESTAMP                      | 11        | DATE와 같으나 밀리초 단위까지 저장됨                         | timestamp                               |
| X             | TIMESTAMP WITH TIME ZONE       | 13        | 날짜 및 시간대 형태의 데이터 형식                            | time [(p)] with time zone(별칭: timetz) |
| X             | TIMESTAMP WITH LOCAL TIME ZONE | 11        | 날짜 및 시간대 데이터 형식, 단 조회시에는 클라이언트의 시간대로 보여짐 | -                                       |

### 01.5 기타데이터형식

| 자주사용 유무 | Oracle   | 바이트 수 | 설명                                                         | PostgreSql |
| ------------- | -------- | --------- | ------------------------------------------------------------ | ---------- |
| X             | RAWID    | 10        | 행의 물리적인 주소를 저장하기 위한 데이터 형식으로 모든 행에 자동으로 RAWID열이 생성됨 | ctid       |
| X             | XML Type | N/A       | XML 데이터를 저장하기 위한 데이터 형식                       | xml        |
| X             | URLType  | N/A       | URL 형식의 데이터를 저장하기 위한 데이터 형식                | url        |

### 01.6 Oracle PostgreSQL 데이터 타입 (Data Type) 변환 비교

| Oracle                       | PostgreSQL                             |
| ---------------------------- | -------------------------------------- |
| NUMBER (1)                   | BOOL                                   |
| RAW(길이)                    | BYTEA                                  |
| DATETIME                     | DATE                                   |
| TIMESTAMP (0)                | TIME                                   |
| TIMESTAMP(크기)              | 크기0~6: TIMESTAMP크기7~9 VARCHAR (37) |
| NUMBER (3)                   | SMALLINT                               |
| NUMBER (5)                   | SMALLINT                               |
| NUMBER (10)                  | INTEGER                                |
| NUMBER (19)                  | BIGINT                                 |
| NUMBER (p,s)                 | DECIMAL (P,S)                          |
| FLOAT                        | FLOAT4                                 |
| FLOAT                        | FLOAT8                                 |
| VARCHAR2(길이)               | VARCHAR(바이트길이)                    |
| NUMBER (3)                   | SMALLINT                               |
| NUMBER (5)                   | INTEGER                                |
| NUMBER (10)                  | BIGINT                                 |
| NUMBER (19)                  | BIGINT                                 |
| NVARCHAR2(길이)              | VARCHAR (바이트길이)                   |
| BLOB                         | -                                      |
| CLOBSTRING (4000 byte 이상)  | TEXT                                   |
| NCLOBWSTRING(4000 byte 이상) | TEXT                                   |
| XMLTYPE                      | -                                      |

 ## 02.PL/SQL의 바인드 변수

- **형식**

  ```sql
  DECLARE
  	변수이름1 데이터형식;
  	변수이름2 데이터형식;
  BEGIN
  	변수이름1 := 값;
  	SELECT 열 이름 INTO 변수이름2 FROM 테이블;
  END;
  ```

- 사용법

  ```sql
  DECLARE
  	myVar1 NUMBER(3);
  	myVar2 NUMBER(5,2) := 3.14;
  	myVar3 NVARCHAR2(20) := '이승기 키 ->' ;
  BEGIN
  	myVar1 := 5;
  	DBMS_OUTPUT_LINE(myVar1);
  	DBMS_OUTPUT_LINE(myVar1 + myVar2);
  	SELECT height INTO myVar1 FROM userTBL WHERE userName = '이승기' ;
  	DBMS_OUTPUT.PUT_LINE(myVar3 || TO_CHAR(myVar1));
  END;
  ```

  