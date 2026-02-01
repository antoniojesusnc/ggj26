using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ggj26.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Urd.Services;

namespace ggj26
{
    public class SheepSelection : MonoBehaviour
    {
        [SerializeField] private GameObject sheep;
        [SerializeField] private int sheepRows = 2;
        [SerializeField] private int sheepColumns = 8;
        [SerializeField] private float sheepSeparationX = 1.0f;
        [SerializeField] private float sheepSeparationY = 1.5f;
        [SerializeField] private int nextSceneID = 0;
        [SerializeField] private int firstScene = 0;
        [SerializeField] private bool isSelection = true;
        [SerializeField] private GameObject _playAgainButtons;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private GameObject _prePopUp;
        [Header("End Scene")]
        [SerializeField] private GameObject _endScene;
        [SerializeField] private float _timeToShowEndGame;
        [SerializeField] private float _timeToScale;
        [SerializeField] private float _timeToMove;
        [SerializeField] private GameObject _winImage;
        [SerializeField] private GameObject _loseImage;
        [SerializeField] private DOTweenAnimation fadeOut;

        private GameObject[][] sheepFlock;
        private int sheepSkin = 0;
        private SheepController sheepControler;
        private TextMeshPro sheepChar;
        private bool wolfChose = false;

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
                                                          + new Vector3((sheepColumns - 1) * -sheepSeparationX / 2f,
                                                              (sheepRows - 1) * sheepSeparationY / 2f, 0);
                    sheepControler = sheepFlock[i][j].GetComponent<SheepController>();
                    sheepControler.SetSheepMask(sheepSkin);
                    sheepChar = sheepFlock[i][j].GetComponentInChildren<TextMeshPro>();
                    sheepChar.text = SetSheepChar(sheepSkin);
                    sheepSkin++;
                }
            }

            if (_playAgainButtons != null)
            {
                _playAgainButtons.gameObject.SetActive(false);
            }

            if (isSelection)
            {
                if (!AudioService.Instance.IsSoundOfType(Ggj26AudioTypes.MainMenu))
                {
                    AudioService.Instance.PlaySound(Ggj26AudioTypes.MainMenu);
                }
            }
            else
            {
                AudioService.Instance.StopSound(RhythmManager.Instance.CurrentLevel.LevelConfig.AudioTypes);
            }

            if (_winImage != null)
            {
                _winImage.gameObject.SetActive(false);
            }

            if (_loseImage != null)
            {
                _loseImage.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            AudioService.Instance?.StopSound(Ggj26AudioTypes.GameLose);
            AudioService.Instance?.StopSound(Ggj26AudioTypes.GameWin);
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
            if (isSelection && _prePopUp.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _prePopUp.gameObject.SetActive(false);
                }

                return;
            }
            
            if (wolfChose) return;

            int pickedSheep = -1;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                pickedSheep = 0;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                pickedSheep = 1;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                pickedSheep = 2;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                pickedSheep = 3;
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                pickedSheep = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                pickedSheep = 5;
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                pickedSheep = 6;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                pickedSheep = 7;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                pickedSheep = 8;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                pickedSheep = 9;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                pickedSheep = 10;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                pickedSheep = 11;
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                pickedSheep = 12;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                pickedSheep = 13;
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                pickedSheep = 14;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                pickedSheep = 15;
            }


            if (pickedSheep >= 0) 
            {
                wolfChose = true;
                    
                if (isSelection)
                {
                    RhythmManager.Instance.SetWolfID(pickedSheep);
                    StartCoroutine(ChooseCostumeCoroutine(pickedSheep));

                }
                else
                {
                    StartCoroutine(ChooseOptionCoroutine(pickedSheep));
                    enabled = false;
                }
            }
        }

        private IEnumerator ChooseCostumeCoroutine(int pickedSheep)
        {
            fadeOut.tween.Play();
            yield return new WaitForSeconds(2f);
            AudioService.Instance.PlaySound(Ggj26AudioTypes.Zipper);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(nextSceneID);

        }

        private IEnumerator ChooseOptionCoroutine(int pickedSheep)
        {
            List<SheepController> sheeps = new();
            for (int i = 0; i < sheepFlock.Length; i++)
            {
                for (int j = 0; j < sheepFlock[i].Length; j++)
                {
                    sheeps.Add(sheepFlock[i][j].GetComponent<SheepController>());
                }
            }

            var sheepSelected = sheeps.Find(sheep => sheep.SkinId == pickedSheep);
            sheeps.Remove(sheepSelected);

            for (int i = 0; i < sheeps.Count; i++)
            {
                sheeps[i].transform.DOScale(Vector3.zero, _timeToScale);
            }

            sheepSelected.transform.DOLocalMove(Vector3.zero, _timeToMove);

            yield return new WaitForSeconds(Mathf.Max(_timeToMove, _timeToScale));

            AudioService.Instance.PlaySound(Ggj26AudioTypes.Shotgun);
            _endScene.gameObject.SetActive(true);

            yield return new WaitForSeconds(_timeToShowEndGame);
            
            ShowMessage(sheepSelected);
            
        }

        private void ShowMessage(SheepController sheepSelected)
        {
            sheepSelected.gameObject.SetActive(false);
            if (sheepSelected.SkinId == RhythmManager.Instance.WolfID)
            {
                //Wolf dies
                _headerText.text = "LACK OF GROOVE KILLED THE WOLF";
                AudioService.Instance.PlaySound(Ggj26AudioTypes.GameWin);
                _winImage.gameObject.SetActive(true);
            }
            else
            {
                //A sheep dies
                _headerText.text = "SMOOTH WOLF... BON APPETIT!";
                AudioService.Instance.PlaySound(Ggj26AudioTypes.GameLose);
                _loseImage.gameObject.SetActive(true);
            }
            _playAgainButtons.gameObject.SetActive(true);
        }

        public void OnClickInPlayAgain()
        {
            RhythmManager.Instance.OnPlayAgain(); ;
        }
        
        public void OnClickInMainMenu()
        {
            RhythmManager.Instance.LoadMainMenu();
        }
    }
}
