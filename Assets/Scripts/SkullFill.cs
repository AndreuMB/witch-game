using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFill : MonoBehaviour
{
     [SerializeField] GameObject potionFill;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PotionMark>().success.AddListener(FillSkull);
        ResetPotionFill();
    }

    void FillSkull(GameObject newPotion){
        // if (!potionFill.activeInHierarchy) potionFill.SetActive(true);
        Color newColor =  newPotion.GetComponent<SpriteRenderer>().color;
        Color potionColor = potionFill.GetComponent<SpriteRenderer>().color;
        potionColor += newColor;
        potionColor = potionColor/2;
        potionFill.GetComponent<SpriteRenderer>().color = potionColor;
    }

    public void ResetPotionFill(){
        potionFill.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
    }
}
