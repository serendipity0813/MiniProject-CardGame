using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip flip;
    [SerializeField] private AudioSource audioSource;

    public void OpenCard()
    {
        this.audioSource.PlayOneShot(this.flip);

        this.anim.SetBool("isOpen", true);
        this.transform.Find("front").gameObject.SetActive(true);
        this.transform.Find("back").gameObject.SetActive(false);

        if (GameManager.I.firstCard == null)
        {
            GameManager.I.firstCard = this.gameObject;
        }
        else
        {
            GameManager.I.secondCard = this.gameObject;
            GameManager.I.IsMatched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        Destroy(this.gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    private void CloseCardInvoke()
    {
        this.anim.SetBool("isOpen", false);
        this.transform.Find("back").gameObject.SetActive(true);
        this.transform.Find("front").gameObject.SetActive(false);
    }
}
