using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    const int SphereCandyFrequnency = 3;
    int sampleCandyCount;

    public GameObject[] candyPrefabs;
    public GameObject[] candySquarePrefabs;
    public CandyHolder candyHolder;

    public float shotSpeed;
    public float shotTorque;
    public float baseWidth;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    GameObject SampleCandy()
    {
        GameObject prefab = null;

        if (sampleCandyCount % SphereCandyFrequnency == 0)
        {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];

        }
        else
        {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];

        }
        sampleCandyCount++;

        return prefab;

    }
    Vector3 GetInstantiatePosition()
    {
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot()
    {
        if (candyHolder.GetCandyAmount() <= 0) return;

        // 프리팹에서 candy 오브젝트를 생성
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            //transform.position,
            GetInstantiatePosition(),
            Quaternion.identity //회전 없음
            );

        candy.transform.parent = candyHolder.transform;

        // candy 오브젝트의 Rigidbody를 취득하여 힘과 회전을 더한다.
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotSpeed);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        candyHolder.ConsumeCandy();
    }
}
