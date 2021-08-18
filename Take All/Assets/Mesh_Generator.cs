using System.Collections;
using UnityEngine;

public class Mesh_Generator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;                                                                                                        //вершины для рисовки
    int[] triangles;                                                                                                            // Линии между вершинами (!ВАЖНО! заполнять по часовой стрелке)
    Vector2[] uvs;                                                                                                              //Координаты отрио=совки текстуры
 // Color[] colors;

    [SerializeField] private int xSize = 20;                                                                                    // Размер для сетки
    [SerializeField] private int zSize = 20;                                                                                    // Размер для сетки
 // [SerializeField] private Gradient gradient;
    private float minTerrainHight;
    float maxTerrainHight;

    public void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()                                                                                                          //Работа с масивом
    {
        verticles = new Vector3[(xSize + 1) * (zSize + 1)];                                                                     //генерация точек

        for (int i = 0, z = 0; z <= zSize; z++)                                                                                 //Заполнение масива точками
        {
            
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                verticles[i] = new Vector3(x, y, z);

                if (y > maxTerrainHight)
                {
                    y = maxTerrainHight;
                }
                if (y < minTerrainHight)
                {
                    y = minTerrainHight;
                }

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];                                                                                 //Генерация линий

        int vert = 0;                                                                                                           //смешение квадрата по вертикали
        int tris = 0;                                                                                                           //Переменная дляя расширения масива

        for (int z = 0; z < zSize; z++)                                                                                         //наполнение масива линиями
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        /*colors = new Color[verticles.Length];
        for (int i = 0, z = 0; z <= zSize; z++)                                                                                 
        {

            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHight, maxTerrainHight, verticles[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }*/

        //-------------------------------------------
        uvs = new Vector2[verticles.Length];
        for (int i = 0, z = 0; z <= zSize; z++)                                                                                 //Заполнение масива точками
        {

            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }
        /*-------------------------------------------*/

        /* -------------------------------------------
         * Для локального кадрата
         * verticles = new Vector3[]
         {
             new Vector3 (0,0,0),
             new Vector3 (0,0,1),
             new Vector3 (1,0,0),
             new Vector3 (1,0,1)
         };

         triangles = new int[]
         {
             0,1,2,
             3,2,1
         }; 
         ------------------------------------------- */
    }

    void UpdateMesh()                                                                                                           //Прорисовка и нормализация Mash'a
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = triangles;
        //mesh.colors = colors;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
}
