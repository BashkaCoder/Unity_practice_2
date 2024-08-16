using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public delegate void BlankDelegate<in T>(T t);
public delegate void StringInDelegate<in T>(T t, string s);
public delegate T CreateDelegate<out T>(int num);


public class MyObjectPool<T> : IDisposable
  {
    private readonly List<T> _list;
    private readonly CreateDelegate<T> _createFunc;
    private readonly StringInDelegate<T> _initFunc;  
    private readonly StringInDelegate<T> _deInitFunc;
    private readonly BlankDelegate<T> _actionOnRelease;
    private readonly BlankDelegate<T> _actionOnDestroy;
    private readonly int _maxSize;
    private readonly bool _collectionCheck;
 
    public int CountAll { get; private set; }

    public int CountActive => CountAll - CountInactive;

    public int CountInactive => _list.Count;

    public MyObjectPool(
      CreateDelegate<T> createFunc,
      StringInDelegate<T> initFunc = null,   
      StringInDelegate<T> deInitFunc = null,     
      BlankDelegate<T> actionOnRelease = null,
      BlankDelegate<T> actionOnDestroy = null,
      bool collectionCheck = true,
      int defaultCapacity = 10,
      int maxSize = 10000)
    {
      if (createFunc == null)
        throw new ArgumentNullException(nameof (createFunc));
      if (maxSize <= 0)
        throw new ArgumentException("Max Size must be greater than 0", nameof (maxSize));
      
      _list = new List<T>(defaultCapacity);
      _createFunc = createFunc;
      _initFunc = initFunc;
      _deInitFunc = deInitFunc;
      _maxSize = maxSize;
      _actionOnRelease = actionOnRelease;
      _actionOnDestroy = actionOnDestroy;
      _collectionCheck = collectionCheck;
    }

    public T Get()
    {
      T obj;
      if (_list.Count == 0)
      {
        obj = _createFunc(Random.Range(1, 100));
        ++CountAll;
      }
      else
      {
        int index = _list.Count - 1;
        obj = _list[index];
        _list.RemoveAt(index);
      }
      
      _initFunc?.Invoke(obj, "ABOBA | INIT");
      
      return obj;
    }

    public void Release(T element)
    {
      if (_collectionCheck && _list.Count > 0)
      {
        foreach (var t in _list)
        {
          if ((object) element == (object) t)
            throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
        }
      }
      _deInitFunc?.Invoke(element, "ABOBA | DEINIT");
      _actionOnRelease?.Invoke(element);
      
      if (CountInactive < _maxSize) 
        _list.Add(element);
      else 
        _actionOnDestroy?.Invoke(element);
    }

    public void Clear()
    {
      foreach (T obj in _list)
        _actionOnDestroy?.Invoke(obj);
      
      _list.Clear();
      CountAll = 0;
    }

    public void Dispose() => Clear();
  }