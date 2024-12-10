import { AppliedDiscountCode } from "./discountCodes.model";
import { Item } from "./finance.model";

export interface Cart{
    id: string;
    total: number;
    totalBeforeDiscount: number | undefined;
    discountCode: AppliedDiscountCode;
    items: CartItem[];
}

export interface CartItem{
    id: string;
    orginalItem: Item;
    price: number;
}


export interface CartItemForDisplay{
    id: string;
    orginalItem: Item;
    price: number;
    title: string;
    authorName:  string;
    thumbnailUrl: string;
}