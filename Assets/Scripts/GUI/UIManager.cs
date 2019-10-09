using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] GameScreenView gameScreenView;
    private List<BaseScreenView> allScreens = new List<BaseScreenView>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        allScreens.Add(gameScreenView);

        gameScreenView.Show();
    }

    public void InitGameScreen(List<ObjectType> sequence)
    {
        gameScreenView.InitSeqenceView(sequence);
    }
}
