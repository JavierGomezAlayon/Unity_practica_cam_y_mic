using UnityEngine;

public class Recorder : MonoBehaviour
{
    public AudioSource audioSource;  // Asignar desde el inspector
    private AudioClip recordedClip;
    private string mic;

    void Start()
    {
        // Usar el primer micrófono detectado
        mic = Microphone.devices[0];

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Recording started");
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Recording stopped");
            Microphone.End(mic);
            audioSource.Play();
        }
    }


    public void StartRecording()
    {
        recordedClip = Microphone.Start(mic, false, 10, 44100);
        audioSource.clip = recordedClip;
    }
}
