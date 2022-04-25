using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public List<PoolObject> ObjectsToPool = new List<PoolObject>();
   
    public Dictionary<ItemType, List<GameObject>> PoolsCollection = new Dictionary<ItemType, List<GameObject>>();

    void Start()
    {
        foreach (PoolObject item in ObjectsToPool)
        {
            PoolsCollection.Add(item.poolType, new List<GameObject>());
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.Obj);
                obj.SetActive(false);
                PoolsCollection[item.poolType].Add(obj);
            }
        }

    }

    public GameObject GetPooledObject(ItemType type)
    {
       
            for (int i = 0; i < PoolsCollection[type].Count; i++)
            {
                if (!PoolsCollection[type][i].activeInHierarchy)
                {
                    return PoolsCollection[type][i];
                }
            }//yahan sy agy check krna hai
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool.tag == tag)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    
}
