using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject Prefab;
    public List<GameObject> Pool;
    public int poolSize;

    private void Start()
    {
        InitializePool();
    }
    public void InitializePool()
    {
        Pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(Prefab);
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }

    public GameObject GetProjectile()
    {
        
        for (int i = 0; i < Pool.Count; i++) {

            if (!Pool[i].activeInHierarchy) { 
                return Pool[i];
            }
        }
        return null;
    }

}
