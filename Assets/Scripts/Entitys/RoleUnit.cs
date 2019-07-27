using System.Collections.Generic;
using DefaultNamespace.SkillPlayable;
using UnityEngine;

namespace DefaultNamespace.Entitys
{
    public class RoleUnit : MonoBehaviour
    {
        private CharacterCtl _charaCtl;
        
        CharacterInfo _data;

        public ECamp camp;
        
        public bool alive = true;
        
        protected virtual void Awake()
        {
            _charaCtl = GetComponent<CharacterCtl>();
            _charaCtl.RoleUnit = this;
            InitInfoData();
        }


        private void InitInfoData()
        {
            _data = new CharacterInfo();
            _data.hpMax = 500;
            _data.hpCur = 500;
            _data.camp = camp;
        }
        
        public CharacterInfo GetInfoData()
        {
            return _data;
        }
        
        public CharacterCtl CharaCtl
        {
            get
            {
                return _charaCtl;
            }
        }

        public Vector3 Pos
        {
            get { return transform.position; }
        }

        public Vector3 Forward 
        {
            get { return transform.forward; }
            set { transform.forward = value; }
        }


        public void DamageTarget(RoleUnit roleOther, int dmg, Vector3 point)
        {
            if (roleOther.CharaCtl.GetFSM().CurState.type == EBSType.Parry)
            {
                dmg = 0;
                EffectUtil.CreateEffAPos("eff_spark_hit", point, Quaternion.identity);
            }
            else
            {
                EffectUtil.CreateEffAPos("eff_blood", point, Quaternion.identity);
            }
        
            roleOther.GetInfoData().hpCur -= dmg;

            if (dmg > 0)
            { 
                if (roleOther.GetInfoData().hpCur <= 0)
                {
                    roleOther.OnDead();
                }
                else
                {
                    //受击硬直
                    roleOther.CharaCtl.GetFSM().ActionStiff(14);      
                }
            }

            if (GameManager.Inst.targetRole == this)
            {
                //设置攻击目标
                GameManager.Inst.targetRole.atkTarget = roleOther;
            }
        }
        
        /// <summary>
        /// 死亡
        /// </summary>
        private void OnDead()
        {
            CharaCtl.HandleDead();
            alive = false;
        }
        
        /// <summary>
        /// 目标距离是否在指定值内
        /// </summary>
        /// <param name="target"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool CheckDisIsNear(RoleUnit target, float val)
        {
            if (target)
            {
                return (Pos - target.Pos).sqrMagnitude <= val * val;
            }
            return false;
        }

        
        /// <summary>
        /// 计算到区间的距离
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public float CalDisToRange(Vector3 pointA, Vector3 pointB)
        {
            var t1 = pointA;
            var t2 = pointB;
            if (pointA.x > pointB.x)
            {
                t1 = pointB;
                t2 = pointA;
            }

            if (Pos.x < t1.x)
            {
                return Vector3.Distance(Pos, t1);
            }

            if (Pos.x > t2.x)
            {
                return Vector3.Distance(Pos, t2);
            }

            return 0;
        }
        
        /// <summary>
        /// 目标是否朝向自己
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool CheckForwardSelf(RoleUnit target)
        {
            return target.transform.forward.x * (target.Pos.x - this.Pos.x) < 0;
        }
        
        protected  virtual  void Start()
        {
            
        }

        protected virtual void Update()
        {
           
        }
    }
}