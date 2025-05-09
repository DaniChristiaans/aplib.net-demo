using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Aplib.Core;
using Aplib.Core.Belief.BeliefSets;
using Unity.MLAgents;
using Aplib.Integrations.Unity.Actions;
using Aplib.Core.Belief.Beliefs;

namespace MLAgents4Aplib
{
    public class MoveRLAction<TBeliefSet> : UnityPathfinderAction<TBeliefSet> where TBeliefSet : IBeliefSet
    {
        public MoveRLAction(
            Func<TBeliefSet, MLAgentAplib> agentQuery,
            Func<TBeliefSet, Vector3> targetLocation,
            float heightOffset = 0f)
            : base(
                new Metadata(),
                beliefSet => agentQuery(beliefSet).GetComponent<Rigidbody>(),
                targetLocation,
                effect: RLAction(agentQuery),
                heightOffset)
        { }

        public MoveRLAction(
            Func<TBeliefSet, MLAgentAplib> agentQuery,
            Belief<Transform, Vector3> beliefTarget,
            float heightOffset = 0f)
            : this(
                agentQuery,
                beliefSet => beliefTarget.Observation,
                heightOffset)
        { }

        public MoveRLAction(
            Func<TBeliefSet, MLAgentAplib> agentQuery,
            Vector3 constantTarget,
            float heightOffset = 0f)
            : this(
                agentQuery,
                _ => constantTarget,
                heightOffset)
        { }

        private static Action<TBeliefSet, Vector3> RLAction(Func<TBeliefSet, MLAgentAplib> agentQuery)
        {
            return (beliefSet, destination) =>
            {
                var agent = agentQuery(beliefSet);
                if (agent != null)
                {
                    agent.SetTarget(destination);
                    agent.RequestDecision();  // Ask ML-Agent to make a move!
                }
            };
        }
    }
}
