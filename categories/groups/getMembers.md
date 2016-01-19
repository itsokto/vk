---
layout: default
title: Метод Groups.GetMembers
permalink: groups/getMembers/
comments: true
---
# Метод Groups.GetMembers
Возвращает список участников группы.

# Синтаксис
```csharp
public ReadOnlyCollection<long> GetMembers(
	long gid, 
	out int totalCount, 
	int? count = null, 
	int? offset = null, 
	GroupsSort sort = null
)
```

## Параметры
+ **gid** - ID группы, список пользователей которой необходимо получить.
+ **totalCount** - Общее количество пользователей состоящих в группе.
+ **count** - Максимальное количество участников группы, которое необходимо получить. Максимальное значение 1000.
+ **offset** - Число, обозначающее смещение, для получения следующих после него участников.
+ **sort** - Сортировка с которой необходимо вернуть список групп.

## Результат
Возвращает общее количество участников группы totalCount и список идентификаторов пользователей uid.

## Исключения
+ **AccessTokenInvalidException** - не задан или используется неверный AccessToken.
+ **InvalidParamException** - Неправильный идентификатор группы.

## Пример
```csharp
// Получаем идентификаторы всех пользователей, состоящих в группе с id равным 2.
int totalCount;
var ids = vk.Groups.GetMembers(2, out totalCount);

// Получаем идентификаторы 7 пользователей, начиная с 15 позиции, сортированных в порядке возрастания идентификаторов.
int totalCount;
var ids = vk.Groups.GetMembers(2, out totalCount, 7, 15, GroupsSort.IdAsc);
```