using System.Collections.Generic;
using UnityEngine;

// MonoBehavior 상속 받은 애들만 생성 할 수 있게끔
public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool;
    private T prefab;

    public ObjectPool(T prefab, int poolSize)
    {
        this.prefab = prefab;
        pool = new Queue<T>();

        for (int i = 0; i < poolSize; i++)
        {
            T instance = UnityEngine.Object.Instantiate(this.prefab);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        
        // 풀에 남은 오브젝트가 없다면 null
        // 추가하고 싶다, 
        // T newObj = Object.Instantiate(prefab);
        // return newObj;
        return null;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}