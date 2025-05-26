using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class ScaleToFOV : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    
    private float BaseFOV = 25;
     private Vector3 BaseScale = new Vector3(2,2,2);    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        float scale = Mathf.Tan(Mathf.PI * BaseFOV  / 360)/ Mathf.Tan(_camera.fieldOfView * Mathf.PI / 360);
        
        gameObject.transform.localScale = scale * BaseScale;
    }

    
}
