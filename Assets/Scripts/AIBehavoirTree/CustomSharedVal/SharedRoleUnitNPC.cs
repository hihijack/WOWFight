using BehaviorDesigner.Runtime;
using DefaultNamespace.Entitys;

namespace AIBehavoirTree.CustomSharedVal
{
    public class SharedRoleUnitNPC : SharedVariable<RoleUnit_NPC>
    {
        public static implicit operator SharedRoleUnitNPC(RoleUnit_NPC value) { return new SharedRoleUnitNPC { Value = value }; }
    }
}