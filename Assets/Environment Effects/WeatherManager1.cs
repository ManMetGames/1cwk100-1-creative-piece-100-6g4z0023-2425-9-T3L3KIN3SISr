using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float RainIntensity;
    [SerializeField, Range(0f, 1f)] float FogIntensity;

    [SerializeField] float MinFogAttenuationDistance = 10f;
    [SerializeField] float MaxFogAttenuationDistance = 50f;

    [SerializeField] VisualEffect RainVFX;
    [SerializeField] Volume FogVolume;
    [SerializeField] AudioSource RainSFX;  // AudioSource for Rain sound effects

    [SerializeField] GameObject ToggleableObject; // The GameObject to toggle
    [SerializeField] bool ToggleActive = true;    // Checkbox for controlling GameObject active state

    float PreviousRainIntensity;
    float PreviousFogIntensity;
    Fog CachedFogComponent;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Rain VFX and Fog
        RainVFX.SetFloat("Intensity", RainIntensity);
        FogVolume.weight = FogIntensity;

        // Cache the Fog component from the FogVolume profile
        FogVolume.profile.TryGet<Fog>(out CachedFogComponent);

        if (CachedFogComponent != null)
        {
            CachedFogComponent.meanFreePath.Override(Mathf.Lerp(MaxFogAttenuationDistance,
                                                               MinFogAttenuationDistance,
                                                               FogIntensity));
        }

        // Ensure the GameObject starts with the correct active state
        if (ToggleableObject != null)
        {
            ToggleableObject.SetActive(ToggleActive);
        }

        // Set initial volume for RainSFX based on the initial RainIntensity
        if (RainSFX != null)
        {
            RainSFX.volume = RainIntensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the rain intensity and VFX if changed
        if (RainIntensity != PreviousRainIntensity)
        {
            PreviousRainIntensity = RainIntensity;
            RainVFX.SetFloat("Intensity", RainIntensity);

            // Adjust the rain sound volume to match the rain intensity
            if (RainSFX != null)
            {
                RainSFX.volume = RainIntensity;
            }
        }

        // Update fog settings if intensity has changed
        if (FogIntensity != PreviousFogIntensity)
        {
            PreviousFogIntensity = FogIntensity;
            FogVolume.weight = FogIntensity;

            if (CachedFogComponent != null)
            {
                CachedFogComponent.meanFreePath.value = Mathf.Lerp(MaxFogAttenuationDistance,
                                                                   MinFogAttenuationDistance,
                                                                   FogIntensity);
            }
        }
    }

    // This method is called in the editor whenever values are changed
    void OnValidate()
    {
        // Update the GameObject active state based on the checkbox
        if (ToggleableObject != null)
        {
            ToggleableObject.SetActive(ToggleActive);
        }

        // Update the AudioSource volume if RainSFX and RainVFX are set
        if (RainSFX != null && RainVFX != null)
        {
            RainSFX.volume = RainIntensity;
        }
    }
}
