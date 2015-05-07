﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
    public GameObject map;
    public GameObject endFlag;
	public GameObject item1;
	public ArrayList items;
	private GameCamera cam;
    private float raceTime;
    private static float startPoint = 0;
    private static float endPoint;
	private int species1;
	public  static int species2;

	public static Dictionary<string, Dictionary<string, string>> relationship = new Dictionary<string, Dictionary<string, string>>();
	
	private const string FILEPATH = "\\Resources\\ItemLocationFiles\\1.txt";


	// Use this for initialization
	void Start () {
		Debug.Log ("this is specie2 : " + species2);

		species1 = PlayerPrefs.GetInt ("species1");
		
        cam = GetComponent<GameCamera>();
        SpawnMap();
		SpawnItem();
        SpawnPlayer();

        player2 = Instantiate(player2, new Vector3(-19f, 0f, 0f), Quaternion.identity) as GameObject;
        player2.name = "Player_sprite_2(Clone)";

        raceTime = 0;
        //Debug.Log("Before!!!!!!!!!");

        items = new ArrayList();
        //repeat until none
        // item = new GO();
        // PlaItem(item, xy);
        // items.Add (items);

        GameObject.Find("GameLogic").GetComponent<Running>().RunOnce();

		buildRelationship();
  
	}

	// Hard code for now!!
	private void buildRelationship() {

		for (int i = 1; i < 6; i++) {
			relationship.Add("animal" + i.ToString(), new Dictionary<string, string>());
		}
		relationship["animal1"].Add("animal1", "prey");
		relationship["animal1"].Add("animal4", "prey");
		relationship["animal1"].Add("animal5", "prey");
		relationship["animal2"].Add("animal2", "prey");
		relationship["animal2"].Add("animal3", "prey");
		relationship["animal3"].Add("animal3", "prey");
		relationship["animal3"].Add("animal4", "prey");
		relationship["animal3"].Add("animal5", "prey");
		relationship["animal4"].Add("animal4", "prey");
		relationship["animal4"].Add("animal5", "prey");
		relationship["animal5"].Add("animal5", "prey");
	}

	private void SpawnItem() {
		//Debug.Log(Application.dataPath);
		Dictionary<string, string> items = ItemReader.ReadFile(Application.dataPath + FILEPATH);
		List<string> keyList = new List<string>(items.Keys);
		foreach (string x in keyList) {
			PlaceItem(int.Parse(items[x]), float.Parse(x));
		}
	}
	
	private void SpawnPlayer() {
        player1 = Instantiate(player1, new Vector3(-19f, 0f, 0f), Quaternion.identity) as GameObject;
        cam.SetTarget(player1.transform);

        player1.name = "Player_sprite(Clone)";
		Running.player1 = this.player1;
	}

    private void SpawnMap()
    {

        float tempEnd = 20;
        for (int i = 0; i < 7; i++)
        {
           
          // Instantiate(map, new Vector3((float)(20 + (i * 62.9)), 0.507454f, 0), Quaternion.identity);
			//map = Instantiate(Resources.Load("Box")) as GameObject;

			map = Instantiate(Resources.Load("Prefabs/Maps/MapVariation_1"), new Vector3(tempEnd, 0.507454f, 0), Quaternion.identity) as GameObject;
			//map.name = map.name + " " + i;
			map.name = "aMap" + i;
			//(float)(20 + (i * 62.9))
		    tempEnd += 62.9f;

        }

		Instantiate(endFlag, new Vector3(tempEnd - 62.9f + 9, 0.507454f, 0), Quaternion.identity);
    } 

	private void PlaceItem(int speciesId, float x) {
		Debug.Log("Prefabs/Items/item" + speciesId.ToString());
		GameObject aItem = Instantiate (Resources.Load("Prefabs/Items/item" + speciesId.ToString()), new Vector3(x, 10f, 0f), Quaternion.identity) as GameObject;
		aItem.name = "animal" + speciesId;
	}
}
