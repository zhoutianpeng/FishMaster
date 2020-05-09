 using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;

    public Transform[] genPositions;

    public GameObject[] fishPrefabs;

    private float fishGenWaitTime = 0.5f;
    private float waveGenWaitTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeFishes", 0, waveGenWaitTime);
    }

    void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPositions.Length);
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;
        int num = Random.Range( (maxNum / 2) + 1, maxNum);
        int speed = Random.Range( (maxSpeed / 2), maxSpeed);
        int moveType = Random.Range(0, 2);
        int angOffset;
        int angSpeed;

        if(moveType == 0) {
            //直线
            angOffset = Random.Range(-22, 22);
            StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }

        else{
            //曲线
            if(Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(-15, -9);
            }
            else 
            {
                angSpeed = Random.Range(9, 15);
            }
             StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }
    }

    IEnumerator GenStraightFish(int posIndex, int fishPreIndex, int num ,int speed, int angOffset){
        for(int i = 0; i < num ;i++ ){
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPositions[posIndex].localPosition;
            fish.transform.localRotation = genPositions[posIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            yield return new WaitForSeconds(fishGenWaitTime); 
        }
    }

    IEnumerator GenTurnFish(int posIndex, int fishPreIndex, int num ,int speed, int angSpeed){
    for(int i = 0; i < num ;i++ ){
        GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
        fish.transform.SetParent(fishHolder, false);
        fish.transform.localPosition = genPositions[posIndex].localPosition;
        fish.transform.localRotation = genPositions[posIndex].localRotation;
        fish.GetComponent<SpriteRenderer>().sortingOrder += i;
        fish.AddComponent<Ef_AutoMove>().speed = speed;
        fish.AddComponent<Ef_AutoRotate>().speed = angSpeed; 
        yield return new WaitForSeconds(fishGenWaitTime); 
    }
    }
}
