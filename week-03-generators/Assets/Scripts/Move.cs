using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        // Rotate our cube head based on the time elapsed to give it some motion.
        // We use a sine function since it's periodic (ie. loops forever), is
        // continuous (ie. the motion will be gentle and smooth), and has a defined
        // range (ie. it'll always give us back a value between -1 and 1, which we
        // can use however we like.)

        // We can multiply the values it gives us back to change the amplitude (ie.
        // the amount it'll rotate.) If we multiply the time value we pass to the
        // function, that changes the period (ie. how long it takes to loop.)

        // Try doubling some of these numbers to see how the motion changes!
        transform.localEulerAngles = new Vector3(
            Mathf.Sin(Time.time * 4), // Rotate on the x-axis
            10f * Mathf.Sin(Time.time), // Rotate on the y-axis
            2f * Mathf.Sin(Time.time * 3) // Rotate on the z-axis
        );
	}
}
