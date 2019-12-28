using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private FloatReference VerticalAxis;
    [SerializeField]
    private FloatReference HorizontalAxis;
    [SerializeField]
    private FloatReference ShipVelocity;

    public void Move()
    {
        Vector2 newPosition = transform.position;
        Vector2 direction = new Vector2(HorizontalAxis.Value, VerticalAxis.Value);
        if (direction.x != 0 && direction.y != 0)
            direction /= 2;
        Vector2 positionStep = direction * ShipVelocity.Value;

        newPosition = Vector2.Lerp(newPosition, newPosition + positionStep, 0.1f);
        transform.position = newPosition;
    }
}
