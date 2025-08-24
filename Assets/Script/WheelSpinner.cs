using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WheelSpinner : MonoBehaviour
{
    public Transform wheel;
    public int segmentCount = 12;
    public float minSpeed = 500f;
    public float maxSpeed = 1000f;
    public float deceleration = 100f;
    public Button spinButton;
    public Text resultText;
    private bool isSpinning = false;
    private float currentSpeed;
    public GameObject panelGameObject;
    public Button OKButton;
    public TextMeshProUGUI resultTextTMP;
    public int Money = 10;
    public TextMeshProUGUI MoneyText;
    void Start()
    {
        spinButton.onClick.AddListener(StartSpin);
        OKButton.onClick.AddListener(buttonOKClick);
    }
    void StartSpin()
    {
        if (isSpinning) return;
        isSpinning = true;
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        StartCoroutine(SpinCoroutine());
        progressBar.fillAmount = 0f;
        StartCoroutine(CooldownCoroutine());
        Money -= 5;
        MoneyText.text = "Money: " + Money.ToString(); // Update the money text
    }
    void buttonOKClick()
    {
        panelGameObject.SetActive(false);
        progressBar.fillAmount = 0f;
        isSpinning = false;
        if(resultText != null)
            resultText.text = "";
        if(resultTextTMP != null)
            resultTextTMP.text = "";
    }
    IEnumerator SpinCoroutine()
    {
        PopUPController.instance.PlaySpinSound(); // Play the spin sound
        while (currentSpeed > 0)
        {
            wheel.Rotate(0, 0, -currentSpeed * Time.deltaTime);
            currentSpeed -= deceleration * Time.deltaTime;
            yield return null;
        }
        float z = wheel.eulerAngles.z;
        int index = GetRewardIndex(z);
        string reward = GetRewardName(index);
        resultText.text = "Trúng: " + reward;
        Debug.Log("Trúng ô: " + reward);
        Debug.Log("Z: " + wheel.eulerAngles.z + " => Index: " + GetRewardIndex(wheel.eulerAngles.z));
        panelGameObject.SetActive(true);
        resultTextTMP.text = "Trúng: " + reward;
        int rewardValue = int.Parse(reward);
        
        Money += rewardValue; // Assuming Money is an integer that accumulates the rewards
        MoneyText.text = "Money: " + Money.ToString(); // Update the money text
        PopUPController.instance.PauseSpinSound();
        isSpinning = false;
    }
    [SerializeField] private Image progressBar;
    private IEnumerator CooldownCoroutine()
    {
        float timer = 8f; // Start the cooldown timer
        while (timer > 0)
        {
            //textCooldown.text = "Cooldown: " + (timer).ToString("F1") + "s";
            timer -= Time.deltaTime;
            progressBar.fillAmount = 1f - (timer / 10f); // Update progress bar
            yield return null;
        }
        // buttonClick.interactable = true;
    }
    int GetRewardIndex(float z)
    {
        float angle = 360f / segmentCount;
        float correctedZ = ((360f - ((z + 170) % 360f)) + angle/2) % 360f;
        return Mathf.FloorToInt(correctedZ / angle) % segmentCount;
    }
    string GetRewardName(int index)
    {
        string[] rewards = {
            "12","01", "02", "03", "04", "05", 
            "06", "07", "08", "09", "10", "11"
        };
        return rewards[index];
    }
}
