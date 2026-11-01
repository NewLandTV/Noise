#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <Windows.h>

#define NOISE_SIZE 512
#define PIXEL_SIZE 1
#define COLOR_NOISE FALSE

void Init();
void DrawRandomNoise();

int main(void)
{
	Init();
	Sleep(100);
	DrawRandomNoise();
	
	while (1);
	
	return 0;
}

void Init()
{
	srand((unsigned int)time(NULL));
	
	// Setup console
	system("title Random Noise");
	system("mode con cols=64 lines=32");
	
	// Hide cursor
	CONSOLE_CURSOR_INFO consoleCursorInfo;
	
	consoleCursorInfo.bVisible = 0;
	consoleCursorInfo.dwSize = 1;
	
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &consoleCursorInfo);
}

void DrawRandomNoise()
{
	int x;
	int y;
	
	HWND consoleWindow = GetConsoleWindow();
	HDC hdc = GetDC(consoleWindow);
	
	for (x = 0; x + PIXEL_SIZE <= NOISE_SIZE; x += PIXEL_SIZE)
	{
		for (y = 0; y + PIXEL_SIZE <= NOISE_SIZE; y += PIXEL_SIZE)
		{
			int value = rand() % 256;
			
			COLORREF color = RGB(value, value, value);
			
			if (COLOR_NOISE)
			{
				color = RGB(rand() % 256, rand() % 256, rand() % 256);
			}
			
			HBRUSH brush = CreateSolidBrush(color);
			RECT rect = { x, y, x + PIXEL_SIZE, y + PIXEL_SIZE };
			
			FillRect(hdc, &rect, brush);
			DeleteObject(brush);
		}
	}
	
	ReleaseDC(consoleWindow, hdc);
}
