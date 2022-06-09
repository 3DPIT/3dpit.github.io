---
title: oracle과 postgres 10일차비교
date: 2022-06-09 16:51:00 +0900
category: DB
draft: false
---

## 2022-06-09-oracle과-postgres-10일차비교

## 목차

>  01.PL/SQL 프로그래밍
>
>  >  01.1 IF ~ ELSE
>  >
>  >  01.2 CASE
>  >
>  >  > 01.2.1 활용
>  >
>  >  01.3 WHILE LOOP, FOR LOOP와 CONTINUE, EXIT
>  >
>  >  01.4 GOTO, DBMS_LOCK.SLEEP()
>  >
>  >  01.5 예외처리
>  >
>  >  01.6 사용자 정의 예의 처리
>  >
>  >  01.7 동적 SQL
>

## 01.PL/SQL 프로그래밍

### 01.1 IF ~ ELSE

- **Oracle**

  ```sql
  DECLARE
      var1 NUMBER(5) ; -- 변수 선언
  BEGIN
      var1 := 100; -- 변수에 값 대입
      IF  var1 = 100 THEN  -- 만약 var1이 100이라면
          DBMS_OUTPUT.PUT_LINE('100입니다');
      ELSE
          DBMS_OUTPUT.PUT_LINE('100이 아닙니다');
      END IF;
  END ;

  DECLARE
      hireDate DATE ; -- 입사일
      curDate DATE ; -- 오늘
      wDays   NUMBER(5) ; -- 근무한 일수
  BEGIN
      SELECT hire_date INTO hireDate -- hire_date 열의 결과를 hireDATE에 대입
         FROM HR.employees
         WHERE employee_id = 200;
      curDate := CURRENT_DATE(); -- 현재 날짜
      wDays :=  curDate - hireDate; -- 날짜의 차이(일 단위)
      IF (wDays/365) >= 5 THEN -- 5년이 지났다면
          DBMS_OUTPUT.PUT_LINE('입사한지 ' || wdays || 
                      '일이나 지났습니다. 축하합니다!');
      ELSE
          DBMS_OUTPUT.PUT_LINE('입사한지 ' || wdays || 
                      '일밖에 안되었네요. 열심히 일하세요.');
      END IF;
  END ;
  ```

- **Postgres**

  ```sql
  do $$
  DECLARE
      var1 INT ; -- 변수 선언
  BEGIN
      var1 := 100; -- SQLINES DEMO *** 입
      IF  var1 = 100 THEN  -- SQLINES DEMO *** 0이라면
          RAISE NOTICE '%','100입니다';
      ELSE
          RAISE NOTICE '%','100이 아닙니다';
      END IF;
  END $$;
  
  do $$
  DECLARE
  hireDate date ; -- 입사일
  curDate date ; -- 오늘
  wDays integer ; -- 근무한 일수
  BEGIN
  SELECT hire_date INTO hireDate -- SQLINES DEMO *** 결과를 hireDATE에 대입
     FROM employees
     WHERE employee_id = 200;
  curDate := CURRENT_DATE; -- 현재 날짜
  wDays := curDate - hireDate ; -- SQLINES DEMO *** 일 단위)
  IF (wDays/365) >= 5 THEN -- SQLINES DEMO *** 면
      RAISE NOTICE '%','입사한지 ' || wdays ||
                  '일이나 지났습니다. 축하합니다!';
  ELSE
      RAISE NOTICE '%','입사한지 ' || wdays ||
                  '일밖에 안되었네요. 열심히 일하세요.';
      END IF;
  END $$;
  ```

### 01.2 CASE

- **Oracle**

  ```sql
  DECLARE
      pNumber NUMBER(3) ; -- 점수
      credit CHAR(1) ; -- 학점
  BEGIN
      pNumber := 77;
      IF pNumber >= 90 THEN
  		credit := 'A';
      ELSIF pNumber >= 80 THEN
  		credit := 'B';
      ELSIF pNumber >= 70 THEN
  		credit := 'C';
      ELSIF pNumber >= 60 THEN
  		credit := 'D';
      ELSE
  		credit := 'F';
      END IF;
      DBMS_OUTPUT.PUT_LINE('취득점수==>' || pNumber || ', 학점==>' || credit);
  END ;
  
  DECLARE
      pNumber NUMBER(3) ; -- 점수
      credit CHAR(1) ; -- 학점
  BEGIN
      pNumber := 77;
      CASE 
  		WHEN pNumber >= 90 THEN
  			credit := 'A';
  		WHEN pNumber >= 80 THEN
  			credit := 'B';
  		WHEN pNumber >= 70 THEN
  			credit := 'C';
  		WHEN pNumber >= 60 THEN
  			credit := 'D';
  		ELSE
  			pNumber := 'F';
      END CASE;
      DBMS_OUTPUT.PUT_LINE('취득점수==>' || pNumber || ', 학점==>' || credit);
  END ;
  ```

- **Postgres**

  ```sql
  do $$
  DECLARE
      pNumber SMALLINT ; -- 점수
      credit CHAR(1) ; -- 학점
  BEGIN
      pNumber := 77;
      IF pNumber >= 90 THEN
  		credit := 'A';
      ELSIF pNumber >= 80 THEN
  		credit := 'B';
      ELSIF pNumber >= 70 THEN
  		credit := 'C';
      ELSIF pNumber >= 60 THEN
  		credit := 'D';
      ELSE
  		credit := 'F';
      END IF;
      RAISE NOTICE '%','취득점수==>' || pNumber || ', 학점==>' || credit;
  END $$;
  
  
  do $$
  DECLARE
      pNumber SMALLINT ; -- 점수
      credit CHAR(1) ; -- 학점
  BEGIN
      pNumber := 77;
      CASE 
  		WHEN pNumber >= 90 THEN
  			credit := 'A';
  		WHEN pNumber >= 80 THEN
  			credit := 'B';
  		WHEN pNumber >= 70 THEN
  			credit := 'C';
  		WHEN pNumber >= 60 THEN
  			credit := 'D';
  		ELSE
  			pNumber := 'F';
      END CASE;
      RAISE NOTICE '%','취득점수==>' || pNumber || ', 학점==>' || credit;
  END $$;
  ```

#### 01.2.1 활용

- **Oracle**

  ```sql
  SELECT userID, SUM(price*amount) AS "총구매액"
     FROM buyTbl
     GROUP BY userID
     ORDER BY SUM(price*amount) DESC;
  
  SELECT B.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        INNER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY B.userID, U.userName
     ORDER BY SUM(price*amount) DESC;
  
  SELECT B.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY B.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  
  SELECT U.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY U.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  
  SELECT U.userID, U.userName, SUM(price*amount) AS "총구매액",
         CASE  
              WHEN (SUM(price*amount)  >= 1500) THEN  '최우수고객'
              WHEN (SUM(price*amount)  >= 1000) THEN  '우수고객'
              WHEN (SUM(price*amount) >= 1 ) THEN '일반고객'
              ELSE '유령고객'
         END AS "고객등급"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY U.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  
  ```

- **Postgres**

  ```sql
  SELECT userID, SUM(price*amount) AS "총구매액"
     FROM buyTbl
     GROUP BY userID
     ORDER BY SUM(price*amount) DESC;
  
  SELECT B.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        INNER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY B.userID, U.userName
     ORDER BY SUM(price*amount) DESC;
  
  SELECT B.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY B.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  
  SELECT U.userID, U.userName, SUM(price*amount) AS "총구매액"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY U.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  
  SELECT U.userID, U.userName, SUM(price*amount) AS "총구매액",
         CASE  
              WHEN (SUM(price*amount)  >= 1500) THEN  '최우수고객'
              WHEN (SUM(price*amount)  >= 1000) THEN  '우수고객'
              WHEN (SUM(price*amount) >= 1 ) THEN '일반고객'
              ELSE '유령고객'
         END AS "고객등급"
     FROM buyTbl B
        RIGHT OUTER JOIN userTbl U
           ON B.userID = U.userID
     GROUP BY U.userID, U.userName
     ORDER BY SUM(price*amount) DESC NULLS LAST;
  ```

### 01.3 WHILE LOOP, FOR LOOP와 CONTINUE, EXIT

- **Oracle**

  ```sql
  SET SERVEROUTPUT ON; 
  DECLARE
      iNum NUMBER(3) ; -- 1에서 100까지 증가할 변수
      hap NUMBER(5) ; -- 더한 값을 누적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          hap := hap + iNum; -- hap에 iNum를 누적시킴
          iNum := iNum + 1; -- iNum을 1 증가시킴
      END LOOP;
      DBMS_OUTPUT.PUT_LINE(hap);
  END ;
  
  DECLARE
      iNum NUMBER(3) ; -- 1에서 100까지 증가할 변수
      hap NUMBER(5) ; -- 더한 값을 누적할 변수
  BEGIN
      hap := 0;
      FOR iNum IN 1 .. 100 
      LOOP
          hap := hap + iNum; -- hap에 iNum를 누적시킴
      END LOOP;
      DBMS_OUTPUT.PUT_LINE(hap);
  END ;
  
  DECLARE
      iNum NUMBER(3) ; -- 1에서 100까지 증가할 변수
      hap NUMBER(5) ; -- 더한 값을 누적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          IF MOD(iNum, 7) = 0 THEN
              iNum := iNum + 1;
              CONTINUE;
          END IF;
          hap := hap + iNum; -- hap에 iNum를 누적시킴
          IF hap > 1000 THEN
              EXIT;
          END IF;
          iNum := iNum + 1; -- iNum을 1 증가시킴
      END LOOP;
      DBMS_OUTPUT.PUT_LINE(hap);
  END ;
  ```

- **Postgres**

  ```sql
  do $$
  DECLARE
      iNum SMALLINT ; -- SQLINES DEMO ***  증가할 변수
      hap INT ; -- SQLINES DEMO *** 적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          hap := hap + iNum; -- SQLINES DEMO *** �적시킴
          iNum := iNum + 1; -- SQLINES DEMO *** ��킴
      END LOOP;
      RAISE NOTICE '%',hap;
  END $$;
  
  do $$
  DECLARE
      iNum SMALLINT ; -- SQLINES DEMO ***  증가할 변수
      hap INT ; -- SQLINES DEMO *** 적할 변수
  BEGIN
      hap := 0;
      FOR iNum IN 1 .. 100 
      LOOP
          hap := hap + iNum; -- SQLINES DEMO *** �적시킴
      END LOOP;
      RAISE NOTICE '%',hap;
  END $$;
  
  do $$
  DECLARE
      iNum SMALLINT ; -- SQLINES DEMO ***  증가할 변수
      hap INT ; -- SQLINES DEMO *** 적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          IF MOD(iNum, 7) = 0 THEN
              iNum := iNum + 1;
              CONTINUE;
          END IF;
          hap := hap + iNum; -- SQLINES DEMO *** �적시킴
          IF hap > 1000 THEN
              EXIT;
          END IF;
          iNum := iNum + 1; -- SQLINES DEMO *** ��킴
      END LOOP;
      RAISE NOTICE '%',hap;
  END $$;
  ```

### 01.4 GOTO, DBMS_LOCK.SLEEP()

- **Oracle**

  ```sql
  DECLARE
      iNum NUMBER(3) ; -- 1에서 100까지 증가할 변수
      hap NUMBER(5) ; -- 더한 값을 누적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          IF MOD(iNum, 7) = 0 THEN
              iNum := iNum + 1;
              CONTINUE;
          END IF;
          hap := hap + iNum; -- hap에 iNum를 누적시킴
          IF hap > 1000 THEN
              GOTO  my_goto_location;
          END IF;
          iNum := iNum + 1; -- iNum을 1 증가시킴
      END LOOP;
      << my_goto_location >>
      DBMS_OUTPUT.PUT_LINE(hap);
  END ;
  
  BEGIN
      DBMS_LOCK.SLEEP(5); 
      DBMS_OUTPUT.PUT_LINE('5초간 멈춘후 진행되었음');
  END ;
  ```

- **Postgres** | goto 안됨

  ```sql
  do $$
  DECLARE
      iNum SMALLINT ; -- SQLINES DEMO ***  증가할 변수
      hap INT ; -- SQLINES DEMO *** 적할 변수
  BEGIN
      iNum := 1;
      hap := 0;
      WHILE iNum <= 100 
      LOOP
          IF MOD(iNum, 7) = 0 THEN
              iNum := iNum + 1;
              CONTINUE;
          END IF;
          hap := hap + iNum; -- SQLINES DEMO *** �적시킴
          IF hap > 1000 THEN
              GOTO  my_goto_location;
          END IF;
          iNum := iNum + 1; -- SQLINES DEMO *** ��킴
      END LOOP;
      << my_goto_location >>
      RAISE NOTICE '%',hap;
  END ;
  
  BEGIN
      DBMS_LOCK.SLEEP(5); 
      RAISE NOTICE '%','5초간 멈춘후 진행되었음';
  END $$;
  ```

### 01.5 예외처리

- **Oracle**

  ```sql
  DECLARE
      -- 테이블 열의 데이터 타입과 동일하게 변수 타입을 설정
      v_userName userTBL.userName%TYPE; 
  BEGIN
      SELECT userName INTO v_userName FROM userTBL 
              WHERE userName LIKE ('김%'); -- 김범수, 김경호 2명
      DBMS_OUTPUT.PUT_LINE ('김씨 고객 이름은 ' ||v_userName|| '입니다.') ;
      EXCEPTION
          WHEN NO_DATA_FOUND THEN
              DBMS_OUTPUT.PUT_LINE ('김씨 고객이 없습니다.') ;
          WHEN TOO_MANY_ROWS THEN
              DBMS_OUTPUT.PUT_LINE ('김씨 고객이 너무 많네요.') ;            
  END ;
  ```

- **Postgres** | 안됨

  ```sql
  do $$
  DECLARE
      v_userName userTBL.userName%TYPE; 
  BEGIN
      -- SQLINES LICENSE FOR EVALUATION USE ONLY
      SELECT userName INTO v_userName FROM userTBL 
              WHERE userName LIKE ('김%'); -- SQLINES DEMO *** 호 2명
      RAISE NOTICE '%', '김씨 고객 이름은 ' ||v_userName|| '입니다.' ;
      EXCEPTION
          WHEN NO_DATA_FOUND THEN
              RAISE NOTICE '%', '김씨 고객이 없습니다.' ;
          WHEN userException THEN
              RAISE NOTICE '%', '김씨 고객이 너무 많네요.' ;            
  END $$;
  
  ```

### 01.6 사용자 정의 예의 처리

- **Oracle**

  ```sql
  DECLARE
      v_userName userTBL.userName%TYPE; 
      userException EXCEPTION;
      PRAGMA EXCEPTION_INIT(userException, -1422);
  BEGIN
      SELECT userName INTO v_userName FROM userTBL 
              WHERE userName LIKE ('김%'); -- 김범수, 김경호 2명
      DBMS_OUTPUT.PUT_LINE ('김씨 고객 이름은 ' ||v_userName|| '입니다.') ;
      EXCEPTION
          WHEN NO_DATA_FOUND THEN
              DBMS_OUTPUT.PUT_LINE ('김씨 고객이 없습니다.') ;
          WHEN userException THEN
              DBMS_OUTPUT.PUT_LINE ('김씨 고객이 너무 많네요.') ;            
  END ;
  
  ---
  
  DECLARE
      v_userName userTBL.userName%TYPE; 
      zeroDelete EXCEPTION;
  BEGIN
      v_userName := '무명씨';
      DELETE FROM userTBL WHERE userName=v_userName; 
      IF SQL%NOTFOUND THEN
          RAISE zeroDelete;
      END IF;
      EXCEPTION
          WHEN zeroDelete THEN
              DBMS_OUTPUT.PUT_LINE (v_userName || ' 데이터 없음. 확인 바래요^^') ;            
  END ;
  
  ---
  
  DECLARE
      v_userName userTBL.userName%TYPE; 
  BEGIN
      v_userName := '무명씨';
      DELETE FROM userTBL WHERE userName=v_userName; 
      IF  SQL%NOTFOUND  THEN
          RAISE_APPLICATION_ERROR(-20001, '데이터 없음 오류 발생!!');
      END IF;
  END ;
  ```

- **Postgres** | 안됨

  ```sql
  do $$
  DECLARE
      v_userName userTBL.userName%TYPE; 
  BEGIN
      -- SQLINES LICENSE FOR EVALUATION USE ONLY
      SELECT userName INTO v_userName FROM userTBL 
              WHERE userName LIKE ('김%'); -- SQLINES DEMO *** 호 2명
      RAISE NOTICE '%', '김씨 고객 이름은 ' ||v_userName|| '입니다.' ;
      EXCEPTION
          WHEN NO_DATA_FOUND THEN
              RAISE NOTICE '%', '김씨 고객이 없습니다.' ;
          WHEN userException THEN
              RAISE NOTICE '%', '김씨 고객이 너무 많네요.' ;            
  END $$;
  
  ---
  
  do $$
  DECLARE
      v_userName userTBL.userName%TYPE; 
  BEGIN
      v_userName := '무명씨';
      DELETE FROM userTBL WHERE userName=v_userName; 
      IF NOT FOUND THEN
          RAISE zeroDelete;
      END IF;
      EXCEPTION
          WHEN zeroDelete THEN
              RAISE NOTICE '%', v_userName || ' 데이터 없음. 확인 바래요^^' ;            
  END $$;
  
  ---
  
  do $$
  DECLARE
      v_userName userTBL.userName%TYPE; 
  BEGIN
      v_userName := '무명씨';
      DELETE FROM userTBL WHERE userName=v_userName; 
      IF  NOT FOUND  THEN
          RAISE_APPLICATION_ERROR(-20001, '데이터 없음 오류 발생!!');
      END IF;
  END $$;
  
  ```

### 01.7 동적 SQL

- **Oracle**

  ```sql
  DECLARE
      v_sql VARCHAR2(100); -- SQL 문장을 저장할 변수
      v_height userTBL.height%TYPE;  -- 반환될 키를 저장할 변수
  BEGIN
      v_sql := 'SELECT height FROM userTBL WHERE userid = ''EJW'' ' ; 
      EXECUTE IMMEDIATE v_sql INTO v_height;
      DBMS_OUTPUT.PUT_LINE (v_height) ;
  END ;
  
  --- 안됨
  
  DECLARE
      v_year CHAR(4);
      v_month CHAR(2);
      v_day  CHAR(2);
      v_sql VARCHAR2(100);
      v_height userTBL.height%TYPE;
  BEGIN
       v_year := EXTRACT(YEAR FROM SYSDATE);
       v_month := EXTRACT(MONTH FROM SYSDATE);
       v_day := EXTRACT(DAY FROM SYSDATE);
       v_sql := 'CREATE TABLE myTBL' || v_year || '_' || v_month || '_' || 
              v_day || ' (idNum  NUMBER(5), userName NVARCHAR2(10))';
      EXECUTE IMMEDIATE v_sql;
      DBMS_OUTPUT.PUT_LINE ('테이블 생성됨');
  END;
  ```

- **Postgres**

  ```sql
  do $$
  DECLARE
      v_sql VARCHAR(100); -- SQLINES DEMO *** 장할 변수
      v_height userTBL.height%TYPE;  -- SQLINES DEMO *** 저장할 변수
  BEGIN
      v_sql := 'SELECT height FROM userTBL WHERE userid = ''EJW'' ' ; 
      EXECUTE v_sql INTO v_height;
      RAISE NOTICE '%', v_height ;
  END $$;
  
  ---
  
  do $$
  DECLARE
      v_year CHAR(4);
      v_month CHAR(2);
      v_day  CHAR(2);
      v_sql VARCHAR(100);
      v_height userTBL.height%TYPE;
  BEGIN
       v_year := EXTRACT(YEAR FROM CURRENT_TIMESTAMP);
       v_month := EXTRACT(MONTH FROM CURRENT_TIMESTAMP);
       v_day := EXTRACT(DAY FROM CURRENT_TIMESTAMP);
       v_sql := 'CREATE TABLE myTBL' || v_year || '_' || v_month || '_' || 
              v_day || ' (idNum  numeric, userName VARCHAR(10))';
      EXECUTE v_sql;
      RAISE NOTICE '%', '테이블 생성됨';
  END $$;
  ```

  
