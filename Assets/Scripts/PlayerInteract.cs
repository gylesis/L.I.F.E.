using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerInteract : MonoBehaviour {

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private Image dot;

    [SerializeField]
    Text ActionText;

    [SerializeField]
    Transform pointToMove;

    [SerializeField]
    bool DoorIsClosed = true;

    [SerializeField]
    bool isOnPad = false;

    public static bool transitionOver = true;



    void Update() {
        dot.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        ObjectsInteraction();
    }

    private void ObjectsInteraction() {
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out hit)) {


            if (hit.collider.gameObject.tag == "Door") {

                var obj = hit.collider.gameObject.GetComponent<InteractbelObjects>();
                float distance = Vector3.Distance(hit.collider.transform.position, transform.position);
                if (distance < 9.2f) {

                    if (isOnPad) {
                        ActionText.enabled = true;
                        ActionText.text = "Press" + " E " + "to " + obj._nameOfAction;

                        if (Input.GetKeyDown(KeyCode.E)) {                      
                            obj.StartAnimation();
                            StartCoroutine(LevelTransition.Instance.LevlTransition());


                        }
                                       
                    }                  
                }
                else {
                    ActionText.enabled = false;

                }
            }
          


            else if (hit.collider.gameObject.tag == "Interactable") {

                var obj = hit.collider.gameObject.GetComponent<InteractbelObjects>();

                float distance = Vector3.Distance(hit.collider.transform.position, transform.position);


                if (distance < 8.2f) {

                    ActionText.enabled = true;

                    ActionText.text = "Press" + " E " + "to " + obj._nameOfAction;

                    if (Input.GetKeyDown(KeyCode.E)) {
                        Debug.Log("Interacting...");

                        obj.StartAnimation();

                    }
                }
                else {
                    ActionText.enabled = false;
                }

            }
            else {
                ActionText.enabled = false;
            }


        }
    }

    public void TransitionBoolOn() {
        transitionOver = true;
    }
    public void TransitionBoolOff() {
        transitionOver = false;
    }
    public void AnimatorTurnOff() {
        GetComponent<Animator>().enabled = false; 
    }

   
    private void OnTriggerStay(Collider other) {       
        if (other.gameObject.tag == "Pad") {
            isOnPad = true;
        }
        else isOnPad = false;
    }

}

