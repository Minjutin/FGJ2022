using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessManager : MonoBehaviour
{

    DarkCanvas canvas;
    MonsterPathfinding monster;

    [Header("Durations")]
    [SerializeField] float minTimeOn = 5f;
    [SerializeField] float maxTimeOn = 15f;
    [SerializeField] float minTimeOff = 3f;
    [SerializeField] float maxTimeOff = 15f;

    private bool _isHunting = false;

    void Start()
    {
        canvas = FindObjectOfType<DarkCanvas>();
        monster = FindObjectOfType<MonsterPathfinding>();

        // Start the lights on/off timer
        StartCoroutine(LightSwitchTimer());
    }
    private void Update()
    {
        // Checks if the Monster sees Player
        _isHunting = monster.GetHuntingMode();

        // -> Puts the lights on instantly
    }

    public void LightsOut()
    {
        // Puts the lights out
        canvas.PutTheLightsOut();
    }

    public void LightsOn()
    {
        if (!monster.GetHuntingMode())
        {
            canvas.PutTheLightsOn();
        }
        else
        {
            LightsInstantOn();
        }
    }

    public void LightsInstantOn()
    {
        canvas.PutLightsInstantlyOn();
    }

    IEnumerator LightSwitchTimer()
    {
        // Kills other coroutines of same type
        StopCoroutine(LightSwitchTimer());

        while (true)
        {
            // Randomize durations for lights on and off time
            var _timeOn = Random.Range(minTimeOn, maxTimeOn);
            var _timeOff = Random.Range(minTimeOff, maxTimeOff);


            yield return new WaitForSeconds(_timeOn);

            // Don't let the lights go out if the Monster is hunting
            if (!_isHunting)
            {
                // Shut Down the light
                LightsOut();
            }

            yield return new WaitForSeconds(_timeOff);

            // Put the Lights back up again
            LightsOn();

        }
    }

}
