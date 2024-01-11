using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject prefabTest;
    [SerializeField] Sprite spriteTest;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] TMP_Text timeGO;
    [SerializeField] GameObject ratingPanel;
    float timer;
    float y;
    PotionMark pm;
    string rating;
    [SerializeField] List<string> gradesString = new();
    [SerializeField] int rangeGrade;
    Shop shop;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ColorSelector>().calculateScore.AddListener(CalculateScore);
        pm = FindObjectOfType<PotionMark>();
        shop = FindObjectOfType<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timeGO.text = time.ToString(@"mm\:ss");

    }

    public void SpawnTest(){
        Instantiate(prefabTest,spawnPoint.transform.position+new Vector3(0,y,0), Quaternion.identity);
        Instantiate(spriteTest,spawnPoint.transform.position+new Vector3(0,y,0), Quaternion.identity);
        y+=0;
    }

    void CalculateScore(){
        // print("score = " + pm.score);
        // int average = pm.score / (int) Math.Round(timer)/100;
        const int TIME_LIMIT = 60; // only for bonification
        float multiplier = (TIME_LIMIT - (int) Math.Round(timer))/10;
        print("timer = " + (int) Math.Round(timer));
        print("multiplier = " + multiplier);

        if (multiplier<1) multiplier = 1;

        float average = pm.score * multiplier;
        
        if (average <= 0){ // 0 or lower Fail
            rating = "F";
        }else if (average > (gradesString.Count - 1) * rangeGrade  + rangeGrade/3*3){ // break the table SS
            rating = "SS";
        }else{
            for (int i = 0; i < gradesString.Count; i++)
            {
                if (average > (i+1) * rangeGrade) continue;

                if (average < i * rangeGrade + rangeGrade/3*1) // X-
                {
                    rating = gradesString[i]+"-";
                    break;
                } else if (average < i * rangeGrade + rangeGrade/3*2) // X
                {
                    rating = gradesString[i];
                    break;
                } else if (average <= i * rangeGrade + rangeGrade/3*3) // X+ (ak + rangedGrade)
                {
                    rating = gradesString[i]+"+";
                    break;
                }
            }
        }
        
        print("rating = " + rating);
        print("average = " + average);

        shop.UpdateMoney(pm.score/10);
        ratingPanel.SetActive(true);
        RatingPanel rp = ratingPanel.GetComponent<RatingPanel>();
        rp.score.text = pm.score.ToString();
        rp.time.text = timeGO.text;
        rp.rating.text = rating;
        shop.RestockConsumables();
    }

    public void ResetGame(){
        timer = 0;
        if (pm) pm.SetScore(0);
    }
}



