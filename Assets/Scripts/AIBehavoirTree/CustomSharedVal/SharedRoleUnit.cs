using BehaviorDesigner.Runtime;
using DefaultNamespace.Entitys;

namespace AIBehavoirTree.CustomSharedVal
{
    public class SharedRoleUnit : SharedVariable<RoleUnit>
    {
        public static implicit operator SharedRoleUnit(RoleUnit value) { return new SharedRoleUnit { Value = value }; }
    }
}