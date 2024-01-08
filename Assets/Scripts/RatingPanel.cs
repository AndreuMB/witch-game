using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatingPanel : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text time;
    public TMP_Text rating;
    ColorSelector cs;

    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<ColorSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cs.GenerateInstructions();
            gameObject.SetActive(false);
        }
    }
}
