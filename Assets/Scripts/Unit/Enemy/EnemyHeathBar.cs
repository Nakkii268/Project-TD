using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBar : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    [SerializeField] private Image HpBar;

    private void Start()
    {
        Enemy.OnHpChange += Enemy_OnHpChange;
        HpBar.fillAmount = 1;
       gameObject.SetActive(false);
    }

    private void Enemy_OnHpChange(object sender, float e)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        HpBar.fillAmount = e;
    }
}
