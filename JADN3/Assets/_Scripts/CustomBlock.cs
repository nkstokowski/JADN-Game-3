using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Switch type - 
//      MoveOnce:     The block will move from start position to end position.
//                    Will only initiate move once, and only occurs when the 
//                    block receives an ON signal
//
//      MoveTrack:    The block will move on a track. On an ON signal it will
//                    start moving toward the end position. On an OFF signal
//                    it will start moving toward the start position.
//
//      MoveLoop:     NOT IMPLEMENTED. The block, while active, will move back
//                    and forth between the start and end positions. An ON signal
//                    will turn this movement on, and an OFF signal will turn
//                    this movement off. 
//
//      Transforming: When this block receives an ON, OFF, or TRIGGER signal
//                    it will instantiate a new instance of the specified
//                    New Block Object. It will set the layer of the block
//                    equal to this one and then destroy itself.
public enum BlockType
{
    MoveOnce,
    MoveTrack,
    MoveLoop,
    Transforming
	//Portal
}

public class CustomBlock : MonoBehaviour, SwitchTarget
{
    // This Blocks type
    public BlockType customBlockType;

    [Header("Movement")]
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float moveSpeed = 5.0f;
    public bool differentReturnSpeed = false;
    public float returnSpeed = 5.0f;
    private bool movingForward;

    [Header("Transformation")]
    public GameObject newBlockObject;


    void Start()
    {
        // Set start position in case you forget to
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Always move towards the current target position
        Vector3 targetPosition = (movingForward) ? endPosition : startPosition;
        float speed;
        if (differentReturnSpeed)
        {
            speed = (movingForward) ? moveSpeed : returnSpeed;
        }
        else
        {
            speed = moveSpeed;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
    }

    // Spawns a new block from a prefab. Block moves to this block and layer.
    // Destroys self.
    private void CreateBlock()
    {
        Debug.Log("In create");
        GameObject block = Instantiate(newBlockObject, transform.position, transform.rotation);
        block.transform.parent = transform.parent;
        block.GetComponent<Block>().SetBlockLayer(gameObject.layer);
        Destroy(gameObject);
    }

    // Receive the signal that the switch was turned on
    public void HandleSwitchOn()
    {
        switch (customBlockType)
        {
            case BlockType.Transforming:
                CreateBlock();
                break;
            case BlockType.MoveTrack:
            case BlockType.MoveOnce:
                movingForward = true;
                break;
			//case BlockType.Portal:
				//TurnOnPortal ();
				//break;
        }
    }

	void TurnOnPortal()
	{
		transform.Find ("Portal").gameObject.SetActive (true);
	}

    // Receive the signal that the switch was turned off
    public void HandleSwitchOff()
    {
        switch (customBlockType)
        {
            case BlockType.Transforming:
                CreateBlock();
                break;
            case BlockType.MoveTrack:
                movingForward = false;
                break;
        }
    }

    // Receive the signal that the switch was triggered
    public void HandleSwitchTrigger()
    {
        switch (customBlockType)
        {
            case BlockType.Transforming:
                CreateBlock();
                break;
        }
    }

}