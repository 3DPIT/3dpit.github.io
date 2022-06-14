---
title: oracle과 postgres 5일차 6일차 비교
date: 2022-06-02 08:32:00 +0900
category: DB
draft: false
---

## 2022-06-02-oracle과-postgres-5일차-6일차-비교

## 목차

>  01.WITH절과 CTE
>
>  >  01.1 비재귀적 CTE
>  >
>  >  01.2 재귀적 CTE
>
>  02.INSERT
>
>  03.UPDATE
>
>  04.DELETE FROM
>
>  05.MERGE

## 01.WITH절과 CTE

### 01.1 비재귀적 CTE

- **Oracle, Postgres**

  ```sql
  WITH abc(userID, total)
  AS
  (  SELECT userID, SUM(price*amount)  
        FROM buyTbl  GROUP BY userID  )
  SELECT * FROM abc ORDER BY total DESC ;
  
  WITH abc(userID, total)
  AS
  (  SELECT userID, SUM(price*amount)  
        FROM buyTbl  GROUP BY userID  )
  SELECT userID AS "사용자", total AS "총 구매액" FROM abc ORDER BY total DESC ;
  
  WITH cte_userTbl(addr, maxHeight)
  AS
  ( SELECT addr, MAX(height) FROM userTbl GROUP BY addr)
  SELECT AVG(maxHeight) AS "각 지역별 최고키 평균" FROM cte_userTbl;
  
  WITH 
  AAA(userID, total)
     AS
       (SELECT userID, SUM(price*amount) FROM buyTbl GROUP BY userID ),
  BBB(sumtotal)
     AS
        (SELECT SUM(total) FROM AAA ),
  CCC(sumavg)
     AS
        (SELECT  sumtotal / (SELECT count(*) FROM buyTbl) FROM BBB)
  SELECT * FROM CCC;
  ```

### 01.2 재귀적 CTE

- **Oracle**

  ```sql
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
  
  WITH empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName)
  )
  SELECT * FROM empCTE ORDER BY dept, empLevel;
  
  WITH empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName)
  )
  SELECT CONCAT(RPAD(' ㄴ', empLevel*2 + 1, 'ㄴ'),  empName) AS "직원이름", dept AS "직원부서"
     FROM empCTE  ORDER BY dept, empLevel;
  
  WITH empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName
     WHERE empLevel < 2)
  )
  SELECT CONCAT(RPAD(' ㄴ', empLevel*2 + 1, 'ㄴ'),  empName) AS "직원이름", dept AS "직원부서"
     FROM empCTE  ORDER BY dept, empLevel;
  ```

- **Postrgres**

  ```sql
  CREATE TABLE empTbl (emp NCHAR(3), manager NCHAR(3), department NCHAR(3));
  select * from empTBL;
  INSERT INTO empTbl VALUES('나사장','없음','없음')
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
  
  WITH recursive empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName)
  )
  SELECT * FROM empCTE ORDER BY dept, empLevel;
  
  WITH recursive empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName)
  )
  SELECT CONCAT(RPAD(' ㄴ', empLevel*1 + 1, 'ㄴ'),  empName) AS "직원이름", dept AS "직원부서"
     FROM empCTE  ORDER BY dept, empLevel;
  
  WITH recursive empCTE(empName, mgrName, dept, empLevel)
  AS
  (
   ( SELECT emp, manager, department , 0  
         FROM empTbl 
         WHERE manager = '없음' ) -- 상관이 없는 사람이 바로 사장
    UNION ALL
    (SELECT empTbl.emp, empTbl.manager, empTbl.department, empCTE.empLevel+1
     FROM empTbl INNER JOIN empCTE 
          ON empTbl.manager = empCTE.empName
     WHERE empLevel < 2)
  )
  SELECT CONCAT(RPAD(' ㄴ', empLevel*1 + 1, 'ㄴ'),  empName) AS "직원이름", dept AS "직원부서"
     FROM empCTE  ORDER BY dept, empLevel;
  ```

  - 차이  recursive 추가 됨
    - **before**
      - `CONCAT(RPAD(' ㄴ', empLevel*2 + 1, 'ㄴ')`
    - **after**
      - `CONCAT(RPAD(' ㄴ', empLevel*1 + 1, 'ㄴ')`

## 02.INSERT

- **Oracle**

  ```sql
  
  CREATE TABLE testTBL1 (id  NUMBER(4), userName NCHAR(3), age NUMBER(2));
  INSERT INTO testTBL1 VALUES (1, '홍길동', 25);
  
  INSERT INTO testTBL1(id, userName) VALUES (2, '설현');
  
  INSERT INTO testTBL1(userName, age, id) VALUES ('지민', 26,  3);
  
  CREATE TABLE testTBL2 
     (id NUMBER(4), 
      userName NCHAR(3), 
      age NUMBER(2),
      nation NCHAR(4) DEFAULT '대한민국');
  
  
  CREATE SEQUENCE idSEQ
      START WITH 1   -- 시작값
      INCREMENT BY 1 ;  -- 증가값
  
  DROP SEQUENCE idSEQ;
  
  INSERT INTO testTBL2 VALUES (idSEQ.NEXTVAL, '유나' ,25 , DEFAULT);
  INSERT INTO testTBL2 VALUES (idSEQ.NEXTVAL, '혜정' ,24 , '영국');
  SELECT * FROM testTBL2;
  
  INSERT INTO testTBL2 VALUES (11, '쯔위' , 18, '대만'); 
  ALTER SEQUENCE idSEQ  
     INCREMENT BY  10; 
  INSERT INTO testTBL2 VALUES (idSEQ.NEXTVAL, '미나' , 21, '일본'); 
  ALTER SEQUENCE idSEQ  
     INCREMENT BY  1; 
  SELECT * FROM testTBL2;
  
  SELECT idSEQ.CURRVAL FROM DUAL;
  
  SELECT 100*100 FROM DUAL;
  
  CREATE TABLE testTBL3 (id  NUMBER(3));
  CREATE  SEQUENCE cycleSEQ
    START WITH 100
    INCREMENT BY 100
    MINVALUE 100   -- 최소값
    MAXVALUE 300   -- 최대값
    CYCLE           -- 반복설정
    NOCACHE ;       -- 캐시 사용 안함
  INSERT INTO testTBL3 VALUES  (cycleSEQ.NEXTVAL);
  INSERT INTO testTBL3 VALUES  (cycleSEQ.NEXTVAL);
  INSERT INTO testTBL3 VALUES  (cycleSEQ.NEXTVAL);
  INSERT INTO testTBL3 VALUES  (cycleSEQ.NEXTVAL);
  SELECT * FROM testTBL3;
  
  
  CREATE TABLE testTBL4 (empID NUMBER(6), FirstName VARCHAR2(20), 
      LastName VARCHAR2(25), Phone VARCHAR2(20));
  INSERT INTO testTBL4 
    SELECT EMPLOYEE_ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER
      FROM HR.employees ;
  
  CREATE TABLE testTBL5 AS
     (SELECT EMPLOYEE_ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER
         FROM HR.employees) ;
  
  COMMIT;
  ```

- **Postgres**

  ```sql
  CREATE TABLE testTBL1 (id  numeric, userName NCHAR(3), age numeric);
  INSERT INTO testTBL1 VALUES (1, '홍길동', 25);
  
  INSERT INTO testTBL1(id, userName) VALUES (2, '설현');
  
  INSERT INTO testTBL1(userName, age, id) VALUES ('지민', 26,  3);
  
  CREATE TABLE testTBL2 
     (id numeric, 
      userName NCHAR(3), 
      age numeric,
      nation NCHAR(4) DEFAULT '대한민국');
  
  
  CREATE SEQUENCE idSEQ
      START WITH 1   -- 시작값
      INCREMENT BY 1 ;  -- 증가값
  
  DROP SEQUENCE idSEQ;
  
  INSERT INTO testTBL2 VALUES (nextval('idSEQ'), '유나' ,25 , DEFAULT);
  INSERT INTO testTBL2 VALUES (nextval('idSEQ'), '혜정' ,24 , '영국');
  SELECT * FROM testTBL2;
  
  INSERT INTO testTBL2 VALUES (11, '쯔위' , 18, '대만'); 
  
  ALTER SEQUENCE idSEQ  
     INCREMENT BY  10; 
    
  INSERT INTO testTBL2 VALUES (nextval('idSEQ'), '미나' , 21, '일본');
  
  ALTER SEQUENCE idSEQ  
     INCREMENT BY  1; 
    
  SELECT * FROM testTBL2;
  
  SELECT currval('idSEQ');
  
  SELECT 100*100;
  
  CREATE TABLE testTBL3 (id  numeric);
  
  CREATE  SEQUENCE cycleSEQ
    START WITH 100
    INCREMENT BY 100
    MINVALUE 100   -- 최소값
    MAXVALUE 300   -- 최대값
    CYCLE ;          -- 반복설정
    --NOCACHE ;       -- 캐시 문법 없음
    
  INSERT INTO testTBL3 VALUES  (nextval('cycleSEQ'))
  ,(nextval('cycleSEQ'))
  ,(nextval('cycleSEQ'))
  ,(nextval('cycleSEQ'));
  
  SELECT * FROM testTBL3;
  
  
  CREATE TABLE testTBL4 (empID numeric, FirstName VARCHAR(20), 
      LastName VARCHAR(25), Phone VARCHAR(20));
     
  INSERT INTO testTBL4 
    SELECT EMPLOYEE_ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER
      FROM employees ;
  
  CREATE TABLE testTBL5 AS
     (SELECT EMPLOYEE_ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER
         FROM employees) ;
  
  COMMIT;
  ```

  - **before**
    - idSEQ.NEXTVAL
    - FROM DUAL
    - NOCACHE
    - cycleSEQ.NEXTVAL
  - **after**
    - nextval('idSEQ')
    - FROM DUAL (삭제)
    - NOCACHE (삭제)
    - nextval('cycleSEQ')

## 03.UPDATE

- **Oracle, Postgres**

  ```sql
  UPDATE testTBL4
      SET Phone = '없음'
      WHERE FirstName = 'David';
      
  UPDATE buyTBL SET price = price * 1.5 ;
  ```

## 04.DELETE FROM

- **Oracle**

  ```sql
  DELETE FROM testTBL4 WHERE FirstName = 'Peter';
  
  ROLLBACK; -- 앞에서 지운 'Peter'를 되돌림
  
  DELETE FROM testTBL4 WHERE FirstName = 'Peter' AND ROWNUM <= 2;
  ```

  - ROLLBACK을 테스트 하려면 sql developer로 진행

- **Postgres**

  ```sql
  begin;
  
  DELETE FROM testTBL4 WHERE FirstName = 'Peter';
  
  rollback;
  
  DELETE FROM testTBL4 WHERE FirstName = 'Peter' ; -- 상위 두개만 삭제는 더 찾아봐야할 듯
  ```

- **대용량 데이터 생성 삭제**

  - **Oracle, Postgres**

    ```sql
    CREATE TABLE bigTBL1 AS
        SELECT LEVEL AS bigID,
            random()*500000 AS  numData
        FROM GENERATE_SERIES(1,500000) LEVEL;
    
    CREATE TABLE bigTBL2 AS
        SELECT  level AS bigID,
            random()*500000 AS  numData
        FROM GENERATE_SERIES(1,500000) LEVEL;
        
    CREATE TABLE bigTBL3 AS
        SELECT  level AS bigID,
            ROUND(DBMS_RANDOM.VALUE(1, 500000),0) AS  numData
        FROM GENERATE_SERIES(1,500000) LEVEL;
    
    DELETE FROM bigTBL1;
    COMMIT;
    
    DROP TABLE bigTBL2;
    
    TRUNCATE TABLE bigTBL3;
    ```

  - **before**
    - `ROUND(DBMS_RANDOM.VALUE(1, 500000),0) AS  numData`
    - `FROM DUAL CONNECT BY level <= 500000;`

  - **after**
    - `random()*500000 AS  numData`
    - `FROM GENERATE_SERIES(1,500000) LEVEL;`

## 05.MERGE

- **Oracle**

  ```sql
  CREATE TABLE memberTBL AS
     (SELECT userID, userName, addr FROM userTbl ) ;
  SELECT * FROM memberTBL;
  
  CREATE TABLE changeTBL
  ( userID CHAR(8) , 
    userName NVARCHAR2(10), 
    addr NCHAR(2),
    changeType NCHAR(4) -- 변경 사유
    );
  INSERT INTO changeTBL VALUES('TKV', '태권브이', '한국', '신규가입');
  INSERT INTO changeTBL VALUES('LSG', null, '제주', '주소변경');
  INSERT INTO changeTBL VALUES('LJB', null, '영국', '주소변경');
  INSERT INTO changeTBL VALUES('BBK', null, '탈퇴', '회원탈퇴');
  INSERT INTO changeTBL VALUES('SSK', null, '탈퇴', '회원탈퇴');
  
  MERGE INTO memberTBL M  
    USING (SELECT changeType, userID, userName, addr FROM changeTBL)  C  
    ON (M.userID = C.userID)  
    WHEN MATCHED  THEN
        UPDATE SET M.addr = C.addr
        DELETE WHERE C.changeType = '회원탈퇴'
    WHEN NOT MATCHED  THEN
       INSERT (userID, userName,  addr)  VALUES(C.userID, C.userName,  C.addr) ;
  
  SELECT * FROM memberTBL;
  ```

- **Postgres**

  ```sql
  CREATE TABLE memberTBL AS
     (SELECT userID, userName, addr FROM userTbl ) ;
  SELECT * FROM memberTBL;
  
  CREATE TABLE changeTBL
  ( userID CHAR(8) , 
    userName VARCHAR(10), 
    addr NCHAR(2),
    changeType NCHAR(4) -- 변경 사유
  );
  INSERT INTO changeTBL VALUES
  ('TKV', '태권브이', '한국', '신규가입')
  ,('LSG', null, '제주', '주소변경')
  ,('LJB', null, '영국', '주소변경')
  ,('BBK', null, '탈퇴', '회원탈퇴')
  ,('SSK', null, '탈퇴', '회원탈퇴');
  
  WITH upsert AS (
  UPDATE memberTBL TGT 
  SET addr = ORI.addr
  FROM changeTBL ORI
  WHERE TGT.userID = ORI.userID
  returning *
  )
  INSERT INTO memberTBL (userID,userName,addr)
  SELECT 
  userID,
  userName,
  addr
  FROM changeTBL
  WHERE NOT EXISTS (select * from upsert)
  
  
  
  WITH upsert AS (
  DELETE from membertbl WHERE addr = '탈퇴'
  returning *
  )
  INSERT INTO memberTBL (userID,userName,addr)
  SELECT 
  userID,
  userName,
  addr
  FROM changeTBL
  WHERE NOT EXISTS (select * from upsert)
  
  SELECT * FROM memberTBL;
  ```