using System.Collections;
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

    // Method to start the simulation
    // Method to start the simulation
    void SimulateGenerations()
    {
        // Parse the input field text to get the number of generations
        if (int.TryParse(generationsInputField.text, out int numGenerations))
        {
            // Ensure the number of generations is greater than zero
            if (numGenerations > 0)
            {
                // Start the simulation using coroutine
                StartCoroutine(StartSimulation(numGenerations));
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

    // Coroutine to start the simulation for a specified number of generations
    IEnumerator StartSimulation(int numGenerations)
    {
        // Start the program
        instantiater.StartProgram();

        // Wait for a short time to allow initialization
        yield return new WaitForSeconds(0.1f);

        // Fill random cells for each generation
        for (int generation = 0; generation < numGenerations; generation++)
        {
            // Fill random cells
            instantiater.FillRandomCells();

            // Apply rules for each generation
            instantiater.ApplyRules();

            // Render the cells for visualization
            instantiater.RenderCells();

            instantiater.Environment();

            int sampleX = 10;
            int sampleY = 20;
            instantiater.UserEnvironment(sampleX, sampleY);

            // Optional: Delay between generations for visualization purposes
            yield return new WaitForSeconds(instantiater.generationInterval);
        }

        // Stop generating cells after simulating the specified number of generations
        Instantiater.pause = true;
    }

}
