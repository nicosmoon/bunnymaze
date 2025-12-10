using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Manager : MonoBehaviour
{
    public GameObject exit;
    public GameObject wall;
    public GameObject player;
    public GameObject heart;
    public GameObject key;
    public GameObject grid;
    public int w;
    public int h;
    public float tile;
    public int[,] maze;
    public Canvas targetCanvas; 
    Camera newRenderCamera;
    public TextMeshProUGUI keynote;
    public Slider hb;
    public AudioSource asr;
    public AudioClip acp1;
    public AudioClip acp2;
    public AudioClip acp3;
    public AudioClip acp4;
  
  
    
   

    void Start()
    {

         
        maze = new int[w, h];
      
        GenerateMaze();
        Hearts();
        Key();
        MazeBuild();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            Camera playerCamera = playerObj.GetComponentInChildren<Camera>();

            if (targetCanvas != null && playerCamera != null)
            {
                targetCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                targetCanvas.worldCamera = playerCamera;
            }
        }
        else
        {
          
        }


        
        hb.maxValue = 1000;
        hb.minValue = 0;
    }

    
   


    void Update()
    {
        if (Game.ky == false)
        {
            keynote.text = "Find the Key";
        }
        else if (Game.ky == true)
        {
            keynote.text = "Go to Exit";
        }

        Game.hbv -= (4+(Game.floor - 1)) * Time.deltaTime;

        if(Game.hbv >= 1000)
        {
            Game.hbv = 1000;
        }
        hb.value = Game.hbv;

        if (0 >= Game.hbv)
        {
            asr.PlayOneShot(acp4);

            LoadSceneByName("End");
        }
    }


    public void LoadSceneByName(string target)
    {
        SceneManager.LoadScene(target);
    }




    public void GenerateMaze()
    {
        maze = new int[w, h];

        
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                maze[x, y] = 1;

        Vector2Int start;

        int side = UnityEngine.Random.Range(0, 4);

        switch (side)
        {
            case 0:
                start = new Vector2Int(1, UnityEngine.Random.Range(1, h - 1));
                break;
            case 1:
                start = new Vector2Int(w - 2, UnityEngine.Random.Range(1, h - 1));
                break;
            case 2:
                start = new Vector2Int(UnityEngine.Random.Range(1, w - 1), h - 2);
                break;
            default:
                start = new Vector2Int(UnityEngine.Random.Range(1, w - 1), 1);
                break;
        }

        maze[start.x, start.y] = 0;

        Vector2Int startb = start;

        List<Vector2Int> walls = new List<Vector2Int>();

       
        AddFrontierWalls(startb, walls);
        AddExtraConnections(maze);
        RemoveDeadEnds(maze);
        
        //      PRIM'S ALGORITHM
         
        while (walls.Count > 0)
        {
            
            Vector2Int wall = walls[UnityEngine.Random.Range(0, walls.Count)];
            walls.Remove(wall);

            
            Vector2Int[] neighbors = {
            new Vector2Int(wall.x + 1, wall.y),
            new Vector2Int(wall.x - 1, wall.y),
            new Vector2Int(wall.x, wall.y + 1),
            new Vector2Int(wall.x, wall.y - 1)
        };

            int visitedCount = 0;
            Vector2Int visitedCell = Vector2Int.zero;

            foreach (var n in neighbors)
            {
                if (Inside(n) && maze[n.x, n.y] == 0)
                {
                    visitedCount++;
                    visitedCell = n;
                }
            }

            if (visitedCount == 1)
            {
                maze[wall.x, wall.y] = 0;

               
                Vector2Int opposite = new Vector2Int(
                    wall.x + (wall.x - visitedCell.x),
                    wall.y + (wall.y - visitedCell.y)
                );

                if (Inside(opposite) && maze[opposite.x, opposite.y] == 1)
                {
                    maze[opposite.x, opposite.y] = 0;
                    AddFrontierWalls(opposite, walls);
                    
                }

               
                
            }
        }

        Vector2Int exit;

        
        int oppositeb = (side + 2) % 4;

        switch (oppositeb)
        {
            case 0: 
                exit = new Vector2Int(1, UnityEngine.Random.Range(1, h - 2));
                break;

            case 1: 
                exit = new Vector2Int(w - 2, UnityEngine.Random.Range(1, h - 2));
                break;

            case 2: 
                exit = new Vector2Int(UnityEngine.Random.Range(1, w - 2), h - 2);
                break;

            default:
                exit = new Vector2Int(UnityEngine.Random.Range(1, w - 2), 1);
                break;
        }

        maze[exit.x, exit.y] = 2;

        AddExtraConnections(maze);
        //RemoveDeadEnds(maze);
       
        int exitY = UnityEngine.Random.Range(1, h - 2);
        
        maze[startb.x, startb.y] = 21;

       
      
    }

    bool Inside(Vector2Int p)
    {
        return p.x > 0 && p.x < w - 1 && p.y > 0 && p.y < h - 1;
    }

    void AddFrontierWalls(Vector2Int cell, List<Vector2Int> walls)
    {
        Vector2Int[] dirs = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

        foreach (var d in dirs)
        {
            Vector2Int wPos = cell + d;
            if (Inside(wPos) && maze[wPos.x, wPos.y] == 1 && !walls.Contains(wPos))
            {
                walls.Add(wPos);
            }
        }
    }

    void AddExtraConnections(int[,] maze, int extraConnections = 256)
    {
        int width = maze.GetLength(0);
        int height = maze.GetLength(1);

        for (int i = 0; i < extraConnections; i++)
        {
            int x = UnityEngine.Random.Range(1, width - 1);
            int y = UnityEngine.Random.Range(1, height - 1);

            
            if (maze[x, y] == 1) 
            {
                int openNeighbors = 0;

                if (maze[x + 1, y] == 0) openNeighbors++;
                if (maze[x - 1, y] == 0) openNeighbors++;
                if (maze[x, y + 1] == 0) openNeighbors++;
                if (maze[x, y - 1] == 0) openNeighbors++;

                if (openNeighbors >= 2)
                {
                    maze[x, y] = 0; 
               }
            }
        }
    }

    void RemoveDeadEnds(int[,] maze)
    {
        bool changed = true;

        int w = maze.GetLength(0);
        int h = maze.GetLength(1);

        while (changed)
        {
            changed = false;

            for (int x = 1; x < w - 1; x++)
            {
                for (int y = 1; y < h - 1; y++)
                {
                    if (maze[x, y] != 0) continue;

                    int open = 0;
                    if (maze[x + 1, y] == 0) open++;
                    if (maze[x - 1, y] == 0) open++;
                    if (maze[x, y + 1] == 0) open++;
                    if (maze[x, y - 1] == 0) open++;

                 
                    if (open == 1)
                    {
                        maze[x, y] = 1;
                        changed = true;
                    }
                }
            }
        }
    }

    public void Hearts()
    {
        List<Vector2Int> candidates = new List<Vector2Int>();

        int width = maze.GetLength(0);
        int height = maze.GetLength(1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0)
                {
                    candidates.Add(new Vector2Int(x, y));
                }
            }
        }

        int randomh = UnityEngine.Random.Range(1, 16);

        for (int i = 0; i < randomh; i++)
        {

            if (candidates.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, candidates.Count);
                Vector2Int chosen = candidates[index];


                maze[chosen.x, chosen.y] = 3;

            }

        }
       
        
        
    }
    public void Key()
    {
        List<Vector2Int> candidates = new List<Vector2Int>();

        int width = maze.GetLength(0);
        int height = maze.GetLength(1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0)
                {
                    candidates.Add(new Vector2Int(x, y));
                }
            }
        }

      
            if (candidates.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, candidates.Count);
                Vector2Int chosen = candidates[index];


                maze[chosen.x, chosen.y] = 4;




            
        }

       
    }
    public void MazeBuild()
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if(maze[x, y] == 1)
                {
                    Vector3 pos = new
                        Vector3(x * tile, y * tile, 0);
                    Instantiate(wall, pos, Quaternion.identity, transform);
                }
            }
        }

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (maze[x, y] == 2)
                {
                    Vector3 pos = new
                        Vector3(x * tile, y * tile, 0);
                    Instantiate(exit, pos, Quaternion.identity, transform);

                    Vector3 posB = new
                          Vector3(x * tile, y * tile, -2);
                    Instantiate(grid, posB, Quaternion.identity, transform);
                }
            }
        }

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (maze[x, y] == 21)
                {
                    Vector3 pos = new
                        Vector3(x * tile, y * tile, -1);
                    Instantiate(player, pos, Quaternion.identity, transform);
                }
            }
        }
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (maze[x, y] == 3)
                {
                    Vector3 pos = new
                        Vector3(x * tile, y * tile, 0);
                    Instantiate(heart, pos, Quaternion.identity, transform);
                }
            }
        }
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (maze[x, y] == 4)
                {
                    Vector3 pos = new
                        Vector3(x * tile, y * tile, 0);
                    Instantiate(key, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}
