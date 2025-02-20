using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllianceAttackRange : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private List<Vector2> attackRange;
    [SerializeField] private List<Vector2> extendedRange;
    public event EventHandler OnAttackRangeChange;
    public List<Vector2> AttackRange { get { return attackRange; } }

  
   
    public void SetAttackRange(Vector2 dir)
    {
        attackRange = CalcAttackRange(alliance.Stat.AttackRange,dir);

    }
    public List<Vector2> CalcAttackRange(List<Vector2> ranges,Vector2 dir)
    {
        if (dir == Vector2.zero) return null;
        List<Vector2> range = new();
        float angle = Vector2.SignedAngle(new Vector2(-1, 0), dir) * Mathf.Deg2Rad;

        for (int i = 0; i < alliance.Stat.AttackRange.Count; i++)
        {

           float tempx = alliance.GetUnitPos().x + ranges[i].x * Mathf.Cos(angle) - (ranges[i].y) * Mathf.Sin(angle);
           float tempy = alliance.GetUnitPos().y + ranges[i].y * Mathf.Cos(angle) + (ranges[i].x) * Mathf.Sin(angle);
            range.Add(new Vector2((float)Math.Round(tempx,1), (float)Math.Round(tempy, 1)));

        }
        return range;
    }
    public List<Vector2> RangeExtended(List<Vector2> extendRange, Vector2 dir)
    {
        return CalcAttackRange(extendRange, dir);
    }
    private void AddRange()
    {

        attackRange.AddRange(RangeExtended(extendedRange,alliance.GetUnitDir()));
        OnAttackRangeChange?.Invoke(this, EventArgs.Empty);
    }
    private void RemoveRange()
    {
        for (int i = 0; i < extendedRange.Count; i++)
        {
            attackRange.Remove(extendedRange[i]);
        }
        extendedRange.Clear();
        OnAttackRangeChange?.Invoke(this, EventArgs.Empty);

    }
    
    public List<Vector2> GetAttackRange()
    {
       return attackRange; 
    }

}
