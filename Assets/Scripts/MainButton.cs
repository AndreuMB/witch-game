using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainButton : MonoBehaviour
{
    [SerializeField] GameObject potionPrefab;
    [SerializeField] GameObject spawnPoint;
    GameObject potion;
    bool pressed = false;
    public UnityEvent<GameObject> buttonRelease;
    // Start is called before the first frame update
    void Start()
    {
        // buttonRelease = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) pressed = true;

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
                potion.transform.localScale = new Vector3(1.7f,0,1.7f);
            }
            potion.transform.localScale = new Vector3(1.7f,potion.transform.localScale.y+0.1f,-1f);
        }
    }
}
