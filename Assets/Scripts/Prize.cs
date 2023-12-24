using UnityEngine;

public class Prize : MonoBehaviour
{
    private SceneController sceneController;
    [SerializeField] private int enemys;
    [SerializeField] private bool active = false;
    [SerializeField] private string sceneName;

    private void Start()
    {
        sceneController = GetComponent<SceneController>();
    }
    
    public void Nigeria(int number)
    {
        enemys -= number;
        if(enemys == 0)
        {
            active = true;
        }
    }

    private void OnTriggerEnter2D()
    {
        if(active)
        {
            sceneController.LoadScene(sceneName);
        }
    }
}
