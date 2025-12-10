using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Over : MonoBehaviour
{
    public int final;
    public TextMeshProUGUI fs;
    ManagerMC bmc;

    void Start()
    {
        GameObject gobmc = GameObject.FindGameObjectWithTag("GameController");
        bmc = gobmc.GetComponent<ManagerMC >();
        final = ((Game.floor * 10) + (Game.pth * 5)) - 10;
    }


    void Update()
    {
        fs.text = final.ToString();

        if (Input.GetKey(KeyCode.Space))
        {
            bmc.asr.PlayOneShot(bmc.acp3);

            Game.floor = 1;
            Game.pth = 0;
            Game.ky = false;
            Game.hbv = 100;
            LoadSceneByName("Menu");
        }
    }

    public void LoadSceneByName(string target)
    {
        SceneManager.LoadScene(target);
    }


}
