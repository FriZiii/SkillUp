
export interface Section{
    id: string;
    title: string;
    index: number;
    isPublished: boolean;
    elements: Element[];
}

export interface Element{
    id: string;
    title: string;
    description: string;
    type: AssetType;
    index: number;
    isFree: boolean;
    hasAsset: boolean;
}

export enum AssetType{
    Article = "Article",
    Video = "Video",
    Exercise = "Exercise"
}

export interface Attachment{
    id: string;
    elementId: string;
    name: string;
    type: string;
}