using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManagerFacade
{
    public static AudioManager AudioManager { get; private set; }

    public static void SetAudioManager(AudioManager audioManager)
    {
        AudioManager = audioManager;
    }

    public static void PlaySound(string soundName)
    {
        AudioManager.OnPlaySound(soundName);
    }
}

public class AudioManager : MonoBehaviour {

    public AudioClip clickSound;
    public AudioClip dropSound;

    public const string CLICK_SOUND = "ClickSound";
    public const string DROP_SOUND = "DropSound";

    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioManagerFacade.SetAudioManager(this);
    }

        void Start () {
        AudioSource = GetComponent<AudioSource>();
    }
	
    public void OnPlaySound(string _nameSound)
    {
        switch (_nameSound)
        {
            case CLICK_SOUND:
                AudioSource.PlayOneShot(clickSound);
                return;
            case DROP_SOUND:
                AudioSource.PlayOneShot(dropSound);
                return;
            default:
                return;
        }
    }
}
