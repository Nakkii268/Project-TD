using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private Camera _camera;
    [SerializeField] private Quaternion _cameraRotation;
    [SerializeField] private Vector3 _basePosition;
    [SerializeField] private Transform _camHolder;


    private void Awake()
    {
        instance = this;
        _cameraRotation=_camera.transform.rotation ;
        _basePosition = _camHolder.transform.position ;
    }
    public Camera GetCamera()
    {
        return _camera;
    }
    public void CamLookat(Transform target)
    {

        float distanceFromCamera = Vector3.Dot(
            target.position - Camera.main.transform.position,
            Camera.main.transform.forward
        );

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, distanceFromCamera);
        Debug.Log(screenCenter + "Screen center");
        Vector3 objectWorldPosFromScreen = Camera.main.ScreenToWorldPoint(screenCenter); //offset for y axis
        Debug.Log(objectWorldPosFromScreen + "Screen center in world pos");

        _camHolder.position = new Vector3(target.position.x,target.position.y-objectWorldPosFromScreen.y + _basePosition.y,_basePosition.z);
    }

    public void SetCameraOriginRotation()
    {     

        _camHolder.position = _basePosition;
    }
}
