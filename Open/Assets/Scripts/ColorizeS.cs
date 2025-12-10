using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizeS : MonoBehaviour
{
    Color[] colors;
    public int palcolnum;
    SpriteRenderer spt;
    Color newColor;
   
    void Start()
    {
        spt = gameObject.GetComponent<SpriteRenderer>();

        colors = new Color[Game.hexcolors.Length];

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
                spt.color = colors[0];  
            }
            if (palcolnum == 2)
            {
                spt.color = colors[1];
            }
        }
        if(Game.pal == 2)
        {
            if (palcolnum == 1)
            {
                spt.color = colors[2];
            }
            if (palcolnum == 2)
            {
                spt.color = colors[3];
            }
        }
        if(Game.pal == 3)
        {
            if (palcolnum == 1)
            {
                spt.color = colors[4];
            }
            if(palcolnum == 2)
            {
                spt.color = colors[5];
            }
        }
    }
}
