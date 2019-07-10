using UnityEngine;
using System.Collections;

public class EffLifeTimer : MonoBehaviour
{
    [Tooltip("存活时间")]
    public float lifeTime;

    bool usePool;
    float curLifeTime = 0;
    bool living = false;

    public delegate void OnEnd();

    public OnEnd onEnd;

    // Update is called once per frame
    void Update()
    {
        if (living)
        {
            curLifeTime += Time.deltaTime;
            if (curLifeTime > lifeTime)
            {
                OnLifeEnd();
            }
        }
    }

    void OnLifeEnd()
    {
        living = false;
        if (usePool)
        {
            PoolManager.Inst.SendToPool(gameObject, name);
        }
        else
        {
            gameObject.SetActive(false);
            DestroyObject(gameObject);
        }
        if (onEnd != null)
        {
            onEnd();
        }
    }

    /// <summary>
    /// 开始生命计时
    /// </summary>
    /// <param name="usePool">是否使用对象池</param>
    public void StartLife(float lifeTime, bool usePool = true)
    {
        curLifeTime = 0;
        living = true;
        this.lifeTime = lifeTime;
        this.usePool = usePool;
    }

    /// <summary>
    /// 开始生命计时
    /// </summary>
    /// <param name="usePool">是否使用对象池</param>
    public void StartLife(bool usePool = true)
    {
        curLifeTime = 0;
        living = true;
        this.usePool = usePool;
    }

    /// <summary>
    /// 立即结束生命周期
    /// </summary>
    public void KillAtOnce()
    {
        OnLifeEnd();
    }
}
