using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] List<GameObject> targets = new List<GameObject>();

    private Animator animator;
    private NavMeshAgent agent;
    private PlayerScript playerScript;
    [SerializeField] private bool haveTarget = false;

    private Vector3 targetTransform;
    private GameObject currentlyStandingFloor;
    private GameObject brickSpawnContainer = null;

    private GameObject availableBridges;
    private GameObject targetBridge;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        StartCoroutine(GetTargets());
    }

    public void SetCurrentlyStandingFloor()
    {
        currentlyStandingFloor = playerScript.currentlyStandingFloor;
        brickSpawnContainer = currentlyStandingFloor.transform.GetChild(1).gameObject;
        availableBridges = currentlyStandingFloor.transform.GetChild(2).gameObject;

        // select a bridge
        targetBridge = availableBridges.transform.GetChild(Random.Range(0,availableBridges.transform.childCount)).transform.GetChild(0).gameObject;
    }

    public IEnumerator GetTargets()
    {
        if (targets.Count == 0)
        {
            while (brickSpawnContainer == null)
            {
                yield return new WaitForSeconds(0.5f);
            }

            //yield return new WaitForSeconds(1f);

            for (int i = 0; i < brickSpawnContainer.transform.childCount; i++)
            {
                if (Color.Equals(playerScript.playerColor, brickSpawnContainer.transform.GetChild(i).gameObject.GetComponent<Collectable>().color))
                {
                    targets.Add(brickSpawnContainer.transform.GetChild(i).gameObject);
                }
            }

            haveTarget = false;
        }
    }

    public void RemoveTargetFromList(GameObject obj)
    {
        targets.Remove(obj);
        haveTarget = false;
    }

    void Update()
    {
        HandleRotation();
        AIStates();
    }

    void AIStates()
    {
        if (!haveTarget && targets.Count > 0)
        {
            // move to target
            targetTransform = targets[0].gameObject.transform.position;
            agent.SetDestination(targetTransform);
            animator.SetBool("Running", true);
            haveTarget = true;
        }
        else if (!haveTarget && targets.Count == 0 && gameObject.GetComponent<StackManager>().isPopable())
        {
            // place bricks to the bridge
            if (targetBridge == null)
                return;

            targetTransform = targetBridge.transform.position;
            agent.SetDestination(targetTransform);
            haveTarget = true;
        }
        else if (haveTarget && targets.Count == 0 && Vector3.Distance(gameObject.transform.position, targetTransform) < 0.3f)
        {
            // go to next bridge
            Debug.Log("Next Area " + currentlyStandingFloor.name);
            StartCoroutine(GetTargets());
            haveTarget = false;

        }
    }

    public void ClearTarget()
    {
        targetTransform = new Vector3(0,0,0);
    }

    private void HandleRotation()
    {
        float targetAngle = Mathf.Atan2(-targetTransform.x, -targetTransform.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    IEnumerator GoFakeTarget()
    {
        Debug.Log("Going fake target " + gameObject.name);
        yield return new WaitForSeconds(2.5f);
        haveTarget = false;
    }

    private void OnDrawGizmos()
    {
        if(playerScript != null)
        {
            Gizmos.color = playerScript.playerColor;
            Gizmos.DrawSphere(targetTransform, 1);
        }
    }

}
