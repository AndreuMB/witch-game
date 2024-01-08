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
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ColorSelector>().calculateScore.AddListener(CalculateScore);
        pm = FindObjectOfType<PotionMark>();
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
        // print("sec = " + Math.Round(timer));
        int average = pm.score / (int) Math.Round(timer);
        
        if (average>=50)
        {
            rating = "S";
        } else if (average>45)
        {
            rating = "A";
        } else if (average>40)
        {
            rating = "B";
        } else if (average>35)
        {
            rating = "C";
        } else
        {
            rating = "F";
        }

        print("rating = " + rating);
        print("average = " + average);

        ratingPanel.SetActive(true);
        RatingPanel rp = ratingPanel.GetComponent<RatingPanel>();
        rp.score.text = pm.score.ToString();
        rp.time.text = timeGO.text;
        rp.rating.text = rating;
    }

    public void ResetGame(){
        timer = 0;
        if (pm) pm.SetScore(0);
    }
}
