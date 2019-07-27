using System;
using DefaultNamespace.Entitys;
using DefaultNamespace.GameData;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{
    public class SkillDirector
    {
        private CharacterCtl owner;

        public float time;

        private int frame;
        
        /// <summary>
        /// 行为结束。自然结束时会触发
        /// </summary>
        public Action onBehavAutoEnd;
        
        private SkillPlayBehaviour curBehav;
        private int skillID;

        public SkillDirector(CharacterCtl owner)
        {
            this.owner = owner;
        }

        public CharacterCtl GetOwner()
        {
            return owner;
        }
        
        public void CreateBehav(int skillID)
        {
           curBehav = new SkillPlayBehaviour(this);
           this.skillID = skillID;
            SKillDataNode skillData = GameDataMgr.Inst.skillTable.GetData(skillID);
            if (skillData.isValid())
            {
                foreach (var playableNodeData in skillData.playableDatas)
                {
                    SkillPlayBehavNodeBase node = null;
                    if (playableNodeData.type == EPlayableNodeType.Effect)
                    {
                        node = CreatePlayableEffNode(playableNodeData);
                    }
                    else if (playableNodeData.type == EPlayableNodeType.PlayAnim)
                    {
                        node = CreatePlayableAnimNode(playableNodeData);
                    }
                    
                    AddBehavNode(node);
                }
            }
        }

        private SkillPlayBehavNodeBase CreatePlayableAnimNode(PlayableNodeData playableNodeData)
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

        private SkillPlayBehavNodeBase CreatePlayableEffNode(PlayableNodeData playableNodeData)
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

        public void AddBehavNode(SkillPlayBehavNodeBase node)
        {
            curBehav.AddBehavNode(node);
        }
        
        public void Process()
        {
            if (curBehav != null)
            {
                time += Time.deltaTime;
                frame++;
                curBehav.ProcessNodes();
                if (curBehav.IsEnd())
                {
                    OnBehavEnd();
                    if (onBehavAutoEnd != null)
                    {
                        onBehavAutoEnd();
                    }
                }
            }
            else
            {
                time = 0;
                frame = 0;
            }
        }

        /// <summary>
        /// 中断
        /// </summary>
        public void Stop()
        {
            OnBehavEnd();
        }

        private void OnBehavEnd()
        {
            curBehav = null;
            time = 0;
            frame = 0;
        }

        public int Frame
        {
            get { return frame; }
        }

        public int SkillId
        {
            get { return skillID; }
        }

        /// <summary>
        /// 取当前帧的伤害盒信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SKillRect[] GetCurDmgBox()
        {
            if (SkillId > 0 && Frame > 0)
            {
                SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(skillID);
                if (data.isValid())
                {
                    var skillBoxInfo = data.FindDmgBoxInfo(Frame);
                    if (skillBoxInfo.isValid())
                    {
                        return skillBoxInfo.rects;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 取当前身位盒信息
        /// </summary>
        /// <returns></returns>
        public SKillRect[] GetCurBodyBox()
        {
            if (SkillId > 0 && Frame > 0)
            {
                SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(skillID);
                if (data.isValid())
                {
                    var skillBoxInfo = data.FindBodyBoxInfo(Frame);
                    if (skillBoxInfo.isValid())
                    {
                        return skillBoxInfo.rects;
                    }
                }
            }
            return null;
        }
    }
}