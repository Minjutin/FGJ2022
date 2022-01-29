using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_change : MonoBehaviour
{

    Text myText;
    public GameObject player;
    
    void Start()
    {
        myText = GetComponent<Text>();
    }
 
    // Update is called once per frame
    void Update()
    {       
        
        //When gun is ready
        if (player.GetComponent<Gun>().canBeUsed){
            myText.text = "Gun is ready to use. Press left mouse button.";
        }
        
        //When gun is ready but you dont have it yet.
        else if (player.GetComponent<Player_Charger_Room>().gunIsReady)
        {
            myText.text = "Gun is ready! Go to charger & press space.";
        }

        //When gun is in charger.
        else if (player.GetComponent<Player_Charger_Room>().thereIsGun)
        {
            myText.text = "Gun is charging! "+ player.GetComponent<Player_Charger_Room>().timer + " sec.";
        }        
        
        //When you are in a charger room. 
        else if (player.GetComponent<Player_Charger_Room>().canCharge)
        {
            myText.text = "You can charge your gun by pressing space.";
        }

        else
        {
            myText.text = "Gun is Empty.";
        }
            
    }
}
