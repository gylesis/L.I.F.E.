using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player {
    public static int money = 100;
    public static float time = 80f;

    public static float _happiness = 60f;
    private static float _saturation = 60f;
    private static float _satisfaction = 50f;
    private static float _sleep = 70f;

    public static float Saturation {

        set {
            if (value < 0) {
                _saturation = 0;
            }

            else if (value > 100) {
                _saturation = 100;
            }
            else _saturation = value;
        }
        get {
            return _saturation;
        }

    }
    public static float Satisfaction {
        set {
            if (value < 0) {
                _satisfaction = 0;
            }

            else if (value > 100) {
                _satisfaction = 100;
            }
            else _satisfaction = value;
        }
        get {
            return _satisfaction;
        }
    }
    public static float Sleep {
        set {
            if (value < 0) {
                _sleep = 0;
            }

            else if (value > 100) {
                _sleep = 100;
            }
            else _sleep = value;
        }
        get {
            return _sleep;
        }
    }
    public static float Happiness {
        set {
            if (value < 0) {
                _happiness = 0;
            }

            else if (value > 100) {
                _happiness = 100;
            }
            else _happiness = value;
        }
        get {
            return _happiness;
        }
    }

    private static float _modifierForHappiness = 1f;

    public static void CalculateHappiness() {
        Happiness = ((Sleep + Saturation) - 80) / _modifierForHappiness;
    }

}

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement Instance;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public static Transform rb;

    [HideInInspector]
    public bool canMove = true;

    void Start() {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Transform>();
        //  Lock cursor
        //  Cursor.lockState = CursorLockMode.Locked;
        //  Cursor.visible = false;
    }

    void Update() {

        if (PlayerInteract.transitionOver) {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
                moveDirection.y = jumpSpeed;
            }
            else {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded) {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove) {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }


        }

    }
}