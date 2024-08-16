# Anonymous🥸 methods and delegates
# Task#2:
Сделать более удобную версию [ObjectPool](https://docs.unity3d.com/ScriptReference/Pool.ObjectPool_1.html) - 
убрать ограничение [IPoolObject](https://docs.unity3d.com/ScriptReference/Pool.IObjectPool_1.html).

Пофиксить ошибки компиляции - убрать Init, DeInit и тд...

Конструктор ObjectPool должен принимать делегат, который будет возвращать T.
Также конструктор должен принимать делегаты инициализации и деиницализации объекта T.
Необходимо хранить все эти делегаты как поля и вызывать добавившееся делегаты по необходимости.

# Выполнение:
- [EntryPoint.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task2/Assets/Scripts/EntryPoint.cs)
- [MyObjectPool.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task2/Assets/Scripts/MyObjectPool.cs)

# Итог:
Таска была выполнена. Преимущество созданного "велосипеда" заключается в том, что теперь создание объекта, его инициализация и тд определяется. Раньше этим занимался сам пул, теперь же - мы сами.