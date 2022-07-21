---
title: 미니프로젝트 IT BestPractice
date: 2022-07-21 11:08:00 +0900
category: PoC
draft: false
---



## 2022-07-21-미니프로젝트-IT-BestPractice

- 이미지 pull

![image-20220721112648880](../../assets/img/post/2022-07-21-미니프로젝트-IT-BestPractice/image-20220721112648880.png)

- 이미지 삭제 |  강제삭제
  - docker image rm -f nginx

- docker hub 계정있으면 
  - 내 레파지토리로 사용할 수 있음

- 도커 이미지 이름 변경 등등

  - docker image tag nginx nginx_kmp

    ![image-20220721113117658](../../assets/img/post/2022-07-21-미니프로젝트-IT-BestPractice/image-20220721113117658.png)

  - 서비스를 하는것은 컨테이너에서 띄우는 것

  - 컨테이너 사용중이라면 강제로 하니 조심해야함

- docker create --name justBron -it alpine /bin/sh

- 도커 띄우기
  - docker container start 
  - 도커 run
    - docker run --name justBorn -d -it alpine /bin/sh
    - 

- nginx 설치
  - docker run --name ngnix-test -d -p 80:80 ngnix

- Centos

  - docker container run --rm -it --hostname yourhost.com centos
  - ![image-20220721114821485](../../assets/img/post/2022-07-21-미니프로젝트-IT-BestPractice/image-20220721114821485.png)
    - 컨테이너 자체를 hostname 하고 싶은 경우 
  - --rm 쓰면 종료가 되면 죽게 됨 테스트 용도로 사용하면 좋음

- docker exec -it nginx-test /bin/sh

  ![image-20220721115208275](../../assets/img/post/2022-07-21-미니프로젝트-IT-BestPractice/image-20220721115208275.png)

  - 기본 설정파일

  ### volume mgmt

  - 자원관리할때 좋음
  - 컨테이너 쓴다는것이 아무말을 안하면 그냥 만들어진곳에 그냥 관리해주는 걸 쓰고 사라지면 다 지워버린다.
  - 위험하기 때문에 파티션이 날아가는것을 방지하기 위함
  - docker run --name myNginx -v $Home:/user/share/nginx/html:ro -d -p 82:80 nginx
    - 볼륨 매핑

  ## network mgnt

  - 대개 그냥  bridge를 사용하게됨
    - 직관적인 이름을 주고 사용하면됨
    - --link를 이용해서 서로 같은 네트워크를 사용

## 백업 & 복원 

- 백업 , 복원 해보기

## docker file and docker compose 

![image-20220721133803617](../../assets/img/post/2022-07-21-미니프로젝트-IT-BestPractice/image-20220721133803617.png)

```y
FROM alpine:3.14
RUN ["echo"]
ENTRYPOINT ["ping", "-c", "3"]
CMD ["yahoo.com"]
```

```
root@DESKTOP-UD0PL4E:/home/mini# docker run image:test
PING yahoo.com (74.6.143.26): 56 data bytes
64 bytes from 74.6.143.26: seq=0 ttl=37 time=180.985 ms
64 bytes from 74.6.143.26: seq=1 ttl=37 time=182.733 ms
64 bytes from 74.6.143.26: seq=2 ttl=37 time=181.374 ms

--- yahoo.com ping statistics ---
3 packets transmitted, 3 packets received, 0% packet loss
round-trip min/avg/max = 180.985/181.697/182.733 ms
root@DESKTOP-UD0PL4E:/home/mini# docker run image:test www.naver.com
PING www.naver.com (223.130.200.107): 56 data bytes
```

- 사이트를 지정해서도 가능함
  - 원하는 이미지 만들면 쉽게 사용할 수 있음

### docker compose 파일

```yaml
#usage:
#docker-compose up -d (백그라운드로 구동)
#docker-compose down
#-p <project name>
#docker-compose -p drupal up -d
#docker-compose -p drupal down
#docker-compose -p drupal -f ./docker-compose-drupal.yml up -d
#docker-compose -p drupal -f ./docker-compose-drupal.yml down

version: '3.1'
services:
  postgres-db:
    image: postgres:11
    environment:
      - POSTGRES_DB=drupal
      - POSTGRES_USER=user
      - POSTGRES_PASSWORD=pass
    volumes:
      - /postgres_db_for_drupal:/var/lib/postgresql/data
  web:
    depends_on:
      - postgres-db
    links:
      - postgres-db:postgres
    image: drupal
    volumes:
      - drupal-modules:/var/www/html/modules
      - drupal-profiles:/var/www/html/profiles
      - drupal-sites:/var/www/html/sites
      - drupal-themes:/var/www/html/themes
    ports:
      - "81:80"
volumes:
  drupal-modules:
  drupal-profiles:
  drupal-sites:
  drupal-themes:
  
#use postgres-db rather than localhost in advanced configuration of drupal web page
```

- php 솔루션 drupal 파일

- docker-compose -p drupal up -d
  - 도커컴포즈 파일 업로드