using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] GameObject img;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;

    //public void ChangeEndingTextTo(string _text)
    //{
    //    winText.GetComponent<Text>().text = _text;
    //}

    public void PlayerDeadEnding()
    {
        img.SetActive(true);
        loseText.SetActive(true);
    }

    public void MonsterDeadEnding()
    {
        img.SetActive(true);
        //txt.text = "you escaped!";
        winText.SetActive(true);
    }

}
