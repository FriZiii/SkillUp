import {
  Component,
  computed,
  Input,
  input,
  OnChanges,
  output,
  signal,
  SimpleChanges,
  ViewEncapsulation,
} from '@angular/core';
import { SuComment } from '../../../models/comment.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddNewCommentComponent } from '../add-new-comment/add-new-comment.component';

@Component({
  selector: 'app-comment',
  standalone: true,
  imports: [InputTextModule, FormsModule, CommonModule, AddNewCommentComponent],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class CommentComponent {
  comment = input.required<SuComment>();
  parrentComment = input<SuComment>();
  elementId = input.required<string>();
  commentAdded = output<SuComment[]>();

  showAddComment = false;
  newCommentContent = '';

  likeComment(comment: any, type: string) {
    if (type === 'up') {
      comment.likes++;
    } else {
      comment.dislikes++;
    }
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

  onCommentAdded(comments: SuComment[]) {
    console.log('CommentComponent:onCommentAdded');
    console.log(comments);
    this.commentAdded.emit(comments);
  }
}
