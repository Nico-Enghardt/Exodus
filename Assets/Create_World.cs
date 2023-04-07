using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Create_World : MonoBehaviour
{

    public Texture2D height_map;
    public Texture2D grassy_map;

    public float scale = 0.2f;
    public float max_height = 0.5f;

    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {

        int width = height_map.width;
        int height = height_map.height;

        width = 200;
        height = 200;

        //Debug.Log(height_map.GetPixels().r);
        //Debug.Log(height_map.GetPixels()[400]);

        float height_scale = 1; //max_height / height_map.getpixels().max().r;
        //print("height_scale " + height_scale);

        Vector3[] vertices_list = new Vector3[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];

        int point_index = 0;
        int triangles_index = 0;

        print("Width and height  " + width + " - " + height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float heightValue = height_map.GetPixel(x, y).grayscale;
                print("x and y " + x + "  -  " + y);
                Vector3 vertex = new Vector3(x * scale, heightValue * height_scale, y * scale);

                print("point_index" + point_index);
                vertices_list[point_index++] = vertex;

                //print(point_index == x + width * y);

                
            }
        }

        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                // Lower left corner triangle

                print("Triangle_index" + triangles_index);
                triangles[triangles_index++] = x + y * width;
                triangles[triangles_index++] = x + y * width + 1;
                triangles[triangles_index++] = x + (y + 1) * width;

                // Upper right corner triangle

                triangles[triangles_index++] = x + y * width + 1;
                triangles[triangles_index++] = x + (y + 1) * width + 1;
                triangles[triangles_index++] = x + (y + 1) * width;
            }
        }


                Mesh mesh = new Mesh();

        mesh.vertices = vertices_list;
        mesh.triangles = triangles;

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;


    }
}
