import {
  Component,
  inject,
  input,
  OnChanges,
  signal,
  SimpleChanges,
} from '@angular/core';
import { ChatService } from '../../../services/chat.service';
import { AuthService } from '../../../../auth/services/auth.service';
import { FormsModule } from '@angular/forms';
import { Chat } from '../../../models/chat.model';
import { Message } from '../../../models/message.model';
import { UserService } from '../../../../user/services/user.service';
import { User } from '../../../../user/models/user.model';
import { MessageItemComponent } from './message-item/message-item.component';

@Component({
  selector: 'app-chat-window',
  standalone: true,
  imports: [FormsModule, MessageItemComponent],
  templateUrl: './chat-window.component.html',
  styleUrl: './chat-window.component.css',
})
export class ChatWindowComponent implements OnChanges {
  chat = input.required<Chat | null>();

  userService = inject(UserService);
  authService = inject(AuthService);

  user = signal<User | null>(null);
  messages = signal<Message[]>([]);

  currentMessage!: string;

  constructor(private chatService: ChatService) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['chat']) {
      if (this.chat()) {
        console.log(this.chat());

        this.chatService.fetchChatHistory(this.chat()!.id).subscribe((res) => {
          this.messages.set(res);
        });

        this.chatService.startConnection(
          this.authService.getAccessToken()!,
          this.chat()!.id
        );

        this.chatService.onReceiveMessage((res) => {
          this.userService.user.subscribe((user) => {
            this.user.set(user);

            const message = {
              id: res.id,
              chatId: res.chatId,
              content: res.content,
              timeStamp: res.timeStamp,
              senderId: res.senderId,
              sendedByYou: res.senderId === this.user()!.id,
            };

            this.messages.set([...this.messages(), message]);
          });
        });
      }
    }
  }

  sendMessage() {
    this.chatService.sendMessage(this.chat()!.id, this.currentMessage);
    this.currentMessage = '';
  }
}
