namespace DefaultNamespace.SkillPlayable
{
    public struct SkillPlayData_Anim
    {
        public string anim;
    }
    
    public class SkillPlayNode_PlayAnim : SkillPlayBehavNodeBase
    {
        public SkillPlayData_Anim data;
        
        public override void OnPlay()
        {
           behavour.GetDirector().GetOwner().animCtl.Play(data.anim);
        }

        public override void OnProcess()
        {
        }

        public override void OnExit()
        {
        }
    }
}