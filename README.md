# Side Projects
This repository contains various side-projects of mine that have *not* been polished enough to be published as their own repositories. Most of these programs were created for my own personal use and weren't intended to be released at all.

Please note that the quality of code is going to be significantly lower than my actual projects.

## Table of Contents  
[MultiSteam](#multisteam)  
[OverwatchFinder](#overwatchfinder)  
[QuickViewCSharp](#quickviewcsharp)  
[Crosswalk](#crosswalk)  
[CSGOSquadButton](#csgosquadbutton)  

<br>

## [MultiSteam](https://github.com/rakijah/SideProjects/tree/master/MultiSteam)
![Screenshot](http://i.imgur.com/fPEfJ9A.png)

(project created ca. March 2016)

MultiSteam is an easy to use account switcher for Steam. It uses the [command line parameters](https://developer.valvesoftware.com/wiki/Command_Line_Options#Command-line_parameters_2) that Steam provides to log in automatically. Unfortunately, this method does not support the "Remember me" option and is thus not very optimal to use in combination with the Mobile Authenticator.

The accounts are stored in an encrypted SQLite database. **I am NOT very well versed in cryptology and security, so I can't really comment on how secure the encryption actually is**
**I strongly recommend** to change the default password in Database.cs before compiling.

## [OverwatchFinder](https://github.com/rakijah/SideProjects/tree/master/OverwatchFinder)
![Screenshot](http://i.imgur.com/SNCD9Vh.png)

(project created ca. April 2017)

A fully automatic tool to find the profile of The Suspect in a CSGO Overwatch case.
This is an extension of my project [OWDemo](https://github.com/rakijah/OWDemo). To use, compile and follow OWDemos install instructions, but additionally place [this config](https://github.com/rakijah/SideProjects/tree/master/OverwatchFinder/cfg/gamestate_integration_overwatch.cfg) in your cfg folder to enable Game State Integration.

New features:
* Remember last used network device
* Automatically downloads the demo once the URL has been found
* Uses [demoinfo](https://github.com/StatsHelix/demoinfo) and Game State Integration (utilising my own library, [CSGSI](https://github.com/rakijah/CSGSI)) to find The Suspect's profile
* Outputs the found suspects to an output file
* Required .dlls are stored in a separate *lib* folder using [PrettyBin](https://github.com/slmjy/PrettyBin)

This version of OWDemo is not as reliable as the old one and may be prone to crashing or might falsely identify The Suspect, which is why it wasn't committed to the master branch and rather uploaded as a standalone project.

## [QuickViewCSharp](https://github.com/rakijah/SideProjects/tree/master/QuickViewCSharp)

(project created ca. March 2014)

A minimalistic image viewer that supports animated .gifs and transparency.
One of my earliest projects and thus one of my worst in terms of code quality. 
To open an image, either drag it onto the executable or use the "Open with..." dialog. Once the program is started, press CTRL + H for help.

## [Crosswalk](https://github.com/rakijah/SideProjects/tree/master/Crosswalk)

![Screenshot](http://i.imgur.com/8bMCoUc.png)

(project created ca. April 2016)

This project was created as a joke after our software development teacher gave us an exercise with a *very* brief description: "Create a crosswalk. Cars should obey traffic law. At least one car has to turn right at the intersection. Usage of external libraries not allowed". That was it. We weren't told whether this was supposed to be a console application / text simulation, a graphical representation or even a game. Me and a few classmates decided to make this application as a joke.
After I finished the basic requirements I began expanding this to be a small game engine, which is why I decided to upload it in the first place.
Since no external libraries were permitted, System.Drawing/GDI+ was my choice for the graphics.
Define the debug flag "INFO" for debug information (FPS, amount of entities etc.) or "TEST" to enable drawing colliders and paths to the screen.

## [CSGOSquadButton](https://github.com/rakijah/SideProjects/tree/master/CSGOSquadButton)

![Screenshot](http://i.imgur.com/MlOSjS9.png)

(project created ca. 2016)

A small Tampermonkey script that adds a CS:GO Squad button to every Steam profile to quickly search for the player on csgosquad.com.

## [StreamCommit](https://github.com/rakijah/SideProjects/tree/master/StreamCommit)

![Screenshot](http://i.imgur.com/puRKGHu.png)

(project created ca. August 2017)

An experimental utility that is supposed to help programming streamers better interact with their viewers. It periodically pushes git commits every X seconds (configurable) of every file that has changed within a Git repository, so viewers can see the changes they make to their code live on Github.

In my experience, it can sometimes be hard to track exactly what a streamer is doing, because they switch between files/scroll through documents too fast for their viewers. With this utility, a viewer can better understand the streamers code architecture and provide better help.

I strongly recommend you create a separate Git account for use with this application, since it will "spam" commits and stores your git credentials as plain text in the App.config (as mentioned earlier, this application is experimental).

### Usage ###

* Create a new Git repository

* `git clone` it to a local directory

* Open StreamCommit

* Click on `Credentials...` and enter your Git account credentials. The email is only used as data for the commit, so you can enter whatever you want here.

* Click on `...` in the main window to set the repository directory.

* Click `Start` to start the commit cycle.

* Provide your git repository URL to your viewers