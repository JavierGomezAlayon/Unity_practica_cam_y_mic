using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;   // Asignar desde el inspector
    public AudioClip sound;           // El sonido de la carpeta adjunta

    // Llama a esta función cuando el guerrero alcance su objetivo
    public void PlaySound()
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Terreno")) 
        {
            PlaySound();
        }
    }
}
