using UnityEngine;
using UnityEngine.Pool;

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
            createFunc: () => new TestObject(),
            initFunc: (_) => InitTestObject(),
            deInitFunc: (obj) => obj.Denitialize(),
            actionOnRelease: (_) => Debug.Log("Release"),
            actionOnDestroy: (_) => Debug.Log("Destroy"),
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 15
        );
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
    
    private void InitTestObject()
    {
        Debug.Log("Test object initialized");
    }
}

public class TestObject
{
    //public void Initialize() => Debug.Log("Test object initialized");
    public void Denitialize() => Debug.Log("Test object deinitialized");
} 
