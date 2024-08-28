using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuild : MonoBehaviour
{

    #region NavMeshSurfaceの取得
    // NavMeshSurfaceコンポーネントの参照
    [SerializeField] private NavMeshSurface surface;
    #endregion


    #region Build関数
    // NavMeshをビルドする関数
    public void Build()
    {
        // NavMeshをビルド
        surface.BuildNavMesh();
    }
    #endregion

}
