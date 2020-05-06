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
      
    }

    public void PlayAnimAttack()
    {
        anim.SetTrigger("Attack");
    }

   
    public void FinAttack()
    {
        connection.monsterAttack = false;
    }

   
}
