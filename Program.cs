using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeEntry2
{


    class Program
    {

        // List to store multiple recipes
        static List<Recipe> recipes = new List<Recipe>();

        static void Main()
        {
            Console.WriteLine("Welcome to the Recipe Entry System!");

            // Main loop for user interaction
            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. View all recipes");
                Console.WriteLine("3. View a specific recipe");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                // Handling user's choice
                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        DisplayRecipeList();
                        break;
                    case "3":
                        ViewRecipe();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        return; // Exit program
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        // Method to add a new recipe
        static void AddRecipe()
        {
            Console.Write("\nEnter the recipe name: ");
            string recipeName = Console.ReadLine();

            Console.Write("Enter the number of ingredients: ");
            int ingredientCount = int.Parse(Console.ReadLine());

            List<Ingredient> ingredients = new List<Ingredient>();

            // Loop to collect ingredient details
            for (int i = 0; i < ingredientCount; i++)
            {
                Console.WriteLine($"\nEnter details for ingredient {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit of measurement: ");
                string unit = Console.ReadLine();

                Console.Write("Calories: ");
                int calories = int.Parse(Console.ReadLine());

                Console.Write("Food Group (e.g., Dairy, Protein, Vegetable): ");
                string foodGroup = Console.ReadLine();

                // Add ingredient to the list
                ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            }

            Console.Write("\nEnter the number of steps: ");
            int stepCount = int.Parse(Console.ReadLine());

            List<string> steps = new List<string>();

            // Loop to collect preparation steps
            for (int i = 0; i < stepCount; i++)
            {
                Console.Write($"Step {i + 1}: ");
                steps.Add(Console.ReadLine());
            }

            // Create a new Recipe object and add it to the list
            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            recipes.Add(recipe);

            Console.WriteLine("\nRecipe added successfully!");
        }

        // Method to display a list of all recipes in alphabetical order
        static void DisplayRecipeList()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            Console.WriteLine("\nRecipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name)) // Sorting recipes alphabetically
            {
                Console.WriteLine($"- {recipe.Name}");
            }
        }

        // Method to view details of a selected recipe
        static void ViewRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            // Display the recipe list
            DisplayRecipeList();

            Console.Write("\nEnter the recipe name you want to view: ");
            string selectedRecipe = Console.ReadLine();

            // Find the selected recipe in the list
            Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(selectedRecipe, StringComparison.OrdinalIgnoreCase));

            if (recipe == null)
            {
                Console.WriteLine("Recipe not found!");
                return;
            }

            // Display the recipe details
            recipe.Display();
        }
    }

    // Ingredient class to store details of each ingredient
    class Ingredient
    {
        public string Name { get; } // Name of the ingredient
        public double Quantity { get; } // Quantity of the ingredient
        public string Unit { get; } // Unit of measurement (e.g., grams, tablespoons)
        public int Calories { get; } // Caloric value of the ingredient
        public string FoodGroup { get; } // Category (e.g., Protein, Dairy)

        // Constructor to initialize an ingredient
        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    // Recipe class to store and display recipe details
    class Recipe
    {
        public string Name { get; } // Name of the recipe
        private List<Ingredient> Ingredients { get; } // List of ingredients
        private List<string> Steps { get; } // List of preparation steps

        // Constructor to initialize a recipe
        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        // Method to display the recipe details
        public void Display()
        {
            Console.WriteLine($"\nRecipe: {Name}");
            Console.WriteLine("\nIngredients:");

            int totalCalories = 0;

            // Display ingredients and calculate total calories
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.FoodGroup}) - {ingredient.Calories} calories");
                totalCalories += ingredient.Calories;
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }

            // Display total calorie count
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            // Warning message if total calories exceed 300
            if (totalCalories > 300)
            {
                Console.WriteLine("⚠️ Warning: This recipe exceeds 300 calories!");
            }
        }

    }
}
