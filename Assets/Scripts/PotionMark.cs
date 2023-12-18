using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PotionMark : MonoBehaviour
{
    [SerializeField] GameObject markPrefab;
    GameObject mark;
    [SerializeField] TMP_Text scoreTxt;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        float yPosition = Random.Range(-transform.localScale.y/2,transform.localScale.y/2);
        Vector3 markPosition = new Vector3(transform.position.x, transform.position.y + yPosition, -1);
        mark = Instantiate(markPrefab, markPosition, Quaternion.identity);
        FindObjectOfType<MainButton>().buttonRelease.AddListener(GetScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetScore(GameObject potion){
        float yDistance = mark.transform.position.y - potion.transform.GetChild(0).transform.position.y;
        yDistance = Mathf.Abs(yDistance);
        print(yDistance);
        if (yDistance<0.1)
        {
            print("perfect");
            score+=100;
        } else if (yDistance<0.5)
        {
            print("good");
            score+=50;
        } else if (yDistance<1)
        {
            print("not bad");
            score+=25;
        } else if (yDistance>1)
        {
            score-=100;
            print("fail");
        }
        print("SCORE = " + score);
        scoreTxt.text = "SCORE: " + score.ToString();
        UpdateMark();
    }

    void UpdateMark(){
        
        float yPosition = Random.Range(-transform.localScale.y/2,transform.localScale.y/2);
        Vector3 markPosition = new Vector3(transform.position.x, transform.position.y + yPosition);
        mark.transform.position = markPosition;
    }
}
