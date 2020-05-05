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
        if (connection.monsterAttack)
        {
            PlayAnimAttack();
        }else if (connection.monsterDie)
        {
            PlayAnimDie();
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

    public void FinDie()
    {
        connection.monsterDie = false;
        gameObject.SetActive(false);
    }
    
}
