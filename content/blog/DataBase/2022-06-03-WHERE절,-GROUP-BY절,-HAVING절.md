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

