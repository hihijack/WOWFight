using DefaultNamespace.GameData;

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
        
        public static SkillPlayBehavNodeBase CreateBehavNode(PlayableNodeData playableNodeData)
        {
            var animData = GameDataMgr.Inst.playableNodeAnimTable.GetData(playableNodeData.targetId);
            var node = new SkillPlayNode_PlayAnim
            {
                timeStart = playableNodeData.frameRange.start / 60f,
                timeDur = (playableNodeData.frameRange.end - playableNodeData.frameRange.start) / 60f,
                data = new SkillPlayData_Anim
                {
                    anim = animData.anim
                }
            };
            return node;
        }
    }
}