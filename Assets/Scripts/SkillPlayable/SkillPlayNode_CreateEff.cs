using DefaultNamespace.GameData;
using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{  
    public struct SkillPlayData_Eff
    {
        public string eff;
        public string point;//bind;ori;@bone
        public Vector3 offset;
    }
    
    public class SkillPlayNode_CreateEff : SkillPlayBehavNodeBase
    {
        public SkillPlayData_Eff effData;
        
        public override void OnPlay()
        {
            EffectUtil.CreateEffWithData(behavour.GetDirector().GetOwner().RoleUnit, effData);
        }

        public override void OnProcess()
        {
           
        }

        public override void OnExit()
        {
        }
        
        public static SkillPlayBehavNodeBase CreateBehavNode(PlayableNodeData playableNodeData)
        {
            var effData = GameDataMgr.Inst.playableNodeEffectTable.GetData(playableNodeData.targetId);
            var node = new SkillPlayNode_CreateEff
            {
                timeStart = playableNodeData.frameRange.start / 60f,
                timeDur = (playableNodeData.frameRange.end - playableNodeData.frameRange.start) / 60f,
                effData = new SkillPlayData_Eff
                {
                    eff = effData.eff,
                    point = effData.point,
                    offset = effData.offset
                }
            };
            return node;
        }
    }
}