/// <summary>
/// 防御状态
/// </summary>
public class BattleStateParry : IBattleState
{
    int durFrame;
    int maxF = 23;
    public BattleStateParry(FSMManager manager) : base(manager)
    {
        type = EBSType.Parry;
    }

    public override void OnStart()
    {
        base.OnStart();
        durFrame = 0;
        fSMManager.ctl.OnBSStartParry();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        durFrame++;
        if (durFrame > maxF)
        {
            fSMManager.ActionIdle();
        }
    }

    public override IBattleState ActionIdle()
    {
        return fSMManager.bsIdle;
    }
}