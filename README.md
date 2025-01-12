
# Skillup

Skillup is a web application developed using .NET and Agular frameworks. It was developed as final university project. Skillup is an e-learing platform that allows users for creating courses with articles, movies and exercises. After buying certain course you gain access to those added elements, chat with course's author and possibility to comment and leave a review.

## Architecture & technologies
### Backend
Backend site of this project is REST API, developed following modular monolith architecture principles. Each module is independed and uses adequate internal architecture. In order to enhance code organization Mediator pattern was used along with CQRS and Repository pattern. Communication tween modules is handled by RabbitMQ. Chat between users was implemeted with help of SignalR library, which allows real-time communication. Files are stored on Amazon Web Service which was also used for running application in a cloud environment. FluentEmail library was used to handle sending email messages. QuestPDF was used for generating pdf course certificates. During development we also used Localstack for emulating AWS and Papercut for testing email module. 
### Frontend
Client-site application uses components provided by PrimeNG library and Tailwind CSS framework which provides CSS classes. Drag and drop functionalities were developed using Angular CDK Drag and Drop.

## Features
- Available for everyone:
    - Two-stage user registration with activation email.
    - Possibility to set new password in case of forgetting password to an existing account.
    - Page with all listed courses and a filtering panel.
    - Course page with information about course, content, target group.
    - Cart with possibility to apply a discount code.
    - Balance page with your balance history, your current balance and possibility to transfer more funds.
    - Course walk through page accessible after buying where students can view videos, articles, complete exercises and leave comments. 
    - Chat tab with conversations with authors of courses you purchased.
    - Notifications tab.
    - Possibility to download certificate of accomplishment after completing a course.
    - Become and instructor page.
- Only for instructors:
    - Creating new course, adding its details, setting price, creating course content uploading files, creating exercises.
    - Performace page with information about your earnings, containing pie and line charts comparing earnings from different courses.
    - Discount codes page with list of your discount codes and possibility to add new ones.
- Only for moderator:
    - Reviews page with list of courses waiting for review, possiblity to start new review, add comments to course and request changes or publish a course.


## Run

In order to run the application you should follow these steps:
1. Create .env file using .env.example. You should determine whether you will be running appliaction in production or development mode and set variables according to your settings.

### Development
2. Run docker with command.
```bash
  docker-compose --profile dev up -d
```

### Production
2. Update sections Client, AmazonS3 and Cors in appsettings.json file according to your configuration.
3. Update your Smtp server data in module.mails.json file.
4. Run docker with command. 
```bash
  docker-compose --profile prod up -d
```

### Visual Studio
If you are running the app in Visual Studio you should:
1. Set startup project to Docker Compose.
![image](https://github.com/user-attachments/assets/d8270f34-83c9-4b59-aa2d-f1504c0443b4)

3. Set Launch Settings accordingly.
![image](https://github.com/user-attachments/assets/c603c29f-f7e3-4a00-af66-9e54586bb94e)

4. If you deveopCheck if init.sh file has end of line sequence set to LF.
5. You can read more about running Docker Compose [here](https://learn.microsoft.com/pl-pl/visualstudio/containers/launch-profiles?view=vs-2022/).

## Screenshots

### Hero Page
![mainpage](https://github.com/user-attachments/assets/c1aaff61-4efd-4b86-9398-9329011971ea)





