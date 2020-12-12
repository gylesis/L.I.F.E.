using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractbelObjects : MonoBehaviour
{
    [SerializeField]
    string _nameOfAnimator;

    [SerializeField]
    public string _nameOfAction;

    public void StartAnimation() {
        var animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger(_nameOfAnimator);
    }
    public void DeleteCollider() {
      gameObject.GetComponent<BoxCollider>().enabled = false;
       
    }

}
