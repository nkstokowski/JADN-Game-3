using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScript : MonoBehaviour {

    public Camera topCamera;
    public Camera bottomCamera;

    public Color topBackground;
    public Color bottomBackground;

    public Color topGround;
    public Color bottomGround;

    public Material groundMat;

    private Quaternion targetRotation;
    private float flipAngle = 0f;
    private bool flipping = false;
    private bool faceUp = true;
    private bool halfway = false;
    public float speed = 2f;

    public GameManager gameManager;


	// Use this for initialization
	void Start () {
        targetRotation = transform.rotation;
        gameManager.faceUp = faceUp;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.F) && !flipping)
        {
            gameManager.flipStatus(!faceUp);
            flipAngle = (flipAngle == 180) ? 0f : 180f;
            targetRotation = Quaternion.AngleAxis(flipAngle, Vector3.left);
            flipping = true;
        }

        if (flipping)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * speed * Time.deltaTime);
            Color targetColor = (faceUp) ? bottomBackground : topBackground;
            topCamera.backgroundColor = Color.Lerp(topCamera.backgroundColor, targetColor, 10 * speed * Time.deltaTime);
            bottomCamera.backgroundColor = Color.Lerp(bottomCamera.backgroundColor, targetColor, 10 * speed * Time.deltaTime);
            targetColor = (faceUp) ? bottomGround : topGround;
            //groundMat.color = Color.Lerp(groundMat.color, targetColor, 100 * speed * Time.deltaTime);

            if (!halfway && Quaternion.Angle(transform.rotation, targetRotation) < 130f)
            {
                topCamera.gameObject.SetActive(!faceUp);
                bottomCamera.gameObject.SetActive(faceUp);
                halfway = true;
            }

            if(Quaternion.Angle(transform.rotation, targetRotation) < .2f)
            {
                targetRotation = Quaternion.AngleAxis(flipAngle - .1f, Vector3.left);
                transform.rotation = targetRotation;
                flipping = false;
                halfway = false;
                faceUp = !faceUp;
            }
        }

    }
}
