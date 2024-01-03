using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPaths : Graphic
{
    public MapEncounters scb;
    public int[][] encounter;
    public float thickness = 10f;
    public List<Vector2> Map;
    public int paths = 0;





    protected override void OnPopulateMesh(VertexHelper vh)
    {
        scb = GetComponent<MapEncounters>();
        encounter = scb.enc;
        Map = scb.Map;
        vh.Clear();
        for (int i = 0; i < Map.Count; i++)
        {                
            Vector2 point = Map[i];                
            DrawLine(point, vh);
        }
        for (int i = 0; encounter[i] != null; i++)
        {
            int index1 = i * 2;
            for (int j = 3; j <= 8 ? encounter[i][j] != 0 : false; j++)
            {
                int index2 = encounter[i][j]*2;
                vh.AddTriangle(index1, index1+1, index2 + 1);
                vh.AddTriangle(index2 + 1, index2, index1);
            }
        }
    }
    void DrawLine(Vector2 point, VertexHelper vh)
    {

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3((float)point.x, (float)point.y);
        vh.AddVert(vertex);

        vertex.position = new Vector3(thickness / 2, 0);
        vertex.position += new Vector3((float)point.x, (float)point.y);
        vh.AddVert(vertex);

    }
}
