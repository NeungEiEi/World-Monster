using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public GameObject bear, spider, wolf;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(MonsterData.type == 1)
        {
            wolf.SetActive(true);
            spider.SetActive(false);
            bear.SetActive(false);
        }else if(MonsterData.type == 2)
        {
            wolf.SetActive(false);
            spider.SetActive(true);
            bear.SetActive(false);
        }else if (MonsterData.type == 3) 
        {
            wolf.SetActive(false);
            spider.SetActive(false);
            bear.SetActive(true);
        }
    }
}
