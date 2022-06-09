---
title: oracle과 postgres 10일차비교
date: 2022-06-09 16:51:00 +0900
category: DB
draft: false
---

## 2022-06-09-oracle과-postgres-10일차비교

## 목차

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
      hireDate TIMESTAMP(0) ; -- 입사일
      curDate TIMESTAMP(0) ; -- 오늘
      wDays   INT ; -- 근무한 일수
  BEGIN
      -- SQLINES LICENSE FOR EVALUATION USE ONLY
      SELECT hire_date INTO hireDate -- SQLINES DEMO *** 결과를 hireDATE에 대입
         FROM employees
         WHERE employee_id = 200;
      curDate := CURRENT_DATE; -- 현재 날짜
      wDays :=  curDate - hireDate; -- SQLINES DEMO *** 일 단위)
      IF (wDays/365) >= 5 THEN -- SQLINES DEMO *** 면
          RAISE NOTICE '%','입사한지 ' || wdays || 
                      '일이나 지났습니다. 축하합니다!';
      ELSE
          RAISE NOTICE '%','입사한지 ' || wdays || 
                      '일밖에 안되었네요. 열심히 일하세요.';
      END IF;
  END $$;
  ```



