using BattleMage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    //Base
   
    public List<Agent> agents = new List<Agent>();

    #region Singleton
    public static AgentHandler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    //Spawning
    #region Spawning
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public Agent AgentPrefab;
        [Range(5, 30)] public int PoolSize;
    }

    

    public List<Pool> pools;
    public Dictionary<string, Queue<Agent>> poolDictionary;
    private void FirstSpawn()
    {
        poolDictionary = new Dictionary<string, Queue<Agent>>();

        foreach (Pool pool in pools)
        {
            Queue<Agent> AgentPool = new Queue<Agent>();
            for (int i = 0; i < pool.PoolSize; i++)
            {
                Vector3 spawnPos = GetSpawnPos();
                Agent newAgent = Instantiate(pool.AgentPrefab, spawnPos, Quaternion.LookRotation(-spawnPos, Vector3.up));
                newAgent.name = "Agent " + i;
                newAgent.gameObject.SetActive(false);
                AgentPool.Enqueue(newAgent);
                agents.Add(newAgent);
            }
            poolDictionary.Add(pool.tag, AgentPool);
        }
    }
    
    public void SpawnFromPool(string tag, Agent agent)
    {       
        if (agent.Respawn)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " does not exist.");
                return;
            }
            float theta = Random.Range(0, 360);
            float randomRadius = Random.Range(insideRadius, outsideRadius);
            Vector3 SpawnPos = new Vector3(randomRadius * Mathf.Cos(theta), .5f, randomRadius * Mathf.Sin(theta));

            
            Agent agentToSpawn = agent;
            Debug.Log("the agent is " + agent.name + " and the Agent to Spawn is " + agentToSpawn.name);

            agentToSpawn.gameObject.SetActive(true);
            agentToSpawn.transform.position = SpawnPos;
            agentToSpawn.transform.rotation = Quaternion.LookRotation(-SpawnPos, Vector3.up);

            poolDictionary[tag].Enqueue(agentToSpawn);
            agent.Respawn = false;
            agent.IsAlive = true;
        }
    }


    [SerializeField] private float _SpawnInterval = 2f;
    private float insideRadius = 35f;
    private float outsideRadius = 45f;

    //change this to object pooling later
    
    public Vector3 GetSpawnPos()
    {
        float theta = Random.Range(0, 360);
        float randomRadius = Random.Range(insideRadius, outsideRadius);
        Vector3 SpawnPos = new Vector3(randomRadius * Mathf.Cos(theta), .5f, randomRadius * Mathf.Sin(theta));
        return SpawnPos;
    }

    #endregion

    //Behaviour
    #region Behaviour
    public void MoveToPlayer(Agent agent, float Gravity)
    {
        if (agent.IsGrounded)
        {

        }
    }
    #endregion
    private void Start()
    {
        FirstSpawn();
    }
    private void Update()
    {
        //if the agent is dead, then recycle them
        
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
