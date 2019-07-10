using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public static class EffectUtil
{
    private static GameObject effRoot;
    internal static void CreateEffAPos(string eff, Vector3 point, Quaternion rot)
    {
        if (effRoot == null)
        {
            effRoot = new GameObject("_EffRoot");
        }
        GameObject gobjEff = PoolManager.Inst.GetFromPool(eff);
        if (gobjEff == null)
        {
            gobjEff = Object.Instantiate(Resources.Load<GameObject>("Eff/" + eff));
        }

        gobjEff.transform.parent = effRoot.transform;
        gobjEff.name = eff;
        gobjEff.transform.position = point;
        gobjEff.transform.rotation = rot;
        EffCtl effCtl = GameUtil.GetOrAdd<EffCtl>(gobjEff);
           
        effCtl.Play();
            
    }
}