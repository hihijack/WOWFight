using UnityEngine;
using System.Collections;


public class PoolManager : MonoBehaviour
{
    static PoolManager inst;
    public static PoolManager Inst
    {
        get
        {
            if (inst == null)
            {
                GameObject gobj = new GameObject("PoolManager", typeof(PoolManager));
                inst = gobj.GetComponent<PoolManager>();
            }
            return inst;
        }
    }

   public GameObject GetFromPool(string resName)
    {
        GameObject gobj = null;
        Transform tfChild = transform.Find(resName);
        if (tfChild != null)
        {
            gobj = tfChild.gameObject;
        }

        if (gobj != null)
        {
            gobj.SetActive(true);
        }
        return gobj;
    }

    public void SendToPool(GameObject gobj, string resName)
    {
        if (gobj != null)
        {
            gobj.name = resName;
            gobj.transform.parent = transform;
            gobj.transform.localPosition = Vector3.zero;
            gobj.SetActive(false);
        }
    }
}
