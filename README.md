# ArticleManagement WebHook Sample

This project is a sample about how to develop and maintain webhook system with a message broker 

## Tech Stack

- .Net 5.0
- Postgresql RDBS
- Rabbit MQ (Message Broker)
- Masstransit
- Redis
- MediatR 
- Publish-Subscribe Pattern
- Domain Driven Design
- CQS
- Docker Container

------



![](https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/webhookArchitecture.PNG)









## Use-Case

This project is an article management system. We can insert article with integration codes and also we can send asynchronously this article data to all integration project.

1. run `docker-compose up` command in "ArticleManagementSystem" folder
2. Create an user in First Journal Api project 
3. Create an user in Second Journal Api project
4. Insert settings about these integration projects in Article Api project.
5. Insert an article data with integration codes in Article Api project and check this article data in integrations projects

I am sharing screenshots about how we can try this webhook system and see results. 

<img src="https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/firstJournalRegister.PNG" style="zoom: 50%;" />  Use "register" to create an user for integrations

  <img src="https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/integrationSettingsGetResponse.PNG" style="zoom: 45%;" />       Insert settings datas about integration projects

 <img src="https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/postArticle.PNG" style="zoom:50%;" />    Insert an article with codes in Article Api

<img src="https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/firstJournalArticle.PNG" style="zoom: 50%;" />              You can see the article data in First Journal Api

<img src="https://github.com/AkinSabriCam/ArticleManagement-WebHook-Sample/blob/main/images/secondJournalArticle.PNG" style="zoom:50%;" />  You can see the article in Second Journal Api



## How Can log in In Integration Projects?

We should use related IdentityServer projects to login in integration projects

First Journal Api Client Id = **fj.api.client**

First Journal Api Client Secret = **fj.api.secret**

Second Journal Api Client Id = **sj.api.client**

Second Journal Api Client Secret = **sj.api.secret**





 

