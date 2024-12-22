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

export interface CourseDetailRating{
    rating: {
        courseId: string;
        averageStars: number;
        ratingsCount: number;
    }
    userRatings: UserRatingDetail[];
}

export interface UserRatingDetail{
    id: string;
    courseId: string;
    ratedById: string;
    stars: number;
    feedback: string;
    timestamp: Date;
    ratedBy: {
        id: string;
        firstName: string;
        lastName: string;
        email: string;
        profilePicture: string;
    }
}
