export interface Order{
    id: string;
    created: Date;
    title: string;
    totalPrice: {
        amount: number;
    };
    items: OrderItem[];
}

export interface OrderItem{
    itemId: string;
    itemPrice: number;
}