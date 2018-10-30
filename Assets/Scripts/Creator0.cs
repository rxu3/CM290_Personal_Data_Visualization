using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator0 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CreatePrefab ();
	}
	
	// Update is called once per frame

	public GameObject Red1;
	public GameObject Prefab1;
	public GameObject Prefab2;

	public float[] morning=new float[]{40f,39.5f,39.1f,38.8f,38f,37f,35f,32f,28f,25f};
	public float[] afternoon=new float[]{37f,36.5f,36.1f,35.8f,30f,29f,23f,23f,23f,23f};
	public float[] night = new float[]{30f, 31f, 34f, 40f, 23f, 30f, 34f, 30f, 30f, 30f};

	public float[] wemorning=new float[]{40f,39.5f,39.1f,38.8f,38f,37f,35f,35f,35f,34f};
	public float[] weafternoon=new float[]{37f,37f,36.1f,35.8f,34f,35.2f,35.3f,35f,34f,34f};
	public float[] wenight = new float[]{25f, 24.5f, 24.4f, 24f, 23.9f, 20f, 21f, 23f, 20f, 30f};
	void CreatePrimitive(){
		Red1=GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//Red1.transform.position = new Vector3(68, 13, 21);
	}
	void CreatePrefab(){
		for (int i=0;i<10;i++){
			Instantiate(Prefab1,new Vector3(67.0f+i, morning[i]-5, 10.0f),Quaternion.identity);
		//Debug.Log ("I am creating prefab");
		}
		for (int j=0;j<10;j++){
			Instantiate(Prefab1,new Vector3(77+j,afternoon[j]-5,10),Quaternion.identity);
		}
		for (int k=0;k<10;k++){
			Instantiate(Prefab1,new Vector3(87+k,night[k]-5,10),Quaternion.identity);
		}
		for (int i=0;i<10;i++){
			Instantiate(Prefab2,new Vector3(67.0f+i, wemorning[i]-5, 0.0f),Quaternion.identity);
			//Debug.Log ("I am creating prefab");
		}
		for (int j=0;j<10;j++){
			Instantiate(Prefab2,new Vector3(77+j,weafternoon[j]-5,0),Quaternion.identity);
		}
		for (int k=0;k<10;k++){
			Instantiate(Prefab2,new Vector3(87+k,wenight[k]-5,0),Quaternion.identity);
		}
	}
}
