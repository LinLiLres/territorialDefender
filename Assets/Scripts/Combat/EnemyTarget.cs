using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player;


namespace RPG.Combat
{
    public class EnemyTarget : MonoBehaviour
    {
        [SerializeField] float autoAttackTime;
        [SerializeField] int CanAttack = 1;
        [SerializeField] int basisAttackDmg;
        AIController aiController;
        // Start is called before the first frame update
        void Start()
        {
            aiController = GetComponent<AIController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Attack()
        {
            if(CanAttack == 1)
            {
                StartCoroutine(basicAttack());
            }
           
        }
        IEnumerator basicAttack()
        {
            CanAttack = 0;
            GetComponent<Animator>().SetTrigger("BasicAttack1");
            yield return new WaitForSeconds(autoAttackTime);
            CanAttack = 1;
            Attack();
        }
        public void EnemyHit()
        {
            PlayerHealth healthCom = aiController.Target.GetComponent<PlayerHealth>();
            healthCom.TakeDamage(basisAttackDmg);
        }
    }
}

