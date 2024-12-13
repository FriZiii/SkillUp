import { Section } from "./course-content.model";
import { CourseLevel } from "./course-level.model";
import { CourseStatus } from "./course-status.model";

export interface AddCourse{
    title: string;
    categoryId: string;
    subcategoryId: string;
}

export interface Course{
    id: string;
    title: string;
    authorId: string;
    authorName:  string;
    status: CourseStatus;
    category: {
        id: string;
        name: string;
        slug: string;
        subcategory: {
            id: string;
            name: string;
            slug: string;
        };
    };
    thumbnailUrl: string;
}

export interface CourseListItem{
    id: string;
    title: string;
    authorId: string;
    authorName:  string;
    status: CourseStatus;
    category: {
        id: string;
        name: string;
        slug: string;
        subcategory: {
            id: string;
            name: string;
            slug: string;
        };
    };
    thumbnailUrl: string;
    price: number;
}

export interface CourseDetail{
    id: string;
    title: string;
    authorId: string;
    authorName:  string;
    status: CourseStatus;
    category: {
        id: string;
        name: string;
        slug: string;
        subcategory: {
            id: string;
            name: string;
            slug: string;
        };
    };
    thumbnailUrl: string;
    subtitle: string;
    description: string;
    level: CourseLevel;
    objectivesSummary: string[];
    intendedFor: string[];
    mustKnowBefore: string[];
    sections: Section[];
    price: number;
}
