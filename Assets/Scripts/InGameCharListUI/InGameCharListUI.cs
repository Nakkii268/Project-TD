using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharListUI : MonoBehaviour
{
    [SerializeField] private List<InGameCharUI> charList;
    [SerializeField] private LevelDPManager levelDPManager;


    private void Start()
    {
        foreach (InGameCharUI single in charList)
        {
            single.OnCharDrop += Single_OnCharDrop;
            single.OnCharSelect += Single_OnCharSelect;
        }
    }

    private void Single_OnCharSelect(object sender, System.EventArgs e)
    {
        LevelManager.instance.HandleRaycast();
    }

    private void Single_OnCharDrop(object sender, CharacterData e)
    {
        LevelManager.instance.HanlderOnCharDrop(e);
        charList[e.charIndex].gameObject.SetActive(false);
        
    }

    public List<InGameCharUI> GetCharList()
    {
        return charList;    
    }


    
}
