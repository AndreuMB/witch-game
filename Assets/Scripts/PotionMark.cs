using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PotionMark : MonoBehaviour, IShop
{
    [SerializeField] GameObject markPrefab;
    GameObject mark;
    public int score;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] GameObject floatScorePrefab;
    ColorSelector cs;
    public int givenScore = 100;
    public int shield;
    // Start is called before the first frame update
    void Start()
    {
        mark = Instantiate(markPrefab);
        UpdateMark();

        FindObjectOfType<MainButton>().buttonRelease.AddListener(GetScore);
        cs = FindObjectOfType<ColorSelector>();
    }

    public void GetScore(GameObject potion){
        // offset from mark to potion fill for better results
        const float OFFSET = -0.05f;
        float yDistance = mark.transform.position.y - (potion.transform.GetChild(0).transform.position.y + OFFSET);
        // float yDistance = mark.transform.position.y - potion.transform.GetChild(0).transform.position.y;
        yDistance = Mathf.Abs(yDistance);
        // print(yDistance);

        string scoreMessage;
        if (yDistance<0.08)
        {
            scoreMessage = "perfect";
            score+=givenScore;
        } else if (yDistance<0.2)
        {
            scoreMessage = "good";
            score+=givenScore/2;
        } else if (yDistance<0.5)
        {
            scoreMessage = "not bad";
            score+=givenScore/4;
        } else
        {
            if (shield > 0)
            {
                shield--;
                return;
            }
            scoreMessage = "fail";
            score-=100;
        }
        scoreTxt.text = "SCORE: " + score.ToString();
        Quaternion zRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-20,20)));
        GameObject floatScore = Instantiate(floatScorePrefab, new Vector3(0.5f,2,0), Quaternion.identity).transform.GetChild(0).gameObject;
        floatScore.transform.rotation = zRotation;
        floatScore.GetComponent<TextMesh>().text = scoreMessage;
        float thrust = 200f;
        floatScore.GetComponent<Rigidbody2D>().AddForce(floatScore.transform.up * thrust);

        AnimatorClipInfo[] animationInfo = floatScore.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        Destroy(floatScore.transform.parent.gameObject, animationInfo[0].clip.length);
        
        cs.SetNextInstructionPotionsSelector();
        UpdateMark();
    }

    void UpdateMark(){
        float yPosition = Random.Range(-transform.localScale.y/4,transform.localScale.y/2);
        Vector3 markPosition = new Vector3(transform.position.x, transform.position.y + yPosition, -1);
        mark.transform.position = markPosition;
    }

    public void SetScore(int newScore){
        score = newScore;
        scoreTxt.text = "SCORE: " + score.ToString();
    }

    void ScoreUpgrade(){
        givenScore+=100;
    }

    void Shield(){
        shield++;
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.PotionUpgrade:
                ScoreUpgrade();
            break;
            case Item.ItemType.Protection:
                Shield();
            break;
        }
    }
}
