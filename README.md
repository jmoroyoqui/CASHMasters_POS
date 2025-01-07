# CASH Masters - POS Change Calculator

This is a practical exercise to implement a routine for calculating the correct change in a point-of-sale (POS) system. The solution meets the requirements provided in the task, focusing on simplicity and clarity.  

## Overview  
The program calculates the optimal change (minimum number of bills and coins) to return to a customer based on the price of the items and the amount they paid. It also ensures the solution can adapt to different currencies by allowing configuration of denominations.  

## Features  
- Calculates the correct change efficiently.  
- Handles different currency denominations (e.g., USD, MXN).  
- Validates input to ensure the provided amount is sufficient.  
- Includes error handling for invalid cases.  
- Unit tests are included for core functionality.  

## How to Run  
1. Clone this repository or download the project files.  
2. Open the solution in Visual Studio.  
3. Run the project to see the console-based demonstration of the routine.  

### Example  
#### Input:  
- Price: `$5.25`  
- Amount provided: `$10.00`  

#### Output:  
- Change returned: `2: $2.000, 1: $0.50, 1: $0.25`  

#### Errors:  
- If the amount provided is less than the price, an error message is displayed.  

## Unit Tests  
Unit tests are included to verify:  
- Correct change calculation.  
- Negative values.

## Author  
This exercise was completed by **Julio Cesar Moroyoqui Gil** as part of a Luxoft technical challenge.  

- Email: moroyoquigil.julio@gmail.com  
- LinkedIn: [linkedin.com/in/juliocesarmoroyoquigil](https://linkedin.com/in/juliocesarmoroyoquigil)  
