export interface Review{
    id: string;
    courseId: string;
    createdAt: Date;
    resolvedAt: Date;
    status: ReviewStatus;
    comments: ReviewComment[];
}

export interface ReviewComment{
    id: string;
    courseElementId: string | null;
    comment: string;
    isResolved: boolean;
    createdAt: Date;
}

export enum ReviewStatus{
    Finalized,
    FinalizedWithRequiredChanges,
    InProgress
}