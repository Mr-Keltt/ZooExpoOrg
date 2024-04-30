Hello. This is my course project for the DSR company in the field .Net

This is a Service for creating and participating in animal shows.

The project has a Database, and the Api is fully implemented. There is a part of the frontend (unfortunately, I did not have time to finish everything I planned)

The project is launched very simply. 
1) You need to open the root folder of the project in the terminal.
2) Write the docker-compose build command (wait for the process to complete)
3) Write the docker-compose up command (wait for the process to complete)
4) Everything. The project is running, you can check :)

The project has a swager on the link http://localhost:10000/docs In fact, it is the whole documentation for using the ZooExpoOrg Api.

By the link http://localhost:10100 The web application interface is located. It's kind of intuitive.

In the env file. the api has 2 lines responsible for auto-adding admin and demodata
Database__Init__AddDemoData=true
Database__Init__AddAdministrator=true
By default, they are in the true state. If you want to disable the generation of demodata, change true to false

I think that's all I wanted to tell you here. I hope you enjoy what I have prepared.
Thank you for your attention, have a nice day!
