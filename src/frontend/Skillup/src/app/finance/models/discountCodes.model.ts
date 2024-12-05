export interface AppliedDiscountCode{
    id: string;
    type: DiscountCodeType;
    code: string;
    discountValue: number;
}

export enum DiscountCodeType{
    Percentage = "Percentage",
    FixedAmount = "FixedAmount",
}