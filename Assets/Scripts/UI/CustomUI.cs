using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomUI : MonoBehaviour
{

    [SerializeField] private float Xvalue;
    [SerializeField] private float Yvalue;
    [SerializeField] private Corner corner=Corner.TopLeft;
    [SerializeField] private RectTransform[] mRectTransforms;
    [SerializeField] private float scrWidth;

    private void Start()
    {
        
        mRectTransforms = transform.GetComponentsInChildren<RectTransform>();
       
        CalculateValue(GetActiveChild());

    }
    public void ReArrangeChild()
    {
        CalculateValue(GetActiveChild());

    }
    public void CalculateValue(List<RectTransform> childList)
    {
        if (childList.Count <= 0) return;
        scrWidth = Screen.width;
        Yvalue = Screen.height * 0.16f;
        Xvalue = Screen.width / childList.Count;
        if (Xvalue >= Yvalue)
        {
            Xvalue = Yvalue;
        }
        for (int i = 0; i < childList.Count; i++)
        {
            childList[i].sizeDelta = new Vector2(Xvalue, Yvalue);
            childList[i].localPosition = CalculatePosition(new Vector2(Xvalue, Yvalue), i, (int)corner);
        }
        
    }
    public Vector2 CalculatePosition( Vector2 size,int index,int corner = 0)
    {

        switch (corner)
        {
            case 0:
                return new Vector2(size.x / 2 + size.x * index - transform.position.x,  transform.position.y - size.y / 2 );
                
            case 1:
                return new Vector2(Screen.width - size.x / 2 - size.x * index - transform.position.x, transform.position.y - size.y / 2);
                
            case 2:
                return new Vector2( size.x / 2 + size.x * index - transform.position.x, size.y / 2 - transform.position.y);
                
            case 3:
                return new Vector2(Screen.width - size.x/2 -size.x * index - transform.position.x, size.y/2 - transform.position.y);
               


        }
        return new Vector2(0, size.y);
    }

    public List<RectTransform> GetActiveChild()
    {
        List<RectTransform> active = new List<RectTransform>();
        for (int i = 0; i < mRectTransforms.Length; i++)
        {
            if (mRectTransforms[i].gameObject.activeInHierarchy)
            {
                active.Add(mRectTransforms[i]);
            }
        }
        return active;
    }

}
    [Serializable]
    public enum Corner
    {
        TopLeft = 0,
        TopRight = 1,
        BottomLeft = 2,
        BottomRight = 3
    }
