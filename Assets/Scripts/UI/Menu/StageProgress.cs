using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{

    [SerializeField] private Progress CurrentProgress;
    [SerializeField] public Progress LastStage; //last stage player complete;
    [SerializeField] private StageSelectUI _stageSelectUI;
    [SerializeField]private int MaxChapter;
    
}
