// mConvay's Game of Life : Unity C# version by mgear : http://unitycoder.com/blog/

/* ORIGINAL INFO:
Conway by Mike Davis.
This program is a simple version of Conway's game of Life.
A lit point turns off if there are fewer than two or more than three surrounding lit points.
An unlit point turns on if there are exactly three lit neighbors.
The 'density' parameter determines how much of the board will start out lit.
All Examples Written by Casey Reas and Ben Fry  unless otherwise stated.  
processingjs.org
*/

using UnityEngine;
using System.Collections;

public class mConvay1 : MonoBehaviour 
{
	private float lastResetTime = 0.0f;
	public float resetTime = 60.0f;
	private int sx;
	private int sy;
    public int width = 64;
	public int height = 64;   
    private float density = 0.5f;   
	int[,,] world;
	private int size;
	private Texture2D texture;
	private Texture2D origtexture;
	Color[] clearArray;
	Color[] tempArray;

	public Color clearColor;
	public Color pixelColor;
	
	public float speed = 0.1f; // seconds between iterations of the simulation
	private float lastTime = 0;
	
    void Start()   
    {
		Reset();
	}   
	
	void Reset()
	{
		world = new int[width, height, 2];
		// create new texture
		texture = new Texture2D(width, height, TextureFormat.RGB24, false);
		GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Point;
		GetComponent<Renderer>().material.mainTexture.anisoLevel = 1;
		sx = width;  
		sy = height;  
		size = width*height;
		
		// Set random cells to 'on'   
		for (int i = 0; i < sx * sy * density; i++) 
		{
			world[(int)(Random.value*sx),(int)(Random.value*sy),1] = 1;   
		}   
	  
		// array to be used for clearing the texture
		clearArray = new Color[width*height];
		// array for setpixelS(), instead of using setpixel()
		tempArray = new Color[width*height];
	  
		// fill our clearing array
		for(int i = 0; i < height; i++)
		{
			for(int j = 0; j < width; j++)
			{
				clearArray[i*height+j] = clearColor;
			}
		}	
		lastResetTime = Time.time;
	}
	
	void FixedUpdate()
	{
		if ( Time.time - lastTime >= speed )
		{
			Iterate();
			lastTime = Time.time;
		}
	}
	
	void Iterate()
	{
		for (int x = 0; x < sx; x=x+1) 
		{
			for (int y = 0; y < sy; y=y+1) 
			{
				int count = Neighbors(x, y);
				if (count == 3 && world[x,y,0] == 0)
				{
					world[x,y,1] = 1;
				}
				if ((count < 2 || count > 3) && world[x,y,0] == 1)
				{
					world[x,y,1] = -1;
				}
			}
		}		
	}
	
	// mainloop
    void Update()   
    {   

			if ( Time.time - lastResetTime >= resetTime )
			{
				Reset();	
			}
			// clear texture by pre-filled array
			texture.SetPixels(clearArray);
			
			// clear temp array (re-init)
			tempArray = new Color[size];
			
			// Drawing and update cycle   
			for (int x = 0; x < sx; x=x+1) 
			{
				for (int y = 0; y < sy; y=y+1) 
				{
					if ((world[x,y,1] == 1) || (world[x,y,1] == 0 && world[x,y,0] == 1))   
					{   
						world[x,y,0] = 1;   
						// here we get 1 white pixel
						tempArray[x*height+y]=pixelColor;
					}   
					if (world[x,y,1] == -1)   
					{   
						world[x,y,0] = 0;   
					}   
					world[x,y,1] = 0;   
				}
			}
	
			// set all pixels, using our array
			texture.SetPixels(tempArray,0);
			texture.Apply(false);
		
//			// Birth and death cycle   
//			for (int x = 0; x < sx; x=x+1) 
//			{
//				for (int y = 0; y < sy; y=y+1) 
//				{
//					int count = Neighbors(x, y);
//					if (count == 3 && world[x,y,0] == 0)
//					{
//						world[x,y,1] = 1;
//					}
//					if ((count < 2 || count > 3) && world[x,y,0] == 1)
//					{
//						world[x,y,1] = -1;
//					}
//				}
//			}

	}
       
    // Count the number of adjacent cells 'on'   
	int Neighbors(int x, int y)   
    {   
      return world[(x + 1) % sx,y,0] +   
             world[x,(y + 1) % sy,0] +   
             world[(x + sx - 1) % sx,y,0] +   
             world[x,(y + sy - 1) % sy,0] +   
             world[(x + 1) % sx,(y + 1) % sy,0] +   
             world[(x + sx - 1) % sx,(y + 1) % sy,0] +   
             world[(x + sx - 1) % sx,(y + sy - 1) % sy,0] +   
             world[(x + 1) % sx,(y + sy - 1) % sy,0];   
    }  
}
