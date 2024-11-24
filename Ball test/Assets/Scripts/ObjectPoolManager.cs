using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    // Получает объект из пула с указанным именем. Если пул не существует или пуст, создает новый объект
    public GameObject GetObject(string poolName, GameObject prefab)
    {
        if (!pools.ContainsKey(poolName))
        {
            CreatePool(poolName, prefab);
        }

        Queue<GameObject> pool = pools[poolName];
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return CreateNewObject(prefab);
        }
    }

    // Возвращает объект в пул
    public void ReturnObject(string poolName, GameObject obj)
    {
        obj.SetActive(false);
        pools[poolName].Enqueue(obj);
    }

    //Создает новый пул
    private void CreatePool(string poolName, GameObject prefab)
    {
        pools.Add(poolName, new Queue<GameObject>());
    }

    // Создает новый экземпляр префаба
    private GameObject CreateNewObject(GameObject prefab)
    {
        GameObject newObj = Instantiate(prefab);
        return newObj;
    }
}
