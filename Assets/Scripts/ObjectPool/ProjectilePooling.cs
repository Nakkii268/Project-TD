using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    public GameObject ProjectilePrefab;
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
            GameObject obj = Instantiate(ProjectilePrefab);
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
