using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ChapterSO")]
public class ChapterSO : ScriptableObject
{
    public int ChapterIndex;
    public string ChapterID;
    public string ChapterName;
    public string StageUIPath;
    public int StageQuantity;
}
