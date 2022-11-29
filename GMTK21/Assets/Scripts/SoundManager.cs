using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Music
    {

        Count
    }

    public enum Sfx
    {
        Count
    };

    public AudioClip[] MusicAudioClips;
    public AudioClip[] SfxAudioClips;

    public AudioSource MusicAudioSource { get; private set; }

    public void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Resources.html
        MusicAudioClips = Resources.LoadAll<AudioClip>("audio/music");

        SfxAudioClips = Resources.LoadAll<AudioClip>("audio/sfx");

        // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
        MusicAudioSource = gameObject.AddComponent<AudioSource>();
        MusicAudioSource.loop = false;
        MusicAudioSource.volume = 0.08f;
    }

    public void Play(Music music, bool loop = false, float volume = 0.08f)
    {
        MusicAudioSource.clip = MusicAudioClips[(int)music];
        MusicAudioSource.Play();
        MusicAudioSource.volume = volume;
        MusicAudioSource.loop = loop;
    }

    public void Play(Sfx sfx, float volume = 0.08f)
    {
        AudioSource.PlayClipAtPoint(SfxAudioClips[(int)sfx], transform.position, volume);
    }
}
