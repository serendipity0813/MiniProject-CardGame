using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    private void Awake() { I = this; }

    [HideInInspector] public GameObject firstCard;
    [HideInInspector] public GameObject secondCard;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject card;

    [SerializeField] private AudioClip match;
    [SerializeField] private AudioSource audioSource;

    private float time = 0.0f;

    private void Start()
    {
        Time.timeScale = 1.0f;

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            var newCard = Instantiate(this.card);
            newCard.transform.SetParent(GameObject.Find("Cards").transform);

            var x = (i / 4) * 1.4f - 2.1f;
            var y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            var rtanName = "rtan" + rtans[i];
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    private void Update()
    {
        this.time += Time.deltaTime;
        this.timeText.text = this.time.ToString("N2");

        if (this.time > 30.0f)
        {
            this.endText.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void IsMatched()
    {
        var firstCardImage = this.firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        var secondCardImage = this.secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
    
        if (firstCardImage == secondCardImage)
        {
            this.audioSource.PlayOneShot(this.match);

            this.firstCard.GetComponent<Card>().DestroyCard();
            this.secondCard.GetComponent<Card>().DestroyCard();

            var cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2) 
            {
                this.endText.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            this.firstCard.GetComponent<Card>().CloseCard();
            this.secondCard.GetComponent<Card>().CloseCard();
        }

        this.firstCard = null;
        this.secondCard = null;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
