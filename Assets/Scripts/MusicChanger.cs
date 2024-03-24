using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip _generalMusic;
    [SerializeField] private AudioClip _bossMusic;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip =  _generalMusic;
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ScaleHealth>())
        {
            _audioSource.clip = _bossMusic;
            _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<ScaleHealth>())
            return;

        Collider[] cols = Physics.OverlapSphere(transform.position, transform.localScale.x);

        if (!other.GetComponent<ScaleHealth>())
            return;
        _audioSource.clip = _generalMusic;
        _audioSource.Play();
    }
}