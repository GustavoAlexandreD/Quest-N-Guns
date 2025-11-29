using UnityEngine;

public class PontoDeQuestao : MonoBehaviour
{
    private bool jaFoiRespondido = false;
    private SpriteRenderer visual;
    void Start()
    {
        visual = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !jaFoiRespondido)
        {
            Debug.Log("Jogador pisou no ponto de quiz!");
            if (QuizUI.Instance != null)
            {
                QuizUI.Instance.ExibirProximaQuestao();
                
                jaFoiRespondido = true;
                if(visual != null)
                {
                    visual.color = Color.gray;
                }
            }
        }
    }
}
