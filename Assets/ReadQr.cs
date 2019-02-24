using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public struct QRData
{
    public int[] heights;
    public int[] colors;

    public QRData(int[] heights, int[] colors)
    {
        this.heights = heights;
        this.colors = colors;
    }
}

public class ReadQr : MonoBehaviour
{
    public GameObject[] stateArray = new GameObject[50];

    private WebCamTexture camTexture;
    private Rect screenRect;

    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width*.25f, Screen.height * .25f);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight =  Screen.height;
        camTexture.requestedWidth = Screen.width;
        
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect,camTexture, ScaleMode.ScaleAndCrop);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            // Decodes the current frame.
            IBarcodeReader barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if(result != null)
            {
                // Decodes the QR code.
                Debug.Log("Decoded from QR: " + result.Text);
                QRData qrData = DecodeTextQR(result.Text);

                // Creates a debug string.
                String debugHeights = "Heights: ";
                String debugColors = "Colors: ";
                for(int i = 0; i < 50; i++)
                {
                    debugHeights += qrData.heights[i] + " ";
                    debugColors += qrData.colors[i] + " ";
                }
                Debug.Log(debugHeights);
                Debug.Log(debugColors);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

    /// <summary>
    /// Turns a QR string into a QRData struct.
    /// </summary>
    /// <returns>QRData struct</returns>
    /// <param name="qr">QR data string</param>
    public QRData DecodeTextQR(String qr)
    {
        String encoding = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,.";
        int[] heights = new int[50];
        int[] colors = new int[50];
        try
        {
            // Decodes the heights.
            for (int i = 0; i < 50; i++)
            {
                heights[i] = encoding.IndexOf(qr[i]);
                stateArray[i].transform.localScale = new Vector3(1, 1, heights[i]*.2f);
            }

            // Decodes the colors.
            for (int i = 0; i < 50; i++)
            {
                colors[i] = encoding.IndexOf(qr[i + 50]);
            }

            return new QRData(heights, colors);
        }
        catch (Exception)
        {
            // Invalid QR format.
            return new QRData(heights, colors);
        }
    }


    // Update is called once per frame
    void Update()
    {
       

    }


   }
