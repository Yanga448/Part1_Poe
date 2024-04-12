using System;

namespace RecipeApp
{
    // This class manages the Recipe Application
    class RecipeApp
    {
        private Recipe currentRecipe;

        // Constructor initializes the Recipe object
        public RecipeApp()
        {
            currentRecipe = new Recipe(); // Create a new Recipe instance
        }

        // This method starts the Recipe Application
        public void StartApp()
        {
            Console.WriteLine("Welcome to the Recipe App!"); // Welcome message

            while (true) // Loop until the user decides to exit
            {
                // Adding ingredients
                Console.WriteLine("\nEnter the number of ingredients:");
                if (!int.TryParse(Console.ReadLine(), out int numOfIngredients) || numOfIngredients <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive whole number.");
                    continue;
                }

                for (int i = 0; i < numOfIngredients; i++)
                {
                    Console.WriteLine($"\nEnter details for ingredient {i + 1}:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Quantity: ");
                    if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number.");
                        i--;
                        continue;
                    }
                    Console.Write("Unit of Measurement: ");
                    string unit = Console.ReadLine();

                    currentRecipe.AddIngredient(name, quantity, unit); 
                }

                // Adding steps
                Console.WriteLine("\nEnter the number of steps:");
                if (!int.TryParse(Console.ReadLine(), out int numOfSteps) || numOfSteps <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive whole number.");
                    continue;
                }

                for (int i = 0; i < numOfSteps; i++)
                {
                    Console.WriteLine($"\nEnter step {i + 1}:");
                    string step = Console.ReadLine();
                    currentRecipe.AddStep(step); 
                }

                currentRecipe.DisplayRecipe();

                // Scaling the recipe
                Console.WriteLine("\nDo you want to scale the recipe? (0.5/half, 2/double, 3/triple, no option)");
                string response = Console.ReadLine().ToLower();
                switch (response)
                {
                    case "0.5":
                    case "half":
                        currentRecipe.ScaleRecipe(0.5);
                        Console.WriteLine("\nRecipe  unit measurement scaled to half unit measurement.");
                        currentRecipe.DisplayRecipe();
                        break;
                    case "2":
                    case "double":
                        currentRecipe.ScaleRecipe(2);
                        Console.WriteLine("\nRecipe  unit measurement scaled to double.");
                        currentRecipe.DisplayRecipe();
                        break;
                    case "3":
                    case "triple":
                        currentRecipe.ScaleRecipe(3);
                        Console.WriteLine("\nRecipe  unit measurement scaled to triple.");
                        currentRecipe.DisplayRecipe();
                        break;
                    case "no option":
                        Console.WriteLine("\nRecipe unit measurement scaling skipped.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid response. Recipe scaling stopped.");
                        break;
                }

                // Resetting ingredient quantities
                Console.WriteLine("\nDo you want to reset ingredient quantities? (yes/no)");
                response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    currentRecipe.ResetIngredientQuantities();
                    Console.WriteLine("\nThe ingredient quantities have been reset.");
                    currentRecipe.DisplayRecipe();
                }

                // Clearing recipe data
                Console.WriteLine("\nDo you want to clear the recipe data to enter a new recipe? (yes/no)");
                response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    currentRecipe.ClearRecipe();
                    continue;
                }

                // Prompting to continue or exit
                Console.WriteLine("\nDo you want to exit the application? (yes/no)");
                response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    break;
                }
            }

            Console.WriteLine("Thank you for using the Recipe App!"); // Farewell message
        }
    }

    // This class represents an ingredient in a recipe
    class Ingredient
    {
        public string Name { get; } 
        public double Quantity { get; set; }
        public string Unit { get; } 

        // Constructor initializes the ingredient properties
        public Ingredient(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }

    // This class represents a step in a recipe
    class Step
    {
        public string Description { get; } // Description of the step

        // Constructor initializes the step description
        public Step(string description)
        {
            Description = description;
        }
    }

    // This class represents a recipe
    class Recipe
    {
         Ingredient[] ingredients; // Array to store ingredients
        double [] originalQuantities; // Array to store original quantities of ingredients
         Step[] steps; // Array to store recipe steps
        int ingredientCount; // Counter for the number of ingredients
        int stepCount; // Counter for the number of steps

        // Constructor initializes arrays and counters
        public Recipe()
        {
            ingredients = new Ingredient[10];
            originalQuantities = new double[10];
            steps = new Step[0];
            ingredientCount = 0;
            stepCount = 0;
        }

        // Method to add an ingredient to the recipe
        public void AddIngredient(string name, double quantity, string unit)
        {
            if (ingredientCount == ingredients.Length)
            {
                Console.WriteLine("You can't add more ingredients. The list is full.");
                return;
            }
            ingredients[ingredientCount] = new Ingredient(name, quantity, unit);
            originalQuantities[ingredientCount] = quantity;
            ingredientCount++;
        }

        // Method to add a step to the recipe
        public void AddStep(string stepDescription)
        {
            if (stepCount == steps.Length)
            {
                Console.WriteLine("You can't add more steps. The list is full.");
                return;
            }
            steps[stepCount++] = new Step(stepDescription);
        }

        // Method to display the recipe details
        public void DisplayRecipe()
        {
            Console.WriteLine("\nRecipe:");
            Console.WriteLine("Ingredients:");
            for (int i = 0; i < ingredientCount; i++)
            {
                Console.WriteLine($"{ingredients[i].Name}: {ingredients[i].Quantity} {ingredients[i].Unit}");
            }
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i].Description}");
            }
        }

        // Method to scale the recipe ingredients by a factor
        public void ScaleRecipe(double factor)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient != null)
                {
                    ingredient.Quantity *= factor;
                }
            }
        }

        // Method to reset ingredient quantities to their original values
        public void ResetIngredientQuantities()
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                ingredients[i].Quantity = originalQuantities[i];
            }
        }

        // Method to clear the recipe data
        public void ClearRecipe()
        {
            Array.Clear(ingredients, 0, ingredients.Length);
            Array.Clear(originalQuantities, 0, originalQuantities.Length);
            Array.Clear(steps, 0, steps.Length);
            ingredientCount = 0;
            stepCount = 0;
            Console.WriteLine("\nRecipe cleared. Ready to enter a new recipe!");
        }
    }

    // Main class containing the entry point of the program
    class Program
    {
        // Entry point of the program
        static void Main(string[] args)
        {
            RecipeApp app = new RecipeApp(); // Create a new RecipeApp instance
            app.StartApp(); // Start the Recipe App
        }
    }
}
