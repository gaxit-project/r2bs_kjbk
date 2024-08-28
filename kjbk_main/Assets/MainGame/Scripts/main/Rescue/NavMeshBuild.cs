using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuild : MonoBehaviour
{

    #region NavMeshSurface�̎擾
    // NavMeshSurface�R���|�[�l���g�̎Q��
    [SerializeField] private NavMeshSurface surface;
    #endregion


    #region Build�֐�
    // NavMesh���r���h����֐�
    public void Build()
    {
        // NavMesh���r���h
        surface.BuildNavMesh();
    }
    #endregion

}
