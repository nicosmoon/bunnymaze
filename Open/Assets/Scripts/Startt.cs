using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Startt : MonoBehaviour
{
    
    ManagerMC bmc;

    private void Start()
    {
        GameObject gobmc = GameObject.FindGameObjectWithTag("GameController");
        bmc = gobmc.GetComponent<ManagerMC>();

        
    }

    void Update()
        {
          

            if (Input.GetKey(KeyCode.Space))
            {
                bmc.asr.PlayOneShot(bmc.acp3);
                Game.floor = 1;
                Game.pth = 0;
                Game.ky = false;
                LoadSceneByName("Game");
            }
            
        if (Input.GetKeyUp(KeyCode.C) )
        {
            Game.pal += 1;
                //Debug.Log("Color");
        }

        if (Game.pal > 3)
        {
            Game.pal = 1;
        }
        } 
    

    public void LoadSceneByName(string target)
    {
        SceneManager.LoadScene(target);
    }

}

