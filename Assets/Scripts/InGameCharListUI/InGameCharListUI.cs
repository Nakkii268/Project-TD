using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharListUI : MonoBehaviour
{
    public static InGameCharListUI Instance;
    [SerializeField] private List<InGameCharUI> charList;
    [SerializeField] private LevelDPManager levelDPManager;
    [SerializeField] private int currentUnitDeloy;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (InGameCharUI single in charList)
        {
            single.OnCharDrop += Single_OnCharDrop;
            single.OnCharSelect += Single_OnCharSelect;
            single.Initialized();
        }
    }

    private void Single_OnCharSelect(object sender, System.EventArgs e)
    {
        LevelManager.instance.MapManager.HandleRaycast();
    }

    private void Single_OnCharDrop(object sender, CharacterData e)
    {
        LevelManager.instance.MapManager.HanlderOnCharDrop(e);
        currentUnitDeloy  = e.charIndex;
        
    }

    public List<InGameCharUI> GetCharList()
    {
        return charList;    
    }
    public void HideDeloyedUnitUI(int index)
    {
        charList[index].gameObject.SetActive(false);
        gameObject.GetComponent<CustomUI>().ReArrangeChild();
    }
    public void ShowRetreatedUnitUI(int index)
    {
        charList[index].gameObject.SetActive(true);
        charList[index].InitCountDown();
        gameObject.GetComponent<CustomUI>().ReArrangeChild();
    }
    
}
