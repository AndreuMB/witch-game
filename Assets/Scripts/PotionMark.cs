using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PotionMark : MonoBehaviour
{
    [SerializeField] GameObject markPrefab;
    GameObject mark;
    public int score;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] GameObject floatScorePrefab;
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
        string scoreMessage = "";
        if (yDistance<0.1)
        {
            scoreMessage = "perfect";
            score+=100;
        } else if (yDistance<0.5)
        {
            scoreMessage = "good";
            score+=50;
        } else if (yDistance<1)
        {
            scoreMessage = "not bad";
            score+=25;
        } else if (yDistance>1)
        {
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
        // StartCoroutine(FloatScoreCoroutine(floatScore));
        UpdateMark();
    }

    void UpdateMark(){
        
        float yPosition = Random.Range(-transform.localScale.y/2,transform.localScale.y/2);
        Vector3 markPosition = new Vector3(transform.position.x, transform.position.y + yPosition, mark.transform.position.z);
        mark.transform.position = markPosition;
    }

    IEnumerator FloatScoreCoroutine(GameObject floatScore){
        yield return new WaitForSeconds(0.4f);
        Destroy(floatScore);
        yield break;
    }
}
