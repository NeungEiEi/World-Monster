using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ConnectionManager : MonoBehaviour
{
    private SocketIOComponent socket;

    public MonsterHealthBar monsterHealthBar;
    public PlayerHealthBar playerHealthBar;
    public GameObject cooldownAttack;
    public GameObject cooldownSkill;
    public GameObject icon2X;
    public GameObject diePanal;
    public float timeCooldownAttack;
    public float timeCooldownSkill;

    public bool monsterAttack;
    public bool monsterDie;

    private void Awake()
    {
        monsterHealthBar = GameObject.Find("MonsterHealthbar").GetComponent<MonsterHealthBar>();
        playerHealthBar = GameObject.Find("PlayerHealthbar").GetComponent<PlayerHealthBar>();

    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.SetMaxHealth(PlayerData.maxHealth);
        playerHealthBar.SetHealth(PlayerData.currentHealth);
        cooldownAttack.SetActive(false);
        cooldownSkill.SetActive(false);
        icon2X.SetActive(false);
        diePanal.SetActive(false);
        timeCooldownAttack = 0;
        timeCooldownSkill = 0;


        socket = GetComponent<SocketIOComponent>();

        socket.On("SpawnMonster", SpawnMonster);
        socket.On("UpdateMonsterData", UpdateMonsterData);
        socket.On("MonsterAttack", MonsterAttack);
        socket.On("Heal", Heal);
        socket.On("BuffAttack", BuffAttack);
        socket.On("Buffend", Buffend);
        socket.On("MonsterDie", MonsterDie);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.currentHealth > 0)
        {
            diePanal.SetActive(false);
            if (timeCooldownAttack <= 0)
            {
                cooldownAttack.SetActive(false);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
                    json.AddField("Damage", PlayerData.damage);
                    socket.Emit("PlayerAttack", json);
                    timeCooldownAttack = PlayerData.cooldownAttack;
                }
            }
            else
            {
                cooldownAttack.SetActive(true);
                timeCooldownAttack -= Time.deltaTime;
            }


            if (timeCooldownSkill <= 0)
            {
                cooldownSkill.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (PlayerData.job == "Healer")
                    {
                        Debug.Log("Heal");
                        socket.Emit("PlayerHeal");
                    }
                    else if (PlayerData.job == "Fighter")
                    {
                        Debug.Log("Attack!!");
                        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
                        json.AddField("Damage", 10);
                        socket.Emit("PlayerAttack", json);
                        timeCooldownAttack = PlayerData.cooldownAttack;
                    }
                    else if (PlayerData.job == "Buffer")
                    {
                        Debug.Log("Buff");
                        socket.Emit("PlayerBuff");
                    }


                    timeCooldownSkill = PlayerData.cooldownSkill;
                }
            }
            else
            {
                cooldownSkill.SetActive(true);
                timeCooldownSkill -= Time.deltaTime;
            }
        }
        else
        {
            diePanal.SetActive(true);
            float timeToQuit = 5f;
            timeToQuit -= Time.deltaTime;

            if (timeToQuit <= 0)
            {
                Application.Quit();
            }
        }

    }

    void SpawnMonster(SocketIOEvent evt)
    {
        Debug.Log(evt.data.ToString());
        MonsterData.type = int.Parse(evt.data["MonsterType"].ToString());
        Debug.Log(MonsterData.type);
        MonsterData.maxHealth = int.Parse(evt.data["MonsterMaxHealth"].ToString());
        Debug.Log(MonsterData.maxHealth);
        monsterHealthBar.SetMaxHealth(MonsterData.maxHealth);
        MonsterData.currentHealth = int.Parse(evt.data["MonsterCurrentHealth"].ToString());
        Debug.Log(MonsterData.currentHealth);
        monsterHealthBar.SetHealth(MonsterData.currentHealth);
    }

    void UpdateMonsterData(SocketIOEvent evt)
    {
        Debug.Log(evt.data.ToString());
        MonsterData.type = int.Parse(evt.data["MonsterType"].ToString());
        Debug.Log(MonsterData.type);
        MonsterData.maxHealth = int.Parse(evt.data["MonsterMaxHealth"].ToString());
        Debug.Log(MonsterData.maxHealth);
        monsterHealthBar.SetMaxHealth(MonsterData.maxHealth);
        MonsterData.currentHealth = int.Parse(evt.data["MonsterCurrentHealth"].ToString());
        Debug.Log(MonsterData.currentHealth);
        monsterHealthBar.SetHealth(MonsterData.currentHealth);
    }

    void MonsterAttack(SocketIOEvent evt)
    {
        monsterAttack = true;
        Debug.Log(evt.data.ToString());
        PlayerData.currentHealth -= int.Parse(evt.data["MonsterDamage"].ToString());
        Debug.Log(PlayerData.maxHealth + " : " + PlayerData.currentHealth);
        playerHealthBar.SetHealth(PlayerData.currentHealth);
    }

    void Heal(SocketIOEvent evt)
    {
        PlayerData.currentHealth = PlayerData.maxHealth;
        playerHealthBar.SetHealth(PlayerData.currentHealth);
    }

    void BuffAttack(SocketIOEvent evt)
    {
        icon2X.SetActive(true);
    }

    void Buffend(SocketIOEvent evt)
    {
        icon2X.SetActive(false);
    }

    void MonsterDie(SocketIOEvent evt)
    {
        monsterDie = true;
        MonsterData.type = 0;
    }

    public void isExit()
    {
        socket.Emit("disconnect");
        Application.Quit();
    }
}
