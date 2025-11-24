using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public Material displayMaterial;        // Material donde se mostrará la cámara
    private WebCamTexture webcamTexture;    // Textura de la cámara
    private int captureCounter = 1;         // Para nombrar archivos guardados

    [Header("Ruta donde se guardan las imágenes")]
    public string saveFolder = "CapturasCamara"; // Carpeta variable (ejemplo)

    void Start()
    {
        // Obtener cámaras disponibles
        WebCamDevice[] devices = WebCamTexture.devices;

        // Crear carpeta si no existe
        string fullPath = Path.Combine(Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(fullPath)) {
            Directory.CreateDirectory(fullPath);
            Debug.Log("La carpeta de capturas no existía. Se creó en: " + fullPath);
        } else {
            Debug.Log("Las capturas se guardarán en: " + fullPath);
        }

        if (devices.Length == 0)
        {
            Debug.LogError("No hay cámaras disponibles.");
        }

        // Mostrar lista de cámaras
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("Cámara encontrada: " + devices[i].name);
        }

        // Usar la primera cámara detectada
        webcamTexture = new WebCamTexture(devices[0].name);

        Debug.Log("Usando cámara: " + devices[0].name);
    }

    void Update()
    {
        // Iniciar cámara
        if (Input.GetKeyDown(KeyCode.S))
        {
            displayMaterial.mainTexture = webcamTexture;
            webcamTexture.Play();
            Debug.Log("Captura iniciada.");
        }

        // Pausar cámara
        if (Input.GetKeyDown(KeyCode.P))
        {
            webcamTexture.Pause();
            Debug.Log("Captura pausada.");
        }

        // Detener cámara
        if (Input.GetKeyDown(KeyCode.X))
        {
            webcamTexture.Stop();
            Debug.Log("Captura detenida.");
        }

        // Guardar foto
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (webcamTexture != null && webcamTexture.isPlaying) {
                Debug.Log("Guardando foto...");
                SaveSnapshot();
            }
        }
    }

    void SaveSnapshot()
    {
        // Crear la textura basada en la cámara
        Texture2D snapshot = new Texture2D(webcamTexture.width, webcamTexture.height);
        snapshot.SetPixels(webcamTexture.GetPixels());
        snapshot.Apply();

        // Ruta completa
        string fullPath = Path.Combine(
            Application.persistentDataPath,
            saveFolder,
            "captura_" + captureCounter + ".png"
        );

        // Guardar archivo
        File.WriteAllBytes(fullPath, snapshot.EncodeToPNG());
        Debug.Log("Foto guardada en: " + fullPath);

        captureCounter++;
    }
}

