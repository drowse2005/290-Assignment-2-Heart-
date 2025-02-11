using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShapeCircles : MonoBehaviour
{
    public int pointsCount = 100;  
    public float circleRadius = .5f;
    public Material circleMaterial; 
    public float pulsateSpeed = 2f;
    public float pulsateMagnitude = .5f; 

    void Start()
    {
        CreateHeartCircles();
    }

    void CreateHeartCircles()
    {
        for (int i = 0; i < pointsCount; i++)
        {
        
            float t = (float)i / pointsCount * Mathf.PI * 2;

            // Parametric equations for heart shape
            float x = 16 * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t);
            float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t);

            // Create a sphere for each point in the heart outline
            GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            circle.transform.position = new Vector3(x / 17f, y / 17f, 0); 

            circle.transform.localScale = new Vector3(circleRadius, circleRadius, circleRadius);

            if (circleMaterial != null)
            {
                Renderer rend = circle.GetComponent<Renderer>();
                rend.material = circleMaterial;
            }
            circle.transform.parent = transform;

            //start the pulsating effect for the circle
            StartCoroutine(PulsateCircle(circle));
        }
    }
 IEnumerator PulsateCircle(GameObject circle)
    {
        while (true)
        {
            //calculate the scale factor using Mathf.Sin to oscillate
            float scale = Mathf.Sin(Time.time * pulsateSpeed) * pulsateMagnitude + 1;

            //apply the new scale to the circle
            circle.transform.localScale = new Vector3(circleRadius * scale, circleRadius * scale, circleRadius * scale);

            //wait until the next frame
            yield return null;
        }
    }
}
