using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfomationUI : MonoBehaviour //no need - delete
{
    [SerializeField] private TextMeshProUGUI _playerLevel;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Image _playerLevelProgress;
    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {

    }
}
