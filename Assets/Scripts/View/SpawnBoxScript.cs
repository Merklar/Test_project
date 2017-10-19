using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoxScript : MonoBehaviour
{

    public GameObject[] boxList;

    private GameObject[] FigureContainerCollections;

    void Start()
    {
        FigureContainerCollections = GameObject.FindGameObjectsWithTag("FigureContainer");
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        foreach (GameObject _go in FigureContainerCollections)
        {
            if (_go.transform.childCount == 0) {
                int i = Random.Range(0, boxList.Length);
                int j = Random.Range(0,3);
                float angle;
                switch (j)
                {
                    case 0:
                        angle = 0f;
                        break;
                    case 1:
                        angle = 90f;
                        break;
                    case 2:
                        angle = 180f;
                        break;
                    case 3:
                        angle = 270f;
                        break;
                    default:
                        angle = 0f;
                        break;
                }
                GameObject _currGO = Instantiate(boxList[i], transform.position, Quaternion.Euler(x: 0f, y: 0f, z: angle));
                _currGO.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                _currGO.transform.parent = _go.transform;
                //_go.GetComponent<GridLayoutGroup>().
                _currGO.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
