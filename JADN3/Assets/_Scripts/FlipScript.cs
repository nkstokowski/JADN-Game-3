using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool endSignal = false;
    public float speed = 2f;

	public GameObject flipButton;
	public Sprite toBottomImage;
	public Sprite toTopImage;
	private bool dimension;	//True == showToTop | false == showToBottom

    public GameManager gameManager;
    public Indication indication;

	public bool canFlip = true;

	// Use this for initialization
	void Start () {
        targetRotation = transform.rotation;
        gameManager.faceUp = faceUp;
        flipButton.GetComponent<Image>().sprite = toBottomImage;
	}
	
	// Update is called once per frame
	void Update () {
		if (canFlip) 
		{
			if (Input.GetKeyUp(KeyCode.F))
			{
                TriggerFlip();
			}
		}

        if (flipping)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * speed * Time.deltaTime);
            Color targetColor = (faceUp) ? bottomBackground : topBackground;
            topCamera.backgroundColor = Color.Lerp(topCamera.backgroundColor, targetColor, 10 * speed * Time.deltaTime);
            bottomCamera.backgroundColor = Color.Lerp(bottomCamera.backgroundColor, targetColor, 10 * speed * Time.deltaTime);
            targetColor = (faceUp) ? bottomGround : topGround;
            //groundMat.color = Color.Lerp(groundMat.color, targetColor, 100 * speed * Time.deltaTime);

            // The halway bool is used to swap the cameras
            if (!halfway && Quaternion.Angle(transform.rotation, targetRotation) < 130f)
            {
                topCamera.gameObject.SetActive(!faceUp);
                bottomCamera.gameObject.SetActive(faceUp);
                halfway = true;
            }

            // The endSignal is used to renable the players nav mesh agents
            if(!endSignal && Quaternion.Angle(transform.rotation, targetRotation) < 3f)
            {
                gameManager.EndFlip();
                endSignal = true;
            }

            // This final check is used to finish the rotation and reset all other variables
            if(Quaternion.Angle(transform.rotation, targetRotation) < .2f)
            {
                targetRotation = Quaternion.AngleAxis(flipAngle - .1f, Vector3.left);
                transform.rotation = targetRotation;
                flipping = false;
                halfway = false;
                endSignal = false;
                faceUp = !faceUp;        
            }
        }

    }
    public void TriggerFlip(){
        if (!flipping){
        gameManager.StartFlip(!faceUp);
        flipAngle = (flipAngle == 180) ? 0f : 180f;
        targetRotation = Quaternion.AngleAxis(flipAngle, Vector3.left);
        flipping = true;
        dimension = !dimension;
        SetDimensionButton();
        indication.SetFlashing(false);
        }
    }

     void SetDimensionButton(){
        if(dimension)
        	flipButton.GetComponent<Image>().sprite = toTopImage;
		else
			flipButton.GetComponent<Image>().sprite = toBottomImage;
    }
}
