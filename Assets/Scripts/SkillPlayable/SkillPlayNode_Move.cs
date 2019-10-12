using DefaultNamespace.GameData;
using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{
    public struct SkillPlayData_Move
    {
        public Vector2 speed;
    }
    
    public class SkillPlayNode_Move : SkillPlayBehavNodeBase
    {

        public SkillPlayData_Move data;
        
        public override void OnPlay()
        {
        }

        public override void OnProcess()
        {
            behavour.GetDirector().GetOwner().Translate(data.speed * Time.deltaTime);
        }

        public override void OnExit()
        {
        }

        public static SkillPlayBehavNodeBase CreateBehavNode(PlayableNodeData playableNodeData)
        {
            var moveData = GameDataMgr.Inst.palyableNodeMoveTable.GetData(playableNodeData.targetId);
            var node = new SkillPlayNode_Move
            {
                timeStart = playableNodeData.frameRange.start / 60f,
                timeDur = (playableNodeData.frameRange.end - playableNodeData.frameRange.start) / 60f,
                data = new SkillPlayData_Move
                {
                    speed = moveData.speed
                }
            };
            return node;
        }
    }
}