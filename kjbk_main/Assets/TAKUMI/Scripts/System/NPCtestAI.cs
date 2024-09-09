using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class NPCtestAI : MonoBehaviour
{

    // 自分自身
    [SerializeField] private Transform _self;

    // ターゲット
    [SerializeField] private Transform _target;

    // 視野角（度数法）
    [SerializeField] private float _sightAngle;

    // 視界の最大距離
    [SerializeField] private float _maxDistance = float.PositiveInfinity;









    public Transform point_Living;
    public Transform point_Bed;
    public Transform point_Table;
    public Transform point_Toilet;
    public Text text;

    enum State
    {
        MoveToDestination,
        Eating,
        Sleeping,
        SitOnToilet,

        DoNothing,

        Escape,
        TalkNPC,
        gotoRescue,

    }

    State currentState = State.MoveToDestination;
    State targetState = State.DoNothing;
    bool stateEnter = false;
    float stateTime = 0f;

    enum Desire
    {
        Toilet,
        Eat,
        Sleep,
    }

    Dictionary<Desire, float> desireDictionary = new Dictionary<Desire, float>();


    void ChangeState(State newState)
    {
        currentState = newState;
        stateEnter = true;
        stateTime = 0f;
        Debug.Log(currentState.ToString());
    }

    NavMeshAgent navMeshAgent;
    Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        foreach (Desire desire in Enum.GetValues(typeof(Desire)))
        {
            desireDictionary.Add(desire, 0f);
        }

        ChangeState(State.MoveToDestination);

    }

    private void Update()
    {
        stateTime += Time.deltaTime;

        float speed = navMeshAgent.velocity.magnitude;

        animator.SetFloat("NPCspeed", speed);





        if (currentState != State.Eating)
        {
            desireDictionary[Desire.Eat] += Time.deltaTime / 5f;
        }

        if (currentState != State.Sleeping)
        {
            desireDictionary[Desire.Sleep] += Time.deltaTime / 10f;
        }

        if (currentState != State.SitOnToilet)
        {
            desireDictionary[Desire.Toilet] += Time.deltaTime / 7f;
        }

        var sortedDesire = desireDictionary.OrderByDescending(i => i.Value);

        text.text = "";

        foreach (var sortedDesireElement in sortedDesire)
        {
            text.text += sortedDesireElement.Key.ToString() + ":" + sortedDesireElement.Value + "\n";
        }





        switch (currentState)
        {

            case State.MoveToDestination:
                {
                    if (stateEnter)
                    {

                        var topDesireElement = sortedDesire.ElementAt(0);

                        if (topDesireElement.Value >= 1f)
                        {

                            switch (topDesireElement.Key)
                            {

                                case Desire.Eat:
                                    navMeshAgent.SetDestination(point_Table.position);
                                    targetState = State.Eating;
                                    break;

                                case Desire.Sleep:
                                    navMeshAgent.SetDestination(point_Bed.position);
                                    targetState = State.Sleeping;
                                    break;

                                case Desire.Toilet:
                                    navMeshAgent.SetDestination(point_Toilet.position);
                                    targetState = State.SitOnToilet;
                                    break;
                            }
                        }
                        else
                        {
                            navMeshAgent.SetDestination(point_Living.position);
                            targetState = State.DoNothing;
                        }


                    }

                    //目的地にたどり着いた
                    if (navMeshAgent.remainingDistance <= 0.01f && !navMeshAgent.pathPending)
                    {
                        ChangeState(targetState);
                        return;
                    }

                    return;
                }

            case State.DoNothing:
                {
                    if (stateEnter)
                    {

                    }

                    if (sortedDesire.ElementAt(0).Value >= 1)
                    {
                        ChangeState(State.MoveToDestination);
                        return;
                    }


                    return;
                }

            case State.Eating:
                {
                    if (stateEnter)
                    {

                    }

                    if (stateTime >= 3f)
                    {
                        desireDictionary[Desire.Eat] = 0f;
                        ChangeState(State.MoveToDestination);
                    }

                    return;
                }

            case State.Sleeping:
                {
                    if (stateEnter)
                    {

                    }

                    if (stateTime >= 5f)
                    {
                        desireDictionary[Desire.Sleep] = 0f;
                        ChangeState(State.MoveToDestination);
                    }

                    return;
                }

            case State.SitOnToilet:
                {
                    if (stateEnter)
                    {

                    }

                    if (stateTime >= 3f)
                    {
                        desireDictionary[Desire.Toilet] = 0f;
                        ChangeState(State.MoveToDestination);
                    }

                    return;
                }
        }
    }

    public bool IsVisible()
    {
        // 自身の位置
        var selfPos = _self.position;
        // ターゲットの位置
        var targetPos = _target.position;

        // 自身の向き（正規化されたベクトル）
        var selfDir = _self.forward;

        // ターゲットまでの向きと距離計算
        var targetDir = targetPos - selfPos;
        var targetDistance = targetDir.magnitude;

        // cos(θ/2)を計算
        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        // 自身とターゲットへの向きの内積計算
        // ターゲットへの向きベクトルを正規化する必要があることに注意
        var innerProduct = Vector3.Dot(selfDir, targetDir.normalized);

        // 視界判定
        return innerProduct > cosHalf && targetDistance < _maxDistance;
    }

    private void OnGUI()
    {
        // 視界判定
        var isVisible = IsVisible();

        // 結果表示
        GUI.Box(new Rect(20, 20, 150, 23), $"isVisible = {isVisible}");
    }

    private void LateUpdate()
    {

        if (stateTime != 0)
        {
            stateEnter = false;
        }

    }


}
