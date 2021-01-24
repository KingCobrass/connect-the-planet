import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { MessageDto } from '../Dto/MessageDto';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

   private  connection: any = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/connectPlanetChat")   // mapping to the chathub as in startup.cs
                                         .configureLogging(signalR.LogLevel.Information)
                                         .build();
   readonly POST_URL = "https://localhost:5001/api/ConnectChats/direct"

  private receivedMessageObject: MessageDto = new MessageDto();
  private sharedObj = new Subject<MessageDto>();

  constructor(private http: HttpClient) { 
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveSystemMessage", (email: string, message: string) => { this.mapReceivedMessage(email, message); });
   this.start();                 
  }


  // Strart the connection
  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }

  private mapReceivedMessage(user: string, message: string): void {
    this.receivedMessageObject.fromEmail = user;
    this.receivedMessageObject.message = message;
    this.sharedObj.next(this.receivedMessageObject);
 }

  /* ****************************** Public Mehods **************************************** */

  // Calls the controller method
  public broadcastMessage(msgDto: any) {
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0cmluZyBzdHJpbmciLCJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJuYmYiOjE2MTE0Nzc1ODMsImV4cCI6MTYxMTUxMzU4MiwiaWF0IjoxNjExNDc3NTgzfQ.ZaVaG8jNNdFGWKmEQk0Dvyq35CDxxSZK93EkzfJec34";
    
    let header = new HttpHeaders().set(
      "Authorization",`Bearer ${token}`
    );
    this.http.post(this.POST_URL, msgDto, {headers: header}).subscribe(data => console.log(data));
    // this.connection.invoke("SendMessage1", msgDto.user, msgDto.msgText).catch(err => console.error(err));    // This can invoke the server method named as "SendMethod1" directly.
  }

  public retrieveMappedObject(): Observable<MessageDto> {
    return this.sharedObj.asObservable();
  }


}