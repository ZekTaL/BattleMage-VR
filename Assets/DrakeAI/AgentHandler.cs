using BattleMage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    //Base
    public Agent AgentPrefab;
    List<Agent> agents = new List<Agent>();

    //Spawning
    #region Spawning
    [SerializeField] private float _SpawnInterval = 2f;

    private float insideRadius = 35f;
    private float outsideRadius = 45f;

    //change this to object pooling later
    private IEnumerator SpawnAgents()
    {
        while (!PlayerManager.PlayerDead)
        {
            yield return new WaitForSeconds(_SpawnInterval);
            _SpawnInterval -= _SpawnInterval * 0.01f;

            Spawn();
        }
    }
    private Agent Spawn()
    {
        float theta = Random.Range(0, 360);
        float randomRadius = Random.Range(insideRadius, outsideRadius);
        Vector3 SpawnPos = new Vector3(randomRadius * Mathf.Cos(theta), .5f, randomRadius * Mathf.Sin(theta));
        Agent newAgent = Instantiate(AgentPrefab, SpawnPos, Quaternion.LookRotation(-SpawnPos, Vector3.up));

        agents.Add(newAgent);
        return newAgent;
    }
    #endregion

        
    private void Awake()
    {
        StartCoroutine(SpawnAgents());
    }
    private void Update()
    {
        foreach (Agent agent in agents)
        {
            RunStateMachine(agent);
        }
    }
    private void RunStateMachine(Agent agent)
    {
        AgentState nextState = agent.currentState?.RunCurrentState(agent);
        if (nextState != agent.currentState)
        {
            agent.currentState = nextState;
        }
    }

}
