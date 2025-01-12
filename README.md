
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

5. If you deveopCheck if init.sh file has end of line sequence set to LF.
6. You can read more about running Docker Compose [here](https://learn.microsoft.com/pl-pl/visualstudio/containers/launch-profiles?view=vs-2022/).

## Screenshots

### Hero Page
![mainpage](https://github.com/user-attachments/assets/c1aaff61-4efd-4b86-9398-9329011971ea)
### Course list with filter panel
![search](https://github.com/user-attachments/assets/16e04044-4ebc-4aa7-ad40-e03b08eeda9a)
### Course detail page
![course-detail](https://github.com/user-attachments/assets/bbada29c-b108-4202-a80d-734b45e5efea)
### Registation page
![sign-up](https://github.com/user-attachments/assets/a7a44291-153c-427c-9a5c-d3bb0bd358a7)
### Activate account email
![activation-email](https://github.com/user-attachments/assets/b8ec6b24-b4a2-4819-a30c-f5dfd61f695e)
### Reset password
![resertpassword1](https://github.com/user-attachments/assets/934dad1e-245e-4818-a006-f742fafd3efd)
### Reset password email
![reset-password-email](https://github.com/user-attachments/assets/a114be81-2448-4875-b2f9-e84b352a40b0)
### Profile edit
![profile](https://github.com/user-attachments/assets/c8b1e7e5-5443-44ef-8b5b-2c3c24d539fe)
### Balance page
![balance](https://github.com/user-attachments/assets/b0dd5c16-c342-46f0-9758-9d46cc35538d)
### Cart
![cart](https://github.com/user-attachments/assets/b6a86f2f-232e-4e17-b0aa-7482366d0122)
### Purchased courses
![my-courses](https://github.com/user-attachments/assets/34739cc9-4a9d-4aff-940d-0de94feeb226)
### Add review
![leaving-review](https://github.com/user-attachments/assets/7cbaff84-3441-4623-8d2e-599fd5491030)
### Course walk-through
![course-walk-through](https://github.com/user-attachments/assets/d00b7e00-faf8-4bf6-ba61-0aef6b2542bc)
![walk-through-film-certificate](https://github.com/user-attachments/assets/89a36384-928c-4574-a369-98623ac536b4)
### Comments
![comments](https://github.com/user-attachments/assets/12a86328-1541-4582-be14-016983452203)
### Chat
![chat-2](https://github.com/user-attachments/assets/7900eff3-3ab2-4118-ad32-1e3ddea754a4)
![chat-1](https://github.com/user-attachments/assets/104a2729-0463-4a42-a918-9dd61da090eb)
### Become an instructor
![become-instructor](https://github.com/user-attachments/assets/bf86d597-bd1f-4388-a213-6cceae3a8969)
### Create new course
![newcourse-1](https://github.com/user-attachments/assets/ac27ab85-4583-477a-83e8-8b47c7ed6a02)
### Course essentials
![essentials](https://github.com/user-attachments/assets/acda2b48-735a-416a-bbe8-68059a89a3cc)
### Course creator
![element](https://github.com/user-attachments/assets/12babd17-8d3b-4b9b-87be-17ebf8c2ae27)
### Quiz creator
![quiz](https://github.com/user-attachments/assets/5a88a0b4-9526-452f-8bb8-09b2b26b8a0e)
### Questions creator
![questions](https://github.com/user-attachments/assets/1439ec8c-12da-45a1-8ad9-2fbcd7e52b8a)
### Fill the gap creator
![fillTheGap](https://github.com/user-attachments/assets/7140faeb-e45a-4cdb-a03e-ff117bcf5540)
### Discount codes
![disocunt-codes](https://github.com/user-attachments/assets/e6aece10-7dd8-4958-87b7-40c43477ce14)
### Notifications
![notifications](https://github.com/user-attachments/assets/049f7755-c798-491d-8151-6389c4bca58a)
### Performance page
![revenues](https://github.com/user-attachments/assets/56247461-fc23-4d01-a4bb-8e6ae393b9ff)
### Moderator review
![addReviewComment](https://github.com/user-attachments/assets/87621d86-01a3-426c-97c6-9ecdcfbdc308)
![comment-resolve](https://github.com/user-attachments/assets/7b86a913-e898-4528-9cbe-5ee00d1d320d)





