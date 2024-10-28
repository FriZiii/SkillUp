export interface Item{
    id: string;
    authorid: string;
    type: ItemType;
    price: {
        amount: number;
    };
}

export enum ItemType {
    Course = 0,
  }
  