using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabling : MonoBehaviour
{
    public GameObject disable;
    public static bool disabled = false;
    public GameObject player;
  //  public static bool enable = true;
    // Start is called before the first frame update
    void Start()
    {
        disable.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        disable.SetActive(player.GetComponent<BetterMovement>().objectToggle);
       // Debug.Log(player.GetComponent<BetterMovement>().objectToggle);
        //if (disabled)
           // disable.SetActive(false);
       // else
        //    disable.SetActive(true);

        //if (enable)
           // disable.SetActive(true);
       // else
           // disable.SetActive(false);
    }
}
