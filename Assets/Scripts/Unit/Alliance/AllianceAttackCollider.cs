using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllianceAttackCollider : MonoBehaviour
{
    public PolygonCollider2D collider; //need to set gameobject offset .25 cause the character is .25 up to compare with block
    public Alliance Alliance;
    [SerializeField]private LayerMask[] enemyLayer;
    public event EventHandler<GameObject> OnEnemyIn;
    public event EventHandler<GameObject> OnEnemyOut;

    private void Start()
    {
        enemyLayer = Alliance.GetAllianceUnit().EnemyType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (LayerMask layerMask in enemyLayer) { 
         
            if ((layerMask.value & (1<<collision.gameObject.layer))==0)  return;
           
            if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    OnEnemyIn?.Invoke(this, collision.gameObject);
                    Debug.Log("In e");

                }
            }
            else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
            {
                if (collision.gameObject.CompareTag("Alliance"))
                {
                    OnEnemyIn?.Invoke(this, collision.gameObject);
                    Debug.Log("In a");

                }
            }
            else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Both)
            {
                if (collision.gameObject.CompareTag("Alliance") || collision.gameObject.CompareTag("Enemy"))
                {
                    OnEnemyIn?.Invoke(this, collision.gameObject);
                    Debug.Log("In b");

                }
            }

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (LayerMask layerMask in enemyLayer)
        {
           
            if ((layerMask.value & (1 << collision.gameObject.layer)) == 0) return;
            if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    OnEnemyOut?.Invoke(this, collision.gameObject);
                    Debug.Log("out e");

                }
            }else if(Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
            {
                if (collision.gameObject.CompareTag("Alliance"))
                {
                    OnEnemyOut?.Invoke(this, collision.gameObject);
                    Debug.Log("out a");

                }
            }else if(Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Both)
            {
                if (collision.gameObject.CompareTag("Alliance") || collision.gameObject.CompareTag("Enemy"))
                {
                    OnEnemyOut?.Invoke(this, collision.gameObject);
                    Debug.Log("out b");

                }
            }
        }
    }

    public void SetCollider(Vector2[] range,Vector2 pos)
    {
        collider.points = ArrangedPointInClockWise(GetColliderPoint(range, pos));
        

    }
    private Vector2[] GetColliderPoint(Vector2[] attackRange,Vector2 pos)
    {
        List<Vector2> points = new List<Vector2>();
        foreach (Vector2 point in attackRange) {
           
                points.Add(new Vector2(point.x + .5f - pos.x, point.y - .5f - pos.y));
            
            
                points.Add(new Vector2(point.x + .5f - pos.x, point.y + .5f - pos.y));

            
            
                points.Add(new Vector2(point.x - .5f - pos.x, point.y + .5f - pos.y));

            
            
                points.Add(new Vector2(point.x - .5f - pos.x, point.y - .5f - pos.y));

                


        }
        return points.ToArray();
    } 
    private Vector2[] ArrangedPointInClockWise(Vector2[] range)
    {
        List<Vector2> arranged = new List<Vector2>();
        arranged.AddRange(range);
        List<Vector2> nonDup = new List<Vector2>(RemoveDup(arranged));
        Vector2 center = GetRangeCenter(nonDup);
       
        nonDup.Sort((a, b) =>
        {
            float angleA = ConvertAngle(Vector2.SignedAngle(new Vector2(center.x, center.y), a));
            
            float angleB = ConvertAngle(Vector2.SignedAngle(new Vector2(center.x, center.y), b));
            
            return angleB.CompareTo(angleA);
        });


        return nonDup.ToArray();
    }
    private Vector2 GetRangeCenter(List<Vector2> range)
    {
        float sumX = 0;
        float sumY = 0;
        if (range.Count == 0) return Vector2.zero;
        foreach (Vector2 point in range) {
            sumX += point.x;
            sumY += point.y;
        }
        return new Vector2(sumX / range.Count, sumY / range.Count);
    }
    private List<Vector2> RemoveDup(List<Vector2> range)
    {
        HashSet<Vector2> uniquePointsSet = new HashSet<Vector2>(range);
        return new List<Vector2>(uniquePointsSet);
    }

    private float ConvertAngle(float angle)
    {
        if(angle >=0) return angle;
        angle += 360;
        return angle;
    }
}
