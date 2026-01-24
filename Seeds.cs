using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using TMPro;


public class Game2 : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT = 48;

    public Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    public float speed = 0.1f;
    public float timer = 0;
    public bool Enabletime = false;

    //iterations
    public long counter2=0;
    public Text countText2;


    void Start()
    {
        PlaceCells();
    }

    void Update()
    {
        Mouse();
        if (timer >= speed && Enabletime)
        {
            timer = 0f;
            AddCnt2();
            CountNghb();
            RulesGame2();
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
                grid[x, y] = cell;
                grid[x, y].SetAlive(false);
            }
    }
    
    void CountNghb()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int NrNghb = 0;
                //nord
                if (y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x, y + 1].isAlive) NrNghb++;
                }

                //est
                if (x + 1 < SCREEN_WIDTH)
                {
                    if (grid[x + 1, y].isAlive) NrNghb++;
                }

                //sud
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].isAlive) NrNghb++;
                }

                //vest
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].isAlive) NrNghb++;
                }
                //NE
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x + 1, y + 1].isAlive) NrNghb++;
                }

                //NV
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                    if (grid[x - 1, y + 1].isAlive) NrNghb++;

                //SE
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                    if (grid[x + 1, y - 1].isAlive) NrNghb++;

                //SV
                if (x - 1 >= 0 && y - 1 >= 0)
                    if (grid[x - 1, y - 1].isAlive) NrNghb++;

                grid[x, y].NrNeighbours = NrNghb;
            }
        }
    }

    void RulesGame2()
    {
        //Seeds
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int nr = grid[x, y].NrNeighbours;
                if (nr == 2 && !grid[x, y].isAlive)
                    grid[x, y].SetAlive(true);
                else
                    grid[x, y].SetAlive(false);
            }
        }
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
        counter2 = 0;
        countText2.text = counter2.ToString();
    }

    public void Pause()
    {
        Enabletime = !Enabletime;
    }

    public void ChangeSlider(float value)
    {
        speed = 1 - value;
    }

    public void AddCnt2()
    {
        counter2++;
        countText2.text = counter2.ToString();
    }
}

