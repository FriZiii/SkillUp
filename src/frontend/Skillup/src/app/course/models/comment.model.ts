export interface SuComment{
    id: string;
    elementId: string;
    author: CommentUser;
    content: string;
    createdAt: string;
    likesCount: number;
    isLiked: boolean;
    replies: SuComment[];
}

export interface CommentUser{
    id: string;
    firstName: string
    lastName: string;
    email: string;
    profilePicture: string;
}