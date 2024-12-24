import { Item } from "./finance.model";

export enum DiscountCodeType{
    Percentage = "Percentage",
    FixedAmount = "FixedAmount",
}

export interface AppliedDiscountCode{
    id: string;
    type: DiscountCodeType;
    code: string;
    discountValue: number;
}

export interface DiscountCode{
    id: string;
    type: DiscountCodeType;
    startAt: Date;
    expireAt: Date | null;
    code: string;
    discountValue: number;
    appliesToEntireCart: boolean;
    isActive: boolean;
    isPublic: boolean;
    discountedItems: Item[] | null;
}

export interface AddDiscountCode{
    startAt: Date;
    expireAt: Date | null;
    code: string;
    discountValue: number;
    appliesToEntireCart: boolean;
    isActive: boolean;
    isPublic: boolean;
    
}
