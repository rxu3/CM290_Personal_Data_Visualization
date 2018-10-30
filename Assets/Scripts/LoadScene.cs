using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadScene : MonoBehaviour {

    public void SenceLoader(int SceneIndex){
        SceneManager.LoadScene(SceneIndex);

    }
}
