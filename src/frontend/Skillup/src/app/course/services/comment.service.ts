import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { environment } from "../../../environments/environment";
import { SuComment } from "../models/comment.model";

@Injectable({ providedIn: 'root' })
export class CommentService {
    private httpClient = inject(HttpClient);
    public currentComments = signal<SuComment[]>([]);

    public getCommentsByElementId(elementId: string) {
        return this.httpClient.get<SuComment[]>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Comments');
    }

    public addComment(elementId: string, content: string, parentCommentId: string | null) {
        return this.httpClient.post<SuComment[]>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Comments', {content: content, parentCommentId: parentCommentId});
    }

    public DeleteComment(commentId: string) {
        return this.httpClient.delete(environment.apiUrl + '/Courses/Elements/Comments/' + commentId).subscribe((res) => {
            this.currentComments.update((comments) => 
                this.updateComment(comments, commentId, (comment) => {
                    comment.content = "[Comment deleted]";
                    comment.author.firstName = 'Unknown';
                    comment.author.lastName = '';
                }))
        });
    }

    public ToggleLikeForComment(commentId: string) {
        return this.httpClient.patch<SuComment[]>(environment.apiUrl + '/Courses/Elements/Comments/' + commentId + '/ToggleLike', {}).subscribe((res) => {
            this.currentComments.update((comments) => 
            this.updateComment(comments, commentId, (comment) => {
                comment.likesCount = comment.isLiked ? comment.likesCount - 1 : comment.likesCount + 1;
                comment.isLiked = !comment.isLiked;
            }))
        })
    }

    public updateComment(comments: SuComment[], commentId: string, updateFn: (comment: SuComment) => void) : SuComment[]{
        return comments.map((comment) => {
            if(comment.id === commentId){
                updateFn(comment);
            }
            if(comment.replies.length> 0){
                comment.replies = this.updateComment(comment.replies, commentId, updateFn);
            }
            return comment;
        });
    }
}