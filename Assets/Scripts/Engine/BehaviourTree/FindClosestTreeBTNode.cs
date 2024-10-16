﻿using System;
using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class FindClosestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetTreeService(out TreeService treeService))
                return BTResult.FAILURE;
            
            GameObject character = blackboard.GetCharacter();
            
            if (!treeService.FindClosest(character.transform.position, out GameObject target))
            {
                blackboard.DelTarget();
                return BTResult.FAILURE;
            }
            
            blackboard.SetTarget(target);
            return BTResult.SUCCESS;
        }
    }
}