import { Component, OnInit } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import {MessageDto } from 'src/app/Dto/MessageDto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( (receivedObj: MessageDto) => { this.addToInbox(receivedObj);});  // calls the service method to get the new messages sent
                                                     
  }

  msgDto: MessageDto = new MessageDto();
  msgInboxArray: MessageDto[] = [];

  send(): void {
    if(this.msgDto) {
      if(this.msgDto.fromEmail.length == 0||this.msgDto.toEmail.length == 0 || this.msgDto.message.length == 0){
        window.alert("All Three fields are required.");
        return;
      } else {
        this.chatService.broadcastMessage(this.msgDto);                   // Send the message via a service
        this.msgDto.message = '';
      }
    }
  }

  addToInbox(obj: MessageDto) {
    let newObj = new MessageDto();
    newObj.fromEmail = obj.fromEmail;
    newObj.toEmail = obj.toEmail;
    newObj.message = obj.message;
    this.msgInboxArray.push(newObj);

  }
}

