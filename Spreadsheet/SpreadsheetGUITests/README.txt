Author:     Alejandro Rubio
Partner:    Alejandro Serrano
Date:       2/28/2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment: Assignment 6 - Spreadsheet Front-End Graphical User Interface
Copyright:  CS 3500, Alejandro Rubio and Alejandro Serrano - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

All the Projects include copies of the READMEs and the Solution README has been updated

2. Assignment Specific Topics

We believe the Assignment will take around 13 hours to complete. 
It took us about 15 hours to finish the project.

Alejandro Rubio: 7.5 hours.
Alejandro Serrano: 7.5 hours. 
We worked together every session. Very minimal work was done outside of the Cade Lab.

Unlike the other assignmnets (2-5) where most of the tools we needed we already had learned, 
we didn't take into account the time of doing the research for the GUI. Not
only that, but also creating things on Framework and then passing them to Core took special 
effort. Regardless of this, after some research for our objectives, most of the writing was fast,
espcially when one of us got stuck and the other was there to help. 

Hours Estimated/Worked         Assignment                       Note
       8-10   /    12    - Assignment 1 - Formula Evaluator     Spent 2 extra hours debugging code
      10-12   /    10    - Assignment 2 - Dependency Graph      I tought the assignment was going to be more demanding
        12    /    14    - Assignment 3 - Formula               Spent 2 extra hours making sure every expression error was checked
        10    /    10    - Assignment 4 - Onward to a Spreadsheet
        14    /    15    - Assignment 5 - Spreadsheet Model     Spent 1 more hour doing the whiteboard
        13    /    15    - Assignment 6 - Spreadsheet GUI       Spent 2 hours on the feature

3. Consulted Peers:
 - None. The instructions for the Assignment were clear, and the questions we had 
 we addressed them to the TA(s). 

4. References:

    1. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox?view=netframework-4.8
    2. https://stackoverflow.com - https://stackoverflow.com/questions/11518529/how-to-call-a-button-click-event-from-another-method
    3. https://stackoverflow.com - https://stackoverflow.com/questions/3172731/forms-not-responding-to-keydown-events

5. Good Team Practices:

    - Everytime we decided who would do what, we created the appropriate branch to keep work separate. Purposely we
      worked on things that would not conflict with each other to make the merging process less complicated. This worked
      more often than not. 
    - When creating the feature, instead of coding right away we took 20 minutes to understand how exactly we would
      implement said feature. We drew some images, talked about data structures, efficiency and functionallity, and then we
      started implementing it. This quickened the implementation. 

    - Something we should have stayed consistent with was mentioned above. Even though the branching was consistent, instead of
      sticking to one task, in some branches we did other work that complicated the merging process. 

6. GitHub Repo and Commit Number:

7a730ca04eabe3ebae97c50e1b13da1f4624ff8b
https://github.com/uofu-cs3500-spring20/assignment-six-completed-spreadsheet-teamalejandros 


7. Branching:

A total of 16 branches were created for the Assignment. 

    - A5Fix (Alejandro Rubio): Fixing A5 mistakes
    - A6TextBoxLink (Alejandro Serrano): Linking the textbox to the cells
    - A6LinkingFormula (Alejandro Rubio): Linking spreadsheet and formula to Spreadsheet
    - A6KeyBinding (Alejandro Serrano): Binding WASD to move around the cells
    - A6Save (Alejandro Serrano): Creating save functionallity for the spreadsheet
    - A6LoadXML (Alejandro Rubio): Creating load functionality for the spreadsheet
    - A6LoadSave (Alejandro Rubio): Branch that was created to merge both save and load 
    - A6KeyBindingFix (Alejandro Serrano): Small fixes to the keybinding 
    - A6CellInfo (Alejandro Serrano): Creating textboxes with the info of each cell and the name of the cell 
    - A6SaveAndSaveAs (Alejandro Rubio): Creating the functionallity to keep saving on the same file or creating a new one. 
    - A6SaveAndCellInfo (Alejandro Serrano): Branch that was created to merge both A6SaveAndSaveAs and A6CellInfo
    - A6UndoAndBGWorker (Alejandro Rubio): Creating the undo feature and the addition of the Background Worker
    - A6TryCatchForFormulaFormat (Alejandro Serrano): Addition of try and catch for the FormulaFormatError and CircularExpression, 
      addition of Help MessageBox and README.
    - A6TryCatchAndBGWorker: Branch to merge the try catch errors and Background Worker. 
    - A6Testing (Alejandro Rubio): Some testing methods for the GUI.
    - A6FinalMerge: Final branch to merge everything together. 
    - A total of 20 commits were made. 

    The merging process the first time was complicated since we didn't know how to resolve the conflicts. We also
    did not know we had to accept the merge in order for it to happen. 
    The second time went better. We were more careful when joining the code into master and went smoothly. 
    The third time, everything was under control. 

8. Additional Features and Design Decisions

    - For the additional feature we decided to create an Undo button that would allow 
    the user to return to the previous state the cell was in. It is not as sophisticated as
    the regular Undo which can delete letter for letter, but it deletes the entire content
    of the cell. For this, we used two stacks: one that holds the cell names and another for the
    cell contents. 
    - The reason why we decided to use WASD for movement instead of the arrow keys is because the arrow keys
    were already been used for moving the scroll bars. 
