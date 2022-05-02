---
title: '22-04-12-PostgresSQL-쿼리연습'
date: 2022-04-12 09:18:00 +0900
category: 'DB'
draft: false
---

## 22-04-12-PostgreSQL-쿼리연습

## 목차

> 01.Creat table
>
> 02.타입
>
> > 02.1 Json Quert Example
>
> 03.Shell
>
> 04.DDL
>
> 05.Insert
>
> 06.SELECT - Alias
>
> > 06.1 Extract UTC Time
> >
> > 06.2 JSON
> >
> > 06.3 Post List 추출1
> >
> > 06.4 Post List 추출2
> >
> > 06.5 Post List 부분의 문제
>
> 07.Update
>
> 08.DELETE
>
> 09.쿼리 최적화

## 01.Creat table

```sql
CREATE TABLE post
(
    id 				SERIAL			PRIMARY KEY,
    type			VARCHAR(16) 	NOT NULL default 'default',
    user_id 		INTEGER 		NOT NULL references user(id),
    title			VARCHAR(256)	NOT NULL,
    description		TEXT			NOT NULL,
    child_count 	INTEGER			NOT NULLL default 0,
    state			VARCHAR(16)		NOT NULL DEFAULT 'open',
    mention_users 	INTEGER[],
    ip_address		CHAR(15),
    info			JSON,
    parent			references post(id)
    create_time 	TIMESTAMP without time zone NOT NULL defualt(now() at time zone 'utc')
);
```

- 검색 속도를 위해서는 INTEGER로 하고 해당 테이블을 연관 테이블로 매칭하는 것도 방법
  - 사람이 해당 내역을 알아보려면 계속 쿼리를 해야하고 해당 쿼리에 대한 이득은 크지 않음

- child_count 
  - 위의 경우 사실상 쿼리를 던져서 추출이 가능
    - 통계데이터 경우 속도가 느리고 몇개의 데이터가 있는지 항상 보내줘야하는 데이터의 경우 속도를 위해서 데이터를 저장해서 싱크를 맞춰준다.

- 국제화를 하려면 신경 써야하는부분
  - 언어, 환율, 시간

## 02.타입

- SERIAL :
  - Integer, Auto increase
- References: 
  - foreign key
- Integer[]: 
  - Array - cannot set foreign key
- Char:
  - Static length, more fast varchar
    - 길이가 고정된 데이터에 사용
- Json:
  - json format, cannot set index
    - 쿼리로 접근 가능, 인덱스 걸 수 없어서 느림
    - 분석이 필요없고 클라이언트만 사용하는 데이터 저장하는 것이라면 좋음
- UTC:
  - set without time zone

### 02.1 Json Quert Example

```sql
UPDATE user
SET profile_image =
'{"lg"/assets/images/guest_lg.png","md":"assets/images/guest_md.png","sm":"/assets/images/guest_sm.png"}'
WHERE id =1;
```

## 03.Shell

- 주로 개발에서 사용
- 긴급상황 발생 시 실행 할 수 있음
- **명령**
  - psql -U <user name> -h <ip address> --dbname=<db name>
  - 터미널에서 위와 같이 사용하면 DB에 접속할 수 있음

- Shell에서 직접 쿼리를 적을 수 있지만
  - \i명령으로 작성된 쿼리 파일을 읽어서 실행 가능

## 04.DDL

- SET, Drop not null

  - not null 정책이 변경된 경우

    ```sql
    ALTER TABLE t_user ALTER COLUMN first_name SET not null;
    ALTER TABLE t_user ALTER COLUMN last_name DROP not null;
    ```

- Change column type

  - 타입형 변경, 문자열 길이 변경시

    ```sql
    ALTER TABLE t_post ALTER COLUMN title TYPE varchar(256);
    ```

- Change column name

  - 이름 변경시

    ```sql
    ALTER TABLE t_user_detail RENAME birth_date TO birth_day;
    ```

- Setting default value

  - 디폴트 값 설정시

    ```sql
    ALTER TABLE t_signup ALTER COLUMN expire_date SET DEFAULT now() at time zone 'utc' + time '00:10:03';
    ```

- Create index & Drop index

  - 인덱스 생성 삭제

    ```sql
    CREATE INDEX idx_post_poll ON t_post_poll(post_id, poll);
    
    DROP INDEX idx_post_poll;
    ```


## 05.Insert

- 다른 테이블에서 여러 개수의 데이터 입력

  ```sql
  INSERT INTO user(email, nickname)
  	SELECT email, nickname
  	FROM signup
  	WHERE id in(2,5,10,11,12)
  ```

- 데이터가 없다면 입력, 있으면 업데이트 하기

  ```sql
  INSERT INTO user_medal_season(user_id, medal_point) VALUES(4,1)
  	ON CONFLICT (user_id) DO UPDATE
  	 SET medal_point = user_medal_season_point + 1;
  ```

- INSERT와 WITH구문 같이 쓰기

  ```sql
  WITH
  	daily_count as (SELECT count(*) FROM user WHERE (now() at time zone 'utc' - interval '24 hours') < last_login_time),
  	weekly_count as (SELECT count(*) FROM user WHERE (now() at time zone 'utc' - interval '7 days') < last_login_time),
  	total_count as (SELECT count(*) FORM user)
  INSERT INTO retention_rate(login_user, login_user_week, total_user, rate) VALUES
  (SELECT count FROM daily_count),
  (SELECT count FROM weekly_count),
  (SELECT count FROM total_count),
  (SELECT count FROm daily_count) / (SELECT count::float FORM total_count) * 100
  ```

## 06.SELECT - Alias

```sql
SELECT tb_id as "boardId",
	tc.id as "community Id"
	...
FROM t_board tb,
	 t_community tc
WHERE tc.id = tb.community_id
	AND tc.name = '3dpit programming'
	AND tb.name = 'qna board';
```

- 그냥 as boardID로 하면 그냥 소문자로 되고
  - as "boardID" 이런식으로 쌍따옴표로 감싸면 대소문자를 구분함
  - 대게 소문자와 언더바를 이용하는 낙타표현식을 쓰는데 이유는 json으로 바로 만들어서 사용하기 위함

### 06.1 Extract UTC Time

- UTC로 저장된 시간값을 숫자형태로 추출할 때

  ```sql
  SELECT email,
  	   extract(epoch from expire_time at time zone 'utc')::Integer as "expireTime"
  FROM signup
  WHERE id = 'clkmnaslnfaefn-39nalfn1209n=ldfoi3n';
  ```

  - extract로 하면 string으로 나오기 때문에 ::Integer를 해줘서 형변환을 해줌

### 06.2 JSON

```sql
SELECT tp.title
       ,tp.relative_files
       ,tf.info->'imageNames'->>'md' as image
FROM post tp
	 LEFT JOIN file_info tf ON tf.id = tp.relative_files[1]
WHERE tp.id = 198
```

### 06.3 Post List 추출1

```sql
SELECT tp.id
	   ,tu.id as "userId"
	   ,tu.username
	   ,tc.name as "communityName"
	   ,tp.view_count as "viewCount"
	   ,tp.vote_count as "voteCount"
	   ,tp.comment_count as "commentCount"
	   ,tp.point
```

### 06.4 Post List 추출2

```sql
,(
    SELECT row_to_json(t)
    FROM (
        SELECT tpp.id
        	   ,(SELECT array_agg(row_tojson(t))
                )as "relatvieFiles"
        	   ,tppu.username
               ,tppu.type
               ,extract(epoch from tpp.create_time)::INTEGER as "createTime"
               ,tpp.title
               ,tpp.description
        FROM t_post tpp
             ,t_user tppu
        WHERE tpp.id = tp.parent_post_id
            AND tpp.user_id = tppu.id
    )t
)as "parentPostPreview"
```

- 해당 글에 parent가 있다면 부모글에 미리보기 정보를 제공하는것
- 중첩쿼리를 작성시 안쪽 쿼리부터 작성

- 한개만 있는 경우 `row_to_json()`을 이용
- 여러개의 멀티 row가 있다면 array_agg()를 이용해서 json으로 변경해줌

### 06.5 Post List 부분의 문제

```sql
,(
	SELECT count(*)
    FROM t_post
    WHERE parent_post_id = tp.id
)as "childPostCount"
```

- 초기에는 문제가 없으나 통계관련한 메소드의 경우에는 데이터 양이 많아지면 느려짐

## 07.Update

```sql
UPDATE t_community tc
SET member_count = COALESCE(
	(
        SELECT count(*)
        FROM t_user_community tuc
        WHERE tc.id = tuc.community_id GROUP BY tuc,community_id
    ), 0
);
```

- 커뮤니티의 멤버의 회원수 카운트 싱크를 맞춘 쿼리
- 각 커뮤니티 마다 카운트가 달라서 COALESCE 함수를 이용하여 사용

## 08.DELETE

```sql
DELETE FROM client_error t
WHERE exists(
	SELECT true
    FROM client_error t2
    WHERE t2.id = 3
    	AND t2.message = t.message
    	AND t2.url = t.url
    	AND t2.line_number = t.line_number
    	AND t2.column_number = t.column_number
)
```

## 09.쿼리 최적화

- 일반적으로 인덱스만 조정해도 속도가 빠름

- 쿼리 시간 확인을 위한 방법 explain

  - 데이터베이스 마다 explain 사용 방법 상이

    - PostgreSQL의 경우

      ```sql
      EXPLAIN ANALYZE <<QUERY>>
      ```

      - 해당 분석을 통해 full scan인지 index scan인지 분석하고 해결
      - 프로그래밍 기초에서도 언급했듯이 너무 이른 최적화는 하지 않아도 됨
        - 20ms 이상 속도가 느려질때 분석
