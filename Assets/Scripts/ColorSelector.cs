using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    [SerializeField] GameObject potionColorContainer;
    MainButton mb;
    public UnityEvent calculateScore = new();
    UI ui;
    PotionMark pm;
    // Start is called before the first frame update
    void Start()
    {
        mb = FindObjectOfType<MainButton>();
        ui = FindObjectOfType<UI>();
        pm = FindObjectOfType<PotionMark>();

        foreach (Color color in potionColors)
        {
            GameObject potionPlayer = InstanciatePotion(color, playerPotions);
            potionPlayer.tag = Tags.PotionPlayer.ToString();

            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((ev) => SetPlayerPotion(potionPlayer));
            potionPlayer.GetComponentInChildren<EventTrigger>().triggers.Add(entry);
        }

        selectorPlayerPotions = Instantiate(selectorPrefab);
        
        SetPlayerPotion(playerPotions.transform.GetChild(0).transform.GetChild(0).gameObject);


        GenerateInstructions();


    }

    GameObject InstanciatePotion(Color color, GameObject parent){
        GameObject potionCC = Instantiate(potionColorContainer, parent.transform);
        GameObject potionColor = Instantiate(prefabPC);
        potionColor.transform.SetParent(potionCC.transform);
        // potionColor.transform.SetAsLastSibling();
        potionColor.GetComponent<Image>().color = color;
        return potionColor;
    }

    void PotionSelector(GameObject potion){
        // selector.transform.SetParent(potion.transform,false);
        potion.transform.GetChild(0).gameObject.SetActive(false);
        // selector.GetComponent<Image>().color = Color.black;
        // selector.transform.localScale = new Vector3(2,2,2);
    }

    public void SetNextInstructionPotionsSelector(){
        int potionIndex = instructionPotionsList.IndexOf(selectedInstructionPotion)+1;
        // check end sequencs; count ex 1 2 3; index 0 1 2 3
        if (instructionPotionsList.Count<=potionIndex)
        {
            calculateScore.Invoke();
            return;
        }
        selectedInstructionPotion = instructionPotionsList[potionIndex];
        Destroy(instructionPotions.transform.GetChild(0).gameObject);
        // selectedInstructionPotion = instructionPotionsList[0];
        PotionSelector(selectedInstructionPotion);
    }

    public void GenerateInstructions(){
        ui.ResetGame();
        instructionPotionsList = new();
        int potionNum = Random.Range(25,30);

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
        selectorInstructionPotions.transform.SetAsLastSibling();
        PotionSelector(selectedInstructionPotion);
    }


    void SetPlayerPotion(GameObject potion){
        foreach (Transform playerPotion in playerPotions.transform)
        {
            playerPotion.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        mb.PotionColor(potion.GetComponentInChildren<Image>().color);
        PotionSelector(potion);
    }
}

enum ColorSelectorEnum
{
    playerPotions,
    instructionPotions
}

enum Tags
{
    PotionColor,
    PotionPlayer
}


