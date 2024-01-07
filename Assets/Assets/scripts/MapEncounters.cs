using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;

public class MapEncounters : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject Enc_fight;
    [SerializeField] private GameObject Enc_rest;
    [SerializeField] private GameObject Enc_unk;
    [SerializeField] private GameObject Enc_boss;
    private GameObject currGO;
    public List<Vector2> Map = new List<Vector2>();
    private List<int> dist = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //distribution, max 10 wierszy, tutaj zapisywana jest ilosc encounterów na wiersz
    public int[][] enc = new int[62][]; //max 10 wierszy, max 6 encounterów na wiersz + wiersz 0 (start) + wiersz 11 (boss)
    public float thickness = 10f;
    private int rows = 0;
    private int indexOfTile = 0;
    private int[] itemsInRows = new int[6] { 1, 0, 0, 0, 0, 1 };
    private int[,] indexesOfTiles = new int[6,5];





    void Start()
    {
         RectTransform bgrect = bg.GetComponent<RectTransform>();
        /* 
         tutaj mozna stworzyæ logike tworzenia mapy, czyli w sumie generacji ca³ej gry. proponuje:
         w ka¿dym wierszu prznajmniej dwa encountery, prawdopodobieñstwo wyst¹pienie kolejnych jako:
         1/(2^(n-2)) dla n oznaczaj¹ce numer encountera w wierszu, ograniczyæ dla (2-6)
         iloœæ wierszy albo sta³a albo tez sobie wymyœlcie jakies prawdopodobienstwo idc

         LUB stworzyæ nowy skrypt do generowania i skopiowaæ warttoœci jak w GridRender

         dodaæ generowanie zamiast tych wpisanych na twardo elementów \/ \/ \/
         */



        //enc[0] = new int[] { 0, 1, 1, 1, 2, 3, 4, 5, 0}; // { numer wiersza (0 - 11), numer obiektu w wierszu(1-6), typ obiektu(0, 1, 20, 21, 3), indexy obiektów z ktorymi jest po³¹czony na wyzszym wierszu, dodaæ wiecej wartoœci dla innych zmiennych (typ przeciwnika, poziom itp itd) } 
        //enc[1] = new int[] { 1, 1, 20, 7, 0, 0, 0, 0, 0}; // 0 - walka, 1 - odpoczynek, 20-niewiadomy(walka), 21-niewiadomy(odpoczynek), 3 - boss fight
        //enc[2] = new int[] { 1, 2, 1, 7, 0, 0, 0, 0, 0}; 
        //enc[3] = new int[] { 1, 3, 0, 7, 0, 0, 0, 0, 0};
        //enc[4] = new int[] { 1, 4, 21, 7, 6, 0, 0, 0, 0};
        //enc[5] = new int[] { 1, 5, 21, 7, 6, 0, 0, 0, 0};
        //enc[6] = new int[] { 2, 1, 21, 8, 0, 0, 0, 0, 0 };
        //enc[7] = new int[] { 2, 2, 21, 8, 0, 0, 0, 0, 0 };
        //enc[8] = new int[] { 3, 1, 21, 9, 10, 11, 0, 0, 0};
        //enc[9] = new int[] { 4, 1, 21, 12, 0, 0, 0, 0, 0 };
        //enc[10] = new int[] { 4, 2, 21, 12, 0, 0, 0, 0, 0 };
        //enc[11] = new int[] { 4, 3, 21, 12, 0, 0, 0, 0, 0 };
        //enc[12] = new int[] { 5, 1, 3, 0, 0, 0, 0, 0, 0 }; 

        RandomizeMap();
        CreatePaths();



        for (int i = 0; enc[i] != null; i++) //tak d³ugo a¿ elementy istniej¹
        { 
            dist[enc[i][0]] += 1; //zlicza iloœæ elementów w kazdym wierszu
            rows = rows < enc[i][0] ? enc[i][0] : rows; //zlicza ilosc wygenerowanych wierszy
        }
        rows += 2;

        for (int i = 0; enc[i] != null; i++)
        {
            Spawn(enc[i][0], dist[enc[i][0]], enc[i][1], enc[i][2], rows, bgrect);
        }
    }




    
    public void Spawn(int a, int b, int c, int d, int e, RectTransform bgrect) //automatycznie dodaje encounter na mape w zale¿noœci od tabelki enc[][]
    {
        Vector3 position = new Vector3((float)bgrect.rect.width /-2 , (float)bgrect.rect.height/ -2, 0);
        position += new Vector3(((float)bgrect.rect.width / (1 + b)) * (c),
            ((float)bgrect.rect.height / e) * (1 + a)
            , 0); //dzia³¹, nie ruszaæ
        switch (d)
        {
            case 0:
                currGO = Enc_fight;
                break;
            case 1:
                currGO = Enc_rest;
                break;
            case 20:
                currGO = Enc_unk;
                break;
            case 21:
                currGO = Enc_unk;
                break;
            case 3:
                currGO = Enc_boss;
                break;
        }
        var obj = Instantiate(currGO, bgrect, true);
        obj.transform.localPosition = position;
        Map.Add(obj.GetComponent<RectTransform>().position); //zamienia end[][] w liste stworzonych na jej postawie GameObjectow
    }




    

    // generowanie kafli
    public void RandomizeMap()
    {
        int rowCounter = 0;
        int indexTemp = 0;

        // { numer wiersza (0 - 11),   numer obiektu w wierszu(1-6),   typ obiektu(0, 1, 3, 20, 21),   indexy obiektów z ktorymi jest po³¹czony na wyzszym wierszu,   dodaæ wiecej wartoœci dla innych zmiennych (typ przeciwnika, poziom itp itd) } 
        // 0 - walka, 1 - odpoczynek, 3 - boss fight, 20 - niewiadomy(walka), 21 - niewiadomy(odpoczynek)


        enc[indexOfTile] = new int[] { rowCounter, 1, 1, 0, 0, 0, 0, 0, 0 };  // punkt startowy
        indexesOfTiles[rowCounter, 0] = indexOfTile;
        indexOfTile++;
        rowCounter++;

        while (true)
        {
            if (rowCounter == 5) break;  // ¿eby nie wyjœæ poza mape


            int rows = UnityEngine.Random.Range(2, 5);  // randomowa iloœæ kafelków w wierszu
            for (int i = 1; i <= rows; i++)
            {
                int objectType = UnityEngine.Random.Range(0, 2);  // randomowy typ obiektu (walka / odpoczynek)     docelowo niewiadomy(walka) / niewiadomy(odpoczynek)

                enc[indexOfTile] = new int[] { rowCounter, i, objectType, 0, 0, 0, 0, 0, 0 };
                indexesOfTiles[rowCounter, i-1] = indexOfTile;
                indexOfTile++;
            }



            itemsInRows[rowCounter] = indexOfTile - indexTemp - 1;
            indexTemp = indexOfTile - 1;
            rowCounter++;
            new WaitForSeconds((float)0.1);  // sleep poniewa¿ czêsto mapa generowa³a powtarzaln¹ iloœæ kafli w jednym wierszy (np. 1 3 3 5 5 1)
        }

        enc[indexOfTile] = new int[] { rowCounter, 1, 3, 0, 0, 0, 0, 0, 0 };  // boos fight
        indexesOfTiles[rowCounter, 0] = indexOfTile;
    }






    // tworzenie po³¹czeñ 
    public void CreatePaths()  
    {
        int tempCounter = 0;
        int[] entrancesToTile = new int[30];
        entrancesToTile[0]++;


        


        // po³¹czenie kafla startowego z kaflami z nastêpnego rzêdu
        for (int i = 1; i <= itemsInRows[1]; i++)  
        {
            enc[tempCounter][i+2] = i;
            entrancesToTile[i]++;
        }
        tempCounter++;





        // tworzenie lini dla wygenerowanych kafli
        for (int i=1; i<5; i++)  
        {
            for (int j=0; j<5; j++)
            {
                if (indexesOfTiles[i, j] != 0)
                {
                    int numberOfConnectionsOfCurrentTile = UnityEngine.Random.Range(1, 3);

                    int targetTile = UnityEngine.Random.Range(indexesOfTiles[i+1, 0], indexesOfTiles[i+1, itemsInRows[i+1]-1 ] );
                    enc[tempCounter][3] = targetTile;
                    entrancesToTile[targetTile]++;
                    new WaitForSeconds((float)0.2);

                    if (numberOfConnectionsOfCurrentTile != 1)
                    {
                        targetTile = UnityEngine.Random.Range(indexesOfTiles[i + 1, 0], indexesOfTiles[i + 1, itemsInRows[i + 1] - 1]);
                        enc[tempCounter][4] = targetTile;
                        entrancesToTile[targetTile]++;
                        new WaitForSeconds((float)0.2);
                    }

                    tempCounter++;
                }
            }
        }





        // po³¹czenie ka¿dego kafelka z przedostatniego wiersza z bosem
        for (int i = 1; i <= itemsInRows[4]; i++)  
        {
            enc[indexOfTile - i][3] = indexOfTile;
            tempCounter++;
        }





        // zapewnienie po³¹czeñ dla kafli nie po³¹czonych od do³u (którch nie wylosowa³o wczeœniej)
        for (int i = 1; i < 5; i++)  
        {
            for (int j = 0; j < 5; j++)
            {
                if (entrancesToTile[indexesOfTiles[i, j]] == 0)
                {
                    for (int k = 3; k <= 8; k++)
                    {
                        if (enc[indexesOfTiles[i, j]][k] == 0)
                        {
                            int targetTile = UnityEngine.Random.Range(indexesOfTiles[i - 1, 0], indexesOfTiles[i - 1, 0] + itemsInRows[i - 1] - 1);
                            enc[indexesOfTiles[i, j]][k] = targetTile;

                            break;
                        }
                    }
                }
            }
        }



    }
}