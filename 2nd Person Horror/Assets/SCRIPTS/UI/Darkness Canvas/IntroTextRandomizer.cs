using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTextRandomizer : MonoBehaviour
{
    [SerializeField] Text txt;


    void Start()
    {
        txt.text = randomLine[Random.Range(0, randomLine.Count)];
    }

    public List<string> randomLine = new List<string>
    {
        //"Your presence has awoken something...",
        //"Beware of the unknown",
        //"Take care not to be seen",
        //"In space, no one can hear you scream",
        "You are not alone...",
        "You are being hunted...",
        "Something has its eyes on you...",
        "Don't be blind to the danger",
        "You can run, just not fast enough...",
        "Something is coming"
    };


}
