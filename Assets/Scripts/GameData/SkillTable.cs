using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.GameData
{
    [CreateAssetMenu]
    public class SkillTable : ScriptableObject
    {
        [TableList(ShowIndexLabels = false)] public SKillDataNode[] data;

        public SKillDataNode GetData(int skillId)
        {
            foreach (var t in data)
            {
                if (t.id == skillId)
                {
                    return t;
                }
            }

            return new SKillDataNode();
        }
    }

    [Serializable]
    public struct SKillDataNode
    {
        public int id;
        public string name;
        
        [Header("效果")]
        public SkillDmgInfo[] dmgInfos;
        
        [Title("身位盒", HorizontalLine = false)] public SkillBoxInfo[] bodys;
        [Header("伤害盒")] public SkillBoxInfo[] dmgBoxs;

        [Title("表现")]
        public PlayableNodeData[] playableDatas;

        public bool isValid()
        {
            return id > 0;
        }

        /// <summary>
        /// 找出指定帧的盒信息
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public SkillBoxInfo FindDmgBoxInfo(int frame)
        {
            if (dmgBoxs != null && dmgBoxs.Length > 0)
            {
                foreach (var t in dmgBoxs)
                {
                    if (frame >= t.frameRanges.start && frame <= t.frameRanges.end)
                    {
                        return t;
                    }
                }
            }

            return new SkillBoxInfo();
        }

        public SkillBoxInfo FindBodyBoxInfo(int frame)
        {
            if (bodys != null && bodys.Length > 0)
            {
                foreach (var t in bodys)
                {
                    if (frame >= t.frameRanges.start && frame <= t.frameRanges.end)
                    {
                        return t;
                    }
                }
            }

            return new SkillBoxInfo();
        }
    }

    /// <summary>
    /// 技能效果信息
    /// </summary>
    [Serializable]
    public struct SkillDmgInfo
    {
        [Header("招式类型")]
        public ESkillTrickType trickType;
        [Header("伤害系数")]
        public float dmg;
    }
    
    [Serializable]
    public struct SkillBoxInfo
    {
        [Title("帧范围", HorizontalLine = false)] public SkillFramRange frameRanges;
        public SKillRect[] rects;

        public bool isValid()
        {
            return rects != null && rects.Length > 0;
        }
    }

    [Serializable]
    public struct SKillRect
    {
        public Vector2 center;
        public Vector2 size;
    }

    [Serializable]
    public struct SkillFramRange
    {
        public int start;
        public int end;
    }


    public enum EPlayableNodeType
    {
        PlayAnim,
        Effect
    }

    /// <summary>
    /// 技能招式分类
    /// </summary>
    public enum ESkillTrickType
    {
        Normal,//普通
        Stab, //刺击
        Proj, //投射物
        Engrgy //能量
    }
    
    [Serializable]
    public struct PlayableNodeData
    {
        public SkillFramRange frameRange;
        public EPlayableNodeType type;
        public int targetId;
    }

}