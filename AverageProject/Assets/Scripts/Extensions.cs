using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody2D, Vector2 direction)
    {
        Debug.Log("Raycast method called");
        if (rigidbody2D.isKinematic)
        {
            return false;
        }
        float radius = 0.25f;
        float distance = 0.375f;
        Vector2 startPosition = rigidbody2D.position; // Starting position for the CircleCast

        // Perform the CircleCast
        RaycastHit2D hit = Physics2D.CircleCast(startPosition, radius, direction, distance, layerMask);

        // Visualize the CircleCast with a debug line
        Debug.DrawLine(startPosition, startPosition + direction * distance, Color.red, 2f);

        // Log the result of the CircleCast
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast did not hit");
        }

        // Return true if the CircleCast hit a collider that is not the Rigidbody2D itself
        return hit.collider != null && hit.rigidbody != rigidbody2D;
    }
}