// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using Aplib.Core;
using Aplib.Core.Belief.Beliefs;
using Aplib.Core.Belief.BeliefSets;
using Aplib.Integrations.Unity;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Goal = Aplib.Core.Desire.Goals.Goal<Testing.AplibTests.MyFirstBeliefSet>;
using Action = Aplib.Core.Intent.Actions.Action<Testing.AplibTests.MyFirstBeliefSet>;
using Tactic = Aplib.Core.Intent.Tactics.Tactic<Testing.AplibTests.MyFirstBeliefSet>;
using BdiAgent = Aplib.Core.Agents.BdiAgent<Testing.AplibTests.MyFirstBeliefSet>;

namespace Testing.AplibTests
{
    public class MyFirstBeliefSet : BeliefSet
    {

        /// <summary>
        /// The player object in the scene.
        /// </summary>
        public readonly Belief<GameObject, GameObject> Player = new(reference: GameObject.Find("Player"), x => x);

        /// <summary>
        /// The target position that the player needs to move towards.
        /// </summary>
        public readonly Belief<Transform, Vector3> TargetPosition =
            new(GameObject.Find("Target").transform, x => x.position);
    }

    public class MyFirstAplibTest
    {
        /// <summary>
        /// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use `yield return null;` to skip a frame.
        /// </summary>
        [UnityTest]
        public IEnumerator PerformMyFirstAplibTest()
        {
            // Arrange
            // Create a belief set for the agent.
            MyFirstBeliefSet beliefSet = new();

            // Create an intent for the agent that moves the agent towards the target position.
            Tactic moveTowardsTarget = new Action (
                beliefSet =>
                {
                    GameObject player = beliefSet.Player;
                    const float playerSpeed = 10f;
                    Vector3 playerPosition = player.transform.position;
                    Vector3 targetPosition = beliefSet.TargetPosition;
                    player.transform.position = Vector3.MoveTowards(playerPosition,
                        targetPosition,
                        maxDistanceDelta: playerSpeed * Time.deltaTime
                    );
                }
            );

            // Create a desire for the agent to reach the target position.
            Goal reachTargetGoal = new(
                moveTowardsTarget,
                beliefSet =>
                {
                    GameObject player = beliefSet.Player;
                    Vector3 playerPosition = player.transform.position;
                    Vector3 targetPosition = beliefSet.TargetPosition;
                    return Vector3.Distance(playerPosition, targetPosition) < 0.1f;
                }
            );

            // Setup the agent with the belief set and desire set and initialize the test runner.
            BdiAgent agent = new(beliefSet, reachTargetGoal.Lift().Lift());
            AplibRunner testRunner = new(agent);

            // Act
            yield return testRunner.Test();

            // Assert
            Assert.AreEqual(CompletionStatus.Success, agent.Status);
        }

        [SetUp]
        public void SetUp()
        {
            Debug.Log("Starting test MyFirstAplibTest");
            SceneManager.LoadScene("FirstTestScene");
        }
    }
}
