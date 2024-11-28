using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceStat : MonoBehaviour
{
    [SerializeField] private int unitLevel;
    [SerializeField] private Alliance alliance;
    [SerializeField] private float currentHp;
    public Stat MaxHp;
    public Stat Attack;
    public Stat AttackInterval;
    public Stat Defense;
    public Stat Resistance;
    public Stat RedeployTime;
    public AllianceUnit unit;
    public Vector2Int[] AttackRange;

    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        unit = alliance.GetAllianceUnit();
        MaxHp = new Stat(unit.Heath);
        Attack = new Stat(unit.Attack);
        AttackInterval = new Stat(unit.AttackInterval);
        Defense = new Stat(unit.Defense);
        Resistance = new Stat(unit.Resistance);
        AttackRange = unit.AttackRange;

    }
}
