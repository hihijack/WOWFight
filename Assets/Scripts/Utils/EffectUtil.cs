using DefaultNamespace;
using DefaultNamespace.Entitys;
using DefaultNamespace.SkillPlayable;
using UnityEditor;
using UnityEngine;

public static class EffectUtil
{
    private static GameObject effRoot;
    internal static void CreateEffAPos(string eff, Vector3 point, Quaternion rot, Transform parent = null)
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

        gobjEff.name = eff;
       
        if (parent != null)
        {
            gobjEff.transform.parent = parent;
            gobjEff.transform.localPosition = point;
            gobjEff.transform.localRotation = rot;
        }
        else
        {
            gobjEff.transform.parent = effRoot.transform;
            gobjEff.transform.position = point;
            gobjEff.transform.rotation = rot;
        }
        
       
        EffCtl effCtl = GameUtil.GetOrAdd<EffCtl>(gobjEff);
           
        effCtl.Play();
            
    }

    public static void CreateEffWithData(RoleUnit role, SkillPlayData_Eff effData)
    {
        if (effData.point == "ori")
        {
            CreateEffAPos(effData.eff, role.transform.localToWorldMatrix.MultiplyPoint(effData.offset), Quaternion.identity);
        }
        else if (effData.point == "bind")
        {
            CreateEffAPos(effData.eff, effData.offset, Quaternion.identity, role.transform);
        }
    }
}