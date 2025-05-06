using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : MonoBehaviour
{
    [SerializeField] public CharacterInfoUI characterInfoUI;
    [SerializeField] private List<Button> buttonList;
    [SerializeField] private List<AllianceUnit> unitList;
    private void Start()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.AddListener(() =>
            {
                characterInfoUI.Initialized(unitList[i]);
            });
           // buttonList[i].image = unitList[i].unitSprite.toi;
        }
    }
}
