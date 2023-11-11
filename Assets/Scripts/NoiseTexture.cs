using UnityEngine;

public class NoiseTexture : MonoBehaviour
{
    private int width;
    private int height;
    private float[] noiseMap;

    private SpriteRenderer spriteRenderer;

    public void Initialize(int width, int height, float[] noiseMap)
    {
        this.width = width;
        this.height = height;
        this.noiseMap = noiseMap;

        spriteRenderer = GetComponent<SpriteRenderer>();

        DrawNoiseTexture();
    }

    private void DrawNoiseTexture()
    {
        Color[] colorMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                // Color.black�� Color.white ������ ���� ���� noiseMap[index]�� ���� �����ؼ� ����
                // noiseMap[index] = 0�̸� ������(Color.black)
                // noiseMap[index] = 1�̸� �Ͼ��(Color.white)
                colorMap[index] = Color.Lerp(Color.black, Color.white, noiseMap[index]);
            }
        }

        Texture2D texture = new Texture2D(width, height);

        // Texture2D Ÿ���� �̹����� ColorMap ������ ä�� (��� ������ �̹���)
        texture.SetPixels(colorMap);
        texture.Apply();

        // ������ ��� Texture2D�� Sprite�� ������ spriteRenderer ������Ʈ�� ����
        Rect rect = new Rect(0, 0, texture.width, texture.height);

        spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
}
