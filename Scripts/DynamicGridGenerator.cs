using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGridGenerator : MonoBehaviour 
{
    public InputField numOfRows;
    public InputField numOfColumns;
    public RectTransform panelRow;
    public GameObject gridCell;
		
    public Transform grid;
    public Transform grid2;
    public GridLayoutGroup grid2GLG;
    private int rowSize;
    private int columnSize;

    public GameObject GgenerateBtn;

    bool isRowValidate;
    bool isColumnValidate;

    void Start()
    {
        isRowValidate = false;
        isColumnValidate = false;
        GgenerateBtn.SetActive(false);
    }

    void Initialize()
    {
        if (string.IsNullOrEmpty(numOfRows.text))
            return;
        if (string.IsNullOrEmpty(numOfColumns.text))
            return;
        rowSize = int.Parse(numOfRows.text);
        columnSize = int.Parse(numOfColumns.text);
    }
		
    public void ClearGrid()
    {
        for (int count = 0; count < grid.childCount; count++)
        {
            Destroy(grid.GetChild(count).gameObject);
        }
        for (int count = 0; count < grid2.childCount; count++)
        {
            Destroy(grid2.GetChild(count).gameObject);
        }
    }
		
    public void GenerateGrid()
    {
        ClearGrid();
				
        Initialize();
		
        GameObject cellInputField;
        RectTransform rowParent;
        for (int rowIndex = 0; rowIndex < rowSize; rowIndex++)
        {
            rowParent = (RectTransform)Instantiate(panelRow);
            rowParent.transform.SetParent(grid);
            rowParent.transform.localScale = Vector3.one;
            for (int colIndex = 0; colIndex < columnSize; colIndex++)
            {
                cellInputField = (GameObject)Instantiate(gridCell);
                cellInputField.transform.SetParent(rowParent);
                cellInputField.GetComponent<RectTransform>().localScale = Vector3.one;		
            }
        }
    }
    public void GenerateGrid2()
    {
        if (string.IsNullOrEmpty(numOfRows.text))
            return;
        if (string.IsNullOrEmpty(numOfColumns.text))
            return;

        ClearGrid();

        Initialize();

        GameObject cellInputField;
        grid2GLG.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid2GLG.constraintCount = rowSize;
        for (int rowIndex = 0; rowIndex < rowSize*columnSize; rowIndex++)
        {
                cellInputField = (GameObject)Instantiate(gridCell);
                cellInputField.transform.SetParent(grid2);
                cellInputField.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public void InputRowValidation()
    {
        if (string.IsNullOrEmpty(numOfRows.text))
            return;
        Debug.Log("input validation");
        rowSize = int.Parse(numOfRows.text);
        if(rowSize>1 && rowSize<11)
        {
            Debug.Log("Valid Number");
            isRowValidate = true;
            if (isColumnValidate) GgenerateBtn.SetActive(true);
        }

        else
        {
            Debug.Log("InValid Number");
            numOfRows.text = string.Empty;
        }
    }

    public void InputColumnValidation()
    {
        if (string.IsNullOrEmpty(numOfColumns.text))
            return;
        Debug.Log("input validation");
         columnSize = int.Parse(numOfColumns.text);
        if (columnSize > 1 && columnSize < 11)
        {
            Debug.Log("Valid Number");
            isColumnValidate = true;
            if (isRowValidate) GgenerateBtn.SetActive(true);
        }
        else
        {
            Debug.Log("InValid Number");
            numOfColumns.text = string.Empty;
        }
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Close()
    {
        Application.Quit();
    }

}
