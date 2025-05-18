using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CurrencyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CurrencyManager m_Instance { get; private set; }
    public uint m_Stars = 0;
    public uint m_Coins = 0;
    private string m_CurrencyFileName = "Currency.save";

    public void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(uint CoinsAdded)
    {
        m_Coins += CoinsAdded;
    }

    public void AddStars(uint StarsAdded)
    {
        m_Stars += StarsAdded;
    }

    public bool RemoveCoins(uint CoinsRemoved)
    {
        if(m_Coins < CoinsRemoved)
        {
            return false;
        }
        else
        {
            m_Coins -= CoinsRemoved;
            return true;
        }
    }

    public bool RemoveStars(uint StarsRemoved)
    {
        if (m_Stars < StarsRemoved)
        {
            return false;
        }
        else
        {
            m_Stars -= StarsRemoved;
            return true;
        }
    }
    public void LoadCurrency()
    {
        m_Stars = SaveLoadManager.Load<uint>(m_CurrencyFileName);
        m_Coins = SaveLoadManager.Load<uint>(m_CurrencyFileName);
    }

    public void SaveCurrency()
    {
        SaveLoadManager.Save(m_CurrencyFileName, m_Stars);
        SaveLoadManager.Save(m_CurrencyFileName, m_Coins);
    }
}
