using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navi : MonoBehaviour
{
    public RescueNPC RescueNPC;

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
        if (RescueNPC.IsItNPCrun() && !RescueNPC.IsItActiveIcon())
        {
            _navMeshAgent.destination = RescuePoint.position;
        }
    }
}
