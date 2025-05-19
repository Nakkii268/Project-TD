using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField]protected bool isDestroyOnClose;
    public virtual void SetUp(AllianceUnit unit) { }
    public virtual void SetUp(object t) { }
    public virtual void SetUp() { }
    public virtual void Close(float time) 
    {

        Invoke(nameof(CloseDirectly), time);
    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void CloseDirectly()
    {
        gameObject.SetActive(false);
        if (isDestroyOnClose)
        {
            Destroy(this.gameObject);
        }
    }

}
