using BehaviorDesigner.Runtime.Tasks;

namespace DefaultNamespace.AIBehavoirTree
{
    /// <summary>
    /// 翻滚方向
    /// </summary>
    public enum EAIRollDir
    {
        Forward, //前
        Back    //后
    }
    
    [TaskCategory("MyAI")]
    [TaskDescription("翻滚")]
    public class AIActRoll : Action
    {
        
    }
}