import { Component, inject, input, output } from '@angular/core';
import { UserService } from '../../../../user/services/user.service';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { SuComment } from '../../../models/comment.model';
import { CommonModule } from '@angular/common';
import { CommentService } from '../../../services/comment.service';

@Component({
  selector: 'app-add-new-comment',
  standalone: true,
  imports: [ButtonModule, InputTextModule, FormsModule, CommonModule],
  templateUrl: './add-new-comment.component.html',
  styleUrl: './add-new-comment.component.css'
})
export class AddNewCommentComponent {
  parentComment = input<SuComment>();
  elementId = input.required<string>();
  commentAdded = output<SuComment[]>();
  //Services
  userService = inject(UserService);
  commentService = inject(CommentService);

  //Variables
  user = this.userService.currentUser;
  newCommentContent = '';

  addNewComment(){
    this.commentService.addComment(this.elementId(), this.newCommentContent, this.parentComment()?.id ?? null).subscribe((res) => {
      this.commentAdded.emit(res);
      this.newCommentContent = '';
    });
  }
}
