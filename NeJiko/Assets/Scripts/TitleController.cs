using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    public Text highScoreLabel;
    // Use this for initialization
    public void Start()
    {
    
        
            //highScoreLabel.text = "High Score : ";
        highScoreLabel.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
       

    }
    public void OnStartButtonClicked()
	{
        SceneManager.LoadScene ("Main");
        //Application.LoadLevel("Main");
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
