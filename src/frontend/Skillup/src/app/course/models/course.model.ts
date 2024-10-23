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
        subcategory: {
            id: string;
            name: string;
        };
    };
    thumbnailUrl: string;
}