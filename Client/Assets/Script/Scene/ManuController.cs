using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManuController : MonoBehaviour
{



    public void IsHealer()
    {



        PlayerData.job = "Healer";
        PlayerData.maxHealth = 40;
        PlayerData.currentHealth = PlayerData.maxHealth;
        PlayerData.damage = 2;
        PlayerData.cooldownAttack = 3f;
        PlayerData.cooldownSkill = 5f;
        SceneManager.LoadSceneAsync(1);


    }

    public void IsFighter()
    {

        PlayerData.job = "Fighter";
        PlayerData.maxHealth = 30;
        PlayerData.currentHealth = PlayerData.maxHealth;
        PlayerData.damage = 5;
        PlayerData.cooldownAttack = 1f;
        PlayerData.cooldownSkill = 3f;
        SceneManager.LoadSceneAsync(1);


    }

    public void IsBuffer()
    {



        PlayerData.job = "Buffer";
        PlayerData.maxHealth = 40;
        PlayerData.currentHealth = PlayerData.maxHealth;
        PlayerData.damage = 2;
        PlayerData.cooldownAttack = 2f;
        PlayerData.cooldownSkill = 10f;
        SceneManager.LoadSceneAsync(1);

    }
}
