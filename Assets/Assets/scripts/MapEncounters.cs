using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

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
    private int index = 0;
    private int[] itemsInRows = new int[6] { 1, 0, 0, 0, 0, 1 };





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




    

    public void RandomizeMap()
    {
        int rowCounter = 0;
        int indexTemp = 0;

        // { numer wiersza (0 - 11), numer obiektu w wierszu(1-6),   typ obiektu(0, 1, 3, 20, 21),   indexy obiektów z ktorymi jest po³¹czony na wyzszym wierszu,   dodaæ wiecej wartoœci dla innych zmiennych (typ przeciwnika, poziom itp itd) } 
        // 0 - walka, 1 - odpoczynek, 3 - boss fight, 20 - niewiadomy(walka), 21 - niewiadomy(odpoczynek)


        enc[index] = new int[] { rowCounter, 1, 0, 0, 0, 0, 0, 0, 0 };  // punkt startowy
        index++;
        rowCounter++;

        while (true)
        {
            if (rowCounter == 5) break;  // ¿eby nie wyjœæ poza mape


            int rows = Random.Range(1, 6);  // randomowa iloœæ kafelków w wierszu
            for (int i=1; i<= rows; i++)
            {
                int objectType = Random.Range(0, 2);  // randomowy typ obiektu (walka / odpoczynek)     docelowo niewiadomy(walka) / niewiadomy(odpoczynek)

                enc[index] = new int[] { rowCounter, i, objectType, 0, 0, 0, 0, 0, 0 };
                index++;
            }

            itemsInRows[rowCounter] = index-indexTemp-1;
            indexTemp = index - 1;
            rowCounter++;
        }

        enc[index] = new int[] { rowCounter, 1, 3, 0, 0, 0, 0, 0, 0 };  // boos fight

    }






    public void CreatePaths()
    {
        for (int i = 1; i <= itemsInRows[1]; i++)  // po³¹czenie startu z ka¿dym nastêpnym kafelkiem
        {
            enc[0][i+2] = i;
        }





        for (int i = 1; i <= itemsInRows[4]; i++)  // po³¹czenie ka¿dego kafelka z przedostatniego wiersza z bosem
        {
            enc[index-i][3] = index;
        }

    }
}
