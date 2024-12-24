import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat.model';
import { UserService } from '../../../user/services/user.service';
import { User } from '../../../user/models/user.model';
import { ChatItemComponent } from './chat-item/chat-item.component';
import { ChatWindowComponent } from './chat-window/chat-window.component';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CoursesService } from '../../../course/services/course.service';
import { UserRole } from '../../../user/models/user-role.model';
import { AccordionModule } from 'primeng/accordion';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [ChatWindowComponent, ChatItemComponent, InputTextModule, FormsModule, AccordionModule],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.css',
})
export class ChatsComponent implements OnInit {
  //Services
  userService = inject(UserService);
  chatService = inject(ChatService);
  courseService = inject(CoursesService);

  //Variables
  user = signal<User | null>(null);
  chats = signal<Chat[]>([]);
  filteredChats = signal<Chat[]>([]);
  selectedChat = signal<Chat | null>(null);
  searchValue = '';
  courses = this.courseService.courses;
  availableCourses = computed(() => this.courses().filter(course => 
    this.chats().some(chat => chat.courseId === course.id && chat.authorId === this.user()?.id)
  ));
  UserRole = UserRole;
  searchVisible = false;

  ngOnInit(): void {
    this.userService.user.subscribe((user) => {
      this.user.set(user);

      if (this.user() !== null) {
        this.chatService
          .fetchChats(this.user()!.id)
          .subscribe((chats: Chat[]) => {
            this.chats.set(chats);
            this.filteredChats.set(chats.filter(chats => chats.authorId !== this.user()?.id));
            if(this.filteredChats().length !== 0){
              this.searchVisible = true;
            }
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

  applyFilter(){
    
    const filtered = this.availableCourses()
      .filter(course => {
        const matchesSearch = course.title?.toLowerCase().includes(this.searchValue.toLowerCase());
        return matchesSearch;
      });

      const filteredChats = this.chats().filter(chat => 
        filtered.some(course => course.id === chat.courseId) && chat.authorId !== this.user()?.id
      );
      
      this.filteredChats.set(filteredChats);
  }

  getChatsForCourse(courseId: string) {
    return this.chats().filter(chat => chat.courseId === courseId && chat.authorId === this.user()?.id);
  }
}
