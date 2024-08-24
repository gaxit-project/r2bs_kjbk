using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class navbuild : MonoBehaviour
{
    public float time = 0;
    public GameObject obj;

    private NavMeshSurface surface;
    private int flag = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        surface = this.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        //ŽžŠÔ‚ð”‚¦‚é
        time += Time.deltaTime;

        if(flag == 0 && time > 10)
        {
            Instantiate(obj, new Vector3(50.0f, 5.0f, 64.0f), Quaternion.identity);
            flag = 1;
            //surface.BuildNavMesh();
        }
        else if(flag == 1 && time > 20)
        {
            Instantiate(obj, new Vector3(-50.0f, 5.0f, -20.0f), Quaternion.identity);
            flag = 2;
            //surface.BuildNavMesh();
        }
    }
}
