using UnityEngine;

public class NoiseGraph : MonoBehaviour
{
    [SerializeField]
    private float depth;

    private int width;
    private int height;
    private float[] noiseMap;

    private LineRenderer lineRenderer;

    public void Initialize(int width, int height, float[] noiseMap)
    {
        this.width = width;
        this.height = height;
        this.noiseMap = noiseMap;

        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = width * height;

        DrawNoiseGraph();
    }

    private void DrawNoiseGraph()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                lineRenderer.SetPosition(index, new Vector3(index * depth, noiseMap[index], 0f));
            }
        }
    }
}
