using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuild : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Build()
    {
        surface.BuildNavMesh();
    }
}
