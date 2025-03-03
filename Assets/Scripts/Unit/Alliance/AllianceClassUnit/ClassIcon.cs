using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ClassIcon 
{
    private static Dictionary<Class,Sprite> IconCache = new Dictionary<Class,Sprite>();
    public static void LoadIcon(Class c)
    {
       if( IconCache.TryGetValue(c, out Sprite icon))
        {
            
            return;
        }
        Addressables.LoadAssetAsync<Sprite>(GetClassIconAddress(c)).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) { 
                IconCache.Add(c, handle.Result);
                
            }
        };
   

    }
    private static string GetClassIconAddress(Class c)
    {
        switch (c)
        {
            case Class.Guard: return "ClassIcon[Guard_Icon]";
            case Class.Defender: return "ClassIcon[Degender_Icon]";
            case Class.Sniper: return "ClassIcon[Sniper_Icon]";
            case Class.Mage: return "ClassIcon[Caster_Icon]";
            case Class.Healer: return "ClassIcon[Healer_Icon]";
            case Class.Vanguard: return "ClassIcon[Vanguar_Icon]";
            default: return null;
        }
    }
}
