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
    [SerializeField] private AlliianceInfomation allianceInfo;
    private void Start()
    {
        allianceDirection.OnCancleDeloy += AllianceDirection_OnCancleDeloy;
        allianceDirection.OnDeloyed += AllianceDirection_OnDeloyed;
        LevelManager.instance.OnClickOutSide += LevelManager_OnClickOutSide;
    }

    private void LevelManager_OnClickOutSide(object sender, System.EventArgs e)
    {
        if (allianceInfo.IsPointerIn())
        {
            return;
        }
        CameraManager.instance.SetCameraOriginRotation();
        UIHide();
    }

    private void AllianceDirection_OnDeloyed(object sender, Vector2 e)
    {
        isDeloyed = true;
        direction = e;
    }

    private void AllianceDirection_OnCancleDeloy(object sender, System.EventArgs e)
    {
        Retreat();
    }

    private void SetAttackRange()
    {
        attackRange = GetAttackRange(direction);
       
    }
    public Vector2[] GetAttackRange(Vector2 dir)
    {
        if (dir == Vector2.zero) return null;
        Vector2[] range  = new Vector2[unit.AttackRange.Length];
        float angle = Vector2.SignedAngle(new Vector2(-1,0), dir) * Mathf.Deg2Rad;
        
        for (int i = 0; i < unit.AttackRange.Length; i++)
        {

            range[i].x = unitPos.x + (unit.AttackRange[i].x) * Mathf.Cos(angle) - (unit.AttackRange[i].y) * Mathf.Sin(angle);
            range[i].y = unitPos.y + (unit.AttackRange[i].y ) * Mathf.Cos(angle) + (unit.AttackRange[i].x) * Mathf.Sin(angle);
            Debug.Log("original" + range[i] + "===dirct"+ dir +"----angle" + angle);
        }
        return range;
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
       
        
    }
    public void Retreat()
    {
        isDeloyed = false;
        Block block = GetComponentInParent<Block>();
        block.UnitReTreat();
    }
    public void UIShowOnForcus()
    {
        if (isDeloyed)
        {
            allianceInfo.gameObject.SetActive(true);
        }
    }
    public void UIHide()
    {
        allianceInfo.gameObject.SetActive(false);
        allianceDirection.gameObject.SetActive(false);
    }
    
}
