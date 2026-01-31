using System;
using TMPro;
using UnityEngine;

namespace ggj26
{
    public class SheepSelection : MonoBehaviour
    {
        [SerializeField] private GameObject sheep;
        [SerializeField] private int sheepRows = 2;
        [SerializeField] private int sheepColumns = 8;
        [SerializeField] private float sheepSeparationX = 1.0f;
        [SerializeField] private float sheepSeparationY = 1.5f;


        private GameObject[][] sheepFlock;
        private int sheepSkin = 0;
        private SheepController sheepControler;
        private TextMeshPro sheepChar;

        private void Start()
        {
            sheepFlock = new GameObject[sheepRows][];
            string charID = "Q";
            for (int i = 0; i < sheepRows; i++)
            {
                sheepFlock[i] = new GameObject[sheepColumns];
                for (int j = 0; j < sheepColumns; j++)
                {
                    sheepFlock[i][j] = Instantiate(sheep, transform);
                    sheepFlock[i][j].transform.position = transform.position 
                        + new Vector3(j * sheepSeparationX, -i * sheepSeparationY, 0) 
                        + new Vector3((sheepColumns -1 ) * -sheepSeparationX / 2f, (sheepRows - 1) * sheepSeparationY / 2f, 0);
                    sheepControler = sheepFlock[i][j].GetComponent<SheepController>();
                    sheepControler.SetSheepMask(sheepSkin);
                    sheepChar = sheepFlock[i][j].GetComponentInChildren<TextMeshPro>();
                    sheepChar.text = SetSheepChar(sheepSkin);
                    sheepSkin++;
                }
            }
        }

        private void Update()
        {
            GetSheepSelection();
        }

        private String SetSheepChar(int id)
        {
            if (id == 0)
            {
                return "Q";
            }
            else if (id == 1)
            {
                return "W";
            }
            else if (id == 2)
            {
                return "E";
            }
            else if (id == 3)
            {
                return "R";
            }
            else if (id == 4)
            {
                return "T";
            }
            else if (id == 5)
            {
                return "Y";
            }
            else if (id == 6)
            {
                return "U";
            }
            else if (id == 7)
            {
                return "I";
            }
            else if (id == 8)
            {
                return "A";
            }
            else if (id == 9)
            {
                return "S";
            }
            else if (id == 10)
            {
                return "D";
            }
            else if (id == 11)
            {
                return "F";
            }
            else if (id == 12)
            {
                return "G";
            }
            else if (id == 13)
            {
                return "H";
            }
            else if (id == 14)
            {
                return "J";
            }
            else if (id == 15)
            {
                return "K";
            }

            return "";
        }

        private void GetSheepSelection()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RhythmManager.Instance.SetWolfID(0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                RhythmManager.Instance.SetWolfID(1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RhythmManager.Instance.SetWolfID(2);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RhythmManager.Instance.SetWolfID(3);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                RhythmManager.Instance.SetWolfID(4);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                RhythmManager.Instance.SetWolfID(5);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                RhythmManager.Instance.SetWolfID(6);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                RhythmManager.Instance.SetWolfID(7);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                RhythmManager.Instance.SetWolfID(8);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                RhythmManager.Instance.SetWolfID(9);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                RhythmManager.Instance.SetWolfID(10);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                RhythmManager.Instance.SetWolfID(11);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                RhythmManager.Instance.SetWolfID(12);
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                RhythmManager.Instance.SetWolfID(13);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                RhythmManager.Instance.SetWolfID(14);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                RhythmManager.Instance.SetWolfID(15);
            }
        }
    }
}
