using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorizeI : MonoBehaviour
{
    Color[] colors;
    public int palcolnum;
    Image img;
    Color newColor;

    void Start()
    {
        img = gameObject.GetComponent<Image>();

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
        if (Game.pal == 1)
        {
            if (palcolnum == 1)
            {
                img.color = colors[0];
            }
            if (palcolnum == 2)
            {
                img.color = colors[1];
            }
        }
        if (Game.pal == 2)
        {
            if (palcolnum == 1)
            {
                img.color = colors[2];
            }
            if (palcolnum == 2)
            {
                img.color = colors[3];
            }
        }
        if (Game.pal == 3)
        {
            if (palcolnum == 1)
            {
                img.color = colors[4];
            }
            if (palcolnum == 2)
            {
                img.color = colors[5];
            }
        }
    }
}
