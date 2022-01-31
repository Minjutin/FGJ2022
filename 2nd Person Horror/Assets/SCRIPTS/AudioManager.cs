using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip monsterSawYouSound;

    public void MonsterSawYou()
    {
        AudioSource.PlayClipAtPoint(monsterSawYouSound, FindObjectOfType<TEST_character_movement>().gameObject.transform.position);
    }
}
