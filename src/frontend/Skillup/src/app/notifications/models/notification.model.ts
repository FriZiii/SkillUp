export interface Notification{
    id: string;
    userId: string;
    createdAt: Date;
    type: NotificationType;
    message: string;
    seen: boolean;
}

export enum NotificationType{
    Instructor = "Instructor",
    User = "User",
}