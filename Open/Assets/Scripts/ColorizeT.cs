using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorizeT : MonoBehaviour
{
    Color32[] colors;
    public int palcolnum;
    TMP_Text tmpt;
    Color newColor;
   
    void Start()
    {
        tmpt = GetComponent<TMP_Text>();   

        colors = new Color32[Game.hexcolors.Length];

        for (int i = 0; i < Game.hexcolors.Length; i++)
        {
            if (ColorUtility.TryParseHtmlString(Game.hexcolors[i], out newColor))
            {
                colors[i] = newColor;
            }
        }


    }

    
    void Update()
    {
        if(Game.pal == 1)
        {
            if(palcolnum == 1)
            {
                tmpt.color = colors[0];  
            }
            if (palcolnum == 2)
            {
                tmpt.color = colors[1];
            }
        }
        if(Game.pal == 2)
        {
            if (palcolnum == 1)
            {
                tmpt.color = colors[2];
            }
            if (palcolnum == 2)
            {
                tmpt.color = colors[3];
            }
        }
        if(Game.pal == 3)
        {
            if (palcolnum == 1)
            {
                tmpt.color = colors[4];
            }
            if(palcolnum == 2)
            {
                tmpt.color = colors[5];
            }
        }
    }
}
