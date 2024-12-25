import { Component, computed, inject, input, OnInit, output, signal } from '@angular/core';
import { Chat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';
import { CoursesService } from '../../../../course/services/course.service';
import { UserService } from '../../../../user/services/user.service';
import { User } from '../../../../user/models/user.model';
import { TruncatePipe } from "../../../../utils/pipes/truncate.pipe";
import { UserRole } from '../../../../user/models/user-role.model';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-chat-item',
  standalone: true,
  imports: [CommonModule, TruncatePipe, SkeletonModule],
  templateUrl: './chat-item.component.html',
  styleUrl: './chat-item.component.css',
})
export class ChatItemComponent implements OnInit {
  chat = input.required<Chat>();
  onSelected = output<Chat>();

  isSelected = input.required<boolean>();

  //Services
  courseService = inject(CoursesService);
  userService = inject(UserService);

  //Variables
  course = computed(() => this.courseService.courses().find(c => c.id === this.chat().courseId));
  author = signal<User | null>(null);
  talker = signal<User | null>(null);
  currentUser = this.userService.currentUser;

  ngOnInit(): void {
    this.userService.getUserWithoutDetail(this.chat().authorId).subscribe((res) => {
      this.author.set(res);
    })

    if(this.currentUser()?.isInRole(UserRole.Instructor)){
      if(this.chat()?.userId){
        this.userService.getUserWithoutDetail(this.chat()!.userId).subscribe((res) => {
        this.talker.set(res); 
        });}
      }
  }

  onSelect() {
    this.onSelected.emit(this.chat());
  }
}
