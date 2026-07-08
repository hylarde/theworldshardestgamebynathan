using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static Unity.Burst.Intrinsics.X86;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int deathCount = 0;

    private TextMeshProUGUI deathText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // procura o texto de novo, já que o antigo foi destruído
        GameObject textObj = GameObject.FindGameObjectWithTag("DeathText");
        if (textObj != null)
        {
            deathText = textObj.GetComponent<TextMeshProUGUI>();
            AtualizarUI();
        }
    }

    public void PlayerMorreu()
    {
        deathCount++;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (deathText != null)
            deathText.text = "Mortes: " + deathCount;
    }
}