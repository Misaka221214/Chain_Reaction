using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BuffSelectionManager : MonoBehaviour
{
    public int numToChoose;
    public int numChoice;

    public TextMeshProUGUI helpText;
    public List<UIBuff> buffSelectionList;
    public List<UIBuff> buffInventoryList;

    private int numLeftToChoose;
    private List<ObstacleEnum> availBuffList = new List<ObstacleEnum>();
    void Start()
    {
        availBuffList = new List<ObstacleEnum>(GetBuffToChoose());
        numLeftToChoose = numChoice;

        UpdateHelpTextDisplay();
        UpdateSelectionDisplay();
        UpdateInventoryDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<ObstacleEnum> GetBuffToChoose()
    {
        List<ObstacleEnum> result = new List<ObstacleEnum>();

        for (int i = 0; i < numToChoose; i++)
        {
            var newBuff = ObstacleEnum.BASIC;

            while (newBuff == ObstacleEnum.BASIC)
            {
                int randIndex = Random.Range(0, PrefabDict.prefabDict.Count);
                newBuff = PrefabDict.prefabDict.ElementAt(randIndex).Key;
            }

            
            result.Add(newBuff);
        }

        return result;
    }

    public void UpdateSelectionDisplay()
    {
        for (int i = 0; i < buffSelectionList.Count; i++)
        {
            if (i < availBuffList.Count) {
                buffSelectionList[i].SetBuff(availBuffList[i]);
                buffSelectionList[i].UpdateDisplay();

            } else
            {
                buffSelectionList[i].HideDisplay();
            }
        }
    }

    public int GetNumLeftToChoose()
    {
        return numLeftToChoose;
    }

    public void HandleBuffSelection(ObstacleEnum buff)
    {
        numLeftToChoose--;
        LevelData.playerObstacleList.Add(buff);
        availBuffList.Remove(buff);
        UpdateHelpTextDisplay();
        UpdateSelectionDisplay();
        UpdateInventoryDisplay();

        if (numLeftToChoose <= 0)
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelProgressionManager.Instance.HandleFinishBuffSelection();
    }

    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < buffInventoryList.Count; i++)
        {
            if (i < LevelData.playerObstacleList.Count)
            {
                buffInventoryList[i].SetBuff(LevelData.playerObstacleList[i]);
                buffInventoryList[i].UpdateDisplay();

            }
            else
            {
                buffInventoryList[i].HideDisplay();
            }
        }
    }

    public void UpdateHelpTextDisplay()
    {
        if (numLeftToChoose == 0)
        {
            helpText.text = "Loading Next...";
        } else
        {
            helpText.text = "Choose " + numLeftToChoose + " Buff";
        }      
    }

}
