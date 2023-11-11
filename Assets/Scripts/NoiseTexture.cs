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

                // Color.black과 Color.white 사이의 색상 값을 noiseMap[index]에 따라 보간해서 적용
                // noiseMap[index] = 0이면 검은색(Color.black)
                // noiseMap[index] = 1이면 하얀색(Color.white)
                colorMap[index] = Color.Lerp(Color.black, Color.white, noiseMap[index]);
            }
        }

        Texture2D texture = new Texture2D(width, height);

        // Texture2D 타입의 이미지에 ColorMap 정보를 채움 (흑백 노이즈 이미지)
        texture.SetPixels(colorMap);
        texture.Apply();

        // 제작한 흑백 Texture2D를 Sprite로 변경해 spriteRenderer 컴포넌트에 적용
        Rect rect = new Rect(0, 0, texture.width, texture.height);

        spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
}
