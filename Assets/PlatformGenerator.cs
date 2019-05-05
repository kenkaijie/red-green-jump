using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameController gameController;
    public List<GameObject> PlatformPrefabs;
    public List<float> SpawnableYOffsets;
    public float HardMinPlatformWidth = 5f;
    public float HardMaxPlatformWidth = 5f;
    public float MinPlatformWidth = 5f;
    public float MaxPlatformWidth = 30f;
    public float HardAllowedReactionTimeSeconds = 0.3f;
    public float allowedReactionTimeSeconds = 0.35f;
    private float _waitTimeRemaining = 0f;

    public void SpawnPlatform(GameObject platform, float width, float yOffset)
    {
        GameObject spawnedPlatform = Instantiate(platform, transform);
        spawnedPlatform.transform.position = transform.position + new Vector3(width / 2f, yOffset, 0f);
        spawnedPlatform.GetComponent<PlatformProperties>().PlatformWidth = width;
        spawnedPlatform.GetComponent<PlatformMovement>().GameController = gameController;
    }

    private void Start()
    {
        _waitTimeRemaining = allowedReactionTimeSeconds;
    }

    private void FixedUpdate()
    {
        if (gameController.IsMoving())
        {
            _waitTimeRemaining -= Time.fixedDeltaTime;
        }

        if (_waitTimeRemaining < 0f)
        {
            // generate random platforms
            var effectiveMinPlatformWidth = Mathf.Lerp(MinPlatformWidth, HardMinPlatformWidth, gameController.DifficultyScale / gameController.MaxDifficultyScale);
            var effectiveMaxPlatformWidth = Mathf.Lerp(MaxPlatformWidth, HardMaxPlatformWidth, gameController.DifficultyScale / gameController.MaxDifficultyScale);
            var effectiveAllowedReactionTime = Mathf.Lerp(allowedReactionTimeSeconds, HardAllowedReactionTimeSeconds, gameController.DifficultyScale / gameController.MaxDifficultyScale);
            var platformWidth = Random.Range(effectiveMinPlatformWidth, effectiveMaxPlatformWidth);
            var platformObject = ListUtilities.TakeRandom(PlatformPrefabs);
            var platformYOffset = ListUtilities.TakeRandom(SpawnableYOffsets);
            SpawnPlatform(platformObject, platformWidth, platformYOffset);
            Debug.Log(string.Format("Effective Spawn Parameters: {0}, {1}, {2}", effectiveMinPlatformWidth, effectiveMaxPlatformWidth, effectiveAllowedReactionTime));
            Debug.Log(string.Format("Spawned a {0} width platform, have to wait {1}s before spawning the next one", platformWidth, platformWidth / Mathf.Abs(gameController.GlobalGameMoveSpeed)));
            _waitTimeRemaining = (platformWidth / Mathf.Abs(gameController.GlobalGameMoveSpeed)) + allowedReactionTimeSeconds;
        }
    }
}
