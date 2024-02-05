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
    [SerializeField] GameObject scorePoints;
    [SerializeField] GameObject floatScorePrefab;
    ColorSelector cs;
    public int shield;
    public UnityEvent<GameObject> success;

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
        // const float OFFSET = -0.05f;
        const float OFFSET = 0;
        float yDistance = mark.transform.position.y - (potion.transform.GetChild(0).transform.position.y + OFFSET);
        // float yDistance = mark.transform.position.y - potion.transform.GetChild(0).transform.position.y;
        yDistance = Mathf.Abs(yDistance);
        // print(yDistance);

        Color potionColor = potion.GetComponent<SpriteRenderer>().color;

        Potion potionInfo = cs.potions.Find(playerPotion => playerPotion.color == potionColor);

        int givenScore = potionInfo.points;
        string operatorScorePoints = "+";

        string scoreMessage;
        if (yDistance<0.08)
        {
            scoreMessage = "perfect";
            score+=givenScore;
        } else if (yDistance<0.2)
        {
            scoreMessage = "good";
            givenScore /= 2;
            score+=givenScore;
        } else if (yDistance<0.5)
        {
            scoreMessage = "not bad";
            givenScore /= 4;
            score+=givenScore;
        } else
        {
            if (shield > 0)
            {
                shield--;
                return;
            }
            scoreMessage = "fail";
            operatorScorePoints = "-";
            givenScore = 100;
            score-=100;
        }
        if (operatorScorePoints == "+") success.Invoke(potion);
        scoreTxt.text = "SCORE: " + score.ToString();
        GameObject SP = Instantiate(scorePoints, scoreTxt.transform);
        SP.GetComponent<TMP_Text>().text = operatorScorePoints + givenScore.ToString();
        
        Quaternion zRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-20,20)));
        GameObject floatScore = Instantiate(floatScorePrefab, new Vector3(0.5f,2,-2), Quaternion.identity).transform.GetChild(0).gameObject;
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
        Vector3 markPosition = new Vector3(transform.position.x, transform.position.y + yPosition, -2);
        mark.transform.position = markPosition;
    }

    public void SetScore(int newScore){
        score = newScore;
        scoreTxt.text = "SCORE: " + score.ToString();
    }

    void ScoreUpgrade(Potion potion){
        potion.points+=100;
    }

    void Shield(){
        shield++;
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Potion0Upgrade:
                ScoreUpgrade(cs.potions[0]);
            break;
            case Item.ItemType.Potion1Upgrade:
                ScoreUpgrade(cs.potions[1]);
            break;
            case Item.ItemType.Potion2Upgrade:
                ScoreUpgrade(cs.potions[2]);
            break;
            case Item.ItemType.Protection:
                Shield();
            break;
        }
    }
}
