using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIBuff : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI labelText;

    public ObstacleEnum buff = ObstacleEnum.BASIC;

    private BuffSelectionManager buffSelectionManager;

    private void Awake()
    {
        buffSelectionManager = FindObjectOfType<BuffSelectionManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBuff(ObstacleEnum buff)
    {
        this.buff = buff;
    }

    public void UpdateDisplay()
    {
        if (buff != ObstacleEnum.BASIC)
        {
            labelText.text = buff.ToString();
            gameObject.SetActive(true);
        }

    }

    public void HideDisplay()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("On Click UIBuff");
        buffSelectionManager.HandleBuffSelection(buff);
    }
}
