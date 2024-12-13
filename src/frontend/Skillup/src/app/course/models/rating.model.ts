export interface UserRating{
    id: string;
    courseId: string;
    ratedById: string;
    stars: number;
    feedback: string;
    time: Date;
}

export interface AverageRating{
    courseId: string;
    averageStars: number;
    ratingsCount: number;
}
