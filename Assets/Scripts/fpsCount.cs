using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fpsCount : MonoBehaviour
{
    int current;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
      current = (int)(1f / Time.unscaledDeltaTime);
      text.text = current.ToString() + " " + Screen.currentResolution;
      
    }
}
