import { CommonModule } from '@angular/common';
import { Component, input, ViewEncapsulation } from '@angular/core';
import { SuComment } from '../../models/comment.model';
import { CommentComponent } from "./comment/comment.component";
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [CommonModule, CommentComponent, FormsModule, InputTextModule],
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CommentsComponent {
  comments = input.required<SuComment[]>();

  showAddComment = false;
  newCommentContent = '';
}
