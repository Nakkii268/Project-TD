using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerClickHandle : MonoBehaviour
{
    public event EventHandler OnPointerClick;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerClick?.Invoke(this, EventArgs.Empty);
            
        }
    }
}
