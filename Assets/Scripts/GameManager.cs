using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Start screen")]
    public GameObject startPanel;

    [Header("Candies")]
    public int candiesToWin = 20;
    public TextMeshProUGUI candyText;

    [Header("Lives")]
    public int lives = 3;
    public TextMeshProUGUI livesText;

    [Header("Respawn")]
    public Transform player;
    public Transform playerSpawn;
    public NavMeshAgent ghost;
    public Transform ghostSpawn;

    [Header("Catch scare")]
    public Transform playerCamera;
    public Image blackScreen;
    public float scareCameraHeight = 1.3f;

    [Header("Messages")]
    public TextMeshProUGUI messageText;

    [Header("End screen")]
    public GameObject endScreen;
    public TextMeshProUGUI endText;

    int candies = 0;
    bool isCatching = false;
    Coroutine messageRoutine;

    static bool hasStarted = false;

    void Start()
    {
        UpdateText();
        SetBlack(0f);
        if (endScreen != null) endScreen.SetActive(false);
        if (messageText != null) messageText.gameObject.SetActive(false);

        if (hasStarted)
        {
            if (startPanel != null) startPanel.SetActive(false);
        }
        else
        {
            if (startPanel != null) startPanel.SetActive(true);
            Time.timeScale = 0f;
            player.GetComponent<PlayerController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    public void StartGame()
    {
        hasStarted = true;
        if (startPanel != null) startPanel.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<PlayerController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetWhenPlayPressed()
    {
        hasStarted = false;
    }

    public void ShowMessage(string message, float seconds)
    {
        if (messageText == null) return;

        if (messageRoutine != null) StopCoroutine(messageRoutine);
        messageRoutine = StartCoroutine(MessageRoutine(message, seconds));
    }

    IEnumerator MessageRoutine(string message, float seconds)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        messageText.gameObject.SetActive(false);
    }

    public void AddCandy()
    {
        candies++;
        UpdateText();

        if (candies >= candiesToWin)
        {
            EndGame("You Win!");
        }
    }

    public void PlayerCaught()
    {
        if (isCatching) return;
        StartCoroutine(CatchScare());
    }

    IEnumerator CatchScare()
    {
        isCatching = true;

        PlayerController controls = player.GetComponent<PlayerController>();
        GhostAI ghostBrain = ghost.GetComponent<GhostAI>();
        controls.enabled = false;
        ghostBrain.enabled = false;
        ghost.enabled = false;

        Vector3 inFront = player.position + player.forward * 0.9f;
        ghost.transform.position = inFront;
        ghost.transform.LookAt(new Vector3(player.position.x, inFront.y, player.position.z));

        playerCamera.localRotation = Quaternion.identity;

        Vector3 camStart = playerCamera.localPosition;
        Vector3 scarePosition = new Vector3(0f, scareCameraHeight, 0f);
        float time = 0f;
        while (time < 0.8f)
        {
            playerCamera.localPosition = scarePosition + new Vector3(Random.Range(-0.05f, 0.05f),
                                                                     Random.Range(-0.05f, 0.05f),
                                                                     0f);
            time += Time.deltaTime;
            yield return null;
        }
        playerCamera.localPosition = scarePosition;

        yield return Fade(0f, 1f, 0.5f);

        playerCamera.localPosition = camStart;

        lives--;
        UpdateText();

        if (lives <= 0)
        {
            EndGame("Game Over");
            yield break;
        }

        CharacterController body = player.GetComponent<CharacterController>();
        body.enabled = false;
        player.position = playerSpawn.position;
        player.rotation = playerSpawn.rotation;
        body.enabled = true;

        ghost.transform.position = ghostSpawn.position;
        ghost.enabled = true;
        ghostBrain.enabled = true;

        yield return new WaitForSeconds(0.5f);
        yield return Fade(1f, 0f, 0.5f);

        controls.enabled = true;
        isCatching = false;
    }

    IEnumerator Fade(float from, float to, float seconds)
    {
        float time = 0f;
        while (time < seconds)
        {
            time += Time.deltaTime;
            SetBlack(Mathf.Lerp(from, to, time / seconds));
            yield return null;
        }
        SetBlack(to);
    }

    void SetBlack(float amount)
    {
        if (blackScreen == null) return;
        Color color = blackScreen.color;
        color.a = amount;
        blackScreen.color = color;
    }

    void UpdateText()
    {
        if (candyText != null) candyText.text = "Candies: " + candies + "/" + candiesToWin;
        if (livesText != null) livesText.text = "Lives: " + lives;
    }

    void EndGame(string message)
    {
        player.GetComponent<PlayerController>().enabled = false;

        if (endText != null) endText.text = message;
        if (endScreen != null) endScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }
}
