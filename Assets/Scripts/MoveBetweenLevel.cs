using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenLevel : MonoBehaviour
{
    public GameObject CurrentBuild;

    public GameObject MainCamera;

    public int indexCurrentLevel { get; set; }

    public void Start()
    {
        indexCurrentLevel = 1;
        UpdateVisibleLevel();
    }

    public void UpCurrentLevel()
    {
        if (indexCurrentLevel + 1 <= CurrentBuild.transform.childCount)
        {
            indexCurrentLevel++;
        }
        Debug.Log("UpCurrentLevel");
        UpdateVisibleLevel();
    }

    public void DownCurrentLevel()
    {
        if (indexCurrentLevel - 1 >= 1)
        {
            indexCurrentLevel--;
        }
        Debug.Log("DownCurrentLevel");
        UpdateVisibleLevel();
    }

    public void UpdateVisibleLevel()
    {
        for (int i = 0; i < CurrentBuild.transform.childCount; i++)
        {
            CurrentBuild.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < indexCurrentLevel; i++)
        {
            CurrentBuild.transform.GetChild(i).gameObject.SetActive(true);
        }
        Vector3 prevPosition = MainCamera.transform.position;
        MainCamera.transform.position = new Vector3(prevPosition.x, 7.79f + indexCurrentLevel * 0.284f, prevPosition.z);
        Debug.Log("UpdateVisibleLevel");
    }
}
