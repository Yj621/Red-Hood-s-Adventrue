using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialsManager : MonoBehaviour
{


    public void CheckStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }


}
