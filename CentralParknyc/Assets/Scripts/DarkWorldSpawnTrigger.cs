using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider))]
public class DarkWorldSpawnTrigger : MonoBehaviour
{
    [Header("World References")]
    public GameObject normalWorld;
    public GameObject darkWorld;

    [Header("Player Spawn")]
    public Transform darkWorldSpawnPoint;

    [Header("Fade Transition")]
    public ScreenFade screenFade;

    [Header("Distortion Effect")]
    public GameObject effect;

    [Header("Skybox")]
    public Material darkSkybox;

    [Header("Lighting")]
    public Light directionalLight;
    public Color darkLightColor = Color.red;
    public float darkLightIntensity = 0.4f;

    [Header("Fog Settings")]
    public bool darkFogEnabled = true;
    public Color darkFogColor = new Color(0.25f, 0f, 0.35f);
    public FogMode darkFogMode = FogMode.Exponential;
    public float darkFogDensity = 0.08f;

    [Header("Volumes")]
    public GameObject normalVolume;
    public GameObject darkVolume;

    [Header("Music")]
    public AudioSource normalMusic;
    public AudioSource darkMusic;

    [Header("Trigger Settings")]
    public string triggerTag = "Player";

    private bool hasTriggered = false;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag(triggerTag))
        {
            hasTriggered = true;
            StartCoroutine(SendPlayerToDarkWorld(other.gameObject));
        }
    }

    private IEnumerator SendPlayerToDarkWorld(GameObject player)
    {
        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeOut());

        ActivateDarkWorld(player);

        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeIn());
    }

    private void ActivateDarkWorld(GameObject player)
    {
        if (normalWorld != null)
            normalWorld.SetActive(false);

        if (darkWorld != null)
            darkWorld.SetActive(true);

        if (darkWorldSpawnPoint != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            player.transform.position = darkWorldSpawnPoint.position;
            player.transform.rotation = darkWorldSpawnPoint.rotation;

            if (controller != null)
                controller.enabled = true;
        }

        if (effect != null)
            effect.SetActive(true);

        if (darkSkybox != null)
            RenderSettings.skybox = darkSkybox;

        if (directionalLight != null)
        {
            directionalLight.color = darkLightColor;
            directionalLight.intensity = darkLightIntensity;
        }

        RenderSettings.fog = darkFogEnabled;
        RenderSettings.fogColor = darkFogColor;
        RenderSettings.fogMode = darkFogMode;
        RenderSettings.fogDensity = darkFogDensity;

        if (normalVolume != null)
            normalVolume.SetActive(false);

        if (darkVolume != null)
            darkVolume.SetActive(true);

        if (normalMusic != null)
            normalMusic.Stop();

        if (darkMusic != null && !darkMusic.isPlaying)
            darkMusic.Play();

        DynamicGI.UpdateEnvironment();
    }
}