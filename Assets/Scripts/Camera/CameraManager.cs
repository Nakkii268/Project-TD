using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private Camera _camera;
    [SerializeField] private Quaternion _cameraRotation;

    private void Awake()
    {
        instance = this;
        _cameraRotation=_camera.transform.rotation ;
    }
    public Camera GetCamera()
    {
        return _camera;
    }
    public void CamLookat(Transform target)
    {
        

        _camera.transform.LookAt(target);
    }
    public void SetCameraRotation(Quaternion camRotate)
    {
        _camera.transform.rotation = camRotate;
    }
    public void SetCameraOriginRotation()
    {
        _camera.transform.rotation = _cameraRotation;
    }
}
