using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadingImage : MonoBehaviour
{
    public static string fileName;
    Texture2D loadedImageTexture;
    public float th = .09f;
    static Texture2D img, rImg, gImg, bImg, kImg, roImg, goImg, boImg, koImg, sumImg;
    static float[,] rL, gL, bL, kL, sumL;
    static Color temp;
    static float t;

    EdgeCollider2D myEdgeCollider;
    List<Vector2> colliderPoints;
    Texture newImage;

    Rigidbody2D rb;
    float deltaX, deltaY;
    bool moveBall = false;

    // Start is called before the first frame update
    void Start()
    {
        fileName = CapturePhoto.fileName;
        GameObject ball = GameObject.Find("GameBall (1)");
        rb = ball.GetComponent<Rigidbody2D>();
        byte[] readImage = null;

        readImage = File.ReadAllBytes(Application.persistentDataPath + "/" + fileName + ".png");

        loadedImageTexture = new Texture2D(300, 200, TextureFormat.RGB24, false);
        loadedImageTexture.LoadImage(readImage);

        img = new Texture2D(loadedImageTexture.width, loadedImageTexture.height);

        //initialize textures
        rImg = new Texture2D(img.width, img.height);
        bImg = new Texture2D(img.width, img.height);
        gImg = new Texture2D(img.width, img.height);
        kImg = new Texture2D(img.width, img.height);

        //initialize gradient arrays
        rL = new float[img.width, img.height];
        gL = new float[img.width, img.height];
        bL = new float[img.width, img.height];
        kL = new float[img.width, img.height];
        sumL = new float[img.width, img.height];

        roImg = new Texture2D(img.width, img.height);
        goImg = new Texture2D(img.width, img.height);
        boImg = new Texture2D(img.width, img.height);
        koImg = new Texture2D(img.width, img.height);
        sumImg = new Texture2D(img.width, img.height);

        GameObject col = GameObject.Find("Background");
        myEdgeCollider = col.gameObject.AddComponent<EdgeCollider2D>();

        GetWebCamImage();
    }

    void GetWebCamImage()
    {
        img.SetPixels32(loadedImageTexture.GetPixels32());
        CalculateEdges();
    }

    void CalculateEdges()
    {
        //calculate new textures
        for (int x = 0; x < img.width; x++)
        {
            for (int y = 0; y < img.height; y++)
            {
                temp = img.GetPixel(x, y);
                rImg.SetPixel(x, y, new Color(temp.r, 0, 0));
                gImg.SetPixel(x, y, new Color(0, temp.g, 0));
                bImg.SetPixel(x, y, new Color(0, 0, temp.b));
                t = temp.r + temp.g + temp.b;
                t /= 3f;
                kImg.SetPixel(x, y, new Color(t, t, t));
            }
        }

        rImg.Apply();
        gImg.Apply();
        bImg.Apply();
        kImg.Apply();

        //calculate gradient values
        for (int x = 0; x < img.width; x++)
        {
            for (int y = 0; y < img.height; y++)
            {
                rL[x, y] = gradientValue(x, y, 0, rImg);
                gL[x, y] = gradientValue(x, y, 1, gImg);
                bL[x, y] = gradientValue(x, y, 2, bImg);
                kL[x, y] = gradientValue(x, y, 2, kImg);

                sumL[x, y] = gL[x, y] + bL[x, y];

            }
        }
 
        TextureFromGradientRef(sumL, th, ref sumImg);
   

    }

    // Update is called once per frame
    void OnGUI()
    {
        GameObject backIm = GameObject.Find("Background");
        backIm.GetComponent<RawImage>().texture = sumImg;
    }
    float gradientValue(int ex, int why, int colorVal, Texture2D image)
    {
        float lx = 0f;
        float ly = 0f;
        if (ex > 0 && ex < image.width)
            lx = 0.5f * (image.GetPixel(ex + 1, why)[colorVal] - image.GetPixel(ex - 1, why)[colorVal]);
        if (why > 0 && why < image.height)
            ly = 0.5f * (image.GetPixel(ex, why + 1)[colorVal] - image.GetPixel(ex, why - 1)[colorVal]);
        return Mathf.Sqrt(lx * lx + ly * ly);
    }

    void TextureFromGradientRef(float[,] g, float thres, ref Texture2D output)
    {
        colliderPoints = new List<Vector2>();
        int j = 0;
        int yPrev = 0;

        for (int x = 0; x < output.width; x++)
        {
            for (int y = 0; y < (output.height - 1); y++)
            {
                if (g[x, y] >= thres)
                {
                    output.SetPixel(x, y, Color.black);
                    if (Mathf.Abs(y - yPrev) <= 2)
                    {
                        colliderPoints.Add(new Vector2(x, y));
                    }

                    j++;
                    yPrev = y;
                }
                else
                {
                    output.SetPixel(x, y, Color.white);
                }
            }
        }

        myEdgeCollider.points = colliderPoints.ToArray();
        output.Apply();

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:


                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        moveBall = true;
                    }
                    break;

                case TouchPhase.Moved:

                    if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos) && moveBall)
                        rb.MovePosition(new Vector2(touchPos.x, touchPos.y));
                    break;

                case TouchPhase.Ended:

                    rb.gravityScale = 20;

                    break;
            }
        }
    }
}





