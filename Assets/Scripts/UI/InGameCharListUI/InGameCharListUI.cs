using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharListUI : MonoBehaviour
{
    public static InGameCharListUI Instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<InGameCharUI> charList = new List<InGameCharUI>();
    [SerializeField] private GameObject dragUnitSprite;
    [SerializeField] private int currentUnitDeloy;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        List<LineUpSave> unitlist = GameManager.Instance._playerDataManager.PlayerDataSO.GetLineUp();
       
        for (int i = 0; i < unitlist.Count; i++)
        {
            GameObject unit = Instantiate(prefab.gameObject, this.transform);
            InGameCharUI single = unit.GetComponent<InGameCharUI>();
            single.Init(unitlist[i].Unit, i, unitlist[i].SkillIndex,dragUnitSprite);   
            single.OnCharDrop += Single_OnCharDrop;
            single.OnCharSelect += Single_OnCharSelect;
            charList.Add(single);
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
