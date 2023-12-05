using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class MapPaths : Graphic
{
    //TEN KOD NIE JEST STAR¥ WERSJ¥ WBREW NAZWIE, TA WERSJA JEST NAJNOWSZA I DZIA£AJ¥CA 





    /*[SerializeField] GameObject mesh;
    [SerializeField] GameObject path;
    public MapEncounters scb;
    public int[][] encounter;
    public float thickness = 10f;
    public List<Vector2> Map;
    public int paths = 0;
    

    public void Start()
    {
        RectTransform meshrect = mesh.GetComponent<RectTransform>();
        scb = GetComponent<MapEncounters>();
        encounter = scb.enc;
        Map = scb.Map;

        for (int i = 0; i < Map.Count; i++)
        {
            Vector2 point = Map[i];
           // DrawLine(point, vh, meshrect);
        }
        for (int i = 0; encounter[i] != null; i++)
        {
            Vector2 p1 = Map[i];
            for (int j = 3; j <= 8 ? encounter[i][j] != 0 : false; j++)
            {
                Vector2 p2 = Map[encounter[i][j]];

            }
        }
    }

    void AddPath(Vector2 p1, Vector2 p2, RectTransform pathrect)
    {
        pathrect.rect.height = 10;




    }

    */

    //TEN KOD NIE JEST STAR¥ WERSJ¥ WBREW NAZWIE, TA WERSJA JEST NAJNOWSZA I DZIA£AJ¥CA 
    [SerializeField] GameObject mesh;
     public MapEncounters scb;
     public int[][] encounter;
     public float thickness = 10f;
     public List<Vector2> Map;
     public int paths = 0;
     protected override void OnPopulateMesh(VertexHelper vh) //rysuje dwa trójk¹ty tworz¹ce razem prostok¹t, jedn¹ ze œcie¿ek
     {
         RectTransform meshrect = mesh.GetComponent<RectTransform>();
         scb = GetComponent<MapEncounters>();
         encounter = scb.enc;
         Map = scb.Map;
         vh.Clear();
         for (int i = 0; i < Map.Count; i++)
         {                
             Vector2 point = Map[i];                
             DrawLine(point, vh, meshrect);
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
     void DrawLine(Vector2 point, VertexHelper vh, RectTransform meshrect) //nie umia³em tego naprawiæ, naprawi³ to chat gpt, nie pytajcie sie mnie co sie tu dzieje bo nawet tego nie przeczyta³em
     {                                                                     //dzia³a tylko jeœli bg jest dopasowany do rogu canvasu, inaczej sie rozjezdza, idk moze uda sie komus jeszce to naprawic
        UIVertex vertex = UIVertex.simpleVert;                             // + grubosc lini nie skaluje sie z rozdzialczoscia ekranu, na 4k sa mega cienkie
        vertex.color = color;

        Vector3[] corners = new Vector3[4];
        meshrect.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];  // Assuming the pivot of the mesh is at the center

        Vector3 worldPoint = new Vector3(bottomLeft.x + point.x, bottomLeft.y + point.y, 0f);

        Vector3 localPoint1 = meshrect.InverseTransformPoint(worldPoint + new Vector3(-thickness / 2, 0));
        Vector3 localPoint2 = meshrect.InverseTransformPoint(worldPoint + new Vector3(thickness / 2, 0));

        vertex.position = localPoint1;
        vh.AddVert(vertex);

        vertex.position = localPoint2;
        vh.AddVert(vertex);

    }
}
