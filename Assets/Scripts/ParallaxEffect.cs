using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;
        public float parallaxFactor;
    }

    public ParallaxLayer[] layers;
    public float smoothing = 1f;

    private Vector3 previousCamPos;

    void Start()
    {
        previousCamPos = Camera.main.transform.position;
    }

    void Update()
    {
        foreach (ParallaxLayer layer in layers)
        {
            float parallax = (previousCamPos.x - Camera.main.transform.position.x) * layer.parallaxFactor;
            float targetPosX = layer.layer.position.x + parallax;

            Vector3 targetPosition = new Vector3(targetPosX, layer.layer.position.y, layer.layer.position.z);
            layer.layer.position = Vector3.Lerp(layer.layer.position, targetPosition, smoothing * Time.deltaTime);
        }

        previousCamPos = Camera.main.transform.position;
    }
}
