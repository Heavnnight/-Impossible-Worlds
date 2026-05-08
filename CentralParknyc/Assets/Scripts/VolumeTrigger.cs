using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WorldSwapTrigger : MonoBehaviour
{
    [Header("World References")]
    public GameObject normalWorld;
    public GameObject darkWorld;

    [Header("Distortion Effect")]
    public GameObject effect;

    [Header("Skyboxes")]
    public Material normalSkybox;
    public Material darkSkybox;

    [Header("Optional Lighting")]
    public Light directionalLight;

    public Color normalLightColor = Color.white;
    public Color darkLightColor = Color.red;

    [Header("Fog Settings")]
    public bool normalFogEnabled = false;
    public Color normalFogColor = Color.gray;
    public FogMode normalFogMode = FogMode.Exponential;
    public float normalFogDensity = 0.01f;

    public bool darkFogEnabled = true;
    public Color darkFogColor = new Color(0.25f, 0f, 0.35f);
    public FogMode darkFogMode = FogMode.Exponential;
    public float darkFogDensity = 0.035f;

    [Header("Music Settings")]
    public AudioSource normalMusic;
    public AudioSource darkMusic;

    [Header("Trigger Settings")]
    public string triggerTag = "Player";

    private bool hasSwitched = false;

    private void Start()
    {
        // Make sure the collider works as a trigger
        GetComponent<BoxCollider>().isTrigger = true;

        // Enable the normal world at the start
        if (normalWorld != null)
            normalWorld.SetActive(true);

        // Disable the dark world at the start
        if (darkWorld != null)
            darkWorld.SetActive(false);

        // Disable the distortion effect at the start
        if (effect != null)
            effect.SetActive(false);

        // Set the normal skybox
        if (normalSkybox != null)
            RenderSettings.skybox = normalSkybox;

        // Set the normal light color
        if (directionalLight != null)
            directionalLight.color = normalLightColor;

        // Set normal fog
        RenderSettings.fog = normalFogEnabled;
        RenderSettings.fogColor = normalFogColor;
        RenderSettings.fogMode = normalFogMode;
        RenderSettings.fogDensity = normalFogDensity;

        // Play normal music and stop dark music
        if (normalMusic != null)
            normalMusic.Play();

        if (darkMusic != null)
            darkMusic.Stop();

        DynamicGI.UpdateEnvironment();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger once when the player enters
        if (!hasSwitched && other.CompareTag(triggerTag))
        {
            hasSwitched = true;

            // Disable the normal world
            if (normalWorld != null)
                normalWorld.SetActive(false);

            // Enable the dark world
            if (darkWorld != null)
                darkWorld.SetActive(true);

            // Enable the distortion effect
            if (effect != null)
                effect.SetActive(true);

            // Change the skybox
            if (darkSkybox != null)
                RenderSettings.skybox = darkSkybox;

            // Change the directional light color
            if (directionalLight != null)
                directionalLight.color = darkLightColor;

            // Turn on dark world fog
            RenderSettings.fog = darkFogEnabled;
            RenderSettings.fogColor = darkFogColor;
            RenderSettings.fogMode = darkFogMode;
            RenderSettings.fogDensity = darkFogDensity;

            // Switch music
            if (normalMusic != null)
                normalMusic.Stop();

            if (darkMusic != null)
                darkMusic.Play();

            // Update environment lighting
            DynamicGI.UpdateEnvironment();
        }
    }
}