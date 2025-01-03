import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  input,
  OnChanges,
  signal,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ChatService } from '../../../services/chat.service';
import { AuthService } from '../../../../auth/services/auth.service';
import { FormsModule } from '@angular/forms';
import { Chat } from '../../../models/chat.model';
import { Message } from '../../../models/message.model';
import { UserService } from '../../../../user/services/user.service';
import { User } from '../../../../user/models/user.model';
import { MessageItemComponent } from './message-item/message-item.component';
import { InputTextModule } from 'primeng/inputtext';
import { UserRole } from '../../../../user/models/user-role.model';

@Component({
  selector: 'app-chat-window',
  standalone: true,
  imports: [FormsModule, MessageItemComponent, InputTextModule],
  templateUrl: './chat-window.component.html',
  styleUrl: './chat-window.component.css',
})
export class ChatWindowComponent implements OnChanges {
  chat = input.required<Chat | null>();
  @ViewChild('scrollableDiv') scrollableDiv!: ElementRef;
  
  //Services
  userService = inject(UserService);
  authService = inject(AuthService);

  //Variables
  currentUser = this.userService.currentUser;
  talker = signal<User | null>(null);
  messages = signal<Message[]>([]);

  currentMessage: string = '';

  constructor(private chatService: ChatService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['chat']) {
      if (this.chat()) {
        this.chatService.fetchChatHistory(this.chat()!.id).subscribe((res) => {
          this.messages.set(res);
        });

        this.chatService.startConnection(
          this.authService.getAccessToken()!,
          this.chat()!.id
        );

        this.chatService.onReceiveMessage((res) => {
            const message = {
              id: res.id,
              chatId: res.chatId,
              content: res.content,
              timeStamp: res.timeStamp,
              senderId: res.senderId,
              sendedByYou: res.senderId === this.currentUser()!.id,
            };

            this.messages.set([...this.messages(), message]);
            setTimeout(() => {
              this.scrollToBottom();
            }, 200);
        });

        if(this.chat()?.authorId === this.currentUser()?.id){
          if(this.chat()?.userId){
            this.userService.getUserWithoutDetail(this.chat()!.userId).subscribe((res) => {
              this.talker.set(res); 
            });}
        }
        else{
          if(this.chat()?.authorId){
            this.userService.getUserWithoutDetail(this.chat()!.authorId).subscribe((res) => {
              this.talker.set(res); 
            });}
        }
      }
      setTimeout(() => {
        this.scrollToBottom();
      }, 200);
    }
  }

  sendMessage() {
    if (!this.chatService.isConnected()) {
      this.chatService.startConnection(
        this.authService.getAccessToken()!,
        this.chat()!.id
      );
    }
    this.chatService.sendMessage(this.chat()!.id, this.currentMessage);
    this.currentMessage = '';
    setTimeout(() => {
      this.scrollToBottom();
    }, 200);
  }

  scrollToBottom(): void {
    const scrollableElement = this.scrollableDiv.nativeElement;
    scrollableElement.scrollTop = scrollableElement.scrollHeight;
  }
}
