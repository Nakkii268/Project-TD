using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alliance : MonoBehaviour
{
    [SerializeField] private Vector2 unitPos;
    [SerializeField] private Vector2[] attackRange;
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isDeloyed;
    [SerializeField] private AllianceDirection allianceDirection;
    private void Start()
    {
        allianceDirection.OnCancleDeloy += AllianceDirection_OnCancleDeloy;
       
    }

    private void AllianceDirection_OnCancleDeloy(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SetAttackRange()
    {
        attackRange = new Vector2[unit.AttackRange.Length];
        float angle = Vector2.Angle(-Vector2.one, direction);
        for (int i = 0; i < unit.AttackRange.Length; i++)
        {

            attackRange[i].x = unitPos.x + Mathf.CeilToInt(unit.AttackRange[i].x * Mathf.Cos(angle) - unit.AttackRange[i].y * Mathf.Sin(angle));
            attackRange[i].y = unitPos.y + Mathf.CeilToInt(unit.AttackRange[i].x * Mathf.Cos(angle) + unit.AttackRange[i].y * Mathf.Sin(angle));

        }
    }
    public Vector2[] GetAttackRange()
    {
        return attackRange;
    }
    public void SetUnit(Unit u,Vector2 pos)
    {
        unitPos = pos;
        unit = (AllianceUnit)u;
    }
    public void UnitDeloy(Vector2 dir) {
        allianceDirection.gameObject.SetActive(false);
        direction = dir;
        SetAttackRange();
        isDeloyed = true;
    }
    
}
