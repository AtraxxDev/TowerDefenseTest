using UnityEngine;

public class SoundFeedback : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfxSounds;

    [SerializeField] private AudioSource sfxSource;


    public void PlaySFXSound(SFXType sfxType)
    {
        switch (sfxType)
        {
            case SFXType.Click:
                sfxSource.PlayOneShot(sfxSounds[0]); //sfx para el Click
                break;
            case SFXType.Place:
                sfxSource.PlayOneShot(sfxSounds[1]); //sfx para el Place
                break;
            case SFXType.Remove:
                sfxSource.PlayOneShot(sfxSounds[2]); //sfx para el Remove
                break;
            case SFXType.wrongPlacement:
                sfxSource.PlayOneShot(sfxSounds[3]); //sfx para el wrong que es cuando no se  puede poner
                break;
        }
    }
}

public enum SFXType
{
    Click,
    Place,
    Remove,
    wrongPlacement
}
