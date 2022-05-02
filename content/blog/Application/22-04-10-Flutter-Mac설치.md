---
title: '22-04-10-FlutterMac설치'
date: 2022-04-10 19:20:00 +0900
category: 'Application'
draft: false
---

## 22-04-10-Flutter-Mac설치

## 목차

> 01.xcode 설치
>
> 02.Flutter 설치
>
> > 02.1 Subline Text 설치하기
> >
> > 02.2 flutter 설치 확인
>
> 03.안드로이드 스튜디오 설치
>
> > 03.1 플러터 플러그인 설치
>
> 04.flutter 실행해보기

## 01.xcode 설치

- [macOs install 링크](https://docs.flutter.dev/get-started/install/macos)

![image-20220410192736821](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220410192736821.png)

- Xcode를 클릭한다.

![image-20220410192919033](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220410192919033.png)

- Download 클릭

![image-20220410193022139](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220410193022139.png)

- 앱스토어에 들어가서 다운해도됨 
- 일단 Xcode가 설치되어 있어야한다.

- 설치가 제대로 진행되지 않는다면?

  - https://developer.apple.com/download/all/?q=Xcode
  - 웹사이트에서 애플아이디를 로그인한 후에 설치를 해보자

  ![image-20220410200657460](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220410200657460.png)

![image-20220410235852992](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220410235852992.png)

- 설치가 되면 Agree를 클릭한다.

![image-20220411000003255](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411000003255.png)

- 이렇게 나오면 설치 완료

## 02.Flutter설치

![image-20220411001605487](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411001605487.png)

- 설치파일 클릭

- 다운로드 위치로 가서 압축을 풀어준다.

![image-20220411001708000](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411001708000.png)

- 압축푼 파일을 이동 클릭 후 홈으로 이동 시킨다.

- 그 파일을 이곳으로 옮기면된다.

### 02.1 Subline Text 설치하기

- [설치 링크](https://www.sublimetext.com/)

![image-20220411192347163](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411192347163.png)

- 다운로드 클릭 하여 설치

- 압축을 풀고 Applications로 이동해준다.

![image-20220411192629440](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411192629440.png)

- Sublime Text를 실행후 아래를 복사해서 넣어준다.

- [링크](https://docs.flutter.dev/get-started/install/macos#update-your-path)

  - `export PATH="$PATH:[PATH_OF_FLUTTER_GIT_DIRECTORY]/bin"`

  - export PATH="$PATH:$HOME/flutter/bin"

- 저장하기

  ![image-20220411192943378](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411192943378.png)

  - 자기 이름이 있는 경로가 거의 홈임
    - .zshrc
      - 위의 이름으로 저장함
  - 터미널에 아래를 입력
    - `source $HOME/.zshrc`
  - echo $PATH
    - 위를 입려해보면 제대로 저장됨이 보인다.
  - which flutter
    - flutter만 보여줌

### 02.2 flutter 설치 확인

- `flutter doctor`

![image-20220411193550488](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411193550488.png)

## 03.안드로이드 스튜디오 설치

- [설치 링크](https://developer.android.com/studio?gclid=CjwKCAjwo8-SBhAlEiwAopc9W4G_Kkz9xpP2fUWUlo7Yz0Ee_dNgnj8p2F1cO5T8vCwc_AUy4Hlz9RoCcJcQAvD_BwE&gclsrc=aw.ds)

  ![image-20220411193755726](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411193755726.png)

  - 각자의 cpu에 맞게 설치를 진행함

![image-20220411194236297](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411194236297.png)

- 위 상태로 진행
- 그냥 일반적으로 설치를 진행하면됨

![image-20220411194756229](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411194756229.png)

- More Actions 클릭
  - SDK Manager클릭

![image-20220411194944429](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411194944429.png)

- Android SDK Command-line Tools 클릭 후  Apply 클릭

![image-20220411195314085](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411195314085.png)

- 라이센스 동의하기
  - `flutter doctor --android-licenses`
    - y 를 다 누르고 전체 넘기면됨

![image-20220411195453890](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411195453890.png)

- Cocoapods설치
  - [링크](https://guides.cocoapods.org/using/getting-started.html#installation)
  - `sudo gem install cocoapods`

![image-20220411195659289](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411195659289.png)

- 위와 같이 되어있으면 어느정도 완료됨

### 03.1 플러터 플러그인 설치

![image-20220411195806397](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411195806397.png)

- Plugins 클릭 후 flutter 입력 후 설치 를 진행

## 04.flutter 실행해보기

![image-20220411200155998](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411200155998.png)

- New Flutter Project클릭

![image-20220411200300771](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411200300771.png)

- 아래 순서대로 진행 하고 Next

![image-20220411200346078](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411200346078.png)

- 위대로 적당히 입력 후 Finish 해준다.

![image-20220411200915885](../../assets/img/post/22-04-10-Flutter-Mac설치.assets/image-20220411200915885.png)

- 실행해서 이렇게 나오면 성공한 것 
  - 추후에 직접 폰을 연결해서 되는지 해보자
- 여기까지 설정 완료
