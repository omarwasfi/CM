# CM
CM API is hosted on https://cm-eg.net/swagger/index.html
System architecture
![DataFlowDiagram-system architecture drawio](https://user-images.githubusercontent.com/31694021/160465458-7c25c1d7-e01b-4598-94d7-b303aa9e7120.png)
# Main Features:
**1- JWT Authentication**
- Login & register with username & Password
- Login &  with PhoneNumber
- Login with Facebook & Google

**2- Profile information**
- Change the username & Password
- change & verify the phoneNumber
- Change & verify the Email
- Upload  profilePictures

**3- Personal Chat & Group chat**

- Ability to send Pictures & text messaging
- using realtime signalR connection.
- Inform the user that the message has been sent.

# Backend
![DataFlowDiagram-Backend drawio](https://user-images.githubusercontent.com/31694021/160465832-0521c055-8a7d-4543-8ebc-eac6369319d1.png)

The api is built using [ASP.NET](http://ASP.NET) core. CM library built using the Mediator Pattern.

The Mediator design pattern defines an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it let us vary their interaction independently.

The following diagram shows how every request to the CM Library get processed.

![DataFlowDiagram-mediator pipeline drawio](https://user-images.githubusercontent.com/31694021/160466082-0f7c1524-b9ec-4509-935a-3d58afa50392.png)

So the mediator pipeline as the digram shows does those main things:

1- Log the request

2- Validate the Request

3- If the request Is not valid the return will be validation Exception and it will be logged.

4- Handing the request.

5- If the request is a command that effect the current state we save it as an event to the events database. So that we can track all the actions and when it did happened. 

Then apply the changes to the currentStateDatabase

6- If the request is a query we get the data from the currentStateDatabase directly

7- log the end time of the request.
