import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { SuComment } from "../models/comment.model";

@Injectable({ providedIn: 'root' })
export class CommentService {
    private httpClient = inject(HttpClient);

    public getCommentsByElementId(elementId: string) {
        return this.httpClient.get<SuComment[]>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Comments');
    }

    public addComment(elementId: string, content: string, parentCommentId: string | null) {
        return this.httpClient.post<SuComment[]>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Comments', {content: content, parentCommentId: parentCommentId});
    }

    public DeleteComment(commentId: string) {
        return this.httpClient.delete(environment.apiUrl + '/Courses/Elements/Comments/' + commentId);
    }

    public ToggleLikeForComment(commentId: string) {
        return this.httpClient.patch(environment.apiUrl + '/Courses/Elements/Comments/' + commentId + '/ToggleLike', {});
    }
}