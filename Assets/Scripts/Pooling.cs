using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;
    [SerializeField] List<GameObject> objectToPool = new List<GameObject>();
    [SerializeField] int size;
    Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        CreatePool();
    }

    private void CreatePool()
    {
        foreach (var obj in objectToPool)
        {
            if (!pools.ContainsKey(obj))
            {
                pools.Add(obj, new List<GameObject>());
                for (int i = 0; i < size; i++)
                {
                    GameObject go = Instantiate(obj, this.transform);
                    go.SetActive(false);
                    pools[obj].Add(go);
                }
            }
        }
    }

    public GameObject GetObj(GameObject key)
    {
        if (!pools.ContainsKey(key))
        {
            Debug.LogError($"{key.name} is not exist in pool");
            return null;
        }
        for (int i = 0; i < pools[key].Count; i++)
        {
            int index = i;
            if (!pools[key][index].activeInHierarchy)
                return pools[key][index];
        }

        GameObject go = Instantiate(key, this.transform);
        go.SetActive(false);
        pools[key].Add(go);
        return go;
    }

    public T GetObj<T>(GameObject key)
    {
        if(GetObj(key).TryGetComponent(out T result))
        {
            return result;
        }
        return default;
    }
}
