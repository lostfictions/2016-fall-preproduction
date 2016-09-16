using UnityEngine;
using UnityEngine.SceneManagement;

public class Generate : MonoBehaviour
{
    [Header("Prefabs")]
    public Transform[] eyePrefabs;
    public Transform[] mouthPrefabs;

    [Header("Scene References")]
    public Transform eyeLeftPoint;
    public Transform eyeRightPoint;
    public Transform mouthPoint;

    public Transform eyebrowLeft;
    public Transform eyebrowRight;


    void Start()
    {
        // We start by picking an eye prefab randomly from our list.
        Transform eyeToSpawn = eyePrefabs[Random.Range(0, eyePrefabs.Length)];

        // Now we'll actually instantiate it, ie. make it exist in our scene.
        Transform eyeLeft = Instantiate(eyeToSpawn);

        // We set its parent to the reference point we assigned ahead of time,
        // and set both the local position and local rotation (ie. how much our
        // object is offset from our parents) to zero.
        eyeLeft.SetParent(eyeLeftPoint);
        eyeLeft.localPosition = Vector3.zero;
        eyeLeft.localRotation = Quaternion.identity;

        // Let's do the same for the right eye. We don't need to pick another
        // eye prefab from our list -- we probably want both eyes to be the same
        // style!
        Transform eyeRight = Instantiate(eyeToSpawn);
        eyeRight.SetParent(eyeRightPoint);
        eyeRight.localPosition = Vector3.zero;
        eyeRight.localRotation = Quaternion.identity;

        // Now let's handle the mouth.
        Transform mouthToSpawn = mouthPrefabs[Random.Range(0, mouthPrefabs.Length)];
        Transform mouth = Instantiate(mouthToSpawn);
        mouth.SetParent(mouthPoint);
        mouth.localPosition = Vector3.zero;
        mouth.localRotation = Quaternion.identity;


        // We've created all the face parts, so we could stop here. But maybe we can make
        // things a little more expressive by playing with some other attributes!


        // Let's start with putting a colour on our cube face. To change the colour we need
        // to get our material, and to get our material we need to get a reference to our
        // renderer. We use GetComponent for that.
        Renderer r = GetComponent<Renderer>();

        // The easiest way to set the colour is by assigning a new one to the material's
        // "color" property. To make a new color we need to provide three values: red,
        // green and blue, each as a number between 0 and 1. Read more about the ways you
        // can create a color in the documentation:
        // https://docs.unity3d.com/ScriptReference/Color.html

        // We could make a totally random colour, but that might look a little ugly sometimes.
        // Let's ensure that the colour is never darker than a 50% grey. We'll often get
        // something a little pastel-looking with this setup.
        r.material.color = new Color(
            0.5f + Random.value * 0.5f, // Red. Random.value gives us a number between 0 and 1, so halve it!
            0.5f + Random.value * 0.5f, // Green
            0.5f + Random.value * 0.5f // Blue
        );


        // Now let's tweak the eyebrows -- they convey a lot about someone's mood. In this
        // example they already exist in the scene, so let's rotate them.

        // I've decided that most of the time I want the eyebrows to be mirrored -- we'll
        // rotate the left and right by the same amount (with the right eyebrow rotation
        // being negative so it goes in the opposite direction.)

        // But what if 10% of the time I want the eyebrows to not be mirrored? This might
        // give a quizzical or puzzled expression. We'll do that like this:

        // Random.value gives a number between 0 and 1, so it'll be less than 0.9 90% of the time.
        if(Random.value < 0.9f) {
            float eyebrowRotationAmount = Random.Range(-30f, 30f);
            eyebrowLeft.localEulerAngles = new Vector3(0, 0, eyebrowRotationAmount);
            eyebrowRight.localEulerAngles = new Vector3(0, 0, -eyebrowRotationAmount);
        }
        else {
            // 10% of the time, each eyebrow will have a random value instead of sharing the same value.
            eyebrowLeft.localEulerAngles = new Vector3(0, 0, Random.Range(-30f, 30f));
            eyebrowRight.localEulerAngles = new Vector3(0, 0, Random.Range(-30f, 30f));
        }


        // What about the mouth? Let's say that 15% of the time we want to rotate it a little.
        if(Random.value < 0.15f) {
            mouth.localEulerAngles = new Vector3(0, 0, Random.Range(-10f, 10f));
        }


        // Finally, just for fun, let's also pick a color for the background. We're not using a skybox, so
        // the easiest way to do this is to set the color directly on our main camera. Like this...
        Camera.main.backgroundColor = new Color(
            0.1f + Random.value * 0.1f, // We want the backdrop dark to contrast with our face, so limit it to 20%.
            0.1f + Random.value * 0.1f,
            0.1f + Random.value * 0.1f
        );
    }
	
    void Update()
    {
        // If we press Space, restart the scene.
        // (The scene also has to be added to the list of included scenes
        // in the "Build Settings..." menu or this won't work.)
	    if(Input.GetKeyDown(KeyCode.Space)) {
	        SceneManager.LoadScene(0);
	    }
    }
}
