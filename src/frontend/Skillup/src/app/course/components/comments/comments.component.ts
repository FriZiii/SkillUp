import { CommonModule } from '@angular/common';
import { Component, inject, input, OnChanges, OnInit, output, ViewEncapsulation } from '@angular/core';
import { SuComment } from '../../models/comment.model';
import { CommentComponent } from "./comment/comment.component";
import { AddNewCommentComponent } from "./add-new-comment/add-new-comment.component";

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [CommonModule, CommentComponent, AddNewCommentComponent],
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CommentsComponent implements OnChanges {
  comments = input.required<SuComment[]>();
  elementId = input.required<string>();
  commentAdded = output<SuComment[]>();
  
  ngOnChanges(): void {
    console.log(this.comments());
  }

  onCommentAdded(comments: SuComment[]){
    this.commentAdded.emit(comments)
  }
  
}
