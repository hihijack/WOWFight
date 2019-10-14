using System;

/// <summary>
/// 角色属性：生命，护甲等
/// </summary>
[Serializable]
public class CharacterInfo
{
    public int hpMax;
    public int hpCur;
    public ECamp camp;
    public float hpPercent {
        get { return (float)hpCur / hpMax; }
    }
}

/// <summary>
/// 阵营
/// </summary>
public enum ECamp
{
    Allies, 
    Monster
}
