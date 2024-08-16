using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        var myPool = CreateTestPool();
        
        TestPool(myPool);
    } 
    
    private MyObjectPool<TestObject> CreateTestPool()
    {
        //Note: Консутруктор с дискардами(_) на месте неиспользуемого T(TestObject)
        return new MyObjectPool<TestObject>(
            createFunc: (num) => Create(num),
            initFunc: (obj, s) => obj.Initialize(s),
            deInitFunc: (obj, s) => obj.Deinitialize(s),
            actionOnRelease: (obj) => obj.ID = 0,
            actionOnDestroy: (obj) => obj.ID = -1,
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 15
        );
    }

    private TestObject Create(int num)
    {
        var to = new TestObject();
        to.ID = num;
        return to;
    }
    
    private void TestPool(MyObjectPool<TestObject> myPool)
    {
        // Проверка взятия
        var obj1 = myPool.Get();
        var obj2 = myPool.Get();
        Debug.Log($"CountActive after getting objects: {myPool.CountActive}");
        
        // Проверка релиза
        myPool.Release(obj1);
        myPool.Release(obj2);
        Debug.Log($"CountInactive after releasing objects: {myPool.CountInactive}");
        
        // Проверка уничтожения при переполнении
        for (int i = 0; i < 15; i++)
        {
            myPool.Release(new TestObject());
        }
        Debug.Log($"CountInactive after adding more objects: {myPool.CountInactive}");

        // Очистка пула
        myPool.Clear();
        Debug.Log($"CountAll after clearing: {myPool.CountAll}");
    }
}

public class TestObject
{
    public int ID;
    public void Initialize(string s) => Debug.Log($"Initialized object said {s}");
    public void Deinitialize(string s) => Debug.Log($"Deinitialized object said {s}");
} 