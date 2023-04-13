using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Grass : MonoBehaviour
{
    public Texture2D grass_density_map;
    public GameObject grassblade_prefab;

    public int numGrassBlades = 300;
    public float patch_size = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        Mesh grassblade_mesh = grassblade_prefab.GetComponent<MeshFilter>().sharedMesh;
        CombineInstance[] combine = new CombineInstance[numGrassBlades];

        int texture_width = grass_density_map.width;
        int texture_height = texture_width;
        
        
        for (int i = 0; i < numGrassBlades; i++)
        {
            combine[i].mesh = grassblade_mesh;


            Vector3 offset = new Vector3(Random.Range(0, patch_size), 0 , Random.Range(0, patch_size));

            float grass_height = 0.4f * grass_density_map.GetPixel((int) (offset.x/patch_size * texture_width), (int) (offset.z / patch_size * texture_height)).r;
            offset.y = grass_height * 0.5f;

            Quaternion rotateUpside = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

            Vector3 scale = new Vector3(1,1,1) * (grass_height - .3f);
            Matrix4x4 matrix = Matrix4x4.TRS(offset, Quaternion.AngleAxis(Random.Range(0, 360), new Vector3(0, 1, 0)) * rotateUpside, scale);
            combine[i].transform = matrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        MeshFilter thisMeshFilter = this.GetComponent<MeshFilter>();
        thisMeshFilter.mesh = combinedMesh;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
