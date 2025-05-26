using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragUnitSprite : UICanvas
{
    [SerializeField] private Image DragSprite;
    public override void SetUp(AllianceUnit unit)
    {
        DragSprite.sprite = unit.unitDragSprite;
    }
}
