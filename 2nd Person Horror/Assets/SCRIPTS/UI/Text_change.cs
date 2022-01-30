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
            myText.text = "Gun is ready to use.\nPress LEFT MOUSE BUTTON to shoot.";
        }
        
        //When gun is ready but you dont have it yet.
        else if (player.GetComponent<Player_Charger_Room>().gunIsReady)
        {
            myText.text = "Your gun is ready.\nGo to the charging station.\nress SPACE to obtain your gun.";
        }

        //When gun is in charger.
        else if (player.GetComponent<Player_Charger_Room>().thereIsGun)
        {
            myText.text = "Your gun is charging. "+ player.GetComponent<Player_Charger_Room>().timer + " sec. left.";
        }        
        
        //When you are in a charger room. 
        else if (player.GetComponent<Player_Charger_Room>().canCharge)
        {
            myText.text = "Charging station.\nYou can charge your gun by pressing SPACE.";
        }

        else
        {
            myText.text = "Gun is Empty.\nFind a charging station to reload it.";
        }
            
    }
}
