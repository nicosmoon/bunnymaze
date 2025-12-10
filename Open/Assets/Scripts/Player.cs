using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;


using Cinemachine;



public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject star;
    Manager bmz;
    public GameObject lp;
    List<int> permitidos = new List<int> { 0, 21, 3, 4 };
    public int v3x;
    public int v3y;
    public bool vai = true;


   

    public int boardtag = 21;

    void Start()
    {
        GameObject gobmz = GameObject.FindGameObjectWithTag("GameController");
        bmz = gobmz.GetComponent<Manager>();

        v3x = Mathf.FloorToInt(player.transform.position.x);
        v3y = Mathf.FloorToInt(player.transform.position.y);



    }


    void Update()
    {
       //Debug.Log(Game.floor);
       //Debug.Log(Game.pth);
        //Debug.Log(Game.ky);

        Move();


        if (Game.ky == true)
        {
            permitidos.Add(2);
        }


    }

    
  

    public void Move()
    { 
      
       
        

        int maxX = bmz.maze.GetLength(0);
        int maxY = bmz.maze.GetLength(1);

        

        
        


        // W - Cima
        if (Input.GetKey(KeyCode.W) && vai)
        {
            if (v3y + 1 < maxY && permitidos.Contains(bmz.maze[v3x, v3y + 1]))
            {
                Vector3 start = player.transform.position;
                Vector3 end = new Vector3(v3x, v3y + 1, 0f);



                vai = false; 

                lp.transform.rotation = Quaternion.Euler(0, 0, 90);

                StartCoroutine(MoveLerp(start, end));

                v3y += 1; 
            }
        }

        // S - Baixo
        if (Input.GetKey(KeyCode.S) && vai)
        {
            if (v3y - 1 >= 0 && permitidos.Contains(bmz.maze[v3x, v3y - 1]))
            {
                vai = false;

                Vector3 start = new Vector3(v3x, v3y, 0f);
                Vector3 end = new Vector3(v3x, v3y - 1, 0f);

                v3y -= 1; 

                lp.transform.rotation = Quaternion.Euler(0, 0, 270);

                StartCoroutine(MoveLerp(start, end));
            }
}


        // A - Esquerda
        if (Input.GetKey(KeyCode.A) && vai)
        {
            if (v3x - 1 >= 0 && permitidos.Contains(bmz.maze[v3x - 1, v3y]))
            {
                vai = false;

                Vector3 start = new Vector3(v3x, v3y, 0f);
                Vector3 end = new Vector3(v3x - 1, v3y, 0f);

                v3x -= 1;

                lp.transform.rotation = Quaternion.Euler(0, 0, 180);

                StartCoroutine(MoveLerp(start, end));
            }
        }

        // D - Direita
        if (Input.GetKey(KeyCode.D) && vai)
        {
            if (v3x + 1 < maxX && permitidos.Contains(bmz.maze[v3x + 1, v3y]))
            {
                vai = false;

                Vector3 start = new Vector3(v3x, v3y, 0f);
                Vector3 end = new Vector3(v3x + 1, v3y, 0f);


                v3x += 1;

                lp.transform.rotation = Quaternion.Euler(0, 0, 0);

                StartCoroutine(MoveLerp(start, end));
            }
        }




    }

    IEnumerator MoveLerp(Vector3 start, Vector3 end)
    {
        vai = false;
        float duration = 0.15f;
        float t = 0f;
       

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float smooth = t * t * (3f - 2f * t);
            player.transform.position = Vector3.Lerp(start, end, smooth);
            yield return null;
        }

        player.transform.position = end;
        vai = true;
    }


  

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Exit"))
          {
            bmz.asr.PlayOneShot(bmz.acp1);

            Game.floor++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            bmz.asr.PlayOneShot(bmz.acp2);
            Game.pth++;
            Game.hbv += 50;
        }

        if (other.gameObject.CompareTag("Key"))
        {
            bmz.asr.PlayOneShot(bmz.acp2);
            Game.ky = true;
          

        }
    }

    

}







