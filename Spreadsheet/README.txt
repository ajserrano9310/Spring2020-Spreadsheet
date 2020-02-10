Author:     Alejandro Rubio
Partner:    None
Date:       2/9/2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment 4 - Onward to a Spreadsheet
Copyright:  CS 3500 and Alejandro Rubio - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

All the Projects include copies of the READMEs and the Solution README has been updated
Code coverage from tests for the Spreadsheet class is 100%

2. Assignment Specific Topics

I think the project will take me 10 hours.

It took me about 10 hours to finish the project.

Hours Estimated/Worked         Assignment                       Note
       8-10   /    12    - Assignment 1 - Formula Evaluator     Spent 2 extra hours debugging code
      10-12   /    10    - Assignment 2 - Dependency Graph      I tought the assignment was going to be more demanding
        12    /    14    - Assignment 3 - Formula               Spent 2 extra hours making sure every expression error was checked
        10    /    10    - Assignment 4 - Onward to a Spreadsheet

3. Consulted Peers:

Alejandro Serrano

4. References:

	1. https://stackoverflow.com/questions/494224/how-do-you-determine-if-two-hashsets-are-equal-by-value-not-by-reference

5. Examples of Good Software Practice:

Code re-use: My library uses other libraries which some are made by me like the DependencyGraph and others that are external libraries like the method called SetEquals which compares if two sets are equal with respect to their values and not references.
Testing strategies: My code uses a very good strategy to prove that the code is correct. This is based on a series of tests that test the functionality of each method. In addition, they test extreme cases which can crash the program which would be bad if an user sees that. Also, the test code coverage of the program is 100%.
DRY: My code uses private methods such as the formatValidator which is responsible for verifying if the parameter that enters as the name of the cell has a valid name or not.