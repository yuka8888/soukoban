using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    void PrintArray()
    {
        string debugText = "";
        for(int i = 0;i < map.Length; i++)
        {
            debugText += map[i].ToString() + ", ";
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        //移動先が範囲外なら移動不可
        if(moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }

        //移動先に2があったら
        if (map[moveTo] == 2)
        {
            //どの方向に移動するか
            int velocity = moveTo - moveFrom;

            //箱の移動
            bool success = MoveNumber(2, moveTo, moveTo + velocity);

            if (!success)
            {
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    //配列の宣言
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);

            PrintArray();
        }

        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);

            PrintArray();
        }

    }
}
