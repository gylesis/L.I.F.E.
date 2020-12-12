using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
   public static LevelTransition Instance;

    [SerializeField]
    int indexOfCurrentLevel = 0;

    [SerializeField]
    List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    List<GameObject> roomFrame = new List<GameObject>();

    [SerializeField]
    Transform StartPos;
    [SerializeField]
    Transform MoveToPos;


    [SerializeField]
    GameObject capsulatest;


    private void Start() {
        Instance = this;     
        while(capsulatest.transform.position.z >= MoveToPos.position.z) {

            capsulatest.transform.position = Vector3.Lerp(capsulatest.transform.position, MoveToPos.position , 1);
        }
    }

    public IEnumerator LevlTransition() { 
        Instantiate(levels[indexOfCurrentLevel + 1]);
        roomFrame[indexOfCurrentLevel % 2].SetActive(true);

        yield return new WaitForSeconds(2f);

       // Destroy(levels[indexOfCurrentLevel]);
        roomFrame[indexOfCurrentLevel + 1 % 2].SetActive(false);
        indexOfCurrentLevel++;
    }




}
