using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    //made public for constructor, maybe make static once all ingredients present
    public enum ingredients {Beef, Onion, Carrot, Celery, Garlic, Potatoes, Peas, Spice}
    //Basic ingredients for 8 servings of beef stew, will add more for multiple recipes
    //Maybe condense some to be veggies or fruits

    private string recipeName;
    private int servingAmount;
    private Dictionary<ingredients, int> requiredIngredients = new(); //Leaving unistantiated for changes later on

    public Recipe(string name, int servings, Dictionary<ingredients, int> ingredientsForRecipe) 
    {
        recipeName = name;
        servingAmount = servings;
        foreach (var ingredient in ingredientsForRecipe) {
            requiredIngredients.Add(ingredient.Key, ingredient.Value);
        }
    }
}
