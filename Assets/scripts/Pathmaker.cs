using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// MAZE PROC GEN LAB
// all students: complete steps 1-6, as listed in this file
// optional: if you're up for a bit of a mind safari, complete the "extra tasks" to do at the very bottom

// STEP 1: ======================================================================================
// put this script on a Sphere... it SHOULD move around, and drop a path of floor tiles behind it

public class Pathmaker : MonoBehaviour {

	// STEP 2: ============================================================================================
	// translate the pseudocode below

	//	DECLARE CLASS MEMBER VARIABLES:
	//private int counter = 0;//	Declare a private integer called counter that starts at 0; 		// counter will track how many floor tiles I've instantiated
	public Transform floorPrefab;//	Declare a public Transform called floorPrefab, assign the prefab in inspector;
	public Transform pathmakerSpherePrefab; //	Declare a public Transform called pathmakerSpherePrefab, assign the prefab in inspector; 		// you'll have to make a "pathmakerSphere" prefab later
	public GameObject tilesObj;
	public GameObject SphereObj;
	public static int globalTileCount = 0;
	public static int MaxTile = 0;
	public static int pathmakerLife = 0;
	private float forwardcount = 10;
	public static int counter = 0;
	private int chooseTile;
	public Sprite tile1;
	public Sprite tile2;
	public Sprite tile3;
	public Sprite tile4;
	public GameObject camera1;
	public GameObject canvas1;
	public static Vector3 tilesPos = new Vector3(0, 390, 0);
	public AudioSource sounds;
	public AudioClip spawnsound;
	private bool readytospawn;

	void Start() {
		counter = Random.Range(50, 100);
		chooseTile = Random.Range(1,5);
		tilesPos = new Vector3(0, 390, 0);
		canvas1.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && readytospawn == true)
		{
			readytospawn = false;
			Pathmaker.globalTileCount = 0;
			counter = 0;
			sounds.PlayOneShot(spawnsound);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			Debug.Log("Restart");
		}

		if (counter < 500 && Pathmaker.globalTileCount < 500) 
		{                      
			float randomNum = Random.Range(0.0f, 1.0f);
			Pathmaker.pathmakerLife = Random.Range(1, 20);

			if (randomNum < 0.2f)    //			If random number is less than 0.25f, then rotate myself 90 degrees;
			{
				transform.Rotate(0, 90, 0); //rotate 90
			}
				else if (randomNum == Random.Range(0.3f, 0.4f))
				{
					transform.Rotate(0, -90, 0); //rotate -90
				}
				else if (randomNum == Random.Range(0.4f, 0.5f))
				{
					transform.Rotate(0, 180, 0); //rotate -90
				}
				else if (randomNum == Random.Range(0.5f,0.6f)) 
				{
					transform.Rotate(0, -180, 0);
				}
				else if (randomNum > 0.9f)
				{
					Instantiate(pathmakerSpherePrefab, transform.position, transform.rotation);
				}

				GameObject tile = Instantiate(floorPrefab.gameObject, transform.position, Quaternion.Euler(90, 0, 0));
				tilesPos += transform.position;
				SpriteRenderer tileSprite = tile.GetComponent<SpriteRenderer>();
				if (chooseTile == 1)
				{
					tileSprite.sprite = tile1;
				}
				else if (chooseTile == 2)
				{
					tileSprite.sprite = tile2;
				}
				else if (chooseTile == 3)
				{
					tileSprite.sprite = tile3;
				}
				else if (chooseTile == 4)
				{
					tileSprite.sprite = tile4;
				}

			transform.Translate(Vector3.forward * forwardcount);
				Pathmaker.globalTileCount++;

			Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.5f);

			camera1.transform.position = new Vector3(tilesPos.x / (globalTileCount + 15), 390, tilesPos.z / (globalTileCount + 15));
			canvas1.transform.position = new Vector3(tilesPos.x / (globalTileCount + 15), 0, tilesPos.z / (globalTileCount + 15));

			counter++;

		}
		else//		once 500 count is reached.. 
		{
			Destroy(gameObject); //kiill self
			canvas1.gameObject.SetActive(true);
			readytospawn = true;
		}

	}

} 

// MORE STEPS BELOW!!!........

// STEP 3: =====================================================================================
// implement, test, and stabilize the system

//	IMPLEMENT AND TEST:
//	- save your scene! the code could potentially be infinite / exponential, and crash Unity
//	- put Pathmaker.cs on a sphere, configure all the prefabs in the Inspector, and test it to make sure it works
//	STABILIZE: 
//	- code it so that all the Pathmakers can only spawn a grand total of 500 tiles in the entire world; how would you do that?
//	- hint: declare a "public static int" and have each Pathmaker check this "globalTileCount", somewhere in your code? 
//      -  What is a 'static'?  Static???  Simply speak the password "static" to the instructor and knowledge will flow.
//	- Perhaps... if there already are enough tiles maybe the Pathmaker could Destroy my game object

// STEP 4: ======================================================================================
// tune your values...

// a. how long should a pathmaker live? etc.  (see: static  ---^)
// b. how would you tune the probabilities to generate lots of long hallways? does it... work?
// c. tweak all the probabilities that you want... what % chance is there for a pathmaker to make a pathmaker? is that too high or too low?



// STEP 5: ===================================================================================
// maybe randomize it even more?

// - randomize 2 more variables in Pathmaker.cs for each different Pathmaker... you would do this in Start()
// - maybe randomize each pathmaker's lifetime? maybe randomize the probability it will turn right? etc. if there's any number in your code, you can randomize it if you move it into a variable



// STEP 6:  =====================================================================================
// art pass, usability pass

// - move the game camera to a position high in the world, and then point it down, so we can see your world get generated
// - CHANGE THE DEFAULT UNITY COLORS
// - add more detail to your original floorTile placeholder -- and let it randomly pick one of 3 different floorTile models, etc. so for example, it could randomly pick a "normal" floor tile, or a cactus, or a rock, or a skull
// - or... make large city tiles and create a city.  Set the camera low so and une the values so the city tiles get clustered tightly together.

//		- MODEL 3 DIFFERENT TILES IN BLENDER.  CREATE SOMETHING FROM THE DEEP DEPTHS OF YOUR MIND TO PROCEDURALLY GENERATE. 
//		- THESE TILES CAN BE BASED ON PAST MODELS YOU'VE MADE, OR NEW.  BUT THEY NEED TO BE UNIQUE TO THIS PROJECT AND CLEARLY TILE-ABLE.

//		- then, add a simple in-game restart button; let us press [R] to reload the scene and see a new level generation
// - with Text UI, name your proc generation system ("AwesomeGen", "RobertGen", etc.) and display Text UI that tells us we can press [R]


// OPTIONAL EXTRA TASKS TO DO, IF YOU WANT / DARE: ===================================================

// AVOID SPAWNING A TILE IN THE SAME PLACE AS ANOTHER TILE  https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
// Check out the Physics.OverlapSphere functionality... 
//     If the collider is overlapping any others (the tile prefab has one), prevent a new tile from spawning and move forward one space. 

// DYNAMIC CAMERA:
// position the camera to center itself based on your generated world...
// 1. keep a list of all your spawned tiles
// 2. then calculate the average position of all of them (use a for() loop to go through the whole list) 
// 3. then move your camera to that averaged center and make sure fieldOfView is wide enough?

// BETTER UI:
// learn how to use UI Sliders (https://unity3d.com/learn/tutorials/topics/user-interface-ui/ui-slider) 
// let us tweak various parameters and settings of our tech demo
// let us click a UI Button to reload the scene, so we don't even need the keyboard anymore.  Throw that thing out!

// WALL GENERATION
// add a "wall pass" to your proc gen after it generates all the floors
// 1. raycast downwards around each floor tile (that'd be 8 raycasts per floor tile, in a square "ring" around each tile?)
// 2. if the raycast "fails" that means there's empty void there, so then instantiate a Wall tile prefab
// 3. ... repeat until walls surround your entire floorplan
// (technically, you will end up raycasting the same spot over and over... but the "proper" way to do this would involve keeping more lists and arrays to track all this data)
