using UnityEngine;

namespace ggj26
{
    public class SheepSelection : MonoBehaviour
    {
        [SerializeField] private GameObject sheep;
        [SerializeField] private int sheepRows = 2;
        [SerializeField] private int sheepColumns = 8;
        [SerializeField] private float sheepSeparation = 1.0f;


        private GameObject[][] sheepFlock;
        private int sheepSkin = 0;
        private SheepController sheepAnimationControl;
        private int chosenSheep;
        private void Start()
        {
            sheepFlock = new GameObject[sheepRows][];
            for (int i = 0; i < sheepRows; i++)
            {
                sheepFlock[i] = new GameObject[sheepColumns];
                for (int j = 0; j < sheepColumns; j++)
                {
                    sheepFlock[i][j] = Instantiate(sheep, transform);
                    sheepFlock[i][j].transform.position = transform.position 
                        + new Vector3(j, i, 0) * sheepSeparation 
                        - new Vector3((sheepColumns -1 ) * sheepSeparation / 2f, (sheepRows - 1) * sheepSeparation / 2f, 0);
                    sheepAnimationControl = sheepFlock[i][j].GetComponent<SheepController>();
                    sheepAnimationControl.SetSheepMask(sheepSkin);
                    sheepSkin++;
                }
            }
        }
    }
}
