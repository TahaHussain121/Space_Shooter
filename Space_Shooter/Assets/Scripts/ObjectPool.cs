using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
    }


    public List<PoolObject> ObjectsToPool = new List<PoolObject>();

    public Dictionary<ItemType, List<GameObject>> PoolsCollection = new Dictionary<ItemType, List<GameObject>>();

    private bool _InitializePool()
    {
        foreach (PoolObject item in ObjectsToPool)
        {
            Debug.Log("pool items in list" + item.poolType);
            PoolsCollection.Add(item.poolType, new List<GameObject>());
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.Obj);
                obj.SetActive(false);
                PoolsCollection[item.poolType].Add(obj);
            }
        }
        return true;
    }
    public static bool InitializePool()
    {
        return Instance._InitializePool();
    }


    private GameObject _GetPooledObject(ItemType type)
    {
        Debug.Log("Item type" + type);

        for (int i = 0; i < PoolsCollection[type].Count; i++)
        {
            if (!PoolsCollection[type][i].activeInHierarchy)
            {
                return PoolsCollection[type][i];
            }
        }//yahan sy agy check krna hai
        foreach (ItemType item in PoolsCollection.Keys)
        {
            if (item == type)
            {
                if (IsExpandable(item))
                {
                    GameObject obj = (GameObject)Instantiate(ItemDetails(item).Obj);
                    obj.SetActive(false);
                    PoolsCollection[item].Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
    public static GameObject GetPooledObject(ItemType type)
    {
        return Instance._GetPooledObject(type);
    }

    private bool IsExpandable(ItemType type)
    {

        PoolObject obj = ItemDetails(type);

        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private PoolObject ItemDetails(ItemType type)
    {

        PoolObject obj = ObjectsToPool.Find(x => x.poolType == type && x.isExpandable == true);

        return obj;
    }
    public static void _EndPool()
    {
        Debug.Log("Shutting pool");

        Instance. EndPool();
    }
    private void EndPool()
    {
        foreach (PoolObject item in ObjectsToPool)
        {
            Debug.Log("pool items in list" + item.poolType);

            for (int i = 0; i < item.poolSize; i++)
            {

                GameObject var = PoolsCollection[item.poolType][i];
                Destroy(var);
            }
            PoolsCollection[item.poolType].Clear();
        }
    }



}
