using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Instantiater : MonoBehaviour
{   
    public GameObject cellTemplate;
    public GameObject cellLeader;
    public GameObject cellWarriot;
    public GameObject cellIntr;



    public float generationInterval = 0.2f;
    public static bool pause = false;

    int[ , ] cellsArray;

    public int gridHeight;
    private int gridWidth;

    public GameObject FertileZone;
    public GameObject DeadZone;
    Vector3 fertilePos;
    Vector3 deadPos;

    public static bool zones = true;


    private Camera cam;

    private float cellSize;

    void Update()
    {
        // Check if the 's' key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Start the program
            StartProgram();
        }

        // Check if the 'g' key is pressed to fill random cells
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Fill random cells in the area
            FillRandomArea();
        }
    }

        // Method to fill a random area with cells
    public void FillRandomArea()
    {
        int startX = Random.Range(0, gridWidth); // Random start X position
        int startY = Random.Range(0, gridHeight); // Random start Y position
        int areaWidth = Random.Range(5, 15); // Random width of the area
        int areaHeight = Random.Range(5, 15); // Random height of the area

        for (int i = startY; i < startY + areaHeight && i < gridHeight; i++)
        {
            for (int j = startX; j < startX + areaWidth && j < gridWidth; j++)
            {
                cellsArray[i, j] = Random.Range(0, 2); // Randomly set cell state (0 or 1)
            }
        }
    }
    public void FillRandomCells()
    {
        // Clear existing cells
        ClearCells();

        // Fill random alive cells
        for (int i = 0; i < 1500; i++)
        {
            cellsArray[Random.Range(0, gridHeight), Random.Range(0, gridWidth)] = 1;
        }

        // Render cells after filling random cells
        RenderCells();
    }


    // Method to start the program
   public void StartProgram()
    {
        gridWidth = Mathf.RoundToInt(gridHeight * Camera.main.aspect);

        cellsArray = new int[gridHeight, gridWidth];
        cellSize = (Camera.main.orthographicSize * 2) / gridHeight;
        print ("Dimensions: " + gridWidth + "X" + gridHeight);
        print("Cell size: " + cellSize);

        FertileZone = GameObject.Find("FertileZone");
        fertilePos = FertileZone.transform.position;
        print(fertilePos.x + " " + fertilePos.y);

        DeadZone = GameObject.Find("DeadZone");
        deadPos = DeadZone.transform.position;
        print(deadPos.x + " " + deadPos.y);

        // fill random alive cells
        for (int i = 0; i < 1500; i++)
        {
            cellsArray[Random.Range(20, 70), Random.Range(30, 120)] = 1; 
        }

        cam = Camera.main;

        InvokeRepeating("NewGenerationUpdate", generationInterval, generationInterval);
    }


    void NewGenerationUpdate(){
        if (Input.GetMouseButton(2))
        {
            if (pause==true) pause = false; else pause = true;
            print(" Pause " + pause);
        }
        else
        {           
            if (pause==false)
            {
                RenderCells();
                ApplyRules();
                //Environment();

            }
            UserInput();
        }
    }

    void UserInput()
    {
        if (Input.GetKey("p"))
        {
            
            if (pause == true) pause = false; else pause = true;
            print(" Pause " + pause);
            
        } 
        else if (Input.GetKey("c"))
        {
            ClearCells();
        }
        
        else if (Input.GetMouseButton(0))
        {
            Vector3 point = new Vector3();
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.y = cam.pixelHeight - mousePos.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

                int x = 0;
                int y = 0;
                x = (int)((point.x - cellSize / 2) / cellSize);
                y = (int)((point.y - cellSize / 2) / cellSize);

                print(" x= " + x);
                print(" y= " + y);
                if (x > 0 && x < gridWidth && y > 0 && y < gridHeight) cellsArray[y, x] = 1;
                if(zones)UserEnvironment(x, y);
            }
            RenderCells();
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 point = new Vector3();
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.y = cam.pixelHeight - mousePos.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

                int x = 0;
                int y = 0;
                x = (int)((point.x - cellSize / 2) / cellSize);
                y = (int)((point.y - cellSize / 2) / cellSize);

                print(" x= " + x);
                print(" y= " + y);
                if (x > 0 && x < gridWidth && y > 0 && y < gridHeight) cellsArray[y, x] = 0;
                if(zones)UserEnvironment(x, y);
            }
            RenderCells();
        }
        else if (Input.GetKey("w"))
        {
            Vector3 point = new Vector3();
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.y = cam.pixelHeight - mousePos.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

                int x = 0;
                int y = 0;
                x = (int)((point.x - cellSize / 2) / cellSize);
                y = (int)((point.y - cellSize / 2) / cellSize);

                print(" x= " + x);
                print(" y= " + y);
                if (x > 0 && x < gridWidth && y > 0 && y < gridHeight) cellsArray[y, x] = 3;
                print("Diablo cell");
                if (zones) UserEnvironment(x, y);
            }
            RenderCells();
        }
        else if (Input.GetKey("a"))
        {
            Vector3 point = new Vector3();
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.y = cam.pixelHeight - mousePos.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

                int x = 0;
                int y = 0;
                x = (int)((point.x - cellSize / 2) / cellSize);
                y = (int)((point.y - cellSize / 2) / cellSize);

                print(" x= " + x);
                print(" y= " + y);

                for(int i = y - 10; i <= y + 10; i++)
                {
                    for (int j = x - 10; j <= x + 10; j++)
                    {
                        if (j > 0 && j < gridWidth && i > 0 && i < gridHeight) cellsArray[i, j] = 1;
                    }
                }

                if(zones)UserEnvironment(x, y);
            }
            RenderCells();
        }
        else if (Input.GetKey("r"))
        {
            Vector3 point = new Vector3();
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.y = cam.pixelHeight - mousePos.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

                int x = 0;
                int y = 0;
                x = (int)((point.x - cellSize / 2) / cellSize);
                y = (int)((point.y - cellSize / 2) / cellSize);

                print(" x= " + x);
                print(" y= " + y);
                if (x > 0 && x < gridWidth && y > 0 && y < gridHeight) cellsArray[y, x] = 2;
                print("God cell");
                if (zones) UserEnvironment(x, y);
            }
            RenderCells();
        }
    }

    void ClearCells(){
        // Reset all cells to 0 (dead)
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                cellsArray[i, j] = 0;
            }
        }
    RenderCells(); // Update the visual representation of cells
    }


    public void RenderCells(){
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell")){
            Destroy(cell);
        }

        for (int i = 0; i < gridHeight; i++){
            for (int j = 0; j < gridWidth; j++){
                if (cellsArray[i, j] == 0) continue;
                if (cellsArray[i, j] == 1)
                {
                    Vector3 cellPosition = new Vector3(
                        j * cellSize + cellSize / 2,
                        (cellSize * gridHeight) - (i * cellSize + cellSize / 2),
                        0
                    );
                    Instantiate(cellTemplate, cellPosition, Quaternion.identity);
                }
                if (cellsArray[i, j] == 2)
                {
                    Vector3 cellPosition = new Vector3(
                        j * cellSize + cellSize / 2,
                        (cellSize * gridHeight) - (i * cellSize + cellSize / 2),
                        0
                    );
                    Instantiate(cellLeader, cellPosition, Quaternion.identity);
                }
                if (cellsArray[i, j] == 3)
                {
                    Vector3 cellPosition = new Vector3(
                        j * cellSize + cellSize / 2,
                        (cellSize * gridHeight) - (i * cellSize + cellSize / 2),
                        0
                    );
                    Instantiate(cellWarriot, cellPosition, Quaternion.identity);
                }
            }
        }

    }

    public void ApplyRules()
    {

        FertileZone = GameObject.Find("FertileZone");
        fertilePos = FertileZone.transform.position;
        DeadZone = GameObject.Find("DeadZone");
        deadPos = DeadZone.transform.position;

        int[,] nextGenGrid = new int[gridHeight, gridWidth];
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                int livingNeighbours = CountLivingNeighbours(i, j);


                if (ControlGod(i, j))
                {
                    nextGenGrid[i - 1, j] = 1;
                    nextGenGrid[i, j - 1] = 1;
                    nextGenGrid[i, j + 1] = 1;
                    nextGenGrid[i + 1, j] = 1;
                    nextGenGrid[i, j] = 2;
                }
                else if (ControlDiablo(i, j))
                {
                    nextGenGrid[i - 1, j] = 0;
                    nextGenGrid[i, j - 1] = 0;
                    nextGenGrid[i, j + 1] = 0;
                    nextGenGrid[i + 1, j] = 0;
                    cellsArray[i - 1, j] = 0;
                    cellsArray[i, j - 1] = 0;
                    cellsArray[i, j + 1] = 0;
                    cellsArray[i + 1, j] = 0;

                    nextGenGrid[i, j] = 3;
                }
                else if (livingNeighbours == 3 ) 
                { // reproduction, exactly 3 neighgbours
                    nextGenGrid[i, j] = 1;
                }
                else if (livingNeighbours == 2 && cellsArray[i, j] == 1)
                { // exactly 2 neigh, the live cell survives
                    nextGenGrid[i, j] = 1;
                }


                if (zones)
                {
                    bool fertile = PosFertile(j * cellSize + cellSize / 2, (cellSize * gridHeight) - (i * cellSize + cellSize / 2));
                    bool dead = PosDead(j * cellSize + cellSize / 2, (cellSize * gridHeight) - (i * cellSize + cellSize / 2));
                    if (dead)
                    {
                        //print(i + " " + j + " dead");
                        nextGenGrid[i, j] = 0;
                    }
                    else
                        if (fertile)
                    {
                        if (livingNeighbours == 2 || livingNeighbours == 3)
                        { // reproduction,  2 to 3 neighgbours
                            nextGenGrid[i, j] = 1;
                        }
                        else if ((livingNeighbours == 1 || livingNeighbours == 2) && cellsArray[i, j] == 1)
                        { // 1 or 2 neigh, the live cell survives
                            nextGenGrid[i, j] = 1;
                        }
                    }
                }
            }
        }
        cellsArray = nextGenGrid; // GOING TO THE NEXT GEN!
    }

    public void UserEnvironment(int x, int y)
    {//fertile zone
        if(x > 5 && x<20 && y > 5 && y < 20)
        {
            for(int i = 5; i <= 20; i++)
                for(int j = 5; j <= 20; j++)
                {
                    cellsArray[j, i] = 1;
                }
        }

        //dead zone
        if (x > 5 && x < 20 && y > 50 && y < 70)
        {
            for (int i = 5; i <= 20; i++)
                for (int j = 50; j <= 70; j++)
                {
                    cellsArray[j, i] = 0;
                }
            //print(" Dead Zone ");
        }

        //mine zone
        if (x > 130 && x < 153 && y > 70 && y < 80)
        {
            for (int i = 130; i <= 153; i++)
                for (int j = 70; j <= 80; j++)
                {
                    cellsArray[j, i] = 0;
                }

            for (int j = 71; j <= 75; j++)
            {
                cellsArray[j, 131] = 1;
                cellsArray[j, 135] = 1;
                cellsArray[j, 137] = 1;
            }
            cellsArray[72, 132] = 1;
            cellsArray[72, 134] = 1;
            cellsArray[73, 133] = 1;

            cellsArray[71, 139] = 1;
            cellsArray[72, 139] = 1;
            cellsArray[73, 139] = 1;
            cellsArray[74, 139] = 1;
            cellsArray[75, 139] = 1;

            cellsArray[72, 140] = 1;
            cellsArray[73, 141] = 1;
            cellsArray[74, 142] = 1;
            cellsArray[75, 143] = 1;
            cellsArray[74, 143] = 1;
            cellsArray[73, 143] = 1;
            cellsArray[72, 143] = 1;
            cellsArray[71, 143] = 1;

            cellsArray[71, 145] = 1;
            cellsArray[72, 145] = 1;
            cellsArray[73, 145] = 1;
            cellsArray[74, 145] = 1;
            cellsArray[75, 145] = 1;

            cellsArray[71, 146] = 1;
            cellsArray[73, 146] = 1;
            cellsArray[75, 146] = 1;

            cellsArray[71, 147] = 1;
            cellsArray[75, 147] = 1;

            cellsArray[73, 149] = 1;
            cellsArray[74, 149] = 1;
            cellsArray[71, 150] = 1;
            cellsArray[72, 150] = 1;
            cellsArray[73, 150] = 1;
            cellsArray[74, 150] = 1;
            cellsArray[75, 150] = 1;
            cellsArray[71, 151] = 1;
            cellsArray[73, 151] = 1;
            cellsArray[74, 151] = 1;
            cellsArray[72, 152] = 1;
        
            print(" MINE Zone ");
        }
    }

    public void Environment()
    {
        if (zones)
        {
            int[,] nextGenGrid = new int[gridHeight, gridWidth];
            nextGenGrid = cellsArray;
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    bool fertile = PosFertile(j * cellSize + cellSize / 2, (cellSize * gridHeight) - (i * cellSize + cellSize / 2));
                    bool dead = PosDead(j * cellSize + cellSize / 2, (cellSize * gridHeight) - (i * cellSize + cellSize / 2));
                    int livingNeighbours = CountLivingNeighbours(i, j);

                    if (livingNeighbours == 3)
                    { // reproduction, exactly 3 neighgbours
                        nextGenGrid[i, j] = 1;
                    }
                    else if (livingNeighbours == 2 && cellsArray[i, j] == 1)
                    { // exactly 2 neigh, the live cell survives
                        nextGenGrid[i, j] = 1;
                    }

                    if (dead)
                    {
                        print(i + " " + j);
                        nextGenGrid[i, j] = 0;
                    }
                    else
                    if (fertile)
                    {
                        if (livingNeighbours == 2 || livingNeighbours == 3)
                        { // reproduction,  2 to 3 neighgbours
                            nextGenGrid[i, j] = 1;
                        }
                        else if ((livingNeighbours == 1 || livingNeighbours == 2) && cellsArray[i, j] == 1)
                        { // 1 or 2 neigh, the live cell survives
                            nextGenGrid[i, j] = 1;
                        }
                    }
                }
            }

            cellsArray = nextGenGrid; // GOING TO THE NEXT GEN!
        }
    }

    bool Control(int x,int y)
    {
        if (x > 0 && x < gridWidth && y > 0 && y < gridHeight) return true; else return false;
    }

    bool PosFertile(float x,float y)
        {

        if (x > fertilePos.x - 3.1 && x < fertilePos.x + 3.1 && y > fertilePos.y - 1 && y < fertilePos.y + 1) {
            //print("fertile"); 
            //print(fertilePos.x + " " + fertilePos.y);
            return true; } else return false;
        }

    bool PosDead(float x,float y)
        {
        if (x > deadPos.x - 3.1 && x < deadPos.x + 3.1 && y > deadPos.y - 1 && y < deadPos.y + 1) { 
            //print(x + " " + y + " "  + deadPos.x  +  " dead"); 
            return true; } else return false;
    }

    bool ControlGod(int x , int y)
    {
        if (cellsArray[x, y] == 2) return true;
        return false;
    }

    bool ControlDiablo(int x,int y)
    {
        if (cellsArray[x, y] == 3) return true;
        return false;
    }

    int CountLivingNeighbours(int i, int j){
        int result = 0;
        for (int iNeigh = i-1; iNeigh < i+2; iNeigh++){ // i-1, i, i+1
            for (int jNeigh = j-1; jNeigh < j+2; jNeigh++){ // j-1, j, j+1
                if (iNeigh == i && jNeigh == j) continue;
                try{
                    result += cellsArray[iNeigh, jNeigh];
                }
                catch{}
            }
        }

        return result;
    }

}

