import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  input,
  OnChanges,
  OnInit,
  output,
  ViewEncapsulation,
} from '@angular/core';
import { SuComment } from '../../models/comment.model';
import { CommentComponent } from './comment/comment.component';
import { AddNewCommentComponent } from './add-new-comment/add-new-comment.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [CommonModule, CommentComponent, AddNewCommentComponent, ConfirmDialogModule],
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class CommentsComponent {
  comments = input.required<SuComment[]>();
  elementId = input.required<string>();
}
