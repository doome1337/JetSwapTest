using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Horizontal Movement")]
    [SerializeField] string xHorizontalAxisName;
    [SerializeField] string zHorizontalAxisName;
    [Range(1, 20)] [SerializeField] float movementSpeed;

    [Header("Joystick Number")]
    [SerializeField]
    int joystick;

    private void Movement() {
        float xThrow = Input.GetAxis(xHorizontalAxisName);
        float xMovement = xThrow * movementSpeed * Time.deltaTime;
        float xPos = transform.position.x + xMovement;

        float zThrow = Input.GetAxis(zHorizontalAxisName);
        float zMovement = zThrow * movementSpeed * Time.deltaTime;
        float zPos = transform.position.z + zMovement * -1;

       
        transform.position = new Vector3(xPos,
                                        transform.position.y,
                                         zPos);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}
}
