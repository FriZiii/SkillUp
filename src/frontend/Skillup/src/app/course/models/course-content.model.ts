
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
    attachments: string[];
}

export enum AssetType{
    Article = "Article",
    Video = "Video",
    Exercise = "Exercise"
}