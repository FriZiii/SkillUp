import { Category } from "./category.model";

export interface AddCourse{
    title: string;
    categoryId: string;
    subcategoryId: string;
}

export interface CourseListItem{
    id: string;
    title: string;
    isPublished: boolean;
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

export interface CourseDetail{
    id: string;
    title: string;
    isPublished: boolean;
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
    level: number;
    objectivesSummary: string[];
    intendedFor: string[];
    mustKnowBefore: string[];
    sections: Section[];
}

export interface Section{
    id: string;
    title: string;
}