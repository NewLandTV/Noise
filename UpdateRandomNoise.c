#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <Windows.h>

// Noise defines
#define NOISE_SIZE 512
#define PIXEL_SIZE 1
#define COLOR_NOISE FALSE

// Flags
unsigned char quitEvent;

// Functions
void Init();
void Update();
void Render();

void CheckQuitProcess();

void DrawRandomNoise();

int main(void)
{
	Init();
	
	while (1)
	{
		Update();
		Sleep(100);
	}
	
	return 0;
}

void Init()
{
	srand((unsigned int)time(NULL));
	
	// Setup console
	system("title Update Random Noise");
	system("mode con cols=64 lines=32");
	
	// Screen setup
	CONSOLE_CURSOR_INFO consoleCursorInfo;
	
	consoleCursorInfo.bVisible = 0;
	consoleCursorInfo.dwSize = 1;
	
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &consoleCursorInfo);
}

void Update()
{
	CheckQuitProcess();
	Render();
}

void Render()
{
	DrawRandomNoise();
}

void CheckQuitProcess()
{
	if (GetAsyncKeyState(27) & 0x8000)
	{
		quitEvent = 1;
	}
}

void DrawRandomNoise()
{
	int x;
	int y;
	
	RECT clientRect;
	HWND consoleWindow = GetConsoleWindow();
	
	GetClientRect(consoleWindow, &clientRect);
	
	HDC hdc = GetDC(consoleWindow);
	HDC hMemoryDC = CreateCompatibleDC(hdc);
	HBITMAP hBitmap = CreateCompatibleBitmap(hdc, clientRect.right, clientRect.bottom);
	HBITMAP hOldBitmap = (HBITMAP)SelectObject(hMemoryDC, hBitmap);
	
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
			
			FillRect(hMemoryDC, &rect, brush);
			DeleteObject(brush);
		}
	}
	
	BitBlt(hdc, 0, 0, clientRect.right, clientRect.bottom, hMemoryDC, 0, 0, SRCCOPY);
	SelectObject(hMemoryDC, hOldBitmap);
	ReleaseDC(consoleWindow, hMemoryDC);
	ReleaseDC(consoleWindow, hdc);
}
