using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LevelManager

{
    private static Transform[] allObjects;

    public static void Reload()
    {
        //Find & destroy all objects in scene

        allObjects = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];

        foreach (Transform t in allObjects)
        {
            GameObject.Destroy(t.gameObject);
        }


        Application.LoadLevelAdditive(Application.loadedLevel);

    }
}
