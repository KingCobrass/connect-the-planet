import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { MessageDto } from '../Dto/MessageDto';
import { Guid } from 'guid-typescript';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

   private  connection: any = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/connectPlanetChats")   // mapping to the chathub as in startup.cs
                                         .configureLogging(signalR.LogLevel.Information)
                                         .build();
   readonly POST_URL = "https://localhost:5001/api/ConnectChats/direct"

  private receivedMessageObject: MessageDto = new MessageDto();
  private sharedObj = new Subject<MessageDto>();

  constructor(private http: HttpClient) { 
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveGroupMessage", (email: string, message: string) => { this.mapReceivedMessage(email, message); });
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
    this.receivedMessageObject.userName = user;
    this.receivedMessageObject.message = message;
    this.sharedObj.next(this.receivedMessageObject);
 }

  /* ****************************** Public Mehods **************************************** */

  // Calls the controller method
  public broadcastMessage(msgDto: any) {
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1haGkgVWRkaW4iLCJlbWFpbCI6Im1haGlAZ21haWwuY29tIiwibmJmIjoxNjExNTExNTM3LCJleHAiOjE2NDMwNDc1MzYsImlhdCI6MTYxMTUxMTUzN30._PgXsqwGkpfGzAD9RBbW247dzpOl1sF_wcZA0gAunxU";
  
    msgDto.groupId = Guid.create().toString();
    msgDto.userId = Guid.create().toString();
    msgDto.toConnectionId = Guid.create().toString();
    let header = new HttpHeaders().set(
      "Authorization",`Bearer ${token}`
    );
    this.http.post(this.POST_URL, msgDto, {headers: header}).subscribe(data => console.log(data));
  }

  public retrieveMappedObject(): Observable<MessageDto> {
    return this.sharedObj.asObservable();
  }


}