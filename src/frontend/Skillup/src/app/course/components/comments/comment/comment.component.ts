import {
  Component,
  inject,
  input,
  ViewEncapsulation,
} from '@angular/core';
import { SuComment } from '../../../models/comment.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddNewCommentComponent } from '../add-new-comment/add-new-comment.component';
import { CommentService } from '../../../services/comment.service';
import { UserService } from '../../../../user/services/user.service';
import { ConfirmationDialogHandlerService } from '../../../../core/services/confirmation-handler.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-comment',
  standalone: true,
  imports: [InputTextModule, FormsModule, CommonModule, AddNewCommentComponent, ConfirmDialogModule, RouterModule],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class CommentComponent {
  comment = input.required<SuComment>();
  parrentComment = input<SuComment>();
  elementId = input.required<string>();

  //Services
  commentService = inject(CommentService);
  userService = inject(UserService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);

  //Variables
  currentUser = this.userService.currentUser;
  showAddComment = false;
  newCommentContent = '';

  toggleLike(){
    this.commentService.ToggleLikeForComment(this.comment().id);
  }

  onCommentAdded(){
    this.showAddComment = false;
  }

  timeAgo(inputDate: string): string {
    const date = new Date(inputDate);
    const seconds = Math.floor((new Date().getTime() - date?.getTime()) / 1000);
    const intervals = [
      { label: 'year', seconds: 31536000 },
      { label: 'month', seconds: 2592000 },
      { label: 'day', seconds: 86400 },
      { label: 'hour', seconds: 3600 },
      { label: 'minute', seconds: 60 },
      { label: 'second', seconds: 1 },
    ];
    for (const interval of intervals) {
      const count = Math.floor(seconds / interval.seconds);
      if (count > 0) {
        return `${count} ${interval.label}${count !== 1 ? 's' : ''} ago`;
      }
    }
    return 'just now';
  }

  deleteComment(event: Event){
    this.confirmationDialogService.confirmDelete(event, () => this.commentService.DeleteComment(this.comment().id))
  }
}
