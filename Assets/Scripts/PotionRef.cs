using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PotionRef : MonoBehaviour
{
    [SerializeField] GameObject colorGO;
    Color color;
    public GameObject highlight;

    public void SetColor(Color newColor){
        color = newColor;
        colorGO.GetComponent<Image>().color = color;
    }

    public Color GetColor(){
        return color;
    }
}
