import { Component, inject, OnInit, signal } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat.model';
import { UserService } from '../../../user/services/user.service';
import { User } from '../../../user/models/user.model';
import { ChatItemComponent } from './chat-item/chat-item.component';
import { ChatWindowComponent } from './chat-window/chat-window.component';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [ChatWindowComponent, ChatItemComponent],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.css',
})
export class ChatsComponent implements OnInit {
  userService = inject(UserService);
  chatService = inject(ChatService);

  user = signal<User | null>(null);
  chats = signal<Chat[]>([]);

  selectedChat = signal<Chat | null>(null);

  ngOnInit(): void {
    this.userService.user.subscribe((user) => {
      this.user.set(user);

      if (this.user() !== null) {
        this.chatService
          .fetchChats(this.user()!.id)
          .subscribe((chats: Chat[]) => {
            this.chats.set(chats);
          });
      }
    });
  }

  selectChat(chat: Chat) {
    if (this.selectedChat() === chat) {
      this.selectedChat.set(null);
      this.chatService.disconnect();
      return;
    }
    this.selectedChat.set(chat);
  }

  isChatSelected(chat: Chat): boolean {
    if (this.selectedChat() === null) return false;

    return chat.id === this.selectedChat()!.id;
  }
}
