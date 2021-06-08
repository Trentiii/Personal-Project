using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSet : MonoBehaviour
{
    public string sceneToGoTo;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.nextScene = sceneToGoTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
