using DefaultNamespace.Command;

public class BaseInput
{
    /// <summary>
    /// 移动命令：参数：h：水平位移,v:竖直位移
    /// </summary>
    public CommandMove cMove;
    /// <summary>
    /// 轻攻击命令。参数：无
    /// </summary>
    public CommandAtkL cAtkL;
    /// <summary>
    /// 翻滚。参数：dir
    /// </summary>
    public CommandRoll cRoll;
    /// <summary>
    /// 重攻击。参数：是否按下状态
    /// </summary>
    public CommandAtkH cAtkH;
    /// <summary>
    /// 冲刺。参数：h：水平位移,v:竖直位移
    /// </summary>
    public CommandRush cRush;
    /// <summary>
    /// 防御：参数：无
    /// </summary>
    public CommandParry cParry;

    /// <summary>
    /// 技能招式:参数：技能ID
    /// </summary>
    public CommandSkill cSkill;
    
    public BaseInput()
    {
        cMove = new CommandMove();
        cAtkL = new CommandAtkL();
        cRoll = new CommandRoll();
        cAtkH = new CommandAtkH();
        cRush = new CommandRush();
        cParry = new CommandParry();
        cSkill = new CommandSkill();
    }

    public virtual void OnUpdate() { }
}