using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ContentFitter : MonoBehaviour
{
    [SerializeField] private RectTransform RectTransform;
    [SerializeField] private bool HorizontalFitter;
    [SerializeField] private bool VerticalFitter;
   
    

    private float HorizontalCal()
    {
        if (!HorizontalFitter) return RectTransform.rect.width; //return default width if not horzontal fit
        //min x + max x + 1/2 minx width + 1/2 maxx width
        RectTransform[] childs = gameObject.transform.GetComponentsInChildren<RectTransform>();
        float minX = childs[0].transform.position.x;
        float widthMin = childs[0].rect.width;
        float maxX = childs[0].transform.position.x;
        float widthMax = childs[0].rect.width;
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i].transform.position.x < minX)
            {
                minX = childs[i].transform.position.x;
                widthMin = childs[i].rect.width;
            }
            if (childs[i].transform.position.x > maxX)
            {
                maxX = childs[i].transform.position.x;
                widthMax = childs[i].rect.width;
            }
        }
        return (Mathf.Abs(maxX-minX) + (widthMax+widthMin)/2);
    
    }
    private float VerticalCal()
    {
        if (!VerticalFitter) return RectTransform.rect.height;
       
        float maxY = float.MinValue ;
        float minY =float.MaxValue;
        RectTransform[] child = gameObject.GetComponentsInChildren<RectTransform>();
        for(int i = 0;i < child.Length; i++)
        {
            if (child[i] == RectTransform) continue;
            if (child[i].gameObject.activeInHierarchy)
            {
                Vector3[] corner = new Vector3[4];

                child[i].GetWorldCorners(corner);
                foreach(Vector3 v in corner)
                {
                    

                    if (v.y < minY) minY=v.y;   
                    if(v.y > maxY) maxY=v.y;
                }
                /*Debug.DrawLine(corner[0], corner[1], Color.red, 2f); // bottom edge
                Debug.DrawLine(corner[1], corner[2], Color.green, 2f); // right edge
                Debug.DrawLine(corner[2], corner[3], Color.blue, 2f); // top edge
                Debug.DrawLine(corner[3], corner[0], Color.yellow, 2f); // left edge*/
            }
        }
       
       
        return Mathf.Abs(maxY - minY)*9.35f;
    
    }

    public void UpdateSize()
    {
        RectTransform.sizeDelta = new Vector2 (HorizontalCal(), VerticalCal());
       

    }
   

}
