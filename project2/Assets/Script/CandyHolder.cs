using UnityEngine;
using System.Collections;

public class CandyHolder : MonoBehaviour {

    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;

    int candy = DefaultCandyAmount;
    int counter;

    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }
    public int GetCandyAmount()
    {
        return candy;
    }
    public void AddCandy( int amount )
    {
        candy += amount;
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        // 사탕의 투척 기회수를 표시
        string label = "Candy : " + candy;
        

        if (counter > 0) label = label + "(" + counter + "s)";
        GUI.Label(new Rect(0, 0, 100, 30), label);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(candy < DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
	}
    IEnumerator RecoverCandy()
    {
        counter = RecoverySeconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        candy++;
    }
}
