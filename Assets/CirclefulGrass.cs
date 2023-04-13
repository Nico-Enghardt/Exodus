using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclefulGrass : MonoBehaviour
{
    //public Texture2D grass_density_map;
    public GameObject grassblade_prefab;

    public static int numGrassBlades = 1000;
    public float patch_radius = 10f;

    public Transform player_transform;

    private Mesh grasspatch_mesh;
    private (Vector3, Vector3, Vector3)[] grassblade_data = new (Vector3, Vector3, Vector3)[numGrassBlades];

    // Start is called before the first frame update
    void Start()
    {
        Mesh grassblade_mesh = grassblade_prefab.GetComponent<MeshFilter>().sharedMesh;
        CombineInstance[] combine = new CombineInstance[numGrassBlades];

        Quaternion rotateUpside = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        for (int i = 0; i < numGrassBlades; i++)
        {
            // Position grassblade in circle
            grassblade_data[i] = spawn_grassblade();

            // Create submesh for grassblade
            combine[i].mesh = grassblade_mesh;

            combine[i].transform = Matrix4x4.TRS(grassblade_data[i].Item1, Quaternion.AngleAxis(Random.Range(0, 360), new Vector3(0, 1, 0)) * rotateUpside, grassblade_data[i].Item2);
        }

        grasspatch_mesh = new Mesh();
        grasspatch_mesh.CombineMeshes(combine);

        MeshFilter thisMeshFilter = this.GetComponent<MeshFilter>();
        thisMeshFilter.mesh = grasspatch_mesh;

    }

    (Vector3, Vector3, Vector3) spawn_grassblade() // Todo: Introduce Player view
    {
        float x = player_transform.position.x + Random.Range(-patch_radius, patch_radius);
        float z = player_transform.position.z + Random.Range(-patch_radius, patch_radius);

        Vector3 position = new Vector3(x, 0, z);

        if (Vector3.Distance(position, player_transform.position) > patch_radius)
        {
            return spawn_grassblade();
        }

        // Position, Scale, Rotation

        return (position, new Vector3(1, 1, 1), new Vector3(0, 0, 0));
    }

    void respawn_grassblade()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = grasspatch_mesh.vertices; // Get the vertex data

        for (int i = 0; i < numGrassBlades; i++)
        {
            // Position grassblade in circle
            if (Vector3.Distance(grassblade_data[i].Item1, player_transform.position) > patch_radius)
            {
                Vector3 old_position = grassblade_data[i].Item1;

                grassblade_data[i] = spawn_grassblade();

                Vector3 displacement = grassblade_data[i].Item1 - old_position;

                // Update submesh for this grassblade

                for (int v = i * 6; v < (i + 1) * 6; v++)
                {
                    //int vertexIndex = triangles[v];
                    vertices[v] += displacement; // Modify the position of the vertex
                }
            }

        }

        grasspatch_mesh.vertices = vertices;
        grasspatch_mesh.RecalculateNormals();
        grasspatch_mesh.RecalculateBounds();
    }
}
