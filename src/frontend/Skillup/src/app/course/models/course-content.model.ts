
export interface Section{
    id: string;
    title: string;
    elements: Element[];
}

export interface Element{
    id: string;
    title: string;
    isFree: boolean;
    isPublished: boolean;
}