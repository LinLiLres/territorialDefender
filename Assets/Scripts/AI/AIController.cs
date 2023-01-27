using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using System;

namespace RPG.Player {
    public class AIController : MonoBehaviour
    {
        [SerializeField] float AggroAreaDistance = 5f;

        public GameObject Target;
        [SerializeField] int ChaseDistance;
        [SerializeField] int DistanceToSpanPointReset=30;
        bool returningToPoint = false;
        bool foundTarget = false;
        float distanceToSpan;
        AIMover aiMover;
        Health health;
        EnemyTarget enemyTarget;

        // Start is called before the first frame update
        void Start()
        {
            Target = null;
            aiMover = GetComponent<AIMover>();
            health = GetComponent<Health>();
            enemyTarget = GetComponent<EnemyTarget>();

        }

        // Update is called once per frame
        private void Update()
        {
            if (!health.IsDead())
            {
                Aggro();
            }
            else
            {

            }
            if(Target != null)
            {
                Health targetHealth = Target.GetComponent<Health>();
                if (targetHealth.IsDead())
                {
                    ReturnToSpan();
                }
            }
            

        }

        private void Aggro()
        {
            distanceToSpan = Vector3.Distance(aiMover.spamPoint.transform.position, this.transform.position);
            if (distanceToSpan < 2)
            {
                returningToPoint = false;
            }

            if (!returningToPoint)
            {
                if (Target == null)
                {
                    SerachForTarget();
                }

                if (foundTarget && Target != null)
                {
                    FollowTheTarget();
                }
            }
        }

        void SerachForTarget()
        {
            Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Collider[] hitColliders = Physics.OverlapSphere(center, AggroAreaDistance);
            int i = 0;
            while(i < hitColliders.Length)
            {
                if(hitColliders[i].transform.tag == "Player")
                {
                    Target = hitColliders[i].transform.gameObject;
                    foundTarget = true;
                }
                i++;
            }
        }

        void FollowTheTarget()
        {
            Vector3 targetPosition = Target.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

            float distanceToPlayer = Vector3.Distance(Target.transform.position, this.transform.position);

            
            if(distanceToPlayer < ChaseDistance && distanceToSpan < DistanceToSpanPointReset)
            {
                if(distanceToPlayer < 2.5 && returningToPoint == false)
                {
                    GetComponent<Animator>().SetBool("CanAttack", true);
                    aiMover.WithinRange();
                    enemyTarget.Attack();
                    //GetComponent<Animator>().SetTrigger("BasicAttack1");
                }
                else
                {
                    GetComponent<Animator>().SetBool("CanAttack", false);
                    aiMover.NotWithinRange();
                    aiMover.ChaseTarget();
                }
                

            }
            else
            {
                ReturnToSpan();
            }
        }

        private void ReturnToSpan()
        {
            GetComponent<Animator>().SetBool("CanAttack", false);
            returningToPoint = true;
            foundTarget = false;
            Target = null;
            aiMover.StopChaseTarget();
        }

        //private float DistanceToplayer()
        //{
        //    GameObject player = GameObject.FindWithTag("Player");
        //    return Vector3.Distance(player.transform.position, transform.position);
        //}
    }

}
