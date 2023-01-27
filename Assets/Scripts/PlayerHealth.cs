using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.States;
using RPG.Player;
//using RPG.States;
using RPG.Combat;
using System;
using UnityEngine.SceneManagement;

namespace RPG.Combat
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] public float HealthPoints = 100f;
        PlayerController playerControl;
        PlayerStates pStates;
        bool isDead = false;
        bool die = false;
        float healthp = 1;
        public GameObject GameOverPanel;
        public HealthPoints healthPoints;
        public bool IsDead()
        {
            return isDead;
        }

        void Start()
        {
            healthPoints.SetHealthPoints(100f);
        }

        public void TakeDamage(float damage)
        {
            HealthPoints = Mathf.Max(HealthPoints - damage, 0);
            if (HealthPoints == 0)
            {
                //die = true;
                Death();
            }

            healthPoints.SetHealthPoints(HealthPoints);
        }

        //public void GunTakeDamage(float gunDamage)
        //{
        //    HealthPoints -= gunDamage;
        //    if (HealthPoints == 0)
        //    {
        //        //Die(float HealthPoints);
        //        die = true;
        //        Death();
        //        //Die();
        //    }
        //}


        //public bool Die()
        //{
        //    return die;
        //}



        public void Death()
        {
            if (isDead) return;
            //Debug.Log(isDead);

            //Die();
            //healthp = 0;
            //Die(healthp);
            isDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameOverPanel.SetActive(true);
            //Destroy(gameObject);
            //SceneManager.LoadScene(1);

            //var destroyTime = 3;
            //Destroy(gameObject, destroyTime);

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
