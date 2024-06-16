IDE used for this project: VSCode
.NET version required to compile this project: 8.0

current super user in the database:
username: Sippo
password: 123456
Example survey pincode: 996042

## Start program through executeable.
*The executeable is made for a the x64 chip, if an amd processor is used, a release will need to be build for that, or use *Start Program through VSCode/Terminal**
Double click the 'scivu' executeable file in the zipfolder.
*Debugging hint: if you get permissions denied, you may need to enter "privacy & security" and 'Open Anyway' 

## Start Program through VSCode/terminal
Open terminal at : /SEA/src/scivu/scivu/ \
Enter command 'dotnet run'
A new window will open in which the program is run. 


## Make own executeable for Mac osx-64:
Open /SEA/src/scivu/scivu/ in the terminal \
Write "dotnet publish --configuration Release --runtime osx-x64 --self-contained --output ./publish"
You will get a new 'publish' inside the /SEA/src/scivu/scivu/ folder.\
Inside it there are two important executeables: \
    *backend* which creates the example survey and results\
    *scivu* which runs the app\
The program is dependent on this folder structure to get 'assets' for the example survey and for the database which are both placed inside /src.\
A test survey already exist with the pincode mentioned above, while *backend* can add new surveys of the same type (with new ids) and potentially with new mock results.\

## Use cases:
### Run experiment:
*Do 'Start Program'
There are one survey with two versions in it in the database. \
The survey is loaded by clicking "Experimenter Login"\
Enter the *pin code* mentioned in above.\
*You will reach the start page for taking the survey. There will be some basic statistics as to how many have taken the survey already*\
Click "Start Survey"
*A random version will be selected*\
Take survey, click "next"/"finish" when done with a set of questions\
*After you have clicked finish, you will reach a lock screen.*\
Reenter *pin code* to pick next experimentee or exit to experimenter start screen.

### Create new sample survey
Enter folder "/SEA/src/Model/" in terminal\
run "dotnet run"\
*You will get a prompt if you want to delete the old database*\
click "n" if you want to keep the example survey that this version was cloned with.\
*A new survey will be generated and you will be asked if you want to generate artificial results for it*\
Click 'y' to generate 100 artificial results answering 0-all answers.

### Add SuperUser
Do *Start Program*:\
click "Create Super User"\
add User name and password, both are strings.\
Note: *You cannot create a user that already exist*

### Login to Super User Menu:
Do *Start Program*\
click 'Super User Login'\
enter username and pincode. Default user is in the top of the README

### Modify Survey
This part of the GUI is not working. It is possible to do using the backend commands, the same way that SEA\src\Model\tmp_Moc\CreateExampleSurvey.cs does it.


## Running tests:
Open /SEA/src/Test/ in the terminal\
write "dotnet test"