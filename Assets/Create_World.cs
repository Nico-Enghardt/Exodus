using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Create_World : MonoBehaviour
{

    public Texture2D height_map;
    public Texture2D grassy_map;

    public float scale = 0.2f;
    public float max_height = 1f;

    private MeshFilter meshFilter;

    Vector3[] vertices_list;

    // Start is called before the first frame update
    void Start()
    {

        int width = height_map.width;
        int height = height_map.height;

        width = 257;
        height = 256;
        
        float height_scale = 3f; //max_height / height_map.getpixels().max().r;
        //print("height_scale " + height_scale);

        vertices_list = new Vector3[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];
        Vector2[] uv_list = new Vector2[width * height];

        int point_index = 0;
        int triangles_index = 0;

        print("Width and height  " + width + " - " + height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float heightValue = height_map.GetPixel(x, y).grayscale;
                Vector3 vertex = new Vector3((float)x * scale, heightValue * height_scale, (float)y * scale);

                print("point_index:" + point_index + ", x: " + x + ", y: " + y + ", vertex: " + vertex);

                uv_list[point_index] = new Vector2((float)(x + 1) / height_map.width, (float)(y + 1) / height_map.height);
                vertices_list[point_index] = vertex;

                point_index++;


            }
        }

        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                // Lower left corner triangle

                //triangles[triangles_index++] = y + x * height;
                //triangles[triangles_index++] = y + x * height + 1;
                //triangles[triangles_index++] = y + (x + 1) * height;

                print("triangle:" + (x + y * width) + ", " + (x + y * width + 1) + ", " + (x + (y + 1) * width));

                // Upper right corner triangle

                //triangles[triangles_index++] = x + y * width + 1;
                //triangles[triangles_index++] = x + (y + 1) * width + 1;
                //triangles[triangles_index++] = x + (y + 1) * width;
            }
        }

        OnDrawGizmos();
        Mesh mesh = new Mesh();

        //mesh.vertices = vertices_list;
        //mesh.triangles = triangles;
        //mesh.uv = uv_list;

        //meshFilter = GetComponent<MeshFilter>();
        //meshFilter.mesh = mesh;

        


    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < vertices_list.Length; i++)
        {
            Gizmos.DrawSphere(vertices_list[i], .1f);
        }
    }

}
