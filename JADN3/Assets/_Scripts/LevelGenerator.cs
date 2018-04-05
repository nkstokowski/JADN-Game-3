using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour {

	[HeaderAttribute("Custom Variables")]
	public int standardGridSize = 10;
	public GameObject upperParent;
	public GameObject lowerParent;
	public GameObject levelCamera;

	[HeaderAttribute("Custom Grid")]
	public List<LevelRow> upperGrid = new List<LevelRow>();
	public List<LevelRow> lowerGrid = new List<LevelRow>();

	List<GridSpace> gridSpaces = new List<GridSpace>();

	List<NavMeshSurface> surfaces = new List<NavMeshSurface>();


	void Start () {

		if((upperGrid.Count == 0 && lowerGrid.Count == 0)){	//If we didn't customize the level, generate a plain one
			SpawnPlainGrid();
		} else {
			//This creates a custom grid based on a true/false trigger.
			//This is used for creating custom levels with holes or paths.
			SpawnCustomGrid(upperGrid, 8, upperParent);
			SpawnCustomGrid(lowerGrid, 9, lowerParent);
		}
		SetCameraAngle();
		BakeNavMesh();
	}

	//This creates a standard fully-filled grid of cubes to act as the floor
	void SpawnPlainGrid() {
		Vector3 spawnPlace = Vector3.zero;
		for(int i=0;i<standardGridSize;i++){
			for(int j=0; j<standardGridSize;j++){
				//Calculate the position
				spawnPlace = new Vector3(i,0,j);

				//Create the upperCube
				GameObject upperCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				upperCube.transform.position = spawnPlace;
				upperCube.AddComponent<NavMeshSurface>();
				upperCube.transform.parent = upperParent.transform;
				//Add this gridSpace for reference
				gridSpaces.Add(new GridSpace(spawnPlace,true));
				surfaces.Add(upperCube.GetComponent<NavMeshSurface>());

				//Create the lowerCube
				GameObject lowerCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				lowerCube.transform.position = spawnPlace;
				lowerCube.AddComponent<NavMeshSurface>();
				lowerCube.transform.parent = lowerParent.transform;
				surfaces.Add(lowerCube.GetComponent<NavMeshSurface>());
				//Add this gridSpace for reference
				gridSpaces.Add(new GridSpace(spawnPlace,true));
			}
		}
	}

	void SpawnCustomGrid(List<LevelRow> layer, int layerId, GameObject parent) {
		Vector3 spawnPlace = Vector3.zero;

		//Loop through and create the grid based on marked flags.
		for(int i=0;i<layer.Count;i++){
			for(int j=0; j<layer[i].gridSpaces.Length;j++){
				if(layer[i].gridSpaces[j]){
					//Calculate the position
					spawnPlace = new Vector3(i,0,j);
					//Create the cube
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = spawnPlace;
					cube.layer = layerId;
					//Make it a navmesh surface
					cube.AddComponent<NavMeshSurface>();
					cube.transform.parent = parent.transform;
					//store reference to the surface.
					surfaces.Add(cube.GetComponent<NavMeshSurface>());
					//Add this gridSpace for reference
					gridSpaces.Add(new GridSpace(spawnPlace,layer[i].gridSpaces[j]));
				}
			}
		}
	}

	//Bakes the navmesh at runtime.
	void BakeNavMesh() {
		for(int i = 0;i < surfaces.Count ; i++ ){
			surfaces[i].BuildNavMesh();
		}
	}

	void SetCameraAngle () {
		if(gridSpaces.Count == 0){
			transform.LookAt(Vector3.zero);
			Debug.Log("Default Camera position");
			return;
		}

		float leftPosition = 0f;
		float rightPosition = 0f;
		float upPosition = 0f;
		float downPosition = 0f;

		foreach(GridSpace space in gridSpaces){
			Vector3 spacePosition = space.GetLocation();
			if(spacePosition.x < leftPosition) leftPosition = spacePosition.x;
			else if(spacePosition.x > rightPosition) rightPosition = spacePosition.x;
			else if(spacePosition.z < downPosition) downPosition = spacePosition.z;
			else if(spacePosition.z > upPosition) upPosition = spacePosition.z;
		}

		float hBound = (rightPosition + leftPosition) / 2;
		float vBound = (upPosition + downPosition) / 2;

		Vector3 cameraCenter = new Vector3(hBound,0,vBound);
		Debug.Log(cameraCenter);
		levelCamera.transform.LookAt(cameraCenter);

	}

	//End of Class
}

[System.Serializable]
public struct LevelRow {
	
	int gridSize;
	public bool[] gridSpaces;

	public LevelRow(int size){
		gridSize = size;
		gridSpaces = new bool[gridSize];
	}

	//End of struct
}

[System.Serializable]
public struct GridSpace {
	Vector3 location;
	bool isWalkable;

	public GridSpace(Vector3 location, bool canWalk) {
		this.location = location;
		this.isWalkable = canWalk;
	}

	public Vector3 GetLocation(){ return location; }
	public bool GetWalkable(){ return isWalkable; }

	public void SetLocation(Vector3 location) { this.location = location; }
	public void SetWalkable(bool canWalk) { this.isWalkable = canWalk; }

	//End of struct
}



