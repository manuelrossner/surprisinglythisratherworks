using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Bars
{
    public int index;
    public GameObject Bar;
    public int currNodeIndex;

    public Vector3 UpcomiongPositionHolder;

    public Vector3 startPosition;

    public float barTimer;
}

public class TrainingMover : MonoBehaviour
{
    List<GameObject> PathNode = new List<GameObject>();
    public GameObject PathParent;
    public GameObject BarPrefab;
    public float MoveSpeed;

    float Timer;

    int CurrentNode = 0;
    public int activeNodes = 0;

    public float SpawnTimer = 2f;
    public float SpawnTimerInitial = 2f;

    public Bars[] barArray;

    void Start()
    {
        foreach (Transform child in PathParent.gameObject.transform)
        {
            PathNode.Add(child.gameObject);
        }

        for (int i = 0; i < barArray.Length; i++) {
          barArray[i].Bar.transform.position = PathNode[i*4].transform.position;
          barArray[i].currNodeIndex = i*4;
          barArray[i].index = i*4;
          CheckNode(i);
        }
    }

    void CheckNode(int barIndex)
    {
        int i = barIndex;
        barArray[i].barTimer = 0;
        barArray[i].startPosition = PathNode[barArray[i].currNodeIndex].transform.position;
        barArray[i].UpcomiongPositionHolder = PathNode[barArray[i].currNodeIndex+1].transform.position;
    }

    void Update()
    {
        for (int i = 0; i < barArray.Length; i++)
        {
                barArray[i].barTimer = barArray[i].barTimer+ Time.deltaTime * MoveSpeed;
                if (barArray[i].Bar.transform.position != barArray[i].UpcomiongPositionHolder)
                {
                  barArray[i].Bar.transform.position = Vector3.Lerp(barArray[i].startPosition, barArray[i].UpcomiongPositionHolder, barArray[i].barTimer);
                }
                else
                {
                    if (barArray[i].currNodeIndex < barArray[i].index + 4 )
                    {
                        CheckNode(i);
                        barArray[i].currNodeIndex = barArray[i].currNodeIndex + 1;
                    } else
                    {
                        barArray[i].currNodeIndex = barArray[i].index;
                        CheckNode(i);
                    }
                }
        }
    }
}
