---
title: Grafana Timedate확인
date: 2022-07-06 13:39:00 +0900
category: PoC
draft: false
---

## 2022-07-06-Grafana-Timedate확인

## 목차

> 01.timeFilter, timeFrom, timeTo 확인
>
> > 01.1 timeFilter
> >
> > 01.2 timeFrom
> >
> > 01.3 timeTo
>
> 02.데이터 확인 하기

## 01.timeFilter, timeFrom, timeTo 확인

### 01.1 timeFilter

- timepicker에서 선택한 기간 적용

### 01.2 timeFrom

- timepicker에서 시작 시점

### 01.3 timeTo

- timepicker에서 마지막 시점

## 02.데이터 확인 하기

- 사용한 쿼리 조건
  - `and (($__timeFrom()<=s.start_date or s.start_date <=$__timeTo()) and ($__timeFrom()<=s.end_date or s.end_date <=$__timeTo())) and ($__timeFilter(s.start_date) or $__timeFilter(s.end_date))`

### 02.1 범위안에 있는 경우

- `2022-02-28 T00:00:00` ~ `2022-03-05 T23:59:00`로 지정한 경우

  ![image-20220706132632158](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132632158.png)

  ![image-20220706132623688](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132623688.png)

- `2022-03-01 T00:00:00` ~ `2022-03-04 T23:59:00로 지정한 경우`

  ![image-20220706132650196](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132650196.png) 

  ![image-20220706132723175](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132723175.png)

### 02.2 timeTo()가 걸리는 경우

- `2022-03-06 T00:00:00` ~ `2022-03-10 T23:59:00로 지정한 경우`

  ![image-20220706132734268](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132734268.png)

  ![image-20220706132748581](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132748581.png)

- `2022-03-09 T00:00:00` ~ `2022-03-13 T23:59:00`로 지정한 경우

  ![image-20220706132758650](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132758650.png)

  ![image-20220706132814070](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132814070.png)

###  02.3 시간범위에 안걸리는 경우

- `2022-03- T00:00:00` ~ `2022-03-14 T23:59:00로 지정한 경우`

![image-20220706132827582](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132827582.png)

![image-20220706132840061](C:\Users\km.park\AppData\Roaming\Typora\typora-user-images\image-20220706132840061.png)