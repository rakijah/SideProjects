## OverwatchFinder
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