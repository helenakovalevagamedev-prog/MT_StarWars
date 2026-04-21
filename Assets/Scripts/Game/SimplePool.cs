using UnityEngine;
using UnityEngine.Pool;

public class SimplePool
{
    private GameObject simplePrefab;
    private ObjectPool<GameObject> pool;

    public SimplePool(GameObject prefab, int defaultCapacityValue, int maxSizeValue)
    {
        simplePrefab = prefab;
        pool = new ObjectPool<GameObject>(
            Create,
            OnGet,
            OnRelease,
            OnDestroy,
            collectionCheck: false,
            defaultCapacity: defaultCapacityValue,
            maxSize: maxSizeValue
        );
    }

    public GameObject Get()
    {
        return pool.Get();
    }

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }

    private GameObject Create()
    {
        return Object.Instantiate(simplePrefab);
    }

    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroy(GameObject obj)
    {
        Object.Destroy(obj);
    }
}