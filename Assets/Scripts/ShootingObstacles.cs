using System.Collections.Generic;
using UnityEngine;

public class ShootingObstacles : MonoBehaviour
{
    private List<GameObject> stakeList;
    private List<Vector3> positionList;
    private Camera cam;
    private int randomStakeNumber;
    private bool isGetAnyStake;
    private int stakeNumber;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        createListOfStakes();
        getRandomStake();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGetAnyStake == false)
        {
            if(stakeList.Count > 0)
            {
                getRandomStake();
            }
            else
            {
                List<GameObject>  finishedStakeList = new List<GameObject>
                    (GameObject.FindGameObjectsWithTag("Stake"));
                for (int i = 0; i < finishedStakeList.Count; i++)
                {
                    finishedStakeList[i].transform.position = positionList[i];
                }
                createListOfStakes();
            }
        }
        else
        {
            Vector3 viewPos = cam.WorldToViewportPoint(stakeList[randomStakeNumber].transform.position);
            if (viewPos.y < 1.2f) 
            {
                stakeList[randomStakeNumber].transform.Translate(new Vector3(0f, 10f, 0f) * Time.deltaTime);
            }
            else
            {
                stakeList.RemoveAt(randomStakeNumber);
                isGetAnyStake= false;
            }
        }
    }

    private void createListOfStakes()
    {
        stakeList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Stake"));
        positionList = new List<Vector3>();
        foreach (GameObject stake in stakeList)
        {
            positionList.Add(stake.transform.position);
        }
    }

    private void getRandomStake()
    {
        randomStakeNumber = Random.Range(0, stakeList.Count);
        isGetAnyStake = true;
    }
}
