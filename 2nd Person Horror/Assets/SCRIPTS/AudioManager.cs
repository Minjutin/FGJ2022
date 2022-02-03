using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioClip monsterSawYouSound;
    private Vector3 extraVector = new Vector3(0, 0.2f, 0);
    TEST_character_movement player;

    private void Start()
    {
        player = FindObjectOfType<TEST_character_movement>();
        audio = GetComponent<AudioSource>();
    }

    public void MonsterSawYou()
    {
        Vector3 placement = extraVector + player.gameObject.transform.position;
        //AudioSource.PlayClipAtPoint(monsterSawYouSound, placement);
        audio.PlayOneShot(monsterSawYouSound, 1.5f);
    }

    //private void Update()   // FOR TESTING THE SOUND
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        MonsterSawYou();
    //    }
    //}
}
