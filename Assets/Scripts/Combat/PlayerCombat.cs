using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player;
using RPG.States;
using System;

namespace RPG.Combat
{

    public class PlayerCombat : MonoBehaviour
    {
        public float autoAttackRange;
        public bool canAutoAttackAngle;
        PlayerController playerControl;
        public bool attacking = false;
        PlayerStates pStates;
        public bool inRange = false;
        public int canAttack = 1;
        

        public bool Attacking()
        {
            return attacking;
        }
        // Start is called before the first frame update
        void Start()
        {
            playerControl = GetComponent<PlayerController>();
            pStates = GetComponent<PlayerStates>();
        }

        // Update is called once per frame
        void Update()
        {
            if(playerControl.selectedEnemy != null)
            {
                Health target = playerControl.selectedEnemy.GetComponent<Health>();

                if(playerControl.selectedEnemy !=null && !target.IsDead())
                {
                    autoAttackAngle();
                    checkRange();
                    if (attacking == true)
                    {
                        StartAutoAttack();
                    }
                }else if(playerControl.selectedEnemy != null && target.IsDead())
                {
                    attacking = false;
                    playerControl.anim.SetBool("InCombat", false);
                }

            }
            if(playerControl.selectedEnemy == null)
            {
                attacking = false;
            }

        }
        public void selectEnemy(EnemyTarget target) //left click on enemy
        {
            print("EnemySelected");
        }

        public void autoAttack(EnemyTarget target)
        {
            print("staring autoAttack if we are within the range");
            StartAutoAttack();

           

            if (playerControl.selectedEnemy != null && !inRange)
            {
                print("not within range to autoattack");
                StartAutoAttack();
            }
            else if(playerControl.selectedEnemy != null && inRange && canAutoAttackAngle)
            {
                print("within range to autoattack + angle");
                StartAutoAttack();
            }
            else if(playerControl.selectedEnemy != null && inRange && !canAutoAttackAngle)
            {
                print("I am facing the wrong way");
                StartAutoAttack();
            }
        }
        void autoAttackAngle()
        {
            //Vector3 targetDir = playerControl.selectedEnemy.transform.position - transform.position;
            //Vector3 forward = transform.forward;
            //float angle = Vector3.Angle(targetDir, forward);

            //if(angle > 60)
            //{
            //    canAutoAttackAngle = false;
            //}
            //else
            //{
            //    canAutoAttackAngle = true;
            //}
            canAutoAttackAngle = true;

        }
        void StartAutoAttack()
        {

            Health target = playerControl.selectedEnemy.GetComponent<Health>();
            if (!target.IsDead())
            {

                attacking = true;

                if (playerControl.standinStill == true)
                {
                    playerControl.anim.SetBool("InCombat", true);
                }
                else
                {
                    playerControl.anim.SetBool("InCombat", false);
                }
                if (playerControl.selectedEnemy != null && canAutoAttackAngle && inRange && canAttack == 1)
                {
                    StartCoroutine(basisAttack());
                }
            }
        }

        public Health other;

        IEnumerator basisAttack()
        {
            canAttack = 0;
            playerControl.anim.SetTrigger("AttackNow");

            //other.AnimHit();
            AnimHit();
            yield return new WaitForSeconds(pStates.AutoAttackSpeed);
            canAttack = 1;
            StartAutoAttack();
        }

        void checkRange()
        {
            bool isInRange = Vector3.Distance(this.transform.position, playerControl.selectedEnemy.transform.position) < autoAttackRange;
            if (isInRange)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }

        void AnimHit()
        {
            Health healthCom = playerControl.selectedEnemy.GetComponent<Health>();
            healthCom.TakeDamage(pStates.weaponDamage);
        }

        //public void GunHit(float damage)
        //{
        //    Health healthCom = Health.Hea
        //    healthCom.GunTakeDamage(pStates.gunDamage);
        //}

    }
}