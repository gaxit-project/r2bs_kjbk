using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Rescue_NPC Rescue_NPC;

    //NavMeshAgentŠÖŒW
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] public Transform RescuePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Rescue_NPC.IsItNPCrun() && !Rescue_NPC.IsItActiveIcon())
        {
            _navMeshAgent.destination = RescuePoint.position;
        }
    }
}
