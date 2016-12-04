using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControll : MonoBehaviour {

    public NeJikoController nejiko;
    public Text scoreLabel;
    public LifePanel lifePanel;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int score = CalcScore();
        scoreLabel.text = "Score : " + score + " m";
        lifePanel.UpdateLife(nejiko.Life());

        if(nejiko.Life() <= 0)
        {
            enabled = false; // 업데이트 종료

            //하이스코어 저장.
            if(PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            Invoke("ReturnToTitle", 2.0f); // Invoke는 지정한 함수를 일정 시간 뒤에 실행.
            
        }
	}

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
