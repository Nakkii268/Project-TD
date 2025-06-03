using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBox : MonoBehaviour
{
    [SerializeField] private LevelLifePointManager levelLifePointManager;
    private void Start()
    {
        levelLifePointManager = LevelManager.instance.LevelLifePointManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            levelLifePointManager.LifePointReduce();
            collision.GetComponentInParent<IDamageable>().ReceiveDamaged(99999999,DamageType.TrueDamage);
        }
    }
}
