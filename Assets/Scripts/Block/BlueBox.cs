using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBox : MonoBehaviour
{
    [SerializeField] private LevelLifePointManager levelLifePointManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            levelLifePointManager.LifePointReduce();
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
