## StreamCommit

![Screenshot](http://i.imgur.com/aZAi9yL.png)

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