using UnityEngine;

public class RandomNoise : MonoBehaviour
{
    [SerializeField]
    private NoiseGraph noiseGraph;
    [SerializeField]
    private NoiseTexture noiseTexture;

    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    private float[] noiseMap;

    private bool showGraph;

    private void Awake()
    {
        noiseMap = new float[width * height];

        Noise();
    }

    private void Update()
    {
        showGraph = Input.GetKeyDown(KeyCode.Space) ? !showGraph : showGraph;

        if (showGraph)
        {
            // Draw Noise Graph
            noiseTexture.gameObject.SetActive(false);
            noiseGraph.gameObject.SetActive(true);
            noiseGraph.Initialize(width, height, noiseMap);
        }
        else
        {
            // Draw Noise Texture
            noiseGraph.gameObject.SetActive(false);
            noiseTexture.gameObject.SetActive(true);
            noiseTexture.Initialize(width, height, noiseMap);
        }

        Noise();
    }

    private void Noise()
    {
        string str = "[";

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[y * width + x] = Random.value;

                str += $"{noiseMap[y * width + x]}, ";
            }
        }

#if UNITY_EDITOR
        Debug.Log(str);
#endif
    }
}
