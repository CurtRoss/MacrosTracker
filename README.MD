# Operation Nutrition Ninja

Operation Nutrition Ninja is a final project for Eleven Fifty Academy. The goal was to make a n-tier application using single login users and three custom tables within a database. Operation Nutrition Ninja utilizes a food table, meal table, and a junction table to allow users to track the protein, fat, carbs, and calories in the foods they are consuming.

## Installation
To install, please clone or download the repository from [GitHub]( https://github.com/CurtRoss/MacrosTracker).


## Usage

Open the repository in Visual Studio Community. An API testing tool will also be required.

Set MacrosTracker.WebAPI as the startup project and run the program. Upon running the application, your internet browser will launch and bring you to the ASP.net landing page. From here, select API in the black menu bar at the top of the page. This will bring you to the ASP.NET Web API Help Page where the endpoints within the program are listed. These endpoints may be used with an API testing tool to use this application.

### Registration

Under Account, select POST api/Account/Register. In addition to an email address and password (6-100 characters), a user will need to provide their height in inches, weight, gender, and date of birth.

A bearer authorization token will be needed by your API testing tool to simulate a logged in user. After registration is completed, remove the api/Account/Register from the URL in the API testing tool and replace with /token. In the body of the request, enter username, password, and grant_type (password). The bearer token created must be used in all subsequent requests for this user.

### FoodItem
FoodItem is the basic piece of data used in the application. FoodItems must be created before the rest of the application will be useful. 


#### Entering a FoodItem

From the Web API Help Page, select Post api/FoodItem from the FoodItem section. Enter the name of the food (2-100 characters), the amount (we recommend entering number of servings servings), grams of protein, grams of fat, and total carbs for the food being entered into the API testing tool. The application will calculate the calories for you.

#### Edit a FoodItem

From the Web API Help Page select PUT api/FoodItem from the FoodItem section.  Enter the Id Number of the food to be edited (see Get FoodItem below to obtain the Id Number). In the API testing tool, enter the new information for the food, including FoodName, Amount, Protein, Fat and Carbs.  The application will calculate the calories for you.

#### View All FoodItems

From the Web API Help Page select GET api/FoodItem from the FoodItem section.  Using this endpoint in the API testing tool will return all of the foods for the logged in user. The list will contain the FoodId, the FoodName, the date the food was created in the application, and the date it was last modified.

#### View Food By ID

From the Web API Help Page select GET api/FoodItem/{id} from the FoodItem section.  Using this endpoint in the API testing tool, change {id} to the FoodId for the food item you would like to view. The will bring up the details of the selected FoodItem, including the FoodId, FoodName, Amount, Protein, Fat, Carbs, Carlories, Created date, and Modified date. If the FoodItem has been entered to any Meals (see below), a list of Meal names will also display.

#### Delete a Food

From the Web API Help Page select DELETE api/FoodItem/{id} from the FoodItem section.  Using this endpoint in the API testing tool, change {id} to the FoodId for the food item you would like to delete. 

### Meals

Meals combine FoodItems as a group that can be used again and again in the application.

#### Entering a Meal

Meals combine FoodItems as a group that can be used again and again in the application. From the Web API Help Page, select Post api/Meal from the Meal section. Enter the name of the meal (maximum length of 50 characters) and a list of the FoodItem Ids comprising the meal into the API testing tool. The application will calculate the protein, fat, carbs, and calories for the Meal.

#### Edit a Meal

From the Web API Help Page select PUT api/Meal from the Meal section.  Enter the Id Number of the Meal to be edited (see Get Meals below to obtain the Id Number). In the API testing tool, enter the new MealName and list of food Ids.  

#### View All Meals

From the Web API Help Page select GET api/GetMeal from the Meal section.  Using this endpoint in the API testing tool will return all of the Meals for the logged in user. The list will contain the MealId, the MealName, and the date the meal was created in the application.

#### View Meal By ID

From the Web API Help Page select GET api/Meal/{id} from the FoodItem section.  Using this endpoint in the API testing tool, change {id} to the MealId for the meal you would like to view. The will bring up the details of the selected Meal, including the MealId, MealName, Protein, Fat, Carbs, Calories, List of Food Names, Created date, and Modified date.

#### Delete a Meal

From the Web API Help Page select DELETE api/Meal/{id} from the Meal section.  Using this endpoint in the API testing tool, change {id} to the MealId for the meal you would like to delete. 


## Contributing
Authored by Charles Lucas, Curt Reigelsperger, and Lindsey Robertson.

## License
