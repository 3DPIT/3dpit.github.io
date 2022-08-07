---
title: Confusion Metrix 구현과정
date: 2022-08-07 16:14:00 +0900
category: DashBoard
draft: false
---

## 2022-08-07-Confusion-Metrix-구현과정

## 목차

## 01.C++로 구현해보기

### 01.1 5개가 고정된 경우

```c++
#include <iostream>
#include <sstream>
#include <vector>
using namespace std;
int main(void)
{
	int driLen = 5;
	int rr[] = { 1,2,3,4,5 };
	int ii[] = { 1,2,4,4,5, };
	string s;

	vector<string>vs;

	for (int i = 0; i < driLen; i++) {
		if (rr[i] == ii[i]) {
			for (int j = 0; j < driLen; j++) {
				if (j+1 == rr[i]) s += "1,";
				else s += "0,";
			}
		}
		else if (rr[i] != ii[i]) {
			for (int j = 0; j < driLen; j++) {
				if (j+1 == rr[i]) s += "2,";
				else if (j == rr[i]) s += "1,";
				else s += "0,";
			}
		}
		vs.push_back(s);
		s.clear();
	}

	for (int i = 0; i < driLen; i++) {
			cout << vs[i]<<endl;
	}
	
	return 0;
}
```

![image-20220807162024910](../../assets/img/post/2022-08-07-Confusion-Metrix-구현과정/image-20220807162024910.png)

### 01.2 갯수에 맞게 나오는 경우

```c++
#include <iostream>
#include <sstream>
#include <vector>
using namespace std;
int main(void)
{
	int rr[] = { 1,2,3,4,5,6,7,8,9,10 };
	int ii[] = { 1,2,4,4,5,6,7,8,9,10};
	int idx = 0;
	while (rr[idx++] >0);
	string s;
	int driLen = idx-1;

	vector<string>vs;

	for (int i = 0; i < driLen; i++) {
		if (rr[i] == ii[i]) {
			for (int j = 0; j < driLen; j++) {
				if (j+1 == rr[i]) s += "1,";
				else s += "0,";
			}
		}
		else if (rr[i] != ii[i]) {
			for (int j = 0; j < driLen; j++) {
				if (j+1 == rr[i]) s += "2,";
				else if (j == rr[i]) s += "1,";
				else s += "0,";
			}
		}
		vs.push_back(s);
		s.clear();
	}

	for (int i = 0; i < driLen; i++) {
			cout << vs[i]<<endl;
	}
	
	return 0;
}
```

![image-20220807162614217](../../assets/img/post/2022-08-07-Confusion-Metrix-구현과정/image-20220807162614217-16598572165371.png)

## 02.코드 sql 함수화

```sql
drop table test1;	
create table public.test1(
    no          integer       not null 
  , title       varchar(300)  not null
  , create_date timestamp(0)  not null
);

-- 테스트 테이블 조회
select * from public.test1;


create or replace function public.fn_test_table_insert()
returns integer AS
$$
    declare
		idx integer;
		v_no	integer:=1;
		v_i integer:=1;
		v_number integer[];
		r_i integer;
		i_i integer;
		v_idx integer:=1;
		v_string text:=',';
BEGIN
	delete from test1;
    -- 제목의 필수사항을 확인
	for v_i in 1..10 
		loop
		select inference_code, reference_code into r_i, i_i from dri where defect in (v_i);	
			for v_idx in 1..10
				loop
						if r_i = v_idx then v_string := v_string||'1'||',';
						else  v_string:= v_string||'0'||',';
						end if;
				end loop;
	    insert into public.test1
    		values
      		(i_i,v_string, current_timestamp);
			v_string:=',';
		    end loop;
    	return v_no;	
    -- primary key : no 채번
--    select coalesce(max(no), 0) + 1 into v_no
--      from public.test1;
END;
$$
LANGUAGE plpgsql

select fn_test_table_insert();

select * from public.test1;
```



