using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterNavigation : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    [SerializeField]
    private Transform m_PlayerTarget;

    [SerializeField]
    private Transform m_InjuryTarget;
    private Transform previousInjuryTarget;

    [SerializeField]
    private Transform goal_Target;

    [SerializeField]
    private GameObject navigationObject; 

    private RescueNPC rescueNPC;
    private GameObject Rescue;
    private RescueDiplication DiplicationScript;
    private RescueCount rescueCount;
    private float constantSpeed = 50f;
    private bool isNavigatingToPlayer = true;
    private bool useGoalTarget = false;
    public bool NaviUp = false;
    private float stoppingDistance = 20.0f;
    private Collider objectCollider;
    public Radio_ver4 radioVer4; 
    private SwitchCamera switchCameraScript; 
    private CharacterNavitail characterNavitail;

    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        characterNavitail = FindObjectOfType<CharacterNavitail>();
        rescueNPC = FindObjectOfType<RescueNPC>();
        rescueCount = FindObjectOfType<RescueCount>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        previousInjuryTarget = m_InjuryTarget;
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = constantSpeed;
        m_Agent.acceleration = 1000f;
        m_Agent.autoBraking = false;
        m_Agent.stoppingDistance = stoppingDistance;
        navigationObject.SetActive(false);
        objectCollider = GetComponent<Collider>();
        switchCameraScript = FindObjectOfType<SwitchCamera>();
        SetInjuryTarget();
        StartCoroutine(NavigationLoop());
    }

    void SetInjuryTarget()
    {
        GameObject injuryTargetObject = GameObject.FindGameObjectWithTag("SeriousInjuries");
        if (injuryTargetObject != null)
        {
            m_InjuryTarget = injuryTargetObject.transform;
        }
    }

    IEnumerator NavigationLoop()
    {
        while (true)
        {
            if (switchCameraScript.map_status || switchCameraScript.NiseMapON)
            {
                if (useGoalTarget && goal_Target != null)
                {
                    MoveToTarget(goal_Target);
                }
                else if (m_InjuryTarget != null)
                {
                    MoveToTarget(m_InjuryTarget);
                }
            }
            else if (m_PlayerTarget != null)
            {
                MoveToTarget(m_PlayerTarget);
            }

            while (!HasReachedDestination())
            {
                yield return null;
            }

            yield return new WaitForSeconds(0f); 
        }
    }

    void MoveToTarget(Transform target)
    {
        if (target != null)
        {
            m_Agent.SetDestination(target.position);

            if (target == m_PlayerTarget)
            {
                StartCoroutine(NotEnableColliderWithDelay());
            }
            else
            {
                StartCoroutine(EnableColliderWithDelay());
            }
        }
    }

    IEnumerator NotEnableColliderWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        objectCollider.enabled = false;
    }

    IEnumerator EnableColliderWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        objectCollider.enabled = true;
    }

    void Update()
    {
        if(rescueCount.getNum() == 30)
        {
            DisableNavigationFunctionality();
        }
        else
        {
            RotateTowardsMovementDirection();

            if (rescueNPC.IsItFollow() && DiplicationScript.getFlag())
            {
                NaviUp=true;
                ShowNavigationObject();
                useGoalTarget = true;
            }else{
                useGoalTarget = false;
            }

            if (m_InjuryTarget == null)
            {
                SetInjuryTarget();
                useGoalTarget = false;
            }

            if (previousInjuryTarget != m_InjuryTarget)
            {
                NaviUp = false;
                previousInjuryTarget = m_InjuryTarget;
            }
            CheckTextAndUpdateVisibility();
        }
    }

    void RotateTowardsMovementDirection()
    {
        if (m_Agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(m_Agent.velocity.normalized);
        }
    }

    bool HasReachedDestination()
    {
        if (!m_Agent.pathPending)
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void CheckTextAndUpdateVisibility()
    {
        if (radioVer4 != null)
        {
            string currentText = radioVer4.RadioText.text;

            if (IsTargetText(currentText))
            {
                ShowNavigationObject();
                NaviUp = true;
            }
            else
            {
                if (!NaviUp)
                {
                    HideNavigationObject();
                }
            }
        }
    }

    private bool IsTargetText(string text)
    {
        return !string.IsNullOrEmpty(text) &&
        (text.Contains("助かったよ！<sprite=1>の\r\n奥の方で人が倒れてたの!") ||
        text.Contains("<sprite=6>で人が動けないってい叫んでたわ") ||
        text.Contains("<sprite=5>で人が倒れていたわ") ||
        text.Contains("さっき<sprite=2>に入った人がでてこないの...") ||
        text.Contains("<sprite=3>で物が倒れて動けない人がいるの！") ||
        text.Contains("<sprite=4>で酔っぱらったやつが寝てて起きないんだ！助けてやってくれ"));
    }

    private void ShowNavigationObject()
    {
        if (navigationObject != null)
        {
            navigationObject.SetActive(true);
        }
    }

    private void HideNavigationObject()
    {
        if (navigationObject != null)
        {
            navigationObject.SetActive(false);
        }
    }

    private void DisableNavigationFunctionality()
    {
        StopAllCoroutines();
        m_Agent.isStopped = true;
        m_Agent.enabled = false;
        navigationObject.SetActive(false);
        Destroy(gameObject);
    }
}
