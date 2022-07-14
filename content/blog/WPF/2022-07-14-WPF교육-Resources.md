---
title: WPF교육 Resources
date: 2022-07-14 13:38:00 +0900
category: WPF
draft: false
---

## 2022-07-14-WPF교육-Resources

- 리소스

  - app 은 프로젝트

  - window는 해당 windows에서 쓰이는 것

  - key를 이용해서 명시적으로 사용함

## 목차

## 01.교육내용

### 01.1 x로 시작하는것

- x.name, x.key

  - 키를 쓸때 해당 필드에

    - {StaticResource}

    ```xaml
    <Window.Resources>
    	<Style TargetType ={x.Type Button} x.Key ="Hi">
        	<Setter Property="Background" Value="Blue"/>
        </Style>
    </Window.Resources>
    <!-- <Button Style="{StaticResource Hi}">  -->
    <Button Style="{StaticResource ResourceKey=Hi}">
        Button1
    </Button>
    ```

  - DynamicResource의 부분의 경우 소스랑 연동을 해야하기 때문에 추후로 미룰 예정

- add

  - 한 태그가 들어간것임

  - key는 background같은 것들

  - 그것의 값은 실버색  관련한것

    ```xaml
    <Button Background ="{StaticResource background}">
    </Button>
    ```

    - Style로 하려면 Setter이런것을 해야 가능 위에서는 Background로 해야함
    - Resource는 한곳이 아니고 자식인 곳은 모두 쓸 수 있음

- 실습 진행

  - window resource와 stack panel 
    - window는 컨트롤 하나만 가짐
      - 그래서 오류 걸림
    - 그 오류 해결하는 법
      - 패널 같은 것을 하나 만들면됨

## 02.코드로 xaml로 만들기

```xaml
<StackPanel Name ="sp">
    <StackPanel.Resource>
    	<SolidColorBrush x:Key ="background" Color=:"Auqa"/>
    </StackPanel.Resource>
	<Button Name ="btn1">Button1</Button>
	<Button Name ="btn2">Button2</Button>
	<Button Name ="btn3" Background ="{StaticResource background}">Button3</Button>
</StackPanel>
```

## 03.exe 이외에 있는곳

- 이미지 부분 넣는것 이외의 것

## 04.라이브러리

- 라이브러리?
  - 특정 기능을 가져다 씀
  - 그것의 집합 클래스 라이브러리
  - 프로젝트 이외의 것에 재사용하고 싶음

## 05.과제

- 다른곳에서도 빨간 것