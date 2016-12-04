using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour {

    const int StageTipSize = 30;
    int currentTipIndex;

    public Transform character; // 대상 캐릭터 지정
    public GameObject[] stageTips; // 스테이지 팁의 프리팹 배열
    public int startTipIndex; // 자동 생성 캐시 인덱스
    public int preInstantiate; // 미리 생성해서 읽어들일 스테이지 팁의 개수
    public List<GameObject> generatedStageList = new List<GameObject>();  //생성된 스테이지 팁의 보유 리스트


	// Use this for initialization
	void Start () {
        // 초기화
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
	}
	
	// Update is called once per frame
	void Update () {
        int charaPositionIndex = (int)(character.position.z / StageTipSize); //현재 캐릭터의 포지션 
        
        if(charaPositionIndex + preInstantiate > currentTipIndex) //다음 스테이지 팁에 들어가면 스테이지 업데이트 실시 0+5 > 0
        {
            //Debug.Log(charaPositionIndex + preInstantiate);
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }
    
    void UpdateStage (int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;
        Debug.Log(toTipIndex);
        Debug.Log(currentTipIndex);
        //지정한 스테이지 팁까지 작성
        for (int i = currentTipIndex + 1; i<= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            generatedStageList.Add(stageObject); //생성한 스테이지를 관리에 추가

            /* 스테이지 보유 한도를 초과 한다면 예전 스테이지를 삭제 */
            while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

            currentTipIndex = toTipIndex;

        }


    }

    // 지정 인덱스 위치에 Stage 오브젝트를 임의로 작성
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);
        // 스테이지 생성
        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0,0, tipIndex * StageTipSize),
            Quaternion.identity
            ) ;

        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
