# Anonymous🥸 methods and delegates
# Task#3:
Для каждого используемого делегата в пуле создать собственное определение. Вместо встроенных делегатов Action и Func 
уже созданных в C# использовать свои собственные.

# Выполнение:
- [EntryPoint.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task3/Assets/Scripts/EntryPoint.cs)
- [MyObjectPool.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task3/Assets/Scripts/MyObjectPool.cs)

# Итог:
Таска была выполнена. Были созданы следующие делегаты:
* **_BlankDelegate_** представляет собой делегат, который принимает один параметр типа T и не возвращает значения (т.е. его возвращаемый тип void).

  Примеры использования
  * _Инициализация объекта_: Делегат можно использовать для инициализации объекта перед его использованием. 
  * _Деинициализация объекта_: Делегат может быть вызван для выполнения операций, когда объект возвращается в пул.

        public void InitializeObject(MyObject obj)
        {
        // Инициализация объекта
        }
        
        public void CleanupObject(MyObject obj)
        {
        // Очистка или деинициализация объекта
        }
        
        var pool = new MyObjectPool<MyObject>(
        createFunc: () => new MyObject(),
        onInit: new BlankDelegate<MyObject>(InitializeObject),
        onDeinit: new BlankDelegate<MyObject>(CleanupObject)
        );
* **_StringInDelegate_** представляет собой делегат, который принимает два параметра: один из типа T и один из типа string. Этот делегат не возвращает значения (возвращаемый тип void).

  Примеры использования
  * _Инициализация с параметрами_: Этот делегат может использоваться, чтобы передать дополнительную строку для инициализации объекта.
  * _Деинициализация с параметрами_: Делегат может передавать строку, которая будет использоваться в процессе деинициализации.

          public void InitializeObjectWithString(MyObject obj, string data)
          {
          // Инициализация объекта с использованием строки
          }
          
          public void DeinitializeObjectWithString(MyObject obj, string data)
          {
          // Деинициализация объекта с использованием строки
          }
          
          var pool = new MyObjectPool<MyObject>(
          createFunc: () => new MyObject(),
          onInit: new StringInDelegate<MyObject>(InitializeObjectWithString),
          onDeinit: new StringInDelegate<MyObject>(DeinitializeObjectWithString)
          );

* **_CreateDelegate_** представляет собой делегат, который принимает один параметр типа int и возвращает объект типа T. 
Здесь out T указывает, что делегат возвращает объект типа T, но не принимает параметры типа T на входе (то есть, T используется только как возвращаемый тип). Также out T и то, что T возвращаемый тип свидетельствует о ковариантности.

  Примеры использования
  * _Создание объекта_: Делегат можно использовать для создания нового объекта типа T с использованием целочисленного параметра, который может быть использован в процессе создания.

          public MyObject CreateObject(int id)
          {
          var obj = new MyObject();
          obj.ID = id;
          return obj;
          }
        
          var pool = new MyObjectPool<MyObject>(
          createFunc: new CreateDelegate<MyObject>(CreateObject)
          );