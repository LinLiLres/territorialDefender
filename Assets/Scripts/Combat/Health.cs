using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.States;
using RPG.Player;
//using RPG.States;
using RPG.Combat;
using System;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]public float HealthPoints = 100f;
        PlayerController playerControl;
        PlayerStates pStates;
        public GameObject player;
        bool isDead = false;
        bool die = false;
        float healthp = 1;
        float count = 1;
        public GameObject GameOverPanel;
        public HealthBar healthBar;

        public bool IsDead()
        {
            return isDead;
        }
        void Start()
        {
            //healthBar.SetMaxHealth(HealthPoints);
        }

        public void TakeDamage(float damage)
        {
            HealthPoints = Mathf.Max(HealthPoints - damage, 0);
            if(HealthPoints <= 0)
            {
                die = true;
                Death();
            }
            healthBar.SetHealth(HealthPoints);
            
        }

        public void GunTakeDamage(float gunDamage)
        {
            HealthPoints -= gunDamage;
            if (HealthPoints <= 0)
            {
                //Die(float HealthPoints);
                die = true;
                Death();
                //Die();
            }
            healthBar.SetHealth(HealthPoints);
        }


        public bool Die()
        {
            return die;
        }



        public void Death()
        {
            if (isDead) return;
            //Debug.Log(isDead);

            //Die();
            //healthp = 0;
            //Die(healthp);
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");

            //count += 1;
            //Debug.Log(count);
            player.GetComponent<Count>().CountKilled(count);
            var destroyTime = 1;
            Destroy(gameObject, destroyTime);
            
            

        }

        void Update()
        {
            //Die();
        }

        //internal bool Die()
        //{
        //    throw new NotImplementedException();
        //}

        //public void AnimHit()
        //{
        //    Health healthCom = playerControl.selectedEnemy.GetComponent<Health>();
        //    healthCom.TakeDamage(pStates.weaponDamage);
        //}
    }
}
