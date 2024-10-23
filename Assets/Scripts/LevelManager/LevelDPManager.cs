using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDPManager : MonoBehaviour
{
    [SerializeField] private float CurrentDp;
    [SerializeField] private int StartDp;
    [SerializeField] private int MaxDp;
    [SerializeField] private float DpRegenRate;
    [SerializeField] private TextMeshProUGUI dptext;
    [SerializeField] private Transform dpMaxUI;

    private void Start()
    {
        CurrentDp = StartDp;
        dptext.text = Mathf.FloorToInt(CurrentDp).ToString();

    }

    private void Update()
    {
        if (CurrentDp < MaxDp) {
             CurrentDp +=  DpRegenRate * Time.deltaTime;
            dptext.text = Mathf.FloorToInt(CurrentDp).ToString();
            dpMaxUI.gameObject.SetActive(false);

        }
        else
        {
            dpMaxUI.gameObject.SetActive(true);
        }
    }
    public float GetCurrentDp()
    {
        return CurrentDp;
    }
    public void AddingDP(int amount)
    {
        if (CurrentDp >= MaxDp) return;
        CurrentDp += amount;
        if (CurrentDp > MaxDp)
        {
            CurrentDp = MaxDp;
        }
    }
    public void ReduceDP(int amount)
    {
        CurrentDp -= amount;
        if (CurrentDp < 0)
        {
            CurrentDp = 0;
        }
    }
}
