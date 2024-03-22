using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    public InputField generationsInputField;
    public Button simulateButton;
    public Instantiater instantiater;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the simulate button
        simulateButton.onClick.AddListener(SimulateGenerations);
    }

    // Method to stop the program
    public void StopProgram()
    {
        // Add code here to stop the program
        Debug.Log("Stopping the program...");
        Application.Quit(); // This will quit the application
    }

    // Method to simulate generations based on user input
    void SimulateGenerations()
    {
        // Parse the input field text to get the number of generations
        if (int.TryParse(generationsInputField.text, out int numGenerations))
        {
            // Ensure the number of generations is greater than zero
            if (numGenerations > 0)
            {
                // Start the simulation using coroutine
                StartCoroutine(instantiater.StartSimulation(numGenerations));
            }
            else
            {
                Debug.LogWarning("Number of generations must be greater than zero.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid input for number of generations.");
        }
    }
}
