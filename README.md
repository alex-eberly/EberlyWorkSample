# Multi-Value Dictionary Application Work Sample

## Overview

This command line application stores and handles data manipulation for a multi-value dictionary. It can handle a variety of commands based on user input, including the following:
* KEYS: Prints out a list of all keys in the dictionary
* MEMBERS KeyName: Prints out a list of all members of the specified key
* ADD KeyName ValueName: Adds the key if it does not exist, and adds the member to the key
* REMOVE KeyName ValueName: Removes the member from the key, and removes the key if no members are left
* REMOVEALL KeyName: Removes all members from the specified key and deletes the key
* CLEAR: Removes all keys and members from the dictionary
* KEYEXISTS KeyName: Prints True if the key exists in the dictionary, False if it does not
* MEMBEREXISTS KeyName MemberName: Prints true if the specified member exists for the key, False if the key or member do not exist
* ALLMEMBERS: Prints out a list of all members in the dictionary without the names of the keys
* ITEMS: Prints out a list of all key-value pairs in the dictionary
* MULTIADD KeyName ValueName1 ValueName2: Adds the key if it does not exist, and adds the members to the key
* MULTIREMOVE KeyName ValueName1 ValueName2: Removes the members from the key, and removes the key if no values are left");
* MULTIREMOVEALL KeyName1 KeyName2: Removes all members from the specified keys and deletes the keys
* HELP: Get a list of available commands to use
* QUIT: Quit the application

Within the repository, you will find a solution with two projects that contain the following files:
* EberlyWorkSample -- This project contains the command line application
  * Program.cs -- This is the "jumping off point" for the application
  * Commands
    * Commands.cs -- This stores the commands assigned in the work sample
    * ExtraCreditCommands.cs -- This stores a handful of commands that I felt would be useful to add to the application
* EberlyWorkSampleTests -- This project contains the unit tests for the application
  * CommandTests -- These unit tests ensure that the assigned methods work as intended
  * ExtraCreditCommandTests -- These unit tests ensure that the methods that I felt helpful to add to the application work as intended

## System Requirements
This command line application was developed in Visual Studio 2019. While it is not necessary to use Visual Studio to run and test the code, it will likely be the easiest method. You may read the system requirements for Visual Studio 2019 at this link: https://docs.microsoft.com/en-us/visualstudio/releases/2019/system-requirements

## Getting Started
The simplest way to download the repository is to go to the Code tab for the main branch (only branch) and select the green Code dropdown at the top right of the view. Here, you may choose to copy the git link, open with Git desktop, or download the repository as a .zip folder. I recommend downloading the file as a compressed .zip folder and unzipping the folder wherever you would like to store the repository on your local machine.

Once the repository is on your local machine, in Visual Studio, open the EberlyWorkSample.sln solution file. Both the EberlyWorkSample and EberlyWorkSampleTests projects should appear in the Solution Explorer. To begin running the application in Debug mode, right-click the EberlyWorkSample project and select Debug > Start New Instance.

Once the application is running, it may be helpful to first run the HELP command to conveniently view a list of all available commands. You may execute the commands by typing the name of the command with any parameters separated by spaces, then hitting the ENTER key. If an error message appears, ensure that the command is spelled correctly, and that the correct parameters are entered.

To run the unit tests for the application, in Visual Studio, drop-down Test at the top and open the Test Explorer. The tests in EberlyWorkSampleTests should appear in the Test Explorer. You may choose to run all tests at the top, or expand the directory to select a specific test to run. I have provided 13 tests with the solution, one for each command other than HELP and QUIT.
