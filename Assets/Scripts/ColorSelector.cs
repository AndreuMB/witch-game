using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    // [SerializeField] ColorSelectorEnum cse;
    public List<Color> potionColors;
    [SerializeField] GameObject prefabPC;
    [SerializeField] GameObject playerPotions;
    [SerializeField] GameObject instructionPotions;
    [SerializeField] GameObject selectorPrefab;
    GameObject selectorPlayerPotions;
    GameObject selectorInstructionPotions;
    public List<GameObject> instructionPotionsList;
    public GameObject selectedInstructionPotion;
    MainButton mb;
    // Start is called before the first frame update
    void Start()
    {
        mb = FindObjectOfType<MainButton>();

        foreach (Color color in potionColors)
        {
            InstanciatePotion(color, playerPotions);
        }

        selectorPlayerPotions = Instantiate(selectorPrefab);
        
        PotionSelector(playerPotions.transform.GetChild(0).gameObject, selectorPlayerPotions);

        GenerateInstructions();


    }

    GameObject InstanciatePotion(Color color, GameObject parent){
        GameObject potionColor = Instantiate(prefabPC, parent.transform);
        potionColor.GetComponent<Image>().color = color;
        potionColor.GetComponent<Button>().onClick.AddListener(() => SetPlayerPotion(potionColor));
        return potionColor;
    }

    void PotionSelector(GameObject potion, GameObject selector){
        selector.transform.SetParent(potion.transform,false);
        // selector.GetComponent<Image>().color = Color.black;
        // selector.transform.localScale = new Vector3(2,2,2);
    }

    public void SetNextInstructionPotionsSelector(){
        int potionIndex = instructionPotionsList.IndexOf(selectedInstructionPotion)+1;
        // count ex 1 2 3; index 0 1 2 3
        if (instructionPotionsList.Count<=potionIndex)
        {
            GenerateInstructions();
            return;
        }
        selectedInstructionPotion = instructionPotionsList[potionIndex];
        PotionSelector(selectedInstructionPotion,selectorInstructionPotions);
    }

    void GenerateInstructions(){
        instructionPotionsList = new();
        int potionNum = Random.Range(2,4);

        foreach (Transform potions in instructionPotions.transform)
        {
            Destroy(potions.gameObject);
        }

        for (int i = 0; i <= potionNum; i++)
        {
            int potionType = Random.Range(0,potionColors.Count);
            Color randomPotion = potionColors[potionType];
            GameObject newPotion = InstanciatePotion(randomPotion, instructionPotions);
            instructionPotionsList.Add(newPotion);
        }

        selectedInstructionPotion = instructionPotionsList[0];
        selectorInstructionPotions = Instantiate(selectorPrefab);
        PotionSelector(selectedInstructionPotion, selectorInstructionPotions);
    }


    void SetPlayerPotion(GameObject potion){
        mb.PotionColor(potion.GetComponent<Image>().color);
        PotionSelector(potion,selectorPlayerPotions);
    }
}

enum ColorSelectorEnum
{
    playerPotions,
    instructionPotions
}
