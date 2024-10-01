using UnityEngine;
using UnityEngine.InputSystem; // Add this to use Input System

public class HoldObject : MonoBehaviour
{
    public Transform holdPosition; // Position where the object will be held
    public float pickUpRange = 3f; // Range within which the object can be picked
    public float holdDistance = 2f; // Distance in front of the player where object will be held
    public LayerMask pickUpLayerMask; // Layer for pickup objects (optional)

    private GameObject heldObject = null;
    private Rigidbody heldObjectRb = null;

    void Update()
    {
        // Get R2 trigger input (Right Trigger)
        float triggerValue = Gamepad.current.rightTrigger.ReadValue();

        // When R2 is pressed and held
        if (triggerValue > 0.1f) // Threshold to avoid detecting minor movements
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                HoldObjectInPosition();
            }
        }

        // Release the object when R2 is released
        if (triggerValue <= 0.1f && heldObject != null)
        {
            DropObject();
        }
    }

    // Try to pick up an object
    void TryPickUpObject()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Check if we hit an object within pick up range
        if (Physics.Raycast(transform.position, forward, out hit, pickUpRange, pickUpLayerMask))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    heldObjectRb.useGravity = false; // Disable gravity while holding
                    heldObjectRb.freezeRotation = true; // Prevent object from rotating
                }
            }
        }
    }

    // Hold the object in front of the player
    void HoldObjectInPosition()
    {
        if (heldObject != null)
        {
            // Move the object to the hold position (in front of the player)
            Vector3 targetPosition = holdPosition.position;
            heldObject.transform.position = targetPosition;
        }
    }

    // Drop the object
    void DropObject()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = true; // Re-enable gravity
            heldObjectRb.freezeRotation = false; // Allow object rotation
        }

        heldObject = null;
        heldObjectRb = null;
    }
}
