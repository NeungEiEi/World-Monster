using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public ConnectionManager connection;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        connection = GameObject.Find("NetWork").GetComponent<ConnectionManager>();
    }

    private void Update()
    {
        if (MonsterData.currentHealth > 0)
        {
            if (connection.monsterAttack)
            {
                PlayAnimAttack();
            }
           
        }
        else
        {

            gameObject.SetActive(false);
            
        }

    }

    public void PlayAnimAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayAnimDie()
    {
        anim.SetTrigger("Die");
    }

    public void FinAttack()
    {
        connection.monsterAttack = false;
    }

   
}
