using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MapSO")]
public class MapSO : ScriptableObject
{
    public int StageIndex;
    public int ChapterIndex;
    public string MapName;
    public string StagePath;
    public Wave[] Waves;
    public int TotalEnemy;
    public int LifePoint;
    public float StartDeployPoint;
    public float DpRecoveryRate;
    public int DeployLimit;
    //stamina cost
    //droped material
    public List<ItemsData> DropItem;
}
[Serializable]
public class Wave
{
    public float SpawnTime;
    public Vector2[] Path;
    public EnemyUnit EnemyUnit;
    public int EnemyQuantity;
    public float SpawnDelay; // delay time btw each enemy spawn in wave
    public Vector3 SpawnPos;

}