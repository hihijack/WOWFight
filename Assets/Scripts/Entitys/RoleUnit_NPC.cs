using System;
using BehaviorDesigner.Runtime;
using DefaultNamespace.AI;
using DefaultNamespace.AIBehavoirTree;
using DefaultNamespace.AICore;
using UnityEngine;

namespace DefaultNamespace.Entitys
{
    public class RoleUnit_NPC : RoleUnit
    {
        public  AICfg aiCfg;
        private AIGoal_Think brain;
        private AISensoryMemory _sensoryMemory;
        private BaseInput _cmds;

        private BehaviorTree _behaviorTree;
        
        public bool enableAI;
        
        protected override void Awake()
        {
            base.Awake();
            _sensoryMemory = new AISensoryMemory(this);
            brain = new AIGoal_Think(this);
            _cmds = new BaseInput();
            _behaviorTree = GetComponent<BehaviorTree>();
            _behaviorTree.SetVariableValue("owner", this);
        }

        protected override void Start()
        {
            base.Start();
            if (enableAI)
            {
                _behaviorTree.EnableBehavior();
            }
        }

        protected override void Update()
        {
            if (alive)
            {
                base.Update();
            
                _sensoryMemory.UpdateVision();

                if (!enableAI)
                {
                    _behaviorTree.DisableBehavior();
                }
                
                if (enableAI)
                {
/*                    brain.Process();
                    if (Time.frameCount % 60 == 0)
                    {
                        brain.Arbitare();
                    }*/
                    ProcSetData();
                }
            }
            else
            {
                _behaviorTree.StopAllCoroutines();
            }
        }

        /// <summary>
        /// 更新设置AI行为树需要的数据
        /// </summary>
        private void ProcSetData()
        {
           _behaviorTree.SetVariableValue("dangerDes", CalDangerDes());
           _behaviorTree.SetVariableValue("hpPercent", GetInfoData().hpPercent);
           _behaviorTree.SetVariableValue("target", GetSensoryMemory().target);
        }

        
        public float CalDangerDes()
        {
            float r = 0;
            RoleUnit target = GetSensoryMemory().target;
            if (target != null)
            {
                if (CheckDisIsNear(target, 2) && (target.CharaCtl.IsInState(EBSType.SKill) || target.CharaCtl.IsInState(EBSType.Power)))
                {
                    r = 1;
                }
            }
            return r;
        }
        
        public AISensoryMemory GetSensoryMemory()
        {
            return _sensoryMemory;
        }
    
        public float GetMoveSpeed(EAIGoalMoveType moveType)
        {
            float r = 0;
            switch (moveType)
            {
                case EAIGoalMoveType.Walk:
                    r = CharaCtl.runSpeed * 0.3f;
                    break;
                case EAIGoalMoveType.Run:
                    r = CharaCtl.runSpeed;
                    break;
                case EAIGoalMoveType.Rush:
                    r = CharaCtl.runSpeed * 1.5f;
                    break;
                case EAIGoalMoveType.Roll:
                    r = CharaCtl.rollSpeed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("moveType", moveType, null);
            }

            return r;
        }

        public bool IsAtPosition(Vector3 targetPos)
        {
            return (Pos - targetPos).sqrMagnitude <= 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="moveType"></param>
        /// <param name="keepFaceTo">保持朝向目标，可以为空</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void CommandMoveTo(Vector3 targetPos, EAIGoalMoveType moveType, RoleUnit keepFaceTo)
        {
            float h = Mathf.Sign(targetPos.x - Pos.x);
            switch (moveType)
            {
                case EAIGoalMoveType.Walk:
                    _cmds.cMove.h = 0.3f * h;
                    if (keepFaceTo)
                    {
                        _cmds.cMove.lookTarget = keepFaceTo.CharaCtl;
                    }
                    else
                    {
                        _cmds.cMove.lookTarget = null;
                    }
                    CharaCtl.SendCommand(_cmds.cMove);
                    break;
                case EAIGoalMoveType.Run:
                    _cmds.cMove.h = h;
                    _cmds.cMove.lookTarget = null;
                    CharaCtl.SendCommand(_cmds.cMove);
                    break;
                case EAIGoalMoveType.Rush:
                    _cmds.cRush.h = h;
                    CharaCtl.SendCommand(_cmds.cRush);
                    break;
                case EAIGoalMoveType.Roll:
                    _cmds.cRoll.dir = new Vector3(h, 0, 0);
                    CharaCtl.SendCommand(_cmds.cRoll);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("moveType", moveType, null);
            }
        }

        public void CommandStopRun()
        {
            _cmds.cMove.h = 0;
            CharaCtl.SendCommand(_cmds.cMove);
        }
        
        public AIGoal_Think GetBrain()
        {
            return brain;
        }

        public Vector3[] GetPatrolPos()
        {
            return aiCfg.patrolPoints;
        }

        public void CommandAttack(int skillID)
        {
            _cmds.cSkill.skillID = skillID;
            CharaCtl.SendCommand(_cmds.cSkill);
        }

        public void CommandParry()
        {
            CharaCtl.SendCommand(_cmds.cParry);
        }

        public void CommandJumpBack()
        {
            _cmds.cRoll.dir = Vector3.zero;
            CharaCtl.SendCommand(_cmds.cRoll);
        }

        public void CommandRoll(EAIRollDir dir)
        {
            if (dir == EAIRollDir.Forward)
            {
                _cmds.cRoll.dir = new Vector3(Forward.x, 0, 0);
            }else if (dir == EAIRollDir.Back)
            {
                _cmds.cRoll.dir = new Vector3(-1 * Forward.x, 0, 0);
            }
            
            CharaCtl.SendCommand(_cmds.cRoll);
        }
    }
}