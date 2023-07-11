using UnityEngine;
using UnityEditor;

public class PaintScript : MonoBehaviour
{
    public Material material; //el material de la piel
    public GameObject pen;
    [SerializeField] int penThickness = 30;
    [SerializeField] Texture2D paintedTexture;
    private Color32 skinColor;

    private void Start()
    {
        skinColor = paintedTexture.GetPixel(0, 0);
    }

    private void OnApplicationQuit()
    {
        RedoTextures();
    }
    void RedoTextures()
    {
        
            //Que la painted texture se reinicie
            Color32[] resetColorArray = paintedTexture.GetPixels32();

            for (int i = 0; i < resetColorArray.Length; i++)
            {
                resetColorArray[i] = skinColor;
            }

            paintedTexture.SetPixels32(resetColorArray);
            paintedTexture.Apply();
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Human")
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(collision.GetContact(0).point, pen.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                print("hit");
                Vector2 uv = hit.textureCoord;
                int x = (int)(uv.x * paintedTexture.width);
                int y = (int)(uv.y * paintedTexture.height);

                for (int i = 0; i < penThickness; i++)
                {
                    for (int j = 0; j < penThickness; j++)
                    {
                        paintedTexture.SetPixel(x + i, y + j, pen.GetComponent<MeshRenderer>().material.color);
                    }
                }

                paintedTexture.Apply();
                material.mainTexture = paintedTexture;                
            }
            
        }
    }
}

