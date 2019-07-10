/// <summary>
/// 角色属性：生命，护甲等
/// </summary>
public class CharacterInfo
{
    public int hpMax;
    public int hpCur;
    public ECamp camp;
}

/// <summary>
/// 阵营
/// </summary>
public enum ECamp
{
    Allies, 
    Monster
}
