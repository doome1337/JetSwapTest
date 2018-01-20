using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Game Objects Needed")]

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

    private void UserMovement() {
        float xThrow = Input.GetAxis(LSLeftRight + joystick);
        float xMovement = xThrow * movementSpeed * Time.deltaTime;

        float zThrow = Input.GetAxis(LSUpDown + joystick);
        float zMovement = zThrow * movementSpeed * Time.deltaTime;

        r = new Ray(transform.position, transform.forward);
        GetComponent<swapper>().shoot(r);

        transform.position += transform.rotation * new Vector3(xMovement, 0, -zMovement);

        //Doesn't allow player to go upward's, if they're looking up.
        transform.position = new Vector3(transform.position.x,
                                         0,
                                         transform.position.z);
    }

    private void RotationMovement() {
        float xThrow = Input.GetAxis(RSLeftRight + joystick);
        float xMovement = xThrow * rotationSpeed * Time.deltaTime;
        float xPos = transform.eulerAngles.y + xMovement;

        float yThrow = Input.GetAxis(RSUpDown + joystick);
        float yMovement = yThrow * rotationSpeed * Time.deltaTime;
        float yPos = transform.eulerAngles.x + yMovement;

        transform.eulerAngles = new Vector3(yPos, xPos, transform.eulerAngles.z);
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
