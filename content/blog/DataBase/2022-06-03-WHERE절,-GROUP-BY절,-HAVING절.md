---
title: WHERE절, GROUP BY절, HAVING절
date: 2022-06-03 10:55:00 +0900
category: DB
draft: false
---

## 2022-06-03-WHERE절,-GROUP-BY절,-HAVING절

## 목차

## 01.WHERE절

- 조회하는 결과에 특정한 조건을 줘서, 원하는 데이터만 보고싶을 때 사용

- 형식

  ```sql
  SELECT 필드이름 FROM 테이블이름 WHERE 조건식;
  ```

- 사용법

  ```sql
  SELECT * FROM userTBL WHERE userName = '김경호';
  ```

### 01.1 관계 연산자 사용

#### AND

- 1970이후 출생, 신장이 182이상인 사람의 아이디와 이름 조회

  ```sql
  SELECT userID, userName, FROm userTBL WHERE birthYear >= 1970 AND height >= 182;
  ```

#### OR

- 1970년 이후에 출생 했거나, 신장이 182이상인 사람의 아이디와 이름을 조회

  ```sql
  SELECT userID, userName, FROm userTBL WHERE birthYear >= 1970 OR height >= 182;
  ```

### 01.2 BETWEEN... AND와 IN() 그리고 LIKE

- 키가 180 ~ 183인 사람을 조회

  ```sql
  SELECT userName, height FROM userTBL WHERE height >=180 AND height <= 183;
  ```

#### BETWEEN AND

```sql
SELECT userName, height FROM userTBL WHERE height BETWEEN 180 AND 183;
```

#### OR OR ... -> IN()

```sql
SELECT userName, addr FROM userTBL WHERE addr = '경남' OR addr = '전남' OR add = '경북';
```

- 위와 동일하게 연속적인 값이 아닌 이산적인 값을 위해 IN()을 사용

  ```sql
  SELECT userName, addr FROM userTBL WHERE addr IN ('경남', '전남', '경북');
  ```

####  LIKE

- 문자열의 내용을 검색하기 

  ```sql
  SELECT userName, height FROM userTBL WHERE userName LIKE '김%';
  ```

  - `%`  뒤에 붙인경우 무엇이든 허용

  - `_` 한글자 매칭할 때 사용

    ```sql
    SELECT userName, height FROM userTBL WHERE userName LIKE '_종신';
    ```

### 01.3 ANY/ ALL/ SOME 그리고 서브 쿼리(SubQuery, 하위 쿼리)

- 서브쿼리는 쿼리문안에 또 쿼리문이 들어 있는 것

  ```sql
  SELECT userName, height FROM userTBL
  	WHERE height > (SELECT height FROM userTBL WHERE userName = '김경호');
  ```

- 둘이상 데이터의 서브쿼리의 Where

  ```sql
  SELECT userName, height FROM userTBL
  	WHERE height >= (SELECT height FROM userTBL WHERE addr = '경남');
  ```

  - 위를 진행하면 서브쿼리 결과가 2개라서 에러를 발생함 이 경우 아래로 해결 가능

#### ANY

```sql
SELECT userName, height FROM userTBL
	WHERE height >=  ANY(SELECT height FROM userTBL WHERE addr = '경남');
```

- 서브쿼리 결과가 170, 173이 나오기 때문에 173보다 크거나 같거나 170보다크거나 같거나하는 결과가 나옴

```sql
SELECT userName, height FROM userTBL
	WHERE height =  ANY(SELECT height FROM userTBL WHERE addr = '경남');
	
SELECT userName, height FROM userTBL
	WHERE height IN (SELECT height FROM userTBL WHERE addr = '경남');
```

- 두개 동일한 결과와 의미

### 01.4 ORDER BY | 원하는 순서대로 정렬

```sql
SELECT userName, mDate FROM userTBL ORDER BY mDate;
```

- mDate 중심으로 정렬 

  - 기본은 오름차순 정렬됨 아니면 명시적으로 `ASC`입력

    ```sql
    SELECT userName, mDate FROM userTBL ORDER BY mDate ASC;
    ```

  - 내림차순 정렬하고 싶다면 `DESC`

    ```sql
    SELECT userName, mDate FROM userTBL ORDER BY mDate DESC;
    ```

- 키가 큰순서로 정렬하되, 만약 키가 같을 경우 이름순 정렬

  ```sql
  SELECT userName, mDate FROM userTBL ORDER BY height DESC, userName ASC;
  ```

- ORDER BY의 경우 위치는

  - SELECT, FROM, WHERE, GROUP BY, HAVING, ORDER BY
    - 위 처럼 제일 뒤에 위치 한다.

## 02.중복 제거 DISTINCT

- 중복을 제거하는 것

```sql
SELECT DISTINCT addr FROM userTBL;
```

- 중복된 결과가 있는 경우 한개만 보여준다.

## 03.ROWNUM열과 SAMPLE문

- 제일 앞의 5건만 보여주는 방법

  ```sql
  SELECT * FROM
  	(SELECT employ_id, hire_date FROM employees ORDER BY hire_date ASC)
  	WHERE ROWNUM <= 5;
  ```

  - 결과를 5건만 보여주는 것 일뿐
    - 위의 경우만 ORDER BY를 해서 성능저하 야기
      - Oracle의 성능에는 상당히 나쁜 영향을 미침
      - 모든 데이터를 정렬한 후에 5개만 가져오는 방식

- 임의 데이터를 추출하고 싶은 경우

  - **SAMPLE(퍼센트)**
    - 퍼센트는 0초과 100미만 값

  ```sql
  SELECT employee_id, hire_date FROM EMPLOYEES SAMPLE(5);
  ```

  - 5건 일수 있고 아닐수 있음

## 04.테이블을 복사하는 CREATE TABLE ... AS SELECT

- 형식

  ```sql
  CREATE TABLE 새로운테이블이름 AS (SELECT 복사할 열 FROM 기존 테이블)
  ```

- 사용법

  ````sql
  CREATE TABLE buyTBL2 AS (SELECT * FROM buyTBL);
  
  SELECT * FROM buyTBL2; -- 조회로 실제 결과 확인
  ````

  - 일부열만 복사 할 수 있음

    ```sql
    CREATE TABLE buyTBL3 AS (SELECT userID, prodName FROM buyTBL);
    ```

  - Primary Key 및 Foreign Key는 복사되지 않음

    - 즉, PK나 FK등의 제약 조건은 복사되지 않음

## 05.GROUP BY 및 HABVING 그리고 집계 함수

### 05.1 GROUP BY 

- 말그대로 그룹으로 묶어주는 역할

  ```sql
  SELECT userID, SUM(amount) FROM buyTBL GROUP BY userID;
  ```

  - 이전의 BBK 12의 데이터와 18 의 데이터를 가지고 있었다면 위를 적용하면 30으로 한번에 보여지는 것

  ```sql
  SELECT userID AS "사용자 아이디", SUM(price * amount) AS "총 구매액" FROM buyTBL GROUP BY userID;
  ```

  - 위와 같이 가격 * 수량의 총합을 구할 수 있음

#### 05.1.1 집계 함수

- GROUP BY와 함께 자주 사용되는 집계 함수

| 함수명          |                설명                |
| :-------------- | :--------------------------------: |
| AVG()           |                평균                |
| MIN()           |               최소값               |
| MAX()           |               최대값               |
| COUNT()         |             행의 개수              |
| COUNT(DISTINCT) | 행의 개수를 세고 중복은 1개만 인정 |
| STDEV()         |              표준편차              |
| VARIANCE()      |                분산                |

- 전체 구매자가 구매한 물품의 개수의 평균

  ```sql
  SELECT AVG(amount)AS "평균 구매 개수" FROM buyTBL;
  ```

- 소수점 조절 하는 법

  ```sql
  SELECT CAST(AVG(amount) AS NUMBER(5,3)) AS "평균 구매 개수" FROM buyTBL;
  ```

- 사용자 별로 한 번 구매시 물건의 평균

  ```sql
  SELECT CAST(AVG(amount) AS NUMBER(5,3)) AS "평균 구매 개수" FROM buyTBL GROUP BY userID;
  ```

- 가장 큰키와 가장 작은 키의 회원이름과 키 출력

  ```sql
  SELECT userName, MAX(height), MIN(height) FROM userTBL GROUP BY userName;
  ```

  - 위와 같이 하면 전체가 다 나오게됨

  ```sql
  SELECT userName, height
  	FROM userTBL
  	WHERE height = (SELECT MAX(height)FROM user TBL) OR height = (SELECT MIN(height)FROM userTBL);
  ```

### 05.2 Having절

- 사용자별 총 구매액에서 1000명인 사용자 조회

  ```sql
  SELECT userID AS "사용자", SUM(price*amount) AS "총 구매액"
  	FROM buyTBL
  	WHERE SUM(price * amount) >1000
  	GROUP BY userID;
  ```

  - 위와 같이 하면 에러가 발생함 
  - 집계함수의 경우 WHERE절에 나타날 수 없음

