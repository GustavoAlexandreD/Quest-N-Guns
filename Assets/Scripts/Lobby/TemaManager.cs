using System.Collections.Generic;
using UnityEngine;

public class TemaManager : MonoBehaviour
{
    public static TemaManager Instance { get; private set; }

    // Lista global das matérias ativas
    private HashSet<string> materiasAtivas = new HashSet<string>();

    private void Awake()
    {
        // Singleton seguro
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // persiste entre cenas
    }

    private void Start()
    {
        // Estado inicial: todas as matérias ativas
        materiasAtivas = new HashSet<string>()
        {
            "Matemática",
            "História",
            "Geografia",
            "Inglês",
            "Biologia",
            "Física",
            "Química",
            "Sociologia",
            "Filosofia"
        };
    }

    // Adiciona ou remove uma matéria do conjunto global
    public void SetMateria(string nome, bool ativa)
    {
        if (ativa)
            materiasAtivas.Add(nome);
        else
            materiasAtivas.Remove(nome);
    }

    // Retorna lista das matérias ativas no momento
    public List<string> GetMateriasAtivas()
    {
        return new List<string>(materiasAtivas);
    }

    // Verifica se uma matéria está ativa
    public bool MateriaAtiva(string nome)
    {
        return materiasAtivas.Contains(nome);
    }
}
