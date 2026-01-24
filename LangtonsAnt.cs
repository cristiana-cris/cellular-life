using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using TMPro;


public class Game3 : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT = 48;

    public Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    public float speed = 0.1f;
    public float timer = 0;
    public bool Enabletime = false;

    //iterations
    public long counter=0;
    public Text countText;

    //ant
     public Cell[,] matrix = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    private int xa=27;
    private int ya=27;

    public bool N=false, E=false, V=true, S=false;


    void Start()
    {
        PlaceCells();
        xa = 27; ya = 28;
    }

    void Update()
    {
        Mouse();
        if (timer >= speed && Enabletime)
        {
            timer = 0f;
            AddCnt();
            RulesGame3();
        }
        else
        {
            //timpul care a trecut de la ultimul frame
            timer += Time.deltaTime;
        }

    }

    void Mouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePoint.x);
            int y = Mathf.RoundToInt(mousePoint.y);
            if (x >= 1 && y >= 1 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
                grid[x, y].SetAlive(!grid[x, y].isAlive);
        }
    }

    public void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Graphics/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                Cell ant = Instantiate(Resources.Load("Graphics/Ant", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                matrix[x, y] = ant;
                grid[x, y].SetAlive(false);
                matrix[x, y].SetAlive(false);
            }
        if (Bound(xa, ya))
            matrix[xa, ya].SetAlive(true);
    }

    void RulesGame3()
    {
        //Langton's Ant
        if (Bound(xa, ya))
        {
            int xcopy = xa, ycopy = ya;
            bool life = grid[xa, ya].isAlive;
            matrix[xa, ya].SetAlive(false);
            if (N)
            {
                N = false;
                if (life)
                {
                    ya--;
                    V = true;
                }
                else
                {
                    ya++;
                    E = true;
                }
            }
            else
            if (E)
            {
                E = false;
                if (life)
                {
                    xa--;
                    N = true;
                }
                else
                {
                    xa++;
                    S = true;
                }
            }
            else
            if (S)
            {
                S = false;
                if (life)
                {
                    ya++;
                    E = true;
                }
                else
                {
                    ya--;
                    V = true;
                }
            }
            else
            if (V)
            {
                V = false;
                if (life)
                {
                    xa++;
                    S = true;
                }
                else
                {
                    xa--;
                    N = true;
                }
            }
            grid[xcopy, ycopy].SetAlive(!life);
            if (Bound(xa, ya))
                matrix[xa, ya].SetAlive(true);
            else
            {
                if (xa >= SCREEN_WIDTH)
                {
                    S = false;
                    if (!N && !S && !E && !V) N = true;
                    xa--;
                }
                if (ya >= SCREEN_HEIGHT)
                {
                    E = false;
                    if (!N && !S && !E && !V) V = true;
                    ya--;
                }
                if (ya < 0)
                {
                    ya++; V = false;
                    if (!N && !S && !E && !V) E = true;
                }
                if (xa < 0)
                {
                    xa++; N = false;
                    if (!N && !S && !E && !V) S = true;
                }
            }
        }
    }

    bool Bound(int xx, int yy)
    {
        if (xx < SCREEN_WIDTH && yy < SCREEN_HEIGHT && xx >= 0 && yy >= 0)
            return true;
        else return false;
    }
    bool GetRandom()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand > 75)
            return true;
        else
            return false;
    }

    //butoane
    public void RandomCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
                grid[x, y].SetAlive(GetRandom());
    }

    public void Reset()
    {
        Enabletime = false;
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                grid[x, y].SetAlive(false);
            }
        }
        counter = 0;
        countText.text = counter.ToString();
        matrix[xa, ya].SetAlive(false);
        xa = 27; ya = 28;
    }

    public void Pause()
    {
        Enabletime = !Enabletime;
    }

    public void ChangeSlider(float value)
    {
        speed = 1 - value;
    }

    public void AddCnt()
    {
        counter++;
        countText.text = counter.ToString();
    }
}

