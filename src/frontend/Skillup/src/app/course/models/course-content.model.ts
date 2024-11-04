
export interface Section{
    id: string;
    title: string;
    index: number;
    elements: Element[];
}

export interface Element{
    id: string;
    title: string;
    description: string;
    type: ElementType;
    index: number;
    isFree: boolean;
    isPublished: boolean;
}

export enum ElementType{
    Article = 0,
    Video = 1,
    Exercise = 2
}