using UnityEngine;

public class VowelLipSync : MonoBehaviour
{
    public AudioSource audioSource; // Audio Play
    public SkinnedMeshRenderer faceMesh; // Blend Key Setup Control

    [Header("BlendShape Index")]
    public int aIndex;
    public int eIndex;
    public int iIndex;
    public int oIndex;
    public int uIndex;

    [Header("Settings As Per Current Chara_")]
    public float sensitivity = 100f;
    public int spectrumSize = 1024; //define how many frequency samples are output by an analyzer
    public FFTWindow fftWindow = FFTWindow.BlackmanHarris; // Fast Fourier Transform

    private float[] spectrum;

    void Start()
    {
        spectrum = new float[spectrumSize];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            // Reset blend shapes
            faceMesh.SetBlendShapeWeight(aIndex, 0);
            faceMesh.SetBlendShapeWeight(eIndex, 0);
            faceMesh.SetBlendShapeWeight(iIndex, 0);
            faceMesh.SetBlendShapeWeight(oIndex, 0);
            faceMesh.SetBlendShapeWeight(uIndex, 0);
            return;
        }

        // spectrum data
        audioSource.GetSpectrumData(spectrum, 0, fftWindow);

        // Random energy Counter
        float aVowel = GetFrequencyBand(spectrum, 700, 1200);
        float eVowel = GetFrequencyBand(spectrum, 400, 700);
        float iVowel = GetFrequencyBand(spectrum, 200, 400);
        float oVowel = GetFrequencyBand(spectrum, 500, 1000);
        float uVowel = GetFrequencyBand(spectrum, 200, 500);

        // Assign Blend Value
        faceMesh.SetBlendShapeWeight(aIndex, Mathf.Clamp(aVowel * sensitivity, 0f, 100f));
        faceMesh.SetBlendShapeWeight(eIndex, Mathf.Clamp(eVowel * sensitivity, 0f, 100f));
        faceMesh.SetBlendShapeWeight(iIndex, Mathf.Clamp(iVowel * sensitivity, 0f, 100f));
        faceMesh.SetBlendShapeWeight(oIndex, Mathf.Clamp(oVowel * sensitivity, 0f, 100f));
        faceMesh.SetBlendShapeWeight(uIndex, Mathf.Clamp(uVowel * sensitivity, 0f, 100f));
    }

    // Returns the sum of spectrum data in a frequency band
    float GetFrequencyBand(float[] spectrumData, float minFreq, float maxFreq)
    {
        float sampleRate = AudioSettings.outputSampleRate;
        int minIndex = Mathf.FloorToInt(minFreq / sampleRate * spectrumSize);
        int maxIndex = Mathf.FloorToInt(maxFreq / sampleRate * spectrumSize);

        minIndex = Mathf.Clamp(minIndex, 0, spectrumSize - 1);
        maxIndex = Mathf.Clamp(maxIndex, 0, spectrumSize - 1);

        float sum = 0f;
        for (int i = minIndex; i <= maxIndex; i++)
            sum += spectrumData[i];

        return sum / (maxIndex - minIndex + 1);
    }
}
