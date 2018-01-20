using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Game Objects Needed")]

    [Header("Horizontal Movement")]
    [SerializeField] string xHorizontalAxisName;
    [SerializeField] string zHorizontalAxisName;
    [Range(1, 20)] [SerializeField] float movementSpeed;

    [Header("Rotation Movement")]
    [Range(150, 500)][SerializeField] float rotationSpeed;
    [SerializeField] string xHorizontalRotationAxisName;
    [SerializeField] string yVerticalRotationAxisName;

    [Header("Joystick Number")]
    [SerializeField]
    int joystick;
    public Ray r;

    private void UserMovement() {
        float xThrow = Input.GetAxis(xHorizontalAxisName);
        float xMovement = xThrow * movementSpeed * Time.deltaTime;

        float zThrow = Input.GetAxis(zHorizontalAxisName);
        float zMovement = zThrow * movementSpeed * Time.deltaTime;

        r = new Ray(transform.position, transform.forward);
        GetComponent<swapper>().shoot(r);

        transform.position += transform.rotation * new Vector3(xMovement, 0, -zMovement);
        transform.position = new Vector3(transform.position.x,
                                         0,
                                         transform.position.z);
    }

    private void RotationMovement() {
        float xThrow = Input.GetAxis(xHorizontalRotationAxisName);
        float xMovement = xThrow * rotationSpeed * Time.deltaTime;
        float xPos = transform.eulerAngles.y + xMovement;

        float yThrow = Input.GetAxis(yVerticalRotationAxisName);
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
