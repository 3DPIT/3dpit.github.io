---
title: 2022-04-11-SQL-Join정리
date: 2022-04-11 20:28:00 +0900
category: DB
draft: false
---

## 22-04-11-SQL-Join정리

## 목차

> 01.Join이란?
>
> > 01.1 Join 종류
> >
> > > (inner)Join
> > >
> > > Left(outer)Join
> > >
> > > Right(outer)Join
> > >
> > > Full(outer)Join
>
> 02.PostgreSQL Joins

## 01.Join이란?

- 두 개이상 테이블에서 테이블 사이 연관된 colum을 바탕으로 rows를 결합하는 것

### 01.1 Join 종류

#### (Inner) Join

- 두 테이블에 모두 매치되는 값을 가진  rows를 리턴
- ![제목_없는_아트워크](../../assets/img/post/22-04-11-SQL-Join정리.assets/1.png)

#### Left(outer) Join

- 왼쪽 테이블의 모든 rows를 리턴하고, 오른쪽 테이블에서는 왼쪽 테이블에 매치되는 rows를 리턴함

  ![제목_없는_아트워크 2](../../assets/img/post/22-04-11-SQL-Join정리.assets/2.png)

#### Right(outer)Join

- 오른쪽 테이블의 모든 rows를 리턴하고, 왼쪽 테이블에서는 오른쪽 테이블에 매치되는 rows를 리턴함

  ![제목_없는_아트워크 3](../../assets/img/post/22-04-11-SQL-Join정리.assets/3.png)

#### Full(outer)Join

- 왼쪽 테이블 혹은 오른쪽 테이블에서 매치되는 모든 rows를 리턴함

  ![제목_없는_아트워크 4](../../assets/img/post/22-04-11-SQL-Join정리.assets/4.png)

## 02.PostgreSQL Joins

![image-20220411205758793](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205758793.png)

```sql
SELECT * FROM a INNER JOIN b ON a.key = b. key
```

![image-20220411205818701](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205818701.png)

```sql
SELECT * FROM a LEFT JOIN b ON a.key = b. key
```

![image-20220411205837593](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205837593.png)

```sql
SELECT * FROM a RIGHT JOIN b ON a.key = b. key
```

![image-20220411205852700](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205852700.png)

```sql
SELECT * FROM a INNER JOIN b ON a.key = b. key
WHERE b.key IS NULL
```

![image-20220411205907659](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205907659.png)

```sql
SELECT * FROM a INNER JOIN b ON a.key = b. key
WHERE a.key IS NULL
```

![image-20220411205919813](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205919813.png)

```sql
SELECT * FROM a FULL JOIN b ON a.key = b. key
```

![image-20220411205938795](../../assets/img/post/22-04-11-SQL-Join정리.assets/image-20220411205938795.png)

```sql
SELECT * FROM a FULL JOIN b ON a.key = b. key
WHERE a.key IS NULL OR b.key IS NULL
```



