using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    public InputField InputRoom;
    
    public Material mFoundRoom;
    
    public Material mDefault;

    private MoveBetweenLevel MoveBetweenLevelManager;

    private GameObject oFoundRoom;

    void Start()
    {
        MoveBetweenLevelManager = GetComponent<MoveBetweenLevel>();
    }

    public void SearchRoom(GameObject university)
    {
        if(oFoundRoom != null)
        {
            oFoundRoom.transform.Find("Cube").gameObject.GetComponent<Renderer>().material = mDefault;
        }
        List<GameObject> allBuildings = GetAllBuildings(university);
        if (allBuildings != null)
        {
            foreach (GameObject build in allBuildings)
            {
                List<GameObject> levels = GetLevelsBuilding(build);
                if (levels != null)
                {
                    foreach (GameObject level in levels)
                    {
                        List<GameObject> rooms = GetRoomsLevel(level);
                        if (rooms != null)
                        {
                            foreach (GameObject room in rooms)
                            {
                                if (room != null && room.transform.Find("Text") != null)
                                {
                                    TextMesh textMesh = room.transform.Find("Text").gameObject.GetComponent<TextMesh>();
                                    if (textMesh != null && InputRoom.text == textMesh.text)
                                    {
                                        room.transform.Find("Cube").gameObject.GetComponent<Renderer>().material = mFoundRoom;
                                        oFoundRoom = room;
                                        string[] words = textMesh.text.Split(' ');
                                        MoveBetweenLevelManager.indexCurrentLevel = Convert.ToInt32(words[0]) / 100;
                                        Debug.Log(MoveBetweenLevelManager.indexCurrentLevel);
                                        MoveBetweenLevelManager.UpdateVisibleLevel();
                                        //Debug.Log(textMesh.text);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private List<GameObject> GetAllBuildings(GameObject university)
    {
        if (university.tag == "university")
        {
            //Debug.Log("university tag Ok");
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < university.transform.childCount; i++)
            {
                if (university.transform.GetChild(i).tag == "build")
                {
                    list.Add(university.transform.GetChild(i).gameObject);
                }
            }
            return list;
        }
        Debug.Log("<color=yellow>Error: </color>university tag not found");
        return null;
    }

    private List<GameObject> GetLevelsBuilding(GameObject build)
    {
        if (build.tag == "build")
        {
            //Debug.Log("build tag Ok");
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < build.transform.childCount; i++)
            {
                if (build.transform.GetChild(i).tag == "level")
                {
                    list.Add(build.transform.GetChild(i).gameObject);
                }
            }
            return list;
        }
        Debug.Log("<color=yellow>Error: </color>build tag not found");
        return null;
    }

    private List<GameObject> GetRoomsLevel(GameObject level)
    {
        if (level.tag == "level")
        {
            Debug.Log("level tag Ok");
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < level.transform.childCount; i++)
            {
                if (level.transform.GetChild(i).tag == "room")
                {
                    Debug.Log("room tag Ok");
                    list.Add(level.transform.GetChild(i).gameObject);
                }
            }
            return list;
        }
        Debug.Log("<color=yellow>Error: </color>level tag not found");
        return null;
    }
}
