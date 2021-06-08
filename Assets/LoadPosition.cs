using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManager.playerLoc);
        transform.position = GameManager.playerLoc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
