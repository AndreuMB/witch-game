using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject prefabTest;
    [SerializeField] Sprite spriteTest;
    [SerializeField] GameObject spawnPoint;
    float y;
    GameObject potionFluid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTest(){
        Instantiate(prefabTest,spawnPoint.transform.position+new Vector3(0,y,0), Quaternion.identity);
        Instantiate(spriteTest,spawnPoint.transform.position+new Vector3(0,y,0), Quaternion.identity);
        y+=0;
    }
}
