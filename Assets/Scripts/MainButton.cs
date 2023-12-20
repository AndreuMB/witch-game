using System;
using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) pressed = true;

        if (!potion) return;

        float glassTop = glass.transform.GetChild(0).transform.position.y;
        float potionTop = potion.transform.GetChild(0).transform.position.y;

        if( (glassTop-potionTop) < 0 ) {
            pressed = false;
            potion.transform.GetChild(0).transform.position = new Vector3(0,100,0);
            buttonRelease.Invoke(potion);
            Destroy(potion);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            pressed = false;
            buttonRelease.Invoke(potion);
            Destroy(potion);
        }

        
    }

    void FixedUpdate(){
        if (pressed)
        {
            if (!potion)
            {
                potion = Instantiate(potionPrefab,spawnPoint.transform.position, Quaternion.identity);
                potion.transform.localScale = new Vector3(1.7f,0,1);
            }
            potion.transform.localScale = new Vector3(potion.transform.localScale.x,potion.transform.localScale.y+0.1f,potion.transform.localScale.z);
        }
    }

    public void PotionColor(Color color){
        potionPrefab.GetComponent<SpriteRenderer>().color = color;
    }
}
