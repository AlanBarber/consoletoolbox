ConsoleToolBox 0.1
==================

ConsoleToolBox is an open source c# library that provides a set of useful 
tools for building .net console applications.

The goal of this project is to create a .net library that provides a similar
feature set to the Ncurses C library. 

License
=======

This toolbox is licensed under the MIT License. See the file "LICENSE" 
for more information.

Features
========

The ConsoleToolBox contains a group of tools that can be very useful for 
building rich and robust console applications.

The following tools are currently included or in progress:

ConsoleToolBox.Common
 * ProgressBar - An ascii representation of a progress bar useful for showing
                 how far along a task is running such as processing a large
                 set of data.
 * ProgressSpinner - An simple one character display for showing activity. It 
                     is useful to visually display to a user that work is 
                     being done in cases where a ProgressBar would not work 
                     as you are unable to calculate when you are 100% done.

ConsoleToolBox.Containers
 * Panel - The Panel provides an abstracted console frame in which you can 
           write text to and display on the screen. It includes the ability 
           to display the panel at any absolute position on the screen and 
           upon removal of the Panel have the previous contents of the 
           screen shown.
 
ConsoleToolBox.Dialogs
 * MessageBox - A text version of the often used .net MessageBox. It allows 
                you to display a title and message and wait for user input 
                to select a response.
 * OpenFileDialog - A text version of the .net OpenFileDialog tool.
 * SaveFileDialog - A text version of the .net SaveFileDialog tool.
 
Requirements
============
 
 * Microsoft .Net 4.0 
 * Windows XP SP2, Windows Vista, Windows 7, Windows 8, Windows Server 2003 R2, Windows Server 2008 R2
 * Microsoft Visual Studio 2010 (to build)
 
 3rd Party Libraries
 
 * Apache log4net - http://logging.apache.org/log4net/
 
 
 
 
