using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    // [SerializeField] ColorSelectorEnum cse;
    public List<Potion> potions;
    [SerializeField] GameObject prefabPC;
    [SerializeField] GameObject playerPotions;
    [SerializeField] GameObject instructionPotions;
    [SerializeField] GameObject selectorPrefab;
    GameObject selectorPlayerPotions;
    GameObject selectorInstructionPotions;
    public List<GameObject> instructionPotionsList;
    public GameObject selectedInstructionPotion;
    public GameObject selectedPlayerPotion;
    // [SerializeField] GameObject potionColorContainer;
    MainButton mb;
    public UnityEvent calculateScore = new();
    UI ui;
    PotionMark pm;
    Vector2 originalPosition;
    Vector2 targetPosition;
    int potionIndex;
    // Start is called before the first frame update
    void Start()
    {
        mb = FindObjectOfType<MainButton>();
        ui = FindObjectOfType<UI>();
        pm = FindObjectOfType<PotionMark>();

        foreach (Potion potion in potions)
        {
            GameObject potionPlayer = InstanciatePotion(potion.color, playerPotions);
            potionPlayer.tag = Tags.PotionPlayer.ToString();

            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((ev) => SetPlayerPotion(potionPlayer));
            potionPlayer.GetComponent<EventTrigger>().triggers.Add(entry);
        }

        selectorPlayerPotions = Instantiate(selectorPrefab);
        
        SetPlayerPotion(playerPotions.transform.GetChild(0).gameObject);

        originalPosition = instructionPotions.GetComponent<RectTransform>().anchoredPosition;

        GenerateInstructions();

        selectedInstructionPotion = instructionPotionsList[0];

    }

    GameObject InstanciatePotion(Color color, GameObject parent){
        // GameObject potionCC = Instantiate(potionColorContainer, parent.transform);
        GameObject potionColor = Instantiate(prefabPC);
        // potionColor.transform.SetParent(potionCC.transform);
        potionColor.transform.SetParent(parent.transform);
        potionColor.GetComponent<PotionRef>().SetColor(color);
        return potionColor;
    }

    void PotionSelector(GameObject potion){
        // selector.transform.SetParent(potion.transform,false);
        potion.GetComponent<PotionRef>().highlight.SetActive(false);
        selectedPlayerPotion = potion;
        // selector.GetComponent<Image>().color = Color.black;
        // selector.transform.localScale = new Vector3(2,2,2);
    }

    public void SetNextInstructionPotionsSelector(){
        GameObject currentPotion = selectedInstructionPotion;
        potionIndex = instructionPotionsList.IndexOf(selectedInstructionPotion)+1;
        // check end sequencs; count ex 1 2 3; index 0 1 2 3
        if (instructionPotionsList.Count<=potionIndex)
        {
            calculateScore.Invoke();
            return;
        }
        selectedInstructionPotion = instructionPotionsList[potionIndex];

        Animator animator = currentPotion.GetComponentInChildren<Animator>();
        animator.SetTrigger("complete");
        // currentPotion.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
        // Destroy(currentPotion.GetComponent<VerticalLayoutGroup>());
        

        // AnimatorClipInfo[] animationInfo = animator.GetCurrentAnimatorClipInfo(0);
        // print(animationInfo[0].clip.name + " = " + animationInfo[0].clip.length);
        // Destroy(currentPotion, animationInfo[1].clip.length);
        // Destroy(currentPotion, 1);

        
        

        StartCoroutine(nameof(LerpObject));

        // RectTransform rt = instructionPotions.GetComponent<RectTransform>();

        // rt.anchoredPosition = Vector3.Lerp(instructionPotions.transform.localPosition, new Vector2(instructionPotions.transform.localPosition.x-300,instructionPotions.transform.localPosition.y), 1000);
        

        // selectedInstructionPotion = instructionPotionsList[0];
        PotionSelector(selectedInstructionPotion);
    }

    public void GenerateInstructions(){
        ui.ResetGame();

        RectTransform rt = instructionPotions.GetComponent<RectTransform>(); //getting reference to  component
        rt.anchoredPosition = originalPosition;
        targetPosition = rt.anchoredPosition;

        instructionPotionsList = new();
        int potionNum = Random.Range(25,30);

        foreach (Transform potions in instructionPotions.transform)
        {
            Destroy(potions.gameObject);
        }

        for (int i = 0; i <= potionNum; i++)
        {
            int potionType = Random.Range(0,potions.Count);
            Color randomPotion = potions[potionType].color;
            GameObject newPotion = InstanciatePotion(randomPotion, instructionPotions);
            instructionPotionsList.Add(newPotion);
        }

        selectedInstructionPotion = instructionPotionsList[0];
        selectorInstructionPotions = Instantiate(selectorPrefab);
        selectorInstructionPotions.transform.SetAsLastSibling();
        PotionSelector(selectedInstructionPotion);
    }


    void SetPlayerPotion(GameObject potion){
        // mask/unselect all potions
        foreach (Transform playerPotion in playerPotions.transform)
        {
            playerPotion.GetComponent<PotionRef>().highlight.SetActive(true);
        }
        mb.PotionColor(potion.GetComponent<PotionRef>().GetColor());
        PotionSelector(potion);
    }

    IEnumerator LerpObject(){ 
        float timeOfTravel=0.5f; //time after object reach a target place 
        float currentTime=0; // actual floting time 
        float normalizedValue;

        RectTransform rt = instructionPotions.GetComponent<RectTransform>(); //getting reference to component
        rt.anchoredPosition = targetPosition;
        targetPosition = new(rt.anchoredPosition.x-300,rt.anchoredPosition.y);
        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime; 
            normalizedValue=currentTime/timeOfTravel; // we normalize our time 

            rt.anchoredPosition=Vector3.Lerp(rt.anchoredPosition, targetPosition, normalizedValue);
            yield return null; 
        }
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


