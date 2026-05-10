using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
public class WorldSwapTrigger : MonoBehaviour
{
    [Header("World References")]
    public GameObject normalWorld;
    public GameObject darkWorld;

    [Header("Fade Transition")]
    public ScreenFade screenFade;

    [Header("Distortion Effect")]
    public GameObject effect;

    [Header("Skyboxes")]
    public Material normalSkybox;
    public Material darkSkybox;

    [Header("Optional Lighting")]
    public Light directionalLight;
    public Color normalLightColor = Color.white;
    public Color darkLightColor = Color.red;
    public float normalLightIntensity = 1f;
    public float darkLightIntensity = 0.4f;

    [Header("Fog Settings")]
    public bool normalFogEnabled = false;
    public Color normalFogColor = Color.gray;
    public FogMode normalFogMode = FogMode.Exponential;
    public float normalFogDensity = 0.01f;

    public bool darkFogEnabled = true;
    public Color darkFogColor = new Color(0.25f, 0f, 0.35f);
    public FogMode darkFogMode = FogMode.Exponential;
    public float darkFogDensity = 0.08f;

    [Header("Volumes")]
    public GameObject normalVolume;
    public GameObject darkVolume;

    [Header("Music Settings")]
    public AudioSource normalMusic;
    public AudioSource darkMusic;

    [Header("Trigger Settings")]
    public string triggerTag = "Player";

    private bool hasSwitched = false;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        SetNormalWorld();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSwitched && other.CompareTag(triggerTag))
        {
            hasSwitched = true;
            StartCoroutine(SwitchWithFade());
        }
    }

    private IEnumerator SwitchWithFade()
    {
        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeOut());

        SetDarkWorld();

        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeIn());
    }

    private void SetNormalWorld()
    {
        if (normalWorld != null) normalWorld.SetActive(true);
        if (darkWorld != null) darkWorld.SetActive(false);

        if (effect != null) effect.SetActive(false);

        if (normalSkybox != null) RenderSettings.skybox = normalSkybox;

        if (directionalLight != null)
        {
            directionalLight.color = normalLightColor;
            directionalLight.intensity = normalLightIntensity;
        }

        RenderSettings.fog = normalFogEnabled;
        RenderSettings.fogColor = normalFogColor;
        RenderSettings.fogMode = normalFogMode;
        RenderSettings.fogDensity = normalFogDensity;

        if (normalVolume != null) normalVolume.SetActive(true);
        if (darkVolume != null) darkVolume.SetActive(false);

        if (normalMusic != null && !normalMusic.isPlaying) normalMusic.Play();
        if (darkMusic != null) darkMusic.Stop();

        DynamicGI.UpdateEnvironment();
    }

    private void SetDarkWorld()
    {
        if (normalWorld != null) normalWorld.SetActive(false);
        if (darkWorld != null) darkWorld.SetActive(true);

        if (effect != null) effect.SetActive(true);

        if (darkSkybox != null) RenderSettings.skybox = darkSkybox;

        if (directionalLight != null)
        {
            directionalLight.color = darkLightColor;
            directionalLight.intensity = darkLightIntensity;
        }

        RenderSettings.fog = darkFogEnabled;
        RenderSettings.fogColor = darkFogColor;
        RenderSettings.fogMode = darkFogMode;
        RenderSettings.fogDensity = darkFogDensity;

        if (normalVolume != null) normalVolume.SetActive(false);
        if (darkVolume != null) darkVolume.SetActive(true);

        if (normalMusic != null) normalMusic.Stop();
        if (darkMusic != null && !darkMusic.isPlaying) darkMusic.Play();

        DynamicGI.UpdateEnvironment();
    }
}