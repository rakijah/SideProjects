## MultiSteam

![Screenshot](http://i.imgur.com/fPEfJ9A.png)

(project created ca. March 2016)

MultiSteam is an easy to use account switcher for Steam. It uses the [command line parameters](https://developer.valvesoftware.com/wiki/Command_Line_Options#Command-line_parameters_2) that Steam provides to log in automatically. Unfortunately, this method does not support the "Remember me" option and is thus not very optimal to use in combination with the Mobile Authenticator.

The accounts are stored in an encrypted SQLite database. **I am NOT very well versed in cryptology and security, so I can't really comment on how secure the encryption actually is**
**I strongly recommend** to change the default password in Database.cs before compiling.