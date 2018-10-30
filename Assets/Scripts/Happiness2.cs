using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness2 : MonoBehaviour
{

    public Text texthappiness;
    public float currentHappiness;
    //public Text textobject;
    public float amount =0.002f;
    public float startingHappiness = 100;
    public Slider barhappiness;
    // Use this for initialization
    void Start()
    {
        barhappiness = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentHappiness = startingHappiness;
        // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
		//HappinessUpdate();
		//StartCoroutine(delay());
		StartCoroutine("Delay");

    }
	public float[] we=new float[]{40f,39.5f,39.1f,38.8f,38f,37f,35f,35f,35f,34f,37f,37f,36.1f,35.8f,34f,35.2f,35.3f,35f,34f,34f,25f, 24.5f, 24.4f, 24f, 23.9f, 20f, 21f, 23f, 20f, 30f};
	//public float[] efficiency;
    // Update is called once per frame


	/*void HappinessUpdate(){
		string[] tmp = texthappiness.text.Split(':');
		//Debug.Log ("I am in happiness update");
		for (int i = 0; i < 30; i++) {
			//efficiency [i] = ;
			StartCoroutine(Delay());
			Debug.Log ("I am waiting");
			currentHappiness=we[i] + 60;
			texthappiness.text = tmp[0] + ":" + currentHappiness;
			Debug.Log (currentHappiness);
			//currentHappiness -= amount;
			//if (currentHappiness < 50) {
			//	currentHappiness = 50;
			//} else if (currentHappiness > 100) {
			//	currentHappiness = 100;
		}
		barhappiness.maxValue = startingHappiness;
		barhappiness.value = currentHappiness;
	}*/
		
	IEnumerator Delay(){
		//yield return new WaitForSeconds (5);
		//Debug.Log ("wait is over");
		string[] tmp = texthappiness.text.Split(':');
		//Debug.Log ("I am in happiness update");
		for (int i = 0; i < 30; i++) {
			//efficiency [i] = ;
			//StartCoroutine("Delay");
			//Debug.Log ("I am waiting");
			yield return new WaitForSeconds (5);
			currentHappiness=we[i] + 60;
			texthappiness.text = tmp[0] + ":" + currentHappiness;
			//Debug.Log (currentHappiness);
			//currentHappiness -= amount;
			//if (currentHappiness < 50) {
			//	currentHappiness = 50;
			//} else if (currentHappiness > 100) {
			//	currentHappiness = 100;
			barhappiness.maxValue = startingHappiness;
			barhappiness.value = currentHappiness;
		}

	}

	//energy.minValue = 0;
}
    /*void Update()
    {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = texthappiness.text.Split(':');
        texthappiness.text = tmp[0] + ":" + (int)currentHappiness;
        currentHappiness -= amount;
        if (currentHappiness < 50)
        {
            currentHappiness = 50;
        }
        else if (currentHappiness > 100)
        {
            currentHappiness = 100;
        }
        barhappiness.maxValue = startingHappiness;
        barhappiness.value = currentHappiness;
        //energy.minValue = 0;
    }*/


