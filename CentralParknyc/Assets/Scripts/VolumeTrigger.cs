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

            // Update environment lighting
            DynamicGI.UpdateEnvironment();
        }
    }
}