using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Player;


public class AIMover : MonoBehaviour
{
    AIController AiControl;
    [SerializeField] Transform target;

    public Transform spamPoint;
    // Start is called before the first frame updat

    // Update is called once per frame
    private void Start()
    {
        AiControl = GetComponent<AIController>();
    }
    void Update()
    {
        //GetComponent<NavMeshAgent>().destination = target.position;

        UpdateAnim();
    }

    void UpdateAnim()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    public void ChaseTarget()
    {
        Vector3 Target = AiControl.Target.transform.position;
        GetComponent<NavMeshAgent>().destination = Target;
    }

    public void StopChaseTarget()
    {
        //Vector3 Target
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().destination = spamPoint.position;
    }


    public void WithinRange()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void NotWithinRange()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
    }
}
