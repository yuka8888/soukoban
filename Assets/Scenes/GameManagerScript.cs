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
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }

        //�ړ����2����������
        if (map[moveTo] == 2)
        {
            //�ǂ̕����Ɉړ����邩
            int velocity = moveTo - moveFrom;

            //���̈ړ�
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

    //�z��̐錾
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);

            PrintArray();
        }

        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);

            PrintArray();
        }

    }
}
