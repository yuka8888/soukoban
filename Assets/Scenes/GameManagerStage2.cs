using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;

public class GameManagerStage2 : MonoBehaviour
{
    void Initialize()
    {
        var objects = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var obj in objects) { Destroy(obj); }

        clearText.SetActive(false);

        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] != null)
                {
                    Destroy(field[y, x]);
                }

                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                      playerPrefab,
                      new Vector3(x, map.GetLength(0) - y, 0),
                      Quaternion.identity
                     );
                }

                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }

                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                        goalPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }

                if (map[y, x] == 4)
                {
                    field[y, x] = Instantiate(
                        wallPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }

            }
        }

    }
    bool IsCleard()
    {
        //Vector2Int�^�̉ϒ��z��̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���c�ɂ𔻒f
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //��ł������Ȃ�������������B��
                return false;
            }
        }
        //�������B���łȂ���Ώ����B��
        return true;
    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                //null��������^�O�𒲂ׂ����̗v�f�ֈڂ�
                if (field[y, x] == null) { continue; }
                //null��������continue���Ă���̂�
                //���̍s�ɂ��ǂ蒅���ꍇ��null�łȂ����Ƃ��m��
                //�^�O�̊m�F���s��
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        if (field[moveFrom.y, moveFrom.x].tag == "Player")
        {
            for (int i = 0; i < particle.GetLength(0); i++)
            {
                particle[i] = Instantiate(
                particlePrefab,
                field[moveFrom.y, moveFrom.x].transform.position,
                Quaternion.identity
                  );
            }
        }

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        //field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);

        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }

    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject clearText;
    public GameObject particlePrefab;
    public GameObject wallPrefab;

    //�z��̐錾
    int[,] map;
    int[,] map2;
    GameObject[,] field;//�Q�[���Ǘ��p
    GameObject[] particle;

    // Start is called before the first frame update
    void Start()
    {

        Screen.SetResolution(1280, 720, false);

        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[,] {
            { 4, 4, 4, 4, 4, 4, 4},
            { 4, 0, 0, 0, 0, 0, 4},
            { 4, 0, 3, 1, 3, 0, 4},
            { 4, 0, 0, 2, 0, 0, 4},
            { 4, 0, 2, 3, 2, 0, 4},
            { 4, 0, 0, 0, 0, 0, 4},
            { 4, 4, 4, 4, 4, 4, 4},
        };

        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];


        particle = new GameObject[3];

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Initialize();
        }

        //if (IsCleard())
        {
            //�E�ړ�
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();

                MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
                if (IsCleard())
                {
                    clearText.SetActive(true);

                }
            }

            //���ړ�
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();

                MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
                if (IsCleard())
                {
                    clearText.SetActive(true);
                }
            }

            //��ړ�
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();

                MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
                if (IsCleard())
                {
                    clearText.SetActive(true);
                }
            }

            //���ړ�
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();

                MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
                if (IsCleard())
                {
                    clearText.SetActive(true);
                }
            }

        }

    }
}
