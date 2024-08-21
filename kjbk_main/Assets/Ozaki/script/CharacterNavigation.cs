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
    private GameObject navigationObject; // Reference to the navigation object

    private RescueNPC rescueNPC;
    private GameObject Rescue;
    private RescueDiplication DiplicationScript;
    private RescueCount rescueCount;
    private float constantSpeed = 70f;
    private bool isNavigatingToPlayer = true;
    private bool useGoalTarget = false;
    private bool NaviUp = false;
    private float stoppingDistance = 20.0f;
    private Collider objectCollider;
    public Radio_ver4 radioVer4; // Reference to Radio_ver4

    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        rescueNPC = FindObjectOfType<RescueNPC>();
        rescueCount = FindObjectOfType<RescueCount>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        previousInjuryTarget = m_InjuryTarget; // Save the initial target
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = constantSpeed;
        m_Agent.acceleration = 1000f;
        m_Agent.autoBraking = false;
        m_Agent.stoppingDistance = stoppingDistance;
        navigationObject.SetActive(false);
        SetInjuryTarget();
        objectCollider = GetComponent<Collider>();
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
            if (!useGoalTarget)
            {
                if (isNavigatingToPlayer && m_PlayerTarget != null)
                {
                    MoveToTarget(m_PlayerTarget);

                    while (!HasReachedDestination())
                    {
                        yield return null;
                    }

                    isNavigatingToPlayer = false;
                }
                else if (!isNavigatingToPlayer && m_InjuryTarget != null)
                {
                    MoveToTarget(m_InjuryTarget);

                    while (!HasReachedDestination())
                    {
                        yield return null;
                    }

                    yield return new WaitForSeconds(0f); // 1-second wait
                    isNavigatingToPlayer = true;
                }
            }
            else
            {
                if (isNavigatingToPlayer && goal_Target != null)
                {
                    MoveToTarget(goal_Target);

                    while (!HasReachedDestination())
                    {
                        yield return null;
                    }
                    isNavigatingToPlayer = false;
                }
                else if (!isNavigatingToPlayer && m_InjuryTarget != null)
                {
                    MoveToTarget(m_InjuryTarget);

                    while (!HasReachedDestination())
                    {
                        yield return null;
                    }

                    yield return new WaitForSeconds(0f); // 1-second wait
                    isNavigatingToPlayer = true;
                }
            }
        }
    }

    void MoveToTarget(Transform target)
    {
        if (target != null)
        {
            m_Agent.SetDestination(target.position);

            // Enable or disable the collider based on the target
            if (target == m_PlayerTarget)
            {
                objectCollider.enabled = false; // Disable the collider when navigating to the player
            }
            else
            {
                StartCoroutine(EnableColliderWithDelay()); // Enable the collider for other targets
            }
        }
    }

    IEnumerator EnableColliderWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        objectCollider.enabled = true; // Enable the collider
    }

    void Update()
    {
        if(rescueCount.getNum()==30){
            DisableNavigationFunctionality(); // Disable functionality
        }else{
            RotateTowardsMovementDirection();

            if (rescueNPC.IsItFollow() && DiplicationScript.getFlag())
            {
                useGoalTarget = true; // Switch to using goal_Target
            }

            if (m_InjuryTarget == null)
            {
                SetInjuryTarget(); // Reset m_InjuryTarget
                useGoalTarget = false;
            }

            if (previousInjuryTarget != m_InjuryTarget)
            {
                NaviUp = false; // Set NaviUp to false when the target changes
                previousInjuryTarget = m_InjuryTarget; // Save the current target
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
                NaviUp=true;
            }
            else
            {
                if(!NaviUp){
                    HideNavigationObject();
                }
            }
        }
        else
        {
            Debug.LogWarning("Radio_ver4 is not assigned.");
        }
    }

    private bool IsTargetText(string text)
    {
        return !string.IsNullOrEmpty(text) && 
        (text.Contains("助かったよ！<sprite=1>の\r\n奥の方で人が倒れてたの!")||
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
        else
        {
            Debug.LogWarning("NavigationObject is not assigned.");
        }
    }

    private void HideNavigationObject()
    {
        if (navigationObject != null)
        {
            navigationObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("NavigationObject is not assigned.");
        }
    }

    private void DisableNavigationFunctionality()
    {
        StopAllCoroutines(); // Stop all coroutines
        m_Agent.isStopped = true; // Stop navigation
        m_Agent.enabled = false; // Disable NavMeshAgent
        navigationObject.SetActive(false); // Hide navigationObject
        Destroy(gameObject);
    }
}
