using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceDirectionCircle : MonoBehaviour
{
    public void Initialize(Vector2 dir)
    {
        if(dir== new Vector2(0, 1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if(dir== new Vector2(1, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);

        }
        else if (dir == new Vector2(-1, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);

        }
        else if (dir == new Vector2(0, -1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);

        }
    }
}
