using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    public List<Color> potionColors;
    [SerializeField] GameObject prefabPC;
    MainButton mb;
    // Start is called before the first frame update
    void Start()
    {
        mb = FindObjectOfType<MainButton>();
        foreach (Color color in potionColors)
        {
            GameObject potionColor = Instantiate(prefabPC, gameObject.transform);
            potionColor.GetComponent<Image>().color = color;
            potionColor.GetComponent<Button>().onClick.AddListener(() => mb.PotionColor(color));
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
