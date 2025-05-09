using Assets._Project.Scripts.Core.Effects;
using UnityEngine;
using VContainer;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private SoundLibrary _soundLibrary;
    private AudioSource _audioSource;

    [Inject]
    public void Construct()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
        if (_soundLibrary == null)
        {
            Debug.LogError("SoundLibrary not found in Resources");
        }

        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void PlayWithRandomPitch(AudioClip clip, float volume = 0.5f)
    {
        if (clip == null) return;

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(clip, volume);
    }


    public void PlayCarExplosion() => PlayWithRandomPitch(_soundLibrary.carExplosions);
    public void PlayCarDamage() => PlayWithRandomPitch(_soundLibrary.carDamages);
    public void PlayTurretShot() => PlayWithRandomPitch(_soundLibrary.turretShots);
    public void PlayZombieHit() => PlayWithRandomPitch(_soundLibrary.zombieHits);
}