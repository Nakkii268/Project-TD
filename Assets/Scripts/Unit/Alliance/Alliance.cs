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
    public Collider UnitUICollider;
    public int charIndex;
    private void Start()
    {
    
        allianceDirection.OnDeloyed += AllianceDirection_OnDeloyed;
       
        LevelManager.instance.OnClickOtherTarget += LevelManager_OnClickOtherTarget;
    }

    private void LevelManager_OnClickOtherTarget(object sender, System.EventArgs e)
    {
        if (allianceInfo == null) return;
        if (!isDeloyed)
        {
            Retreat(false);
        }
        if (!allianceInfo.gameObject.activeInHierarchy) return;
        
        if (allianceInfo.IsPointerIn()) return;
        
        
        CameraManager.instance.SetCameraOriginRotation();
        UIHide();
    }

    
    private void AllianceDirection_OnDeloyed(object sender, Vector2 e)
    {
        isDeloyed = true;
        direction = e;
        UnitUICollider.gameObject.SetActive(false);

        InGameCharListUI.Instance.HideDeloyedUnitUI(charIndex);
    }

   

    private void SetAttackRange()
    {
        attackRange = GetAttackRange(direction);
       
    }

    public bool IsDeloyed()
    {
        return isDeloyed;
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
            
        }
        return range;
    }
    public Vector2[] GetAttackRange()
    {
        return attackRange;
    } 
    public int GetUnitCost()
    {
        return unit.UnitDp;
    }
    public int GetUnitIndex()
    {
        return charIndex;
    }
    
    public void SetUnit(Unit u,Vector2 pos, int indx)
    {
        unitPos = pos;
        unit = (AllianceUnit)u;
        charIndex = indx;
    }
    public void UnitDeloy(Vector2 dir) {
        allianceDirection.gameObject.SetActive(false);
        direction = dir;
        SetAttackRange();
       
        
    }
    public void Retreat(bool isRetreat)
    {
        if (isRetreat)
        {
            InGameCharListUI.Instance.ShowRetreatedUnitUI(charIndex);
        }
        isDeloyed = false;
        LevelManager.instance.UnHighLightBlockList(attackRange);
        Block block = GetComponentInParent<Block>();
        block.UnitReTreat();
        
        CameraManager.instance.SetCameraOriginRotation();
        
    }
    public void UIShowOnForcus()
    {
        if (isDeloyed)
        {

            allianceInfo.gameObject.SetActive(true);
            LevelManager.instance.HighLightBlockList(attackRange, 8);
            UnitUICollider.gameObject.SetActive(true);

        }
    }
    public void UIHide()
    {
        allianceInfo.gameObject.SetActive(false);
        allianceDirection.gameObject.SetActive(false);
        LevelManager.instance.UnHighLightBlockList(attackRange);
        UnitUICollider.gameObject.SetActive(false);

    }

}
