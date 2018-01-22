using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Game Objects Needed")]
    [SerializeField] GameObject head;
    [SerializeField] GameObject player;

    [Header("Horizontal Movement")]
    [Range(1, 20)] [SerializeField] float movementSpeed;

    [Header("Rotation Movement")]
    [Range(150, 500)][SerializeField] float rotationSpeed;

    [Header("Joystick Number")]
    [SerializeField] int joystick;

    public Ray r;

    private string LSLeftRight = "LSLeftRight";
    private string LSUpDown = "LSUpDown";
    private string RSLeftRight = "RSLeftRight";
    private string RSUpDown = "RSUpDown";

    private float furthestHeadUp = -95;
    private float furthestHeadDown = -35;

    private enum HeadTopBottom {
        Top,
        Bottom,
        Neither
    }

    private HeadTopBottom headLoc = HeadTopBottom.Neither;

    private void UserMovement() {
        float xThrow = Input.GetAxis(LSLeftRight + joystick);
        float xMovement = xThrow * movementSpeed * Time.deltaTime;

        float zThrow = Input.GetAxis(LSUpDown + joystick);
        float zMovement = zThrow * movementSpeed * Time.deltaTime;

        //r = new Ray(transform.position, transform.forward);
        //GetComponent<swapper>().shoot(r);

        transform.position += transform.rotation * new Vector3(xMovement, 0, -zMovement);

        //Doesn't allow player to go upward's, if they're looking up.
        //transform.position = new Vector3(transform.position.x,
                                         //0,
                                         //transform.position.z);
    }

    private void RotationMovement() {
        RotateLeftRight();
        LookUpDown();
    }

    private void RotateLeftRight() {
        float xThrow = 0;
        if (Input.GetAxis(RSLeftRight + joystick) < 0) {
            xThrow = -rotationSpeed;
        }
        else if (Input.GetAxis(RSLeftRight + joystick) > 0) {
            xThrow = rotationSpeed;
        }
        player.transform.Rotate(Vector3.down * Time.deltaTime * xThrow * -1);
    }

    private void LookUpDown() {
        float yThrow = 0;
        if (Input.GetAxis(RSUpDown + joystick) < 0) {
            yThrow = -rotationSpeed;

        }
        else if (Input.GetAxis(RSUpDown + joystick) > 0) {
            yThrow = rotationSpeed;
        }
        Vector3 rotateHead = Vector3.left * Time.deltaTime * yThrow * -1;
        Quaternion currLocRotation = head.transform.localRotation;
        GameObject tempHead = head;

        //if ((int)(currLocRotation.x * 100) < furthestHeadUp) {
        //    player.transform.Rotate(Vector3.left * Time.deltaTime * yThrow * -1);
        //    print("Here");
        //}
        //else if ((int)(currLocRotation.z * 100) > furthestHeadRight && currMovement == LeftRightMovement.Right)
        //{
        //    player.transform.Rotate(Vector3.down * Time.deltaTime * yThrow * -1);
        //}
        //else
        //{
        if((int)(currLocRotation.x * 100) < furthestHeadUp) {
            headLoc = HeadTopBottom.Top;
        }
        else if((int)(currLocRotation.x * 100) > furthestHeadDown) {
            headLoc = HeadTopBottom.Bottom;
        }

        if((headLoc == HeadTopBottom.Top && yThrow > 0) ||
                (headLoc == HeadTopBottom.Bottom && yThrow < 0) ||
                headLoc == HeadTopBottom.Neither){

            tempHead.transform.Rotate(rotateHead);
            headLoc = HeadTopBottom.Neither;
        }
            
        //}
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        UserMovement();
        RotationMovement();
	}


}
