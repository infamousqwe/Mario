using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for TextMeshPro-UI

public class CoinCollector : MonoBehaviour
{
    private int coinCount = 0;
    public TextMeshProUGUI coinText; // Reference to the TMP Text component

    private Camera mainCamera;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
        UpdateCoinText(); // Call this when enabled to update the initial text

        // Calculate the offset between the camera and the coinText
        if (mainCamera != null && coinText != null)
        {
            offset = coinText.transform.position - mainCamera.transform.position;
        }
    }

    private void OnEnable()
    {
        BlockHit.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        BlockHit.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected()
    {
        coinCount++;
        UpdateCoinText(); // Update the TMP Text whenever a coin is collected
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount; // Update the text with the current count
    }

    private void LateUpdate()
    {
        // Move the coinText smoothly with the camera
        if (mainCamera != null && coinText != null)
        {
            Vector3 targetPosition = mainCamera.transform.position + offset;
            coinText.transform.position = Vector3.Lerp(coinText.transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
