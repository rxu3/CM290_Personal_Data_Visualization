using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Energy2 : MonoBehaviour
{

    public Text textenergy;
    public float currentenergy;
    //public Text textobject;
    public float amount = 0.002f;
    public float startingenergy = 100;
    public Slider barenergy;
	public float[] energy=new float[]{40f,39.5f,39.1f,38.8f,38f,37f,35f,32f,28f,25f,37f,36.5f,36.1f,35.8f,30f,29f,23f,23f,23f,23f,30f, 31f, 34f, 40f, 23f, 50f, 34f, 46f, 45f, 50f};

    // Use this for initialization
    void Start()
    {
        barenergy = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentenergy = startingenergy;
        // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
		StartCoroutine("Delay");
    }

    // Update is called once per frame
    /*void Update()
    {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = textenergy.text.Split(':');
        textenergy.text = tmp[0] + ":" + (int)currentenergy;
        //Debug.Log(currentenergy);
        currentenergy -= amount;
        if (currentenergy < 30)
        {
            currentenergy = 30;
        }
        else if (currentenergy > 100)
        {
            currentenergy = 100;
        }
        barenergy.maxValue = startingenergy;
        barenergy.value = currentenergy;
        //energy.minValue = 0;
    }*/

	IEnumerator Delay(){
		
		//Debug.Log ("wait is over");
		string[] tmp = textenergy.text.Split(':');
		//Debug.Log ("I am in happiness update");
		for (int i = 0; i < 30; i++) {
			//efficiency [i] = ;
			//StartCoroutine("Delay");
			yield return new WaitForSeconds (5);
			//Debug.Log ("I am waiting");
			currentenergy=energy[i] + 50;
			textenergy.text = tmp[0] + ":" + currentenergy;
			//Debug.Log (currentHappiness);
			//currentHappiness -= amount;
			//if (currentHappiness < 50) {
			//	currentHappiness = 50;
			//} else if (currentHappiness > 100) {
			//	currentHappiness = 100;
			barenergy.maxValue = startingenergy;
			barenergy.value = currentenergy;
		}

	}

}
