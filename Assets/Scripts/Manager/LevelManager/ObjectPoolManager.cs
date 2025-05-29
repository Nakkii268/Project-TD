using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] public AllianceProjectile AllyProjectilePrefab;
    [SerializeField] public EnemyProjectile EnemyProjectilePrefab;
    [SerializeField] public Pooling<AllianceProjectile> AllyPool;
    [SerializeField] public Pooling<EnemyProjectile> EnemyPool;

    private void Start()
    {
        
        AllyPool = new Pooling<AllianceProjectile>(AllyProjectilePrefab, 20);
        EnemyPool = new Pooling<EnemyProjectile>(EnemyProjectilePrefab, 20);
        Debug.Log("run");
        Debug.Log(AllyPool.Pool.Count);
        Debug.Log(EnemyPool.Pool.Count);
        
    }
}
