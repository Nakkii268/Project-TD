using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllianceDirection : PointerDetect, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject directionUI;
    [SerializeField] private GameObject moveUI;
    [SerializeField] private Alliance alliance;
    [SerializeField] private Vector2 direction;
    [SerializeField] private CameraManager cam;
    [SerializeField] private Button retreatBtn;
    
    public event EventHandler<Vector2> OnDeloyed;
    [SerializeField]private Vector3 offset = new Vector3(0,-.7f,0);
    protected void Start()
    {
        
        cam = CameraManager.instance;
        retreatBtn.onClick.AddListener(() => {
            alliance.Retreat(false);
          
        });
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        moveUI.GetComponent<RectTransform>().anchoredPosition = ValueClamp(MousePosInWorld());

    }

    public void OnDrag(PointerEventData eventData)
    {
        moveUI.GetComponent<RectTransform>().anchoredPosition = ValueClamp(MousePosInWorld());

        UnHighLightAttackRange();

        direction=CalculateDirection(new Vector2(moveUI.GetComponent<RectTransform>().localPosition.x, moveUI.GetComponent<RectTransform>().localPosition.y));
       
        HighLightAttackRange();
    }

    private Vector3 MousePosInWorld()
    {
        Vector3 mousepos = cam.GetCamera().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        mousepos -= alliance.transform.position - offset;
        mousepos.z = 0f;
        return mousepos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
      direction=  CalculateDirection(new Vector2(moveUI.GetComponent<RectTransform>().localPosition.x, moveUI.GetComponent<RectTransform>().localPosition.y));
        if (direction == Vector2.zero)
        {
            moveUI.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        }
        else
        {
            cam.SetCameraOriginRotation();
            gameObject.SetActive(false);
            UnHighLightAttackRange();
            OnDeloyed?.Invoke(this,direction);
            LevelManager.instance.GetLevelDPManager().ReduceDP(alliance.GetUnitCost());
            LevelManager.instance.GetLevelDPManager().DeploymentSlotOccp();
            alliance.UnitDeloy(direction);
            LevelManager.instance.TimeNormal();
        }
    }

   
    private Vector3 ValueClamp(Vector3 v)
    {
        float x = v.x;
        float y = v.y;

        if (x <= 0 && y <= 0)
        {
            x = Mathf.Clamp(x, -Mathf.Sqrt(2), 0);
            y = Mathf.Clamp(y, -x - Mathf.Sqrt(2), 0);
        }
        else if (x > 0 && y < 0)
        {
            x = Mathf.Clamp(x, 0, Mathf.Sqrt(2));
            y = Mathf.Clamp(y, x - Mathf.Sqrt(2), 0);
        }
        else if (x > 0 && y > 0)
        {
            x = Mathf.Clamp(x, 0, Mathf.Sqrt(2));
            y = Mathf.Clamp(y, 0, -x + Mathf.Sqrt(2));
        }
        else if (x < 0 && y > 0)
        {
            x = Mathf.Clamp(x, -Mathf.Sqrt(2), 0);
            y = Mathf.Clamp(y, 0, x + Mathf.Sqrt(2));
        }
        return new Vector3(x, y, 0);
    }
    
    private Vector2 CalculateDirection(Vector2 v)
    {
        float angle = Vector2.SignedAngle(new Vector2(1, 0), v);
        Vector2 direction;
        if (angle <= 15 && angle >= -15)
        {
            direction = new Vector2(1, 0);
            alliance.AllianceVisual.RotateToDirection(direction);
        }
        else if (75 <= angle && angle <= 105)
        {
            direction = new Vector2(0, 1);

        }
        else if (angle >= 165 || angle <= -165)
        {
            direction = new Vector2(-1, 0);
            alliance.AllianceVisual.RotateToDirection(direction);


        }
        else if(-75 >= angle && angle >= -105)
        {
            direction = new Vector2(0, -1);
        }
        else
        {
            direction = Vector2.zero;
        }
        
        return direction;
    }
    private void HighLightAttackRange()
    {
       List<Vector2> range = alliance.AllianceAttackRange.CalcAttackRange(alliance.Stat.AttackRange, direction);
        LevelManager.instance.MapManager.HighLightBlockList(range, 8);
    }
    private void UnHighLightAttackRange()
    {
        List<Vector2> range = alliance.AllianceAttackRange.CalcAttackRange(alliance.Stat.AttackRange,direction);
        LevelManager.instance.MapManager.UnHighLightBlockList(range);
    }

    protected override void PointerClickHandler_OnPointerClick(object sender, EventArgs e)
    {
        if (!isPointerIn)
        {
            alliance.Retreat(false);
            LevelManager.instance.TimeNormal();

        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
}