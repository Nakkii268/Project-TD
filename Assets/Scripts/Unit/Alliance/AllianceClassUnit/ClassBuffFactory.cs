using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassBuffFactory
{
    public static StatusEffect CreateClassBuff(Class c)
    {
        switch (c)
        {
            case Class.Guard:
                return CreateTestBuff();


            case Class.Defender:
                return CreateTestBuff();
            
            case Class.Sniper:
                return CreateTestBuff();

            case Class.Healer:
                return CreateTestBuff();

            case Class.Mage:
                return CreateTestBuff();

            default:
                return null;
        }
    }

    private static StatusEffect CreateTestBuff()
    {
        return ScriptableObject.CreateInstance<AttackBuff>();
    }

    //do create guard buff
    //do create defender buff
    //do create sniper buff
    //do create healer buff
    //do create mage buff

    //same thi for subclass
}
