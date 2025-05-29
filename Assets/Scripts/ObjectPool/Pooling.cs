using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling<T> where T : MonoBehaviour
{
    
    public List<T> Pool;
    

   
    public  Pooling(T prefab, int initSize)
    {
        Pool = new List<T>();
        for (int i = 0; i < initSize; i++)
        {
            T obj =GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            Pool.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        
        for (int i = 0; i < Pool.Count; i++) {

            if (!Pool[i].gameObject.activeInHierarchy) { 
                return Pool[i];
            }
        }
        return null;
    }
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}
