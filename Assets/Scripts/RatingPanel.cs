using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RatingPanel : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text time;
    public TMP_Text rating;
    public TMP_Text playAgain;
    ColorSelector cs;
    bool lockAction = true;

    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<ColorSelector>();
    }

    void OnEnable(){
        playAgain.gameObject.SetActive(false);
        StartCoroutine(nameof(LockSw));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !lockAction)
        {
            cs.GenerateInstructions();
            lockAction = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator LockSw(){
        yield return new WaitForSeconds(1);
        lockAction = !lockAction;
        playAgain.gameObject.SetActive(true);
        yield return null;
    }
}
