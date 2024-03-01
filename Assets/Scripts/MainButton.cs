using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    [SerializeField] GameObject potionPrefab;
    [SerializeField] GameObject spawnPoint;
    GameObject potion;
    bool pressed = false;
    [SerializeField] GameObject glass;
    public UnityEvent<GameObject> buttonRelease;
    ColorSelector cs;
    [SerializeField] float grow = 0.1f;
    PotionMark pm;
    UI ui;
    Shop shop;


    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<ColorSelector>();
        pm = FindObjectOfType<PotionMark>();
        ui = FindObjectOfType<UI>();
        shop = FindObjectOfType<Shop>();
    }

    // Update is called once per frame
    void Update()
    {

        // print(EventSystem.current.gameObject.CompareTag(Tags.PotionPlayer.ToString()));

        // if (EventSystem.current.IsPointerOverGameObject()) return;

        if (shop.shopPanel.activeInHierarchy) return;
        

        if (Input.GetKeyDown(KeyCode.Mouse0) && cs.selectedPlayerPotion
            && cs.selectedPlayerPotion.CompareTag(Tags.PotionPlayer.ToString())
            && !ui.ratingPanel.activeInHierarchy) {

            //Fetch the Raycaster from the GameObject (the Canvas)
            GraphicRaycaster m_Raycaster = FindObjectOfType<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            EventSystem m_EventSystem = FindObjectOfType<EventSystem>();

            //Set up the new Pointer Event
             PointerEventData  m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "ShopBtn") return;
            }


            pressed = true;

                
        }

            
        

        
        if (pressed)
        {
            if (!potion)
            {
                potion = Instantiate(potionPrefab,spawnPoint.transform.position, Quaternion.identity);
                potion.transform.localScale = new Vector3(4f,0.1f,1);
            }
            // potion.transform.localScale = new Vector3(potion.transform.localScale.x,potion.transform.localScale.y + grow * Time.deltaTime,potion.transform.localScale.z);
            potion.transform.localScale += Vector3.up * grow * Time.deltaTime;
        }

        // print("potion =" + potion);
        

        if (!potion) return;

        float glassTop = glass.transform.GetChild(0).transform.position.y;
        float potionTop = potion.transform.GetChild(0).transform.position.y;

        Color instructionColor = cs.selectedInstructionPotion.GetComponent<PotionRef>().GetColor();
        Color potionColor = potion.GetComponent<SpriteRenderer>().color;

        if( (glassTop-potionTop) < 0 || potionColor != instructionColor) {
            pressed = false;
            potion.transform.GetChild(0).transform.position = new Vector3(0,100,0);
            buttonRelease.Invoke(potion);
            Destroy(potion);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            pressed = false;
            // buttonRelease.Invoke(potion);
            pm.GetScore(potion);
            Destroy(potion);
        }
    }

    void FixedUpdate(){
        // if (pressed)
        // {
        //     if (!potion)
        //     {
        //         potion = Instantiate(potionPrefab,spawnPoint.transform.position, Quaternion.identity);
        //         potion.transform.localScale = new Vector3(1.7f,0,1);
        //     }
        //     potion.transform.localScale = new Vector3(potion.transform.localScale.x,potion.transform.localScale.y+grow,potion.transform.localScale.z);
        // }
    }

    public void PotionColor(Color color){
        potionPrefab.GetComponent<SpriteRenderer>().color = color;
    }
}
